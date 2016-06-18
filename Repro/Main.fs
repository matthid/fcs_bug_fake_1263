
//#load @"C:\PROJ\FSharp.Compiler.Service_585\script.fsx"

open System
open System.IO
open System.Reflection
//Environment.CurrentDirectory <- @"C:\PROJ\FSharp.Compiler.Service_585\bin"
let script1FilePath = Path.GetFullPath @"1.fsx"
let script2FilePath = Path.GetFullPath @"2.fsx"
let script3FilePath = Path.GetFullPath @"3.fsx"
let script4FilePath = Path.GetFullPath @"4.fsx"
File.WriteAllText(script1FilePath, @"
#load ""2.fsx""
#load ""3.fsx""
printfn ""HELLO FROM 1""")
File.WriteAllText(script2FilePath, @"
#load ""4.fsx""
printfn ""HELLO FROM 2 %A"" ``4``.T")
File.WriteAllText(script3FilePath, @"
#load ""4.fsx""
printfn ""HELLO FROM 3""")
File.WriteAllText(script4FilePath, @"
printfn ""HELLO FROM 4""
type T = T")
open Microsoft.FSharp.Compiler.Interactive.Shell

[<EntryPoint>]
let main args =
  let fsiConfig = FsiEvaluationSession.GetDefaultConfiguration()
  let fsiArgs = [| @"C:\fsi.exe" |]
  let inStream = new StringReader("")
  use session = FsiEvaluationSession.Create(fsiConfig, fsiArgs, inStream, Console.Out, Console.Error)
  session.EvalScriptNonThrowing script1FilePath |> printfn "%A"
  //execScript scriptFilePath
  0

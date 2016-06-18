
//#load @"C:\PROJ\FSharp.Compiler.Service_585\script.fsx"

open System
open System.IO
open System.Reflection
//Environment.CurrentDirectory <- @"C:\PROJ\FSharp.Compiler.Service_585\bin"
let script1FilePath = Path.GetFullPath @"1.fsx"
let script2FilePath = Path.GetFullPath @"2.fsx"
File.WriteAllText(script2FilePath, @"
let func = (fun () ->
    printfn ""This shouldn't work""
) )");
File.WriteAllText(script1FilePath, @"
#load ""2.fsx""

``2``.func()");
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

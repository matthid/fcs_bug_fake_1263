// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.


#r @"C:\PROJ\FAKE\build\FakeLib.dll"
//#load @"C:\PROJ\FSharp.Compiler.Service_585\script.fsx"
open Fake
open System
open System.IO
open System.Reflection
let scriptFilePath = @"C:\Test\myscript.fsx"
Assembly.LoadFrom(@"C:\PROJ\FSharp.Compiler.Service_585\bin\FSharp.Data.dll") |> ignore
AppDomain.CurrentDomain.GetAssemblies() |> Seq.iter (fun a -> printfn "%s" a.FullName)
let myTracer =
    { new Fake.TraceListener.ITraceListener with
        member x.Write t = () };;

TraceListener.listeners.Add(myTracer)
// Define your library scripting code here

let fakeLib = typeof<Fake.TraceListener.ITraceListener>.Assembly.Location
File.WriteAllText(scriptFilePath, @"
#r """ + fakeLib.Replace(@"\", @"\\") + @"""
open Fake
traceFAKE ""TEST_FAKE_OUTPUT""");

let execScript s =
  executeBuildScriptWithArgsAndFsiArgsAndReturnMessages s [||] [||] false ;;

execScript scriptFilePath

execScript @"C:\PROJ\FSharp.Compiler.Service_585\script.fsx"
execScript @"C:\Users\dragon\AppData\Local\Temp\tmp1303.tmp.fsx"
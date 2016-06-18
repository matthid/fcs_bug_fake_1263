// --------------------------------------------------------------------------------------
// FAKE build script
// --------------------------------------------------------------------------------------

#I @"packages/FAKE/tools"
#r @"packages/FAKE/tools/FakeLib.dll"

open System
open System.IO
open Fake

Environment.CurrentDirectory <- __SOURCE_DIRECTORY__

Target "BuildRepro" (fun _ -> 
    !! "Repro.sln"
        |> MSBuild "bin/repro" "Build"
            [ "Configuration", "Release" ]
        |> Log "Building LibraryUsingTypeprovider: "
)

Target "RunRepro" (fun _ -> 
    execProcess (fun c ->
      c.FileName <- "bin/repro/Repro.exe"
      ) (TimeSpan.FromMinutes 20.0)
      |> ignore
)

Target "All" DoNothing

"BuildRepro"
  ==> "RunRepro"
  ==> "All"

RunTargetOrDefault "All"

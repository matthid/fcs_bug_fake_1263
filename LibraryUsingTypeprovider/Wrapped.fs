namespace LibraryUsingTypeprovider

// This is only here, because otherwise the F# compiler is smart enough to optimize the reference away...
open FSharp.Data
open FSharp.Data.JsonExtensions

module Wrapped =
  let useSomething (j : JsonValue) =
    j?Major.AsInteger()

module OtherStuff =
  let t = "test"
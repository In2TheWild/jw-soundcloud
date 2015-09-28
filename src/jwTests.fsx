#load @"jw.fsx"
#r @"../packages/NUnit/lib/nunit.framework.dll"
#r @"../packages/FsUnit/lib/FsUnit.NUnit.dll"

open FsUnit
open NUnit.Framework
open Jw.Ploy

[<Test>]
let ShouldGetUserInfo() =
    100 |> should equal 100

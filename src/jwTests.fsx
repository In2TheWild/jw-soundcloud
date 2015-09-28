#load @"jw.fsx"
#r @"../packages/NUnit/lib/nunit.framework.dll"
#r @"../packages/FsUnit/lib/FsUnit.NUnit.dll"

open FsUnit
open NUnit.Framework
open Jw

[<Test>]
let ShouldGetUserInfo() =
    ()

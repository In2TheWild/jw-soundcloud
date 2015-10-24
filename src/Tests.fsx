#load @"Api.fsx"
#r @"../packages/NUnit/lib/nunit.framework.dll"
#r @"../packages/FsUnit/lib/FsUnit.NUnit.dll"

open FsUnit
open NUnit.Framework
open Api.Api

[<Test>]
let ShouldGetUserInfo() =
    100 |> should equal 100

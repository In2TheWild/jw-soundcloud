
#I "packages/FAKE/tools/"
#r "FakeLib.dll"


open Fake
open Fake.FscHelper
open System.IO

let outDir = "out"
let testDll = Path.Combine(outDir, "jwTests.dll")
let exe = Path.Combine(outDir, "jw.exe")

Target "buildExe" (fun _ ->
        ["src/jw.fsx"]
        |> Fsc (fun p -> {p with Output = exe})
)

Target "buildTestDll" (fun _ ->
        ["src/jw.fsx"; "src/jwTests.fsx"]
        |> Fsc (fun p -> { p with FscTarget = Library; Output = testDll })
    )

Target "test" (fun _ ->
        !!(testDll)
        |> NUnit (fun p -> { p  with ToolName = "nunit-console.exe"} )
)

"buildExe"
    ==> "buildTestDll"
    ==> "test"

RunTargetOrDefault "test"

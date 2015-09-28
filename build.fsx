
#I "packages/FAKE/tools/"
#r "FakeLib.dll"

open Fake
open Fake.FscHelper
open System.IO
open System

let outDir = "out"
let testDll = Path.Combine(outDir, "jwTests.dll")
let exe = Path.Combine(outDir, "jw.exe")

Target "buildExe" (fun _ ->
        ["src/jw.fsx"; "src/program.fsx"]
        |> Fsc (fun p ->
            { p with
                References = ["FSharp.Data.dll"]
                Output = exe})
)

Target "buildTestDll" (fun _ ->
        ["src/jw.fsx"; "src/jwTests.fsx"]
        |> Fsc (fun p -> { p with FscTarget = Library; Output = testDll })
    )

Target "test" (fun _ ->
        !!(testDll)
        |> NUnit (fun p -> { p  with ToolName = "nunit-console.exe"} )
)

Target "watch" (fun _ ->
        let watcher = !! "src/*.fsx" |> WatchChanges (fun changes ->
                tracefn "%A" changes
                Run "buildTestDll"
            )
        Console.ReadLine() |> ignore
        watcher.Dispose()
    )

"buildExe"
    ==> "buildTestDll"
    ==> "test"

RunTargetOrDefault "watch"

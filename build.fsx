
#I "packages/FAKE/tools/"
#r "FakeLib.dll"


open Fake
open Fake.FscHelper
open System.IO
open System
open System.Linq

let outDir = "out"
let testDll = Path.Combine(outDir, "sldl.dll")
let exe = Path.Combine(outDir, "sldl.exe")


// https://github.com/fsharp/FAKE/issues/689
let framework = @"C:\Program Files (x86)\Reference Assemblies\Microsoft\FSharp\.NETFramework\v4.0\4.3.1.0\FSharp.Core.dll"
let mscorlib = @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\mscorlib.dll"
let system = @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\System.dll"
let systemCore = @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\System.Core.dll"


if not (Directory.Exists outDir) then Directory.CreateDirectory outDir |> ignore

let GetDll(name: string) =
    DirectoryInfo("packages").GetFiles(name, SearchOption.AllDirectories) |> Seq.head

let Copy(info: FileInfo) =
    printf "|| copy -> %s" info.FullName
    let target = Path.Combine("out", info.Name)
    if not(File.Exists target) then File.Copy(info.FullName, target)

[
    "FSharp.Data.dll"
    "Argu.dll"
    "FSharp.Data.DesignTime.dll" ]
|> Seq.iter (GetDll >> Copy)

Target "buildExe" (fun _ ->
        let os = System.Environment.OSVersion.Platform
        let others =
            match os with
            | PlatformID.Win32NT ->
                [ "--noframework"; "-r"; framework; "-r"; mscorlib; "-r"; system; "-r"; systemCore]
            | _ -> []

        ["src/Api.fsx"; "src/Program.fsx"]
        |> Fsc (fun p ->
            { p with Output = exe; OtherParams = others})
)

Target "buildTestDll" (fun _ ->
        ["src/Api.fsx"; "src/Tests.fsx"]
        |> Fsc (fun p -> { p with FscTarget = Library; Output = testDll; Platform = X86})
    )

Target "test" (fun _ ->
        !!(testDll)
        |> NUnit (fun p -> { p  with ToolName = "nunit-console.exe"} )
)

Target "watch" (fun _ ->
        let watcher = !! "src/*.fsx" |> WatchChanges (fun changes ->
                try
                    tracefn "%A" changes
                    Run "buildTestDll"
                with ex ->
                    Console.WriteLine ex.Message
            )
        Console.ReadLine() |> ignore
        watcher.Dispose()
    )

"buildExe"
    ==> "buildTestDll"
    ==> "test"

RunTargetOrDefault "buildExe"

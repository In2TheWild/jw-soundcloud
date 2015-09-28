open System.Net
open System.IO
open System

let info (str:string) =
    Console.WriteLine str

let loadPaket() =
    let file = "https://github.com/fsprojects/Paket/releases/download/2.4.4/paket.exe"
    let targetDir = ".paket"

    if not (Directory.Exists targetDir) then
        sprintf "|| create directory -> %s " targetDir |> info
        Directory.CreateDirectory targetDir |> ignore

    let target = Path.Combine(targetDir, "paket.exe")
    if not (File.Exists target) then
        sprintf "|| download file -> %s" file |> info
        use client = new WebClient()
        client.DownloadFile(file, target)

loadPaket()

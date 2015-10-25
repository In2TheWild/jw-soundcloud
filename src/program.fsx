#r "../packages/Argu/lib/net40/Argu.dll"
#load "Api.fsx"

open System
open System.IO
open Api.Api
open Nessos.Argu

type Args =
        | User of string
        | Out of string
        | Content of string
    with
        interface IArgParserTemplate with
            member s.Usage =
                match s with
                | User _ -> "specify user id."
                | Out _ -> "specify output directory (default: ./)."
                | Content _ -> "specify content type (mp3 or art) (default: mp3)."

[<EntryPoint>]
let main args =
    let parser = ArgumentParser.Create<Args>()
    let results =  parser.Parse (args, raiseOnUsage = false)
    let help = results.IsUsageRequested

    let showUsage() = printfn "%s" <| results.Usage()

    if not help then
        let config = ConfigLoader().Default()
        let runner =  Runner(config)
        let userId = results.GetResult(<@ User @>, defaultValue = "jannina-weigel")
        let output = results.GetResult(<@ Out @>, defaultValue = "./")
        let content = results.GetResult(<@ Content @>, defaultValue = "mp3")

        if not (Directory.Exists output) then
            Directory.CreateDirectory output |> ignore

        match content with
        | "mp3" -> runner.DownloadStreams(output, userId)
        | "art" -> runner.DownloadArtworks(output, userId)
        | _ -> showUsage()
    else
        showUsage()
    0

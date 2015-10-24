#r "../packages/Argu/lib/net40/Argu.dll"
#load "Api.fsx"

open System
open System.IO
open Api.Api
open Nessos.Argu

type Args =
        | UserId of string
        | Output of string
        | Content of string
    with
        interface IArgParserTemplate with
            member s.Usage =
                match s with
                | UserId _ -> "specify user id."
                | Output _ -> "specify output directory (default: ./)."
                | Content _ -> "specify content type (stream or artwork) (default: stream)."

[<EntryPoint>]
let main args =
    let parser = ArgumentParser.Create<Args>()
    let results =  parser.Parse (args, raiseOnUsage = false)
    let help = results.IsUsageRequested

    let showUsage() = printfn "%s" <| results.Usage()

    if not help then
        let config = ConfigLoader().Default()
        let runner =  Runner(config)
        let userId = results.GetResult(<@ UserId @>, defaultValue = "jannina-weigel")
        let output = results.GetResult(<@ Output @>, defaultValue = "./")
        let content = results.GetResult(<@ Content @>, defaultValue = "stream")

        if not (Directory.Exists output) then
            Directory.CreateDirectory output |> ignore

        match content with
        | "stream" -> runner.DownloadStreams(output, userId)
        | "artwork" -> runner.DownloadArkworks(output, userId)
        | _ -> showUsage()
    else
        showUsage()
    0

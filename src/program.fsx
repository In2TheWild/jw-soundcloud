#load "jw.fsx"

open System
open Jw.Ploy

[<EntryPoint>]
let main args =
    let config = ConfigLoader().Default()
    let runner =  JwRunner(config)
    runner.DownloadArkworks("temp/artworks")
    runner.DownloadStreams("temp/streams")
    0

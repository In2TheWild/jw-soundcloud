#load "jw.fsx"

open System
open Jw.Ploy

[<EntryPoint>]
let main args =
    let config = ConfigLoader().Default()
    let runner =  JwRunner(config)
    runner.GetUserInfo("jannine-weigel").Uri |> Console.WriteLine
    0


#load "jw.fsx"

open System
open Jw.Ploy

[<EntryPoint>]
let main args =
    args |> printf "%A"
    let config = ConfigLoader().Default()
    let runner =  JwRunner(config)
    runner.GetUserInfo("jannine-weigel") |> Console.WriteLine
    0

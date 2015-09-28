
#I "../packages/FSharp.Data/lib/net40/"
#r "FSharp.Data.DesignTime.dll"
#r "FSharp.Data.dll"

open System
open System.Linq
open FSharp.Data

module Ploy =
    type SlUsers = JsonProvider<"/Users/wk/Source/jw/jw-soundcloud/src/json/users.json">
    type SlTracks = JsonProvider<"/Users/wk/Source/jw/jw-soundcloud/src/json/tracks.json">
    type SlUserTracks = JsonProvider<"/Users/wk/Source/jw/jw-soundcloud/src/json/userTracks.json">

    type Config = {
        ClientId: string
        ClientSecret: string
        EndUserAuthentication: string
        Token: string
    }

    type ConfigLoader() =
        member this.Default() = {
                Config.ClientId = "6abc49dd6e4bf58c4d8829def2260ec9"
                ClientSecret = "1b148a720c7d105b0fdae5a3120efff8"
                EndUserAuthentication = "https://soundcloud.com/connect"
                Token = "https://api.soundcloud.com/oauth2/token" }

    type SlApi(config: Config) =
        let baseUrl = "http://api.soundcloud.com"

        member this.UserTracks(user: string) =
            sprintf "%s/users/%s/tracks?client_id=%s" baseUrl user config.ClientId
            |> Http.RequestString
            |> SlUserTracks.Parse

        member this.Tracks(track: string) =
            let url = sprintf "%s/tracks/%s?client_id=%s" baseUrl track config.ClientId
            Http.RequestString url |> SlTracks.Parse

        member this.Users(user: string) =
            let url = sprintf "%s/users/%s?client_id=%s" baseUrl user config.ClientId
            Http.RequestString url |> SlUsers.Parse

    type JwRunner(config: Config) =
        let api = SlApi(config)
        member this.GetUserInfo usr = api.Users usr
        member this.GetUserTracksInfo usr = api.UserTracks usr
        member this.GetTrackInfo track = api.Tracks track

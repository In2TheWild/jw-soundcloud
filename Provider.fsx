
#I @"packages/FSharp.Data/lib/net40/"
#r "FSharp.Data.DesignTime.dll"
#r "FSharp.Data.dll"

open FSharp.Data

type SlUsers = JsonProvider<"data/json/users.json">
type SlTracks = JsonProvider<"data/json/tracks.json">
type SlUserTracks = JsonProvider<"data/json/userTracks.json">
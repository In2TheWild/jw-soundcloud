
#I @"packages/FSharp.Data/lib/net40/"
#r "FSharp.Data.DesignTime.dll"
#r "FSharp.Data.dll"

open FSharp.Data

type SlUsers = JsonProvider<"src/json/users.json">
type SlTracks = JsonProvider<"src/json/tracks.json">
type SlUserTracks = JsonProvider<"src/json/userTracks.json">
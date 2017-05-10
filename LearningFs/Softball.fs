module Softball

type Inning = {
        Number : int
        Playing : list<int> 
        NotPlaying : list<int>
    }

let playersPerInning = 10
let innings = 7 

let getInningsPlayed numberOfPlayers =
    List.init innings (fun a -> { Number = a; Playing = []; NotPlaying = [] })
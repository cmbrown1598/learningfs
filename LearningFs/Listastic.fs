module Listastic

let length list = 
    let rec loop acc =        
        match acc with
        | _::tail -> 
            1 + loop (tail)
        | [] -> 0
    loop list

let rev list = 
    let rec loop lst acc = 
        match lst with
            |[] -> acc
            |head::tail -> loop tail (head::acc)
    match list with
        | [] -> 
            []
        | [x] -> 
            [x]
        | a::b::tail -> 
            loop tail [b;a]


    
    

    
// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
module Program



type SomeType(value1 : int, value2 : float, flag : bool) = 
    member this.Value1 = value1
    member this.Value2 = value2
    member this.Flag = flag
    member this.ComputedValue = (float)value1 * value2

     

type ITPerson = 
    | Programmer of FirstName : string * LastName : string * WritesTests : bool
    | QA of FirstName : string * LastName : string * ExecutesCodeThoroughly : bool
    
let firePerson firstName lastName =
    printfn "Who needs you? You're fired, %s %s!" firstName lastName 

let fireTheItGuy itPerson =
    match itPerson with 
    | Programmer (f,l,_) when f = "Christopher" && l = "Brown" -> firePerson f l
    | Programmer (firstname, lastname, writesTests) -> 
        match writesTests with
        | true ->
            printfn "You can't fire %s, they write tests!" firstname
        | _ ->
            firePerson firstname lastname
    | QA (f, l, _) -> firePerson f l
            
let cb = Programmer ("Christopher", "Brown", true)
let dc = QA ("Dustin", "Cole", true)


open System;
open System.IO;

let m = seq {
                let f = File.OpenRead "C:\Users\christob\Downloads\crawler.js" 
                for m in 1L..f.Length -> f.ReadByte
        }
     
let n = Seq.initInfinite (fun int -> int);;
Seq.skip 1000 n;;

[<EntryPoint>]
let main argv = 
    let a = Seq.length m
    Console.WriteLine ( String.Format ("{0} is the file's length.", a) )

    Console.ReadLine() |> ignore 
    0 // return an integer exit code

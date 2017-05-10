module FizzBuzz

(* 
    F# implementation of the "Fizz Buzz" problem.
*)
let fizzBuzz = 
    let (|DivisibleByThree|DivisibleByFive|DivisibleByBoth|Normal|) i = 
        if (i % 15) = 0 then DivisibleByBoth
        else if (i % 5) = 0 then DivisibleByFive
        else if (i % 3) = 0 then DivisibleByThree
        else Normal

    [ 1 .. 100 ]
    |> List.map (fun i -> 
                    match i with 
                        | DivisibleByBoth -> "FizzBuzz"
                        | DivisibleByFive -> "Buzz"
                        | DivisibleByThree -> "Fizz"
                        | _ -> sprintf "%i" i
                    )


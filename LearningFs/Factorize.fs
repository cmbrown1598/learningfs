module Factorize

let factorize (n : bigint) = 
    let rec find i acc = 
        if i >= n then acc |> List.rev
        elif (n % i = bigint.Zero) then 
            let k = n / i;
            if i <= k then
                (i, k)::acc
                |> find (i + 1I)
            else acc
        else find (i + 1I) acc
    find 2I []
    

    
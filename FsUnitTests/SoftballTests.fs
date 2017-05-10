module SoftballTests

open Softball
open Xunit

[<Fact>] 
let ``Ten girls should each play each inning.``() =
    let retValue = getInningsPlayed 10
    
    List.iteri (fun idx item -> 
                    Assert.Equal(idx, item.Number)
                    Assert.Equal<list<int>>([], item.NotPlaying)
                    Assert.NotEqual<list<int>>([], item.Playing)
                    List.iteri (fun i m -> Assert.Equal(m, i)) item.Playing
                ) retValue
 

namespace LearningFs

type Item = 
    { Name : string
      Sellin : int
      Quality : int }

open Constants

type public GuildedRose() = 
    
    static member public Items = 
        [| { Name = "+5 Dexterity Vest"
             Sellin = 10
             Quality = 20 }
           { Name = AgedBrie
             Sellin = 2
             Quality = 0 }
           { Name = "Elixir of the Mongoose"
             Sellin = 5
             Quality = 7 }
           { Name = Sulfuras
             Sellin = 2
             Quality = 80 }
           { Name = BackstagePasses
             Sellin = 15
             Quality = 20 }
           { Name = ConjuredManaCake
             Sellin = 3
             Quality = 6 } |]
    
    member public __.UpdateQuality(inventory) = 
        inventory |> Array.map (fun item -> 
                         let aqr i = 
                            max (min (item.Quality + i) 50) 0 

                         let si = 
                             { item with Sellin = item.Sellin - 1
                                         Quality = aqr -1 }

                         let ci i = {si with Quality = aqr i}

                         match (item.Name, item.Sellin) with
                         | (Sulfuras, _) -> { item with Quality = 80 }
                         | (AgedBrie, s) when s <= 0 -> ci 2
                         | (AgedBrie, _) -> ci 1
                         | (ConjuredManaCake, s) when s <= 0 -> ci -4
                         | (ConjuredManaCake, _) -> ci -2
                         | (BackstagePasses, s) when s > 10 -> ci 1
                         | (BackstagePasses, s) when s > 5 -> ci 2
                         | (BackstagePasses, s) when s > 0 -> ci 3
                         | (BackstagePasses, _) -> { si with Quality = 0 }
                         | (_, s) when s <= 0 -> ci -2
                         | (_, _) -> si)

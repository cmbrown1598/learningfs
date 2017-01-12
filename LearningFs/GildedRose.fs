(*
   Hi and welcome to team Gilded Rose. As you know, we are a small inn with a prime location in
   a prominent city ran by a friendly innkeeper named Allison. We also buy and sell only the finest goods. 
   Unfortunately, our goods are constantly degrading in quality as they approach their sell by date. 
   We have a system in place that updates our inventory for us. It was developed by a no-nonsense 
   type named Leeroy, who has moved on to new adventures. Your task is to add the new feature to our 
   system so that we can begin selling a new category of items. First an introduction to our system:

	- All items have a SellIn value which denotes the number of days we have to sell the item
	- All items have a Quality value which denotes how valuable the item is
	- At the end of each day our system lowers both values for every item

Pretty simple, right? Well this is where it gets interesting:

	- Once the sell by date has passed, Quality degrades twice as fast
	- The Quality of an item is never negative, and never more than 50
	- "Aged Brie" actually increases in Quality the older it gets
	- "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
	- "Backstage passes", like Aged Brie, increases in Quality as it's SellIn value approaches; 
        Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or 
        less but Quality drops to 0 after the concert

We have recently signed a supplier of conjured items. This requires an update to our system:

	- "Conjured" items degrade in Quality twice as fast as normal items

Feel free to make any changes to the UpdateQuality method and add any new code as 
long as everything still works correctly. However, do not alter the Item class or Items property 
as those belong to the goblin in the corner who will insta-rage and one-shot you 
as he doesn't believe in shared code ownership (you can make the UpdateQuality method and Items 
property static if you like, we'll cover for you). 

Just for clarification, an item can never have its Quality increase above 50, 
however "Sulfuras" is a legendary item and as such its Quality is 80 and it never alters.
 
*)
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

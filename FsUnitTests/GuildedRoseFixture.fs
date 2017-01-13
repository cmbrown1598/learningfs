module GuildedRose

open LearningFs
open Xunit

let sut = new GuildedRose()
[<Literal>]
let item1 = "item1"

[<Fact>]
let `` Quality and sellin is decreased on normal items.``() =
    let results = sut.UpdateQuality([|{ Name = item1; Sellin=5; Quality = 5; }|])
    Assert.Equal({ Name = item1; Sellin=4; Quality = 4; }, results.[0])

[<Fact>]
let `` Once the sell by date has passed, Quality degrades twice as fast.``() =
    let results = sut.UpdateQuality([|{ Name = item1; Sellin=0; Quality = 5; }|])
    Assert.Equal({ Name = item1; Sellin = -1; Quality = 3; }, results.[0])
    
[<Fact>]
let `` Quality doesn't go below Zero.``() =
    let results = sut.UpdateQuality([|{ Name = item1; Sellin=5; Quality = 0; }|])
    Assert.Equal(0, results.[0].Quality)

[<Fact>]
let `` Backstage passes increase in quality ``() =
    let results = sut.UpdateQuality([|{ Name = Constants.BackstagePasses; Sellin=20; Quality = 5; }|])
    Assert.Equal(6, results.[0].Quality)

[<Fact>]
let `` Backstage passes quality set to 0 after the concert``() =
    let results = sut.UpdateQuality([|{ Name = Constants.BackstagePasses; Sellin=0; Quality = 5; }|])
    Assert.Equal(0, results.[0].Quality)


[<Fact>]
let `` Backstage passes increase in quality doubly when under 10 days left to sell it``() =
    let results = sut.UpdateQuality([|{ Name = Constants.BackstagePasses; Sellin=8; Quality = 5; }|])
    Assert.Equal(7, results.[0].Quality)

[<Fact>]
let `` Backstage passes increase in quality triply when under 5 days left to sell it``() =
    let results = sut.UpdateQuality([|{ Name = Constants.BackstagePasses; Sellin=4; Quality = 5; }|])
    Assert.Equal(8, results.[0].Quality)



[<Fact>]
let `` Aged Brie increases in quality.``() =
    let results = sut.UpdateQuality([|{ Name = Constants.AgedBrie; Sellin=5; Quality = 40; }|])
    Assert.Equal(41, results.[0].Quality)

[<Fact>]
let `` Aged Brie maxes out at 50 quality.``() =
    let results = sut.UpdateQuality([|{ Name = Constants.AgedBrie; Sellin=5; Quality = 50; }|])
    Assert.Equal(50, results.[0].Quality)

[<Fact>]
let `` Aged Brie follows the no zero quality rule.``() =
    let results = sut.UpdateQuality([|{ Name = Constants.AgedBrie; Sellin=5; Quality = -2; }|])
    Assert.Equal({ Name = Constants.AgedBrie; Sellin=4; Quality = 0; }, results.[0])

[<Fact>]
let `` Sulfuras will set to quality 80.``() =
    let results = sut.UpdateQuality([|{ Name = Constants.Sulfuras; Sellin=5; Quality = 50; }|])
    Assert.Equal({ Name = Constants.Sulfuras; Sellin=5; Quality = 80; }, results.[0])

[<Fact>]
let `` Sulfuras does not change quality or sell in date.``() =
    let results = sut.UpdateQuality([|{ Name = Constants.Sulfuras; Sellin=5; Quality = 80; }|])
    Assert.Equal({ Name = Constants.Sulfuras; Sellin=5; Quality = 80; }, results.[0])


[<Fact>]
let `` Conjured items degrade twice as fast.``() =
    let results = sut.UpdateQuality([|{ Name = Constants.ConjuredManaCake; Sellin=5; Quality = 5; }|])
    Assert.Equal({ Name = Constants.ConjuredManaCake; Sellin=4; Quality = 3; }, results.[0])
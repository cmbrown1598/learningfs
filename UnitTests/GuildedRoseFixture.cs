using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningFs;
using NUnit.Framework;


namespace UnitTests
{
    [TestFixture]
    public class GuildedRoseFixture
    {
        GuildedRose _store = new GuildedRose();

        [Test]
        public void QualityIsDecreased()
        {
            var item = new Item("Item1", 5, 5);
            var items = _store.UpdateQuality(new[] {item});
            Assert.AreEqual(4, items[0].Quality);
        }
    }

    namespace GildedRose.Tests
    {
        public static class ItemNames
        {
            public static string Standard = "Standard";
            public static string AgedBrie = "Aged Brie";
            public static string SulfurasHandOfRagnaros = "Sulfuras, Hand of Ragnaros";
            public static string BackstagePassesToATafkal80EtcConcert = "Backstage passes to a TAFKAL80ETC concert";
        }

        [TestFixture]
        public class FullUpdateQualityTests
        {
            [Test]
            public void StandardItem_QualityDecreasesBy1()
            {
                // Arrange
                var item = new Item
                (
                    ItemNames.Standard,
                    10,
                    15
                );

                // Act
                var result = UpdateItem(item);

                // Assert
                Assert.That(result.Sellin, Is.EqualTo(9), "Unexpected Sellin Value:");
                Assert.That(result.Quality, Is.EqualTo(14), "Unexpected Quality Value:");
            }

            [Test]
            public void StandardItem_NearlyExpired_QualityDecreasesBy1()
            {
                // Arrange
                var item = new Item(ItemNames.Standard, 1, 15);

                // Act
                var result = UpdateItem(item);

                // Assert
                Assert.That(result.Sellin, Is.EqualTo(0), "Unexpected Sellin Value:");
                Assert.That(result.Quality, Is.EqualTo(14), "Unexpected Quality Value:");
            }

            [Test]
            public void StandardItem_Expired_QualityDecreasesBy2()
            {
                // Arrange
                var item = new Item(ItemNames.Standard, 0, 15);

                // Act
                var result = UpdateItem(item);

                // Assert
                Assert.That(result.Sellin, Is.EqualTo(-1), "Unexpected Sellin Value:");
                Assert.That(result.Quality, Is.EqualTo(13), "Unexpected Quality Value:");
            }

            [Test]
            public void StandardItem_At0Qaulity_QualityStays0()
            {
                // Arrange
                var item = new Item(ItemNames.Standard, 10, 0);

                // Act
                var result = UpdateItem(item);

                // Assert
                Assert.That(result.Sellin, Is.EqualTo(9), "Unexpected Sellin Value:");
                Assert.That(result.Quality, Is.EqualTo(0), "Unexpected Quality Value:");
            }

            [Test]
            public void StandardItem_ExpiredAt0Qaulity_QualityStays0()
            {
                // Arrange
                var item =  new Item(ItemNames.Standard, 0, 0);


                // Act
                var result = UpdateItem(item);

                // Assert
                Assert.That(result.Sellin, Is.EqualTo(-1), "Unexpected Sellin Value:");
                Assert.That(result.Quality, Is.EqualTo(0), "Unexpected Quality Value:");
            }

            [Test]
            public void StandardItem_ExpiredAt1Qaulity_QualityGoesTo0()
            {
                // Arrange
                var item = new Item(ItemNames.Standard, 0, 1);

                // Act
                var result = UpdateItem(item);

                // Assert
                Assert.That(result.Sellin, Is.EqualTo(-1), "Unexpected Sellin Value:");
                Assert.That(result.Quality, Is.EqualTo(0), "Unexpected Quality Value:");
            }

            [Test]
            public void AgedBrie_QualityIncreasesBy1()
            {
                // Arrange

                var item = new Item(ItemNames.AgedBrie, 10, 15);

                // Act
                var result = UpdateItem(item);

                // Assert
                Assert.That(result.Sellin, Is.EqualTo(9), "Unexpected Sellin Value:");
                Assert.That(result.Quality, Is.EqualTo(16), "Unexpected Quality Value:");
            }

            [Test]
            public void AgedBrie_Expired_QualityIncreasesBy2()
            {
                // Arrange
                var item = new Item(ItemNames.AgedBrie, 0, 15);

                // Act
                var result = UpdateItem(item);

                // Assert
                Assert.That(result.Sellin, Is.EqualTo(-1), "Unexpected Sellin Value:");
                Assert.That(result.Quality, Is.EqualTo(17), "Unexpected Quality Value:");
            }

            [Test]
            public void AgedBrie_AtMaxQuality_QualityDoesNoChange()
            {
                // Arrange
                var item = new Item(ItemNames.AgedBrie, 10, 50);

                // Act
                var result = UpdateItem(item);

                // Assert
                Assert.That(result.Sellin, Is.EqualTo(9), "Unexpected Sellin Value:");
                Assert.That(result.Quality, Is.EqualTo(50), "Unexpected Quality Value:");
            }

            [Test]
            public void AgedBrie_ExpiredAtMaxQuality_QualityDoesNoChange()
            {
                // Arrange
                var item = new Item(ItemNames.AgedBrie, 0, 50);

                // Act
                var result = UpdateItem(item);

                // Assert
                Assert.That(result.Sellin, Is.EqualTo(-1), "Unexpected Sellin Value:");
                Assert.That(result.Quality, Is.EqualTo(50), "Unexpected Quality Value:");
            }

            [Test]
            public void AgedBrie_ExpiredAtOneBelowMaxQuality_QualityGoesToMaximum()
            {
                // Arrange
                var item = new Item(ItemNames.AgedBrie, 0, 49);

                // Act
                var result = UpdateItem(item);

                // Assert
                Assert.That(result.Sellin, Is.EqualTo(-1), "Unexpected Sellin Value:");
                Assert.That(result.Quality, Is.EqualTo(50), "Unexpected Quality Value:");
            }

            [Test]
            public void Sulfuras_DoesNotChange()
            {
                // Arrange

                var item = new Item(ItemNames.SulfurasHandOfRagnaros, 0, 80);

                // Act
                var result = UpdateItem(item);

                // Assert
                Assert.That(result.Sellin, Is.EqualTo(0), "Unexpected Sellin Value:");
                Assert.That(result.Quality, Is.EqualTo(80), "Unexpected Quality Value:");
            }

            [Test]
            public void BackstagePasses_GreaterThan10DaysRemaining_QualityIncreasesBy1()
            {
                // Arrange
                var item = new Item(ItemNames.BackstagePassesToATafkal80EtcConcert, 11, 20);
                // Act
                var result = UpdateItem(item);

                // Assert
                Assert.That(result.Sellin, Is.EqualTo(10), "Unexpected Sellin Value:");
                Assert.That(result.Quality, Is.EqualTo(21), "Unexpected Quality Value:");
            }

            [Test]
            public void BackstagePasses_AtMaxQualityGreaterThan10DaysRemaining_QualityDoesNotChange()
            {
                // Arrange
                var item = new Item(ItemNames.BackstagePassesToATafkal80EtcConcert, 11, 50);

                // Act
                var result = UpdateItem(item);

                // Assert
                Assert.That(result.Sellin, Is.EqualTo(10), "Unexpected Sellin Value:");
                Assert.That(result.Quality, Is.EqualTo(50), "Unexpected Quality Value:");
            }

            [Test]
            public void BackstagePasses_Between10and5DaysRemaining_QualityIncreasesBy2()
            {
                // Arrange
                var item = new Item(ItemNames.BackstagePassesToATafkal80EtcConcert, 10, 20);

                // Act
                var result = UpdateItem(item);

                // Assert
                Assert.That(result.Sellin, Is.EqualTo(9), "Unexpected Sellin Value:");
                Assert.That(result.Quality, Is.EqualTo(22), "Unexpected Quality Value:");
            }

            [Test]
            public void BackstagePasses_AtMaxQualityBetween10and5DaysRemaining_QualityDoesNotChange()
            {
                // Arrange
                var item = new Item(ItemNames.BackstagePassesToATafkal80EtcConcert, 10, 50);

                // Act
                var result = UpdateItem(item);

                // Assert
                Assert.That(result.Sellin, Is.EqualTo(9), "Unexpected Sellin Value:");
                Assert.That(result.Quality, Is.EqualTo(50), "Unexpected Quality Value:");
            }

            [Test]
            public void BackstagePasses_CloseToMaxQualityBetween10and5DaysRemaining_QualityGoesToMax()
            {
                var item = new Item(ItemNames.BackstagePassesToATafkal80EtcConcert, 10, 49);


                // Act
                var result = UpdateItem(item);

                // Assert
                Assert.That(result.Sellin, Is.EqualTo(9), "Unexpected Sellin Value:");
                Assert.That(result.Quality, Is.EqualTo(50), "Unexpected Quality Value:");
            }

            [Test]
            public void BackstagePasses_LessThan5DaysRemaining_QualityIncreasesBy3()
            {
                // Arrange
                var item = new Item(ItemNames.BackstagePassesToATafkal80EtcConcert, 4, 20);


                // Act
                var result = UpdateItem(item);

                // Assert
                Assert.That(result.Sellin, Is.EqualTo(3), "Unexpected Sellin Value:");
                Assert.That(result.Quality, Is.EqualTo(23), "Unexpected Quality Value:");
            }

            [Test]
            public void BackstagePasses_AtMaxQualityLessThan5DaysRemaining_QualityDoesNotChange()
            {
                var item = new Item(ItemNames.BackstagePassesToATafkal80EtcConcert, 4, 50);


                // Act
                var result = UpdateItem(item);

                // Assert
                Assert.That(result.Sellin, Is.EqualTo(3), "Unexpected Sellin Value:");
                Assert.That(result.Quality, Is.EqualTo(50), "Unexpected Quality Value:");
            }

            [Test]
            public void BackstagePasses_CloseToMaxQualityLessThan5DaysRemaining_QualityGoesToMax()
            {
                var item = new Item(ItemNames.BackstagePassesToATafkal80EtcConcert, 4, 49);


                // Act
                var result = UpdateItem(item);

                // Assert
                Assert.That(result.Sellin, Is.EqualTo(3), "Unexpected Sellin Value:");
                Assert.That(result.Quality, Is.EqualTo(50), "Unexpected Quality Value:");
            }

            [Test]
            public void BackstagePasses_Expiring_QualityGoesTo0()
            {
                var item = new Item(ItemNames.BackstagePassesToATafkal80EtcConcert, 0, 35);

                // Act
                var result = UpdateItem(item);

                // Assert
                Assert.That(result.Sellin, Is.EqualTo(-1), "Unexpected Sellin Value:");
                Assert.That(result.Quality, Is.EqualTo(0), "Unexpected Quality Value:");
            }

            [Test]
            public void AllItemsGetUpdated()
            {
                // Arrange
                var items = new List<Item>
                {
                    new Item(ItemNames.Standard, 0, 0),
                    new Item(ItemNames.Standard, 0, 0)
                };

                // Act
                var results = UpdateAllItems(items);

                // Assert
                foreach (var item in results)
                {
                    Assert.That(item.Sellin, Is.EqualTo(-1), "Unexpected Sellin Value:");
                    Assert.That(item.Quality, Is.EqualTo(0), "Unexpected Quality Value:");
                }
            }

            private Item UpdateItem(Item item)
            {
                var items = new List<Item>
                {
                    item
                };

                return UpdateAllItems(items)[0];
            }

            private static Item[] UpdateAllItems(List<Item> items)
            {
                var program = new GuildedRose();

                return program.UpdateQuality(items.ToArray());
            }
        }
    }
}

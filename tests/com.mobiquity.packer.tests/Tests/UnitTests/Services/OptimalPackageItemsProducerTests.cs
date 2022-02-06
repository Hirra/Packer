using com.mobiquity.packer.Business.Models;
using com.mobiquity.packer.Services;
using NUnit.Framework;
using System.Collections.Generic;

namespace com.mobiquity.packer.tests
{
    [TestFixture]
    public class OptimalPackageItemsProducerTests
    {
        private IOptimalPackageItemsProducer<Package> producer;

        [SetUp]
        public void Setup()
        {
            producer = new OptimalPackageItemsProducer();
        }

        [Test]
        public void ProduceOptimalPackage_SuccessFlow()
        {
            //Assert
            var package = new Package
            {
                PackgeWeight = 8100,
                Items = new List<Item> {
                        new Item
                        {
                            Index = 1,
                            Weight = 5338,
                            Cost = 45
                        } ,
                        new Item
                        {
                            Index = 2,
                            Weight = 8862,
                            Cost = 98
                        },
                        new Item
                        {
                            Index = 3,
                            Weight = 7848,
                            Cost =  3
                        },
                        new Item
                        {
                            Index = 4,
                            Weight = 7230,
                            Cost =  76
                        },
                        new Item
                        {
                            Index = 5,
                            Weight = 3018,
                            Cost =  9
                        },
                        new Item
                        {
                            Index = 6,
                            Weight = 4634,
                            Cost =  48
                        }
                }
            };

            //Act 
            var actual = producer.ProduceOptimalPackingItems(package);

            //Assert
            Assert.That(actual, Is.EqualTo("4"));
        }

        [Test]
        public void ProduceOptimalPackage_EmptyList_SuccessFlow()
        {
            //Assert
            var package = new Package
            {
                PackgeWeight = 800,
                Items = new List<Item> { }
            };

            //Act 
            var actual = producer.ProduceOptimalPackingItems(package);

            //Assert
            Assert.That(actual, Is.EqualTo("-"));
        }

        [TearDown]
        public void TearDown()
        {
            producer = null;
        }
    }
}

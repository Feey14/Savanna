using System;
using System.Collections.Generic;
using AnimalTypeClassLibrary;
using Savanna.Animals.Predators;
using Savanna.SavannaRandomNumbers;
using Xunit;

namespace AnimalTests
{
    public class AnimalTests
    {
        [Fact]
        public void AnimalDies()
        {
            Lion animal = new Lion
            {
                Health = 20
            };
            animal.Die();
            Assert.Equal(0,animal.Health);
        }
        [Fact]
        public void AnimalStraysTopLeft()
        {
            SavannaFakeRandomNumbers random = new SavannaFakeRandomNumbers();
            random.fakeRandomNumber = -1;
            List<Animal> animals = new List<Animal>();
            Lion animal = new Lion
            {
                WidthCoordinate = 1,
                HeightCoordinate = 1
            };
            animal.Stray(animals, 5, 5,random);
            Assert.Equal(0, animal.WidthCoordinate);
            Assert.Equal(0, animal.HeightCoordinate);
        }
        [Fact]
        public void AnimalStraysBottomRight()
        {
            SavannaFakeRandomNumbers random = new SavannaFakeRandomNumbers();
            random.fakeRandomNumber = 1;
            List<Animal> animals = new List<Animal>();
            Lion animal = new Lion
            {
                WidthCoordinate = 1,
                HeightCoordinate = 1
            };
            animal.Stray(animals, 5, 5, random);
            Assert.Equal(2, animal.WidthCoordinate);
            Assert.Equal(2, animal.HeightCoordinate);
        }

    }
    class SavannaFakeRandomNumbers : ISavannaRandomNumbers
    {
        public int fakeRandomNumber { get; set; }

        public int GetRandomNumber(int fakeRandomNumber)
        {
            return fakeRandomNumber;
        }

        public int GetRandomNumber(int from, int to)
        {
            return fakeRandomNumber;
        }
    }
}

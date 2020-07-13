using System.Collections.Generic;
using AnimalTypeClassLibrary;
using Savanna.Animals.NonPredators;
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
            Assert.Equal(0, animal.Health);
        }

        [Fact]
        public void AnimalStraysTopLeft()
        {
            SavannaFakeRandomNumbers random = new SavannaFakeRandomNumbers
            {
                FakeRandomNumber = -1
            };
            List<Animal> animals = new List<Animal>();
            Lion animal = new Lion
            {
                WidthCoordinate = 1,
                HeightCoordinate = 1
            };
            animal.Stray(animals, 5, 5, random);
            Assert.Equal(0, animal.WidthCoordinate);
            Assert.Equal(0, animal.HeightCoordinate);
        }

        [Fact]
        public void AnimalStraysBottomRight()
        {
            SavannaFakeRandomNumbers random = new SavannaFakeRandomNumbers
            {
                FakeRandomNumber = 1
            };
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

        [Fact]
        public void BreedingIsSuccesfullParentsStray()
        {
            Lion animal1 = new Lion()
            {
                WidthCoordinate = 1,
                HeightCoordinate = 1
            };
            Lion animal2 = new Lion()
            {
                WidthCoordinate = 2,
                HeightCoordinate = 1
            };
            List<Animal> nearbyanimals = new List<Animal>();
            List<BabyAnimal> babyAnimals = new List<BabyAnimal>();
            BabyAnimal babyAnimal = new BabyAnimal(animal1, animal2)
            {
                RoundCount = 4
            };

            babyAnimals.Add(babyAnimal);
            Assert.True(animal1.Breed(nearbyanimals, babyAnimals, 5, 5));
        }

        [Fact]
        public void CantBreedNoAnimalsNearby()
        {
            Lion animal = new Lion()
            {
                WidthCoordinate = 1,
                HeightCoordinate = 1
            };
            List<Animal> nearbyanimals = new List<Animal>();
            List<BabyAnimal> babyAnimals = new List<BabyAnimal>();
            Assert.False(animal.Breed(nearbyanimals, babyAnimals, 5, 5));
        }

        [Fact]
        public void BabyAnimalIsCreated()
        {
            Lion animal1 = new Lion()
            {
                WidthCoordinate = 1,
                HeightCoordinate = 1
            };
            Lion animal2 = new Lion()
            {
                WidthCoordinate = 2,
                HeightCoordinate = 1
            };
            List<Animal> nearbyanimals = new List<Animal>();
            List<BabyAnimal> babyAnimals = new List<BabyAnimal>();
            BabyAnimal babyAnimal = new BabyAnimal(animal1, animal2);
            animal1.Breed(nearbyanimals, babyAnimals, 5, 5);
            Assert.NotNull(babyAnimal);
        }

        [Fact]
        public void MoveTowardsAnimalTopLeft()
        {
            Lion lion = new Lion()
            {
                WidthCoordinate = 3,
                HeightCoordinate = 3
            };
            Antelope antelope = new Antelope()
            {
                WidthCoordinate = 0,
                HeightCoordinate = 0
            };
            List<Animal> nearbyanimals = new List<Animal>() { antelope };
            lion.MoveTowardsAnimal(antelope, nearbyanimals);
            Assert.Equal(2, lion.WidthCoordinate);
            Assert.Equal(2, lion.HeightCoordinate);
        }

        [Fact]
        public void MoveTowardsAnimalBottomRight()
        {
            Lion lion = new Lion()
            {
                WidthCoordinate = 0,
                HeightCoordinate = 0
            };
            Antelope antelope = new Antelope()
            {
                WidthCoordinate = 3,
                HeightCoordinate = 3
            };
            List<Animal> nearbyanimals = new List<Animal>() { antelope };
            lion.MoveTowardsAnimal(antelope, nearbyanimals);
            Assert.Equal(2, lion.WidthCoordinate);
            Assert.Equal(2, lion.HeightCoordinate);
        }
    }

    internal class SavannaFakeRandomNumbers : ISavannaRandomNumbers
    {
        public int FakeRandomNumber { get; set; }

        public int GetRandomNumber(int number)
        {
            return FakeRandomNumber;
        }

        public int GetRandomNumber(int from, int to)
        {
            return FakeRandomNumber;
        }
    }
}
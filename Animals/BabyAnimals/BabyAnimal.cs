﻿using System;

namespace Savanna.Animals

{
    public class BabyAnimal
    {
        public int RoundCount { get; set; } = 0;
        public Animal Parent1 { get; set; }
        public Animal Parent2 { get; set; }
        public int Parent1WidthCoordinates { get; set; }
        public int Parent2WidthCoordinates { get; set; }
        public int Parent1Heightcoordinates { get; set; }
        public int Parent2Heightcoordinates { get; set; }

        /// <summary>
        /// constructor that captures parent data
        /// </summary>
        public BabyAnimal(Animal parent1, Animal parent2)
        {
            Parent1 = parent1;
            Parent2 = parent2;
            Parent1WidthCoordinates = Parent1.WidthCoordinate;
            Parent2WidthCoordinates = Parent2.WidthCoordinate;
            Parent1Heightcoordinates = Parent1.HeightCoordinate;
            Parent2Heightcoordinates = Parent2.HeightCoordinate;
        }

        /// <summary>
        /// Move function for BabyAnimal makes new animal if 3 rounds has passed
        /// also checks if all criterias for baby to be born are completed such as parents didnt move
        /// </summary>
        public Animal Move()
        {
            if (RoundCount > 3 || Parent1.WidthCoordinate != Parent1WidthCoordinates || Parent2.WidthCoordinate != Parent2WidthCoordinates || Parent1.HeightCoordinate != Parent1Heightcoordinates || Parent2.HeightCoordinate != Parent2Heightcoordinates || Parent1 == null || Parent2 == null) return null;
            if (RoundCount == 3)
            {
                Type classname = Parent1.GetType();
                Animal an = (Animal)Activator.CreateInstance(classname);
                RoundCount++;
                return an;
            }
            else
            {
                RoundCount++;
                return null;
            }
        }
    }
}
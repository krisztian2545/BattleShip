using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Data
{
    public class Ship
    {

        public int Length { get; }
        public Vector[] Coordinates;
        public bool IsHorizontal;
        
        public Ship(int length)
        {
            Length = length;
            IsHorizontal = true;
            Coordinates = new Vector[Length];
            for (int i = 0; i < Length; i++)
                Coordinates[i] = new Vector(-1, -1);
        }

        public void GotHitAt(Vector coords)
        {

        }
        
        public bool IsDestroyed()
        {

            return false; // for now
        }

        public void Mirror()
        {
            IsHorizontal = !IsHorizontal;
        }

        public void Replace(Vector head)
        {

        }

        public static Ship[] CreateCrew(params int[] lengths)
        {
            Ship[] ships = new Ship[lengths.Length];

            for (int i = 0; i < lengths.Length; i++)
                ships[i] = new Ship(lengths[i]);

            return ships;
        }

    }
}

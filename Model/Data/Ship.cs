using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Data
{
    public class Ship
    {

        public int Length { get; }
        public Vector[] Coordinates { get; private set; }
        public bool IsHorizontal;
        
        public Ship(int length)
        {
            Length = length;
            IsHorizontal = true;
            Coordinates = new Vector[Length];
            for (int i = 0; i < Length; i++)
                Coordinates[i] = new Vector(-1, -1);
        }

        public Ship(Ship p)
        {
            this.Length = p.Length;
            this.Coordinates = (Vector[])p.Coordinates.Clone();
            this.IsHorizontal = p.IsHorizontal;
        }

        public void GotHitAt(Vector coords)
        {

        }
        
        public bool IsDestroyed()
        {

            return false; // for now
        }

        public void Rotate()
        {
            IsHorizontal = !IsHorizontal;
            Replace(Coordinates[0]);
        }

        public void Replace(Vector head)
        {
            Coordinates[0] = head;
            if(IsHorizontal)
            {
                for (int i = 1; i < Length; i++)
                    Coordinates[i] = new Vector(head.X + i, head.Y);
            } else
            {
                for (int i = 1; i < Length; i++)
                    Coordinates[i] = new Vector(head.X, head.Y + i);
            }
        }

        public static Ship[] CreateCrew(params int[] lengths)
        {
            Ship[] ships = new Ship[lengths.Length];

            for (int i = 0; i < lengths.Length; i++)
                ships[i] = new Ship(lengths[i]);

            return ships;
        }

        public static bool CollisionDetection(Ship s1, Ship s2)
        {
            //s1 = new Ship(s1);
            //s2 = new Ship(s2);

            if(s1.IsHorizontal)
            {
                if(s2.IsHorizontal)
                {

                } else
                {

                }
            } else
            {
                if (s2.IsHorizontal)
                {

                }
                else
                {

                }
            }

            return false;
        }

    }
}

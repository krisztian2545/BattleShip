using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Data
{
    public class Ship
    {

        public int Length { get; }
        public Vector[] Coordinates { get; private set; }
        public bool[] Hits { get; private set; }
        public bool IsHorizontal;
        
        public Ship(int length)
        {
            Length = length;
            IsHorizontal = true;
            Coordinates = new Vector[Length];
            Hits = new bool[Length];
            Replace(new Vector(0, 0));
        }

        public Ship(Ship p)
        {
            this.Length = p.Length;
            this.Coordinates = (Vector[])p.Coordinates.Clone();
            this.IsHorizontal = p.IsHorizontal;
        }

        public bool GotHitAt(Vector coords)
        {
            for (int i = 0; i < Length; i++)
                if (Coordinates[i] == coords)
                {
                    Hits[i] = true;
                    return true;
                }

            return false;
        }
        
        public bool IsDestroyed()
        {
            foreach (bool b in Hits)
                if (!b)
                    return false;

            return true;
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

            if ((s1.Coordinates[0].X <= s2.Coordinates[s2.Length - 1].X +1) && (s1.Coordinates[s1.Length - 1].X >= s2.Coordinates[0].X -1) && // horizontally collides
                (s1.Coordinates[0].Y <= s2.Coordinates[s2.Length - 1].Y +1) && (s1.Coordinates[s1.Length - 1].Y >= s2.Coordinates[0].Y -1)) // vertically collides
            {
                return true;
            }

            return false;
        }

    }
}

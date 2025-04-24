using System.Numerics;

namespace Projet_S.Component.MAP
{
    public struct Coordinates
    {
        public readonly int columns;
        public readonly int rows;

        public Coordinates(int columns, int rows)
        {
            this.columns = columns;
            this.rows = rows;
        }


        public static Coordinates zero => new Coordinates(0, 0);
        public static Coordinates left => new Coordinates(-1, 0);
        public static Coordinates right => new Coordinates(1, 0);
        public static Coordinates top => new Coordinates(0, -1);
        public static Coordinates bottom => new Coordinates(0, 1);

        public static Coordinates operator -(Coordinates a)
        {
            return new Coordinates(-a.columns, -a.rows);
        }


        public static Coordinates operator *(Coordinates coord, int scalar)
        {
            return new Coordinates(coord.columns * scalar, coord.rows * scalar);
        }
        public static Coordinates operator *(int scalar, Coordinates coord)
        {
            return new Coordinates(coord.columns * scalar, coord.rows * scalar);
        }

        public static Coordinates operator -(Coordinates a, Coordinates b)
        {
            return new Coordinates(a.columns - b.columns, a.rows - b.rows);
        }

        public static Coordinates operator +(Coordinates a, Coordinates b)
        {
            return new Coordinates(a.columns + b.columns, a.rows + b.rows);
        }

        public static bool operator ==(Coordinates a, Coordinates b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Coordinates a, Coordinates b)
        {
            return !a.Equals(b);
        }

        public override bool Equals(object obj)
        {
            return obj is Coordinates other && columns == other.columns && rows == other.rows;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(columns, rows);
        }

        public static Coordinates getRandom(int pMaxCols, int pMaxRows)
        {
            Random r = new Random();
            return new Coordinates(r.Next(0, pMaxCols), r.Next(0, pMaxRows));

        }

        public Vector2 ToVector => new Vector2(columns, rows);
    }
}

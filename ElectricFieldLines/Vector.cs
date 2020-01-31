using System;

namespace ElectricFieldLines
{
    public struct Vector
    {
        public double X { get; }
        public double Y { get; }

        private double _module;

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
            _module = Math.Sqrt(x * x + y * y);
        }

        public static Vector operator +(Vector v1, Vector v2)
        {   
            return new Vector(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector operator *(Vector v1, double scalar)
        {
            return new Vector(v1.X * scalar, v1.Y * scalar);
        }

        public static Vector operator /(Vector v1, double scalar)
        {
            return new Vector(v1.X / scalar, v1.Y / scalar);
        }


        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y);
        }

        public double Module()
        {
            return _module;
        }

        public override string ToString()
        {
            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y} (Module: {_module})";
        }
    }
}
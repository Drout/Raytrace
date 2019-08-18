using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
//using System.Windows.Media.Media3D;

namespace RayTraceHelper
{
    public class Class1
    {
        //Vector3
    }

    public struct Point3D
    {
        public Point3D(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        //public Point3D()
        //{ }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public static Point3D operator +(Point3D a, Point3D b) => new Point3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        public static Point3D operator -(Point3D a, Point3D b) => new Point3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        public static Point3D operator *(Point3D a, Point3D b) => new Point3D(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
        public static Point3D operator *(Point3D a, float b) => new Point3D(a.X * b, a.Y * b, a.Z * b);
    }

    public struct Sphere
    {
        public Sphere(Vector3 c, float r)
        {
            Center = c;
            Radius = r;

        }
        public Vector3 Center { get; set; }
        public float Radius { get; set; }
        public float Q
        {
            get
            {
                return Radius * Radius;
            }
        }
    }
}

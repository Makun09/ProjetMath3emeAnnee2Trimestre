using System;
using Math._2D;
using UnityEngine;

namespace Math._2D
{
    // Transformation affine 2D : z -> a*z + t
    public class Transform2D
    {
        private ComplexNumber a; // rotation + homothétie
        private ComplexNumber t; // translation

        public Transform2D()
        {
            a = new ComplexNumber(1, 0); // identité
            t = new ComplexNumber(0, 0);
        }

        public static Transform2D Translation(double tx, double ty)
        {
            Transform2D tr = new Transform2D();
            tr.t = new ComplexNumber(tx, ty);
            return tr;
        }

        public static Transform2D Homothetie(double k)
        {
            Transform2D tr = new Transform2D();
            tr.a = new ComplexNumber(k, 0);
            return tr;
        }

        public static Transform2D Rotation(double angleRad)
        {
            Transform2D tr = new Transform2D();
            double c = System.Math.Cos(angleRad);
            double s = System.Math.Sin(angleRad);
            tr.a = new ComplexNumber(c, s);
            return tr;
        }

        public static Transform2D Similitude(double k, double angleRad)
        {
            Transform2D tr = new Transform2D();
            double c = System.Math.Cos(angleRad);
            double s = System.Math.Sin(angleRad);
            tr.a = new ComplexNumber(k * c, k * s);
            return tr;
        }

        // Applique la transformation à un complexe
        public ComplexNumber Apply(ComplexNumber z)
        {
            return a.Multiply(z).Add(t);
        }

        // Applique la transformation à un Vector2 Unity
        public Vector2 Apply(Vector2 v)
        {
            ComplexNumber z = new ComplexNumber(v.x, v.y);
            ComplexNumber r = Apply(z);
            return new Vector2((float)r.Real, (float)r.Imaginary);
        }
    }
}
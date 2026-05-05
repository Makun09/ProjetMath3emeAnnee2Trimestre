using System;

namespace Math._2D
{
    public class ComplexNumber
    {
        public double Real;
        public double Imaginary;

        // Constructeur
        public ComplexNumber(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        // Addition
        public ComplexNumber Add(ComplexNumber other)
        {
            return new ComplexNumber(Real + other.Real, Imaginary + other.Imaginary);
        }

        // Soustraction
        public ComplexNumber Subtract(ComplexNumber other)
        {
            return new ComplexNumber(Real - other.Real, Imaginary - other.Imaginary);
        }

        // Multiplication
        public ComplexNumber Multiply(ComplexNumber other)
        {
            double r = Real * other.Real - Imaginary * other.Imaginary;
            double i = Real * other.Imaginary + Imaginary * other.Real;
            return new ComplexNumber(r, i);
        }

        // Division
        public ComplexNumber Divide(ComplexNumber other)
        {
            double denom = other.Real * other.Real + other.Imaginary * other.Imaginary;

            if (denom == 0)
                throw new DivideByZeroException("Division par un complexe nul");

            double r = (Real * other.Real + Imaginary * other.Imaginary) / denom;
            double i = (Imaginary * other.Real - Real * other.Imaginary) / denom;

            return new ComplexNumber(r, i);
        }

        // Conjugaison
        public ComplexNumber Conjugate()
        {
            return new ComplexNumber(Real, -Imaginary);
        }

        // Module
        public double Magnitude()
        {
            return System.Math.Sqrt(Real * Real + Imaginary * Imaginary);
        }

        // Argument (angle en radians)
        public double Argument()
        {
            return System.Math.Atan2(Imaginary, Real);
        }

        // Conversion complexe -> matrice 2x2
        public Matrix2x2 ToMatrix()
        {
            return new Matrix2x2(
                Real, -Imaginary,
                Imaginary, Real
            );
        }

        // Override ToString (pratique debug)
        public override string ToString()
        {
            return $"{Real} + {Imaginary}i";
        }
    }
}
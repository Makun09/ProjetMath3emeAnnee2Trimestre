using System;

public class Vector4Custom
{
    public double X;
    public double Y;
    public double Z;
    public double W;

    public Vector4Custom(double x, double y, double z, double w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public Vector4Custom Add(Vector4Custom other)
    {
        return new Vector4Custom(X + other.X, Y + other.Y, Z + other.Z, W + other.W);
    }

    public Vector4Custom Subtract(Vector4Custom other)
    {
        return new Vector4Custom(X - other.X, Y - other.Y, Z - other.Z, W - other.W);
    }

    public Vector4Custom Multiply(double scalar)
    {
        return new Vector4Custom(X * scalar, Y * scalar, Z * scalar, W * scalar);
    }

    // Produit scalaire 4D
    public double Dot(Vector4Custom other)
    {
        return X * other.X + Y * other.Y + Z * other.Z + W * other.W;
    }

    public double Magnitude()
    {
        return System.Math.Sqrt(X * X + Y * Y + Z * Z + W * W);
    }

    public Vector4Custom Normalize()
    {
        double magnitude = Magnitude();

        if (magnitude == 0)
            throw new Exception("Impossible de normaliser un vecteur 4D nul.");

        return new Vector4Custom(X / magnitude, Y / magnitude, Z / magnitude, W / magnitude);
    }

    public override string ToString()
    {
        return $"({X}, {Y}, {Z}, {W})";
    }
}
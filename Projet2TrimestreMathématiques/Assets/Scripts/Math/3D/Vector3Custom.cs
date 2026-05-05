using System;

public class Vector3Custom
{
    public double X;
    public double Y;
    public double Z;

    public Vector3Custom(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public Vector3Custom Add(Vector3Custom other)
    {
        return new Vector3Custom(X + other.X, Y + other.Y, Z + other.Z);
    }

    public Vector3Custom Subtract(Vector3Custom other)
    {
        return new Vector3Custom(X - other.X, Y - other.Y, Z - other.Z);
    }

    public Vector3Custom Multiply(double scalar)
    {
        return new Vector3Custom(X * scalar, Y * scalar, Z * scalar);
    }

    // Produit scalaire : mesure l'alignement entre deux vecteurs
    public double Dot(Vector3Custom other)
    {
        return X * other.X + Y * other.Y + Z * other.Z;
    }

    // Produit vectoriel : retourne un vecteur perpendiculaire aux deux vecteurs
    public Vector3Custom Cross(Vector3Custom other)
    {
        return new Vector3Custom(
            Y * other.Z - Z * other.Y,
            Z * other.X - X * other.Z,
            X * other.Y - Y * other.X
        );
    }

    public double Magnitude()
    {
        return System.Math.Sqrt(X * X + Y * Y + Z * Z);
    }

    public Vector3Custom Normalize()
    {
        double magnitude = Magnitude();

        if (magnitude == 0)
            throw new Exception("Impossible de normaliser un vecteur nul.");

        return new Vector3Custom(X / magnitude, Y / magnitude, Z / magnitude);
    }

    public override string ToString()
    {
        return $"({X}, {Y}, {Z})";
    }
}
using System;

public class QuaternionCustom
{
    public double W;
    public double X;
    public double Y;
    public double Z;

    public QuaternionCustom(double w, double x, double y, double z)
    {
        W = w;
        X = x;
        Y = y;
        Z = z;
    }
    
    public QuaternionCustom Add(QuaternionCustom other)
    {
        return new QuaternionCustom(
            W + other.W,
            X + other.X,
            Y + other.Y,
            Z + other.Z
        );
    }

    public QuaternionCustom Subtract(QuaternionCustom other)
    {
        return new QuaternionCustom(
            W - other.W,
            X - other.X,
            Y - other.Y,
            Z - other.Z
        );
    }
    
    public QuaternionCustom Multiply(QuaternionCustom other)
    {
        double newW = W * other.W - X * other.X - Y * other.Y - Z * other.Z;
        double newX = W * other.X + X * other.W + Y * other.Z - Z * other.Y;
        double newY = W * other.Y - X * other.Z + Y * other.W + Z * other.X;
        double newZ = W * other.Z + X * other.Y - Y * other.X + Z * other.W;

        return new QuaternionCustom(newW, newX, newY, newZ);
    }
    
    public QuaternionCustom Conjugate()
    {
        return new QuaternionCustom(W, -X, -Y, -Z);
    }
    
    public double Norm()
    {
        return System.Math.Sqrt(W * W + X * X + Y * Y + Z * Z);
    }
    
    public QuaternionCustom Normalize()
    {
        double norm = Norm();

        if (norm == 0)
            throw new Exception("Impossible de normaliser un quaternion nul.");

        return new QuaternionCustom(W / norm, X / norm, Y / norm, Z / norm);
    }

    public bool IsUnit(double tolerance = 0.0001)
    {
        return System.Math.Abs(Norm() - 1.0) < tolerance;
    }

    public override string ToString()
    {
        return $"{W} + {X}i + {Y}j + {Z}k";
    }
}
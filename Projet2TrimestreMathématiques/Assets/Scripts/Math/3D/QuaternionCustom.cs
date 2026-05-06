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
    
    public Matrix4x4Custom ToMatrix4x4()
    {
        return new Matrix4x4Custom(
            W, -X, -Y, -Z,
            X,  W, -Z,  Y,
            Y,  Z,  W, -X,
            Z, -Y,  X,  W
        );
    }
    
    public static QuaternionCustom FromAxisAngle(Vector3Custom axis, double angleDegrees)
    {
        Vector3Custom normalizedAxis = axis.Normalize();

        double angleRadians = angleDegrees * System.Math.PI / 180.0;
        double halfAngle = angleRadians / 2.0;

        double w = System.Math.Cos(halfAngle);
        double sinHalfAngle = System.Math.Sin(halfAngle);

        double x = normalizedAxis.X * sinHalfAngle;
        double y = normalizedAxis.Y * sinHalfAngle;
        double z = normalizedAxis.Z * sinHalfAngle;

        return new QuaternionCustom(w, x, y, z).Normalize();
    }
    
    public Matrix3x3 ToRotationMatrix3x3()
    {
        QuaternionCustom q = Normalize();

        double w = q.W;
        double x = q.X;
        double y = q.Y;
        double z = q.Z;

        return new Matrix3x3(
            1 - 2 * y * y - 2 * z * z,
            2 * x * y - 2 * z * w,
            2 * x * z + 2 * y * w,

            2 * x * y + 2 * z * w,
            1 - 2 * x * x - 2 * z * z,
            2 * y * z - 2 * x * w,

            2 * x * z - 2 * y * w,
            2 * y * z + 2 * x * w,
            1 - 2 * x * x - 2 * y * y
        );
    }
    
    public Vector3Custom RotatePoint(Vector3Custom point)
    {
        QuaternionCustom q = Normalize();

        QuaternionCustom pointQuaternion = new QuaternionCustom(
            0,
            point.X,
            point.Y,
            point.Z
        );

        QuaternionCustom rotatedQuaternion = q
            .Multiply(pointQuaternion)
            .Multiply(q.Conjugate());

        return new Vector3Custom(
            rotatedQuaternion.X,
            rotatedQuaternion.Y,
            rotatedQuaternion.Z
        );
    }
    
    public Vector3Custom RotatePointAroundPivot(Vector3Custom point, Vector3Custom pivot)
    {
        Vector3Custom localPoint = point.Subtract(pivot);

        Vector3Custom rotatedLocalPoint = RotatePoint(localPoint);

        Vector3Custom finalPoint = rotatedLocalPoint.Add(pivot);

        return finalPoint;
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
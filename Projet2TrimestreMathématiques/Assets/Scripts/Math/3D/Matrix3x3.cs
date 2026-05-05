using System;

public class Matrix3x3
{
    public double M11, M12, M13;
    public double M21, M22, M23;
    public double M31, M32, M33;

    public Matrix3x3(
        double m11, double m12, double m13,
        double m21, double m22, double m23,
        double m31, double m32, double m33)
    {
        M11 = m11; M12 = m12; M13 = m13;
        M21 = m21; M22 = m22; M23 = m23;
        M31 = m31; M32 = m32; M33 = m33;
    }

    public Matrix3x3 Multiply(Matrix3x3 other)
    {
        return new Matrix3x3(
            M11 * other.M11 + M12 * other.M21 + M13 * other.M31,
            M11 * other.M12 + M12 * other.M22 + M13 * other.M32,
            M11 * other.M13 + M12 * other.M23 + M13 * other.M33,

            M21 * other.M11 + M22 * other.M21 + M23 * other.M31,
            M21 * other.M12 + M22 * other.M22 + M23 * other.M32,
            M21 * other.M13 + M22 * other.M23 + M23 * other.M33,

            M31 * other.M11 + M32 * other.M21 + M33 * other.M31,
            M31 * other.M12 + M32 * other.M22 + M33 * other.M32,
            M31 * other.M13 + M32 * other.M23 + M33 * other.M33
        );
    }

    public Vector3Custom Multiply(Vector3Custom vector)
    {
        return new Vector3Custom(
            M11 * vector.X + M12 * vector.Y + M13 * vector.Z,
            M21 * vector.X + M22 * vector.Y + M23 * vector.Z,
            M31 * vector.X + M32 * vector.Y + M33 * vector.Z
        );
    }
    
    public QuaternionCustom ToRotationQuaternion()
    {
        double trace = M11 + M22 + M33;

        double w, x, y, z;

        if (trace > 0)
        {
            double s = System.Math.Sqrt(trace + 1.0) * 2.0;

            w = 0.25 * s;
            x = (M32 - M23) / s;
            y = (M13 - M31) / s;
            z = (M21 - M12) / s;
        }
        else if (M11 > M22 && M11 > M33)
        {
            double s = System.Math.Sqrt(1.0 + M11 - M22 - M33) * 2.0;

            w = (M32 - M23) / s;
            x = 0.25 * s;
            y = (M12 + M21) / s;
            z = (M13 + M31) / s;
        }
        else if (M22 > M33)
        {
            double s = System.Math.Sqrt(1.0 + M22 - M11 - M33) * 2.0;

            w = (M13 - M31) / s;
            x = (M12 + M21) / s;
            y = 0.25 * s;
            z = (M23 + M32) / s;
        }
        else
        {
            double s = System.Math.Sqrt(1.0 + M33 - M11 - M22) * 2.0;

            w = (M21 - M12) / s;
            x = (M13 + M31) / s;
            y = (M23 + M32) / s;
            z = 0.25 * s;
        }

        return new QuaternionCustom(w, x, y, z).Normalize();
    }
    
    public bool ApproximatelyEquals(Matrix3x3 other, double tolerance = 0.0001)
    {
        return AreApproximatelyEqual(M11, other.M11, tolerance) &&
               AreApproximatelyEqual(M12, other.M12, tolerance) &&
               AreApproximatelyEqual(M13, other.M13, tolerance) &&

               AreApproximatelyEqual(M21, other.M21, tolerance) &&
               AreApproximatelyEqual(M22, other.M22, tolerance) &&
               AreApproximatelyEqual(M23, other.M23, tolerance) &&

               AreApproximatelyEqual(M31, other.M31, tolerance) &&
               AreApproximatelyEqual(M32, other.M32, tolerance) &&
               AreApproximatelyEqual(M33, other.M33, tolerance);
    }
    
    public static Matrix3x3 FromAxisAngle(Vector3Custom axis, double angleDegrees)
    {
        Vector3Custom normalizedAxis = axis.Normalize();

        double x = normalizedAxis.X;
        double y = normalizedAxis.Y;
        double z = normalizedAxis.Z;

        double angleRadians = angleDegrees * System.Math.PI / 180.0;

        double cos = System.Math.Cos(angleRadians);
        double sin = System.Math.Sin(angleRadians);
        double oneMinusCos = 1.0 - cos;

        return new Matrix3x3(
            cos + x * x * oneMinusCos,
            x * y * oneMinusCos - z * sin,
            x * z * oneMinusCos + y * sin,

            y * x * oneMinusCos + z * sin,
            cos + y * y * oneMinusCos,
            y * z * oneMinusCos - x * sin,

            z * x * oneMinusCos - y * sin,
            z * y * oneMinusCos + x * sin,
            cos + z * z * oneMinusCos
        );
    }

    private bool AreApproximatelyEqual(double a, double b, double tolerance)
    {
        return System.Math.Abs(a - b) < tolerance;
    }

    public static Matrix3x3 Identity()
    {
        return new Matrix3x3(
            1, 0, 0,
            0, 1, 0,
            0, 0, 1
        );
    }

    public override string ToString()
    {
        return $"[{M11}, {M12}, {M13}]\n[{M21}, {M22}, {M23}]\n[{M31}, {M32}, {M33}]";
    }
}
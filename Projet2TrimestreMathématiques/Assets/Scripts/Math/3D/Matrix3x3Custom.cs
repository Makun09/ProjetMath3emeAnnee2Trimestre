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
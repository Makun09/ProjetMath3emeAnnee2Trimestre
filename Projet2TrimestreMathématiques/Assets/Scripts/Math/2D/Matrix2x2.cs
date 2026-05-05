using System;

namespace Math._2D
{
    public class Matrix2x2
    {
        public double M11, M12;
        public double M21, M22;

        // Constructeur
        public Matrix2x2(double m11, double m12, double m21, double m22)
        {
            M11 = m11;
            M12 = m12;
            M21 = m21;
            M22 = m22;
        }

        // Addition
        public Matrix2x2 Add(Matrix2x2 other)
        {
            return new Matrix2x2(
                M11 + other.M11, M12 + other.M12,
                M21 + other.M21, M22 + other.M22
            );
        }

        // Multiplication matricielle
        public Matrix2x2 Multiply(Matrix2x2 other)
        {
            return new Matrix2x2(
                M11 * other.M11 + M12 * other.M21,
                M11 * other.M12 + M12 * other.M22,

                M21 * other.M11 + M22 * other.M21,
                M21 * other.M12 + M22 * other.M22
            );
        }

        // Déterminant
        public double Determinant()
        {
            return M11 * M22 - M12 * M21;
        }

        // Inverse
        public Matrix2x2 Inverse()
        {
            double det = Determinant();

            if (det == 0)
                throw new Exception("Matrice non inversible");

            return new Matrix2x2(
                M22 / det, -M12 / det,
                -M21 / det, M11 / det
            );
        }

        // Conversion matrice -> complexe
        public ComplexNumber ToComplex()
        {
            // Doit être de la forme :
            // [ a  -b ]
            // [ b   a ]

            if (M11 == M22 && M12 == -M21)
            {
                return new ComplexNumber(M11, M21);
            }
            else
            {
                throw new Exception("La matrice ne correspond pas à un nombre complexe valide");
            }
        }

        // Override ToString
        public override string ToString()
        {
            return $"[{M11}, {M12}] \n[{M21}, {M22}]";
        }
    }
}
using Math._2D;
using UnityEngine;

namespace Fractals
{
    public class Mandelbrot : MonoBehaviour
    {
        [Header("Paramètres de la texture")]
        public int width = 512;
        public int height = 512;
        public int maxIter = 100;
        public float zoom = 1f;
        public Vector2 center = new Vector2(-0.5f, 0f);

        private Texture2D _texture;

        void Start()
        {
            _texture = new Texture2D(width, height, TextureFormat.RGB24, false);
            GetComponent<Renderer>().material.mainTexture = _texture;
            Render();
        }

        void Render()
        {
            for (int y = 0; y < height; y++)
            {
                double im = (y - height * 0.5) / (0.5 * zoom * height) + center.y;

                for (int x = 0; x < width; x++)
                {
                    double re = (x - width * 0.5) / (0.5 * zoom * width) + center.x;

                    ComplexNumber c = new ComplexNumber(re, im);
                    ComplexNumber z = new ComplexNumber(0, 0);

                    int i = 0;
                    while (i < maxIter && z.Magnitude() <= 2.0)
                    {
                        z = z.Multiply(z).Add(c);
                        i++;
                    }

                    _texture.SetPixel(x, y, Colorize(i, maxIter));
                }
            }
            _texture.Apply();
        }

        Color Colorize(int iter, int maxIter)
        {
            if (iter >= maxIter) return Color.black;
            float t = iter / (float)maxIter;
            return new Color(t, t * t, 1f - t);
        }
    }
}
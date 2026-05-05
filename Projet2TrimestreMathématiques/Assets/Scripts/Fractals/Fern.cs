using UnityEngine;

namespace Fractals
{
    public class FernGerator : MonoBehaviour
    {
        [Header("Paramètres de la texture")]
        public int width = 512;
        public int height = 512;
        public int maxIterations = 100;

        private Texture2D fractalTexture;

        void Start()
        {
            fractalTexture = new Texture2D(width, height);

            GetComponent<Renderer>().material.mainTexture = fractalTexture;

            DrawFern();
        }

        void DrawFern()
        {
            float x = 0;
            float y = 0;
            for (int i = 0; i < 50000; i++)
            {
                float xprime, yprime;
                int pourcentage = UnityEngine.Random.Range(1, 101);

                if (pourcentage < 2)
                {
                    xprime = 0;
                    yprime = 0.16f * y;
                }
                else if (pourcentage < 87)
                {
                    xprime = 0.85f * x + 0.04f * y;
                    yprime = -0.04f * x + 0.85f * y + 1.6f;
                }else if (pourcentage < 94)
                {
                    xprime = 0.2f * x - 0.26f * y;
                    yprime = 0.23f * x + 0.22f * y + 1.6f;
                }
                else
                {
                    xprime = -0.15f * x + 0.28f * y;
                    yprime = 0.26f * x + 0.24f * y + 0.44f;
                }

                x = xprime;
                y = yprime;

                float percentX = (x + 3f) / 6f;

                float percentY = y / 10f;

                int pixelX = (int)(percentX * width);
                int pixelY = (int)(percentY * height);

                if (pixelX >= 0 && pixelX < width && pixelY >= 0 && pixelY < height)
                {
                    fractalTexture.SetPixel(pixelX, pixelY, Color.red);
                }
            }
            fractalTexture.Apply();
        }
    }
}
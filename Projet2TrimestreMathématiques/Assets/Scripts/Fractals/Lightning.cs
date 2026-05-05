using UnityEngine;

namespace Fractals
{
    public class Lightning : MonoBehaviour
    {
        [Header("Paramètres de la texture")]
        public int width = 512;
        public int height = 512;

        private Texture2D fractalTexture;

        void Start()
        {
            fractalTexture = new Texture2D(width, height);
            GetComponent<Renderer>().material.mainTexture = fractalTexture;

            Color[] fondNoir = new Color[width * height];
            for (int i = 0; i < fondNoir.Length; i++)
            {
                fondNoir[i] = Color.black;
            }
            fractalTexture.SetPixels(fondNoir);

            DrawLightning(new Vector2(width / 2, height - 1), new Vector2(width / 2, 0), 7, 100f);

            fractalTexture.Apply();
        }

        void DrawLightning(Vector2 start, Vector2 end, int iterations, float displacement)
        {
            if (iterations == 0)
            {
                DrawLine((int)start.x, (int)start.y, (int)end.x, (int)end.y, Color.white);
            }
            else
            {
                Vector2 midPoint = (start + end) / 2f;

                midPoint.x += Random.Range(-displacement, displacement);
                midPoint.y += Random.Range(-displacement, displacement);

                DrawLightning(start, midPoint, iterations - 1, displacement / 2f);
                DrawLightning(midPoint, end, iterations - 1, displacement / 2f);
            }
        }

        void DrawLine(int x0, int y0, int x1, int y1, Color color)
        {
            int dx = Mathf.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            int dy = -Mathf.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
            int err = dx + dy, e2;

            while (true)
            {
                if (x0 >= 0 && x0 < width && y0 >= 0 && y0 < height) fractalTexture.SetPixel(x0, y0, color);
                if (x0 == x1 && y0 == y1) break;
                e2 = 2 * err;
                if (e2 >= dy) { err += dy; x0 += sx; }
                if (e2 <= dx) { err += dx; y0 += sy; }
            }
        }
    }
}
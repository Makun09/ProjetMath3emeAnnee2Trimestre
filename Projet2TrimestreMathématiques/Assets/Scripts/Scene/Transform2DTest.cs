using Math._2D;
using UnityEngine;

namespace Scene
{
    public class Transform2DTest : MonoBehaviour
    {
        public LineRenderer line;

        public float rotationSpeed = 45f; // deg/sec
        public float scaleBase = 1.2f;
        public float scaleAmplitude = 0.2f;
        public float translationRadius = 0.5f;

        private float _angleDeg = 0f;

        private readonly Vector2[] _square = new Vector2[]
        {
            new Vector2(-1, -1),
            new Vector2( 1, -1),
            new Vector2( 1,  1),
            new Vector2(-1,  1),
            new Vector2(-1, -1)
        };

        void Start()
        {
            if (line == null)
            {
                line = gameObject.AddComponent<LineRenderer>();
                line.positionCount = _square.Length;
                line.widthMultiplier = 0.05f;
            }

            ApplyTransformAndDraw(scaleBase, Vector2.zero);
        }

        void Update()
        {
            float t = Time.time;
            _angleDeg += rotationSpeed * Time.deltaTime;

            float scale = scaleBase + scaleAmplitude * Mathf.Sin(t);
            Vector2 translation = new Vector2(
                Mathf.Cos(t) * translationRadius,
                Mathf.Sin(t) * translationRadius
            );

            ApplyTransformAndDraw(scale, translation);
        }

        void ApplyTransformAndDraw(float scale, Vector2 translation)
        {
            Transform2D rotScale = Transform2D.Similitude(scale, _angleDeg * Mathf.Deg2Rad);
            Transform2D tr = Transform2D.Translation(translation.x, translation.y);

            for (int i = 0; i < _square.Length; i++)
            {
                Vector2 p = rotScale.Apply(_square[i]);
                p = tr.Apply(p);
                line.SetPosition(i, new Vector3(p.x, p.y, 0f));
            }
        }
    }
}
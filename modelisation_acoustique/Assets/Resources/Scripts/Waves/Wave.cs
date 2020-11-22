using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Resources.Scripts.Waves
{
    public class Wave
    {
        private Vector3 direction;
        private Vector3 origin;
        private Color rayColor;
        private float maxDistance = 100f;

        public Wave(Vector3 _direction, Vector3 _origin)
        {
            direction = _direction;
            origin = _origin;
            rayColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }

        public Vector3 Direction
        {
            get => direction;
            set => direction = value;
        }
        
        public Vector3 Origin
        {
            get => origin;
            set => origin = value;
        }
        
        public Color RayColor
        {
            get => rayColor;
            set => rayColor = value;
        }
        
        public float MaxDistance
        {
            get => maxDistance;
            set => maxDistance = value;
        }
    }
    
}
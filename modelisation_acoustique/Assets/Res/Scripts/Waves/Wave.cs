using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Res.Scripts.Waves
{
    public class Wave
    {
        private Vector3 _direction;
        private Vector3 _origin;
        private Color _rayColor;
        private float _maxDistance = 100f;
        
        public Wave(Vector3 direction, Vector3 origin)
        {
            _direction = direction;
            _origin = origin;
            _rayColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }

        public Vector3 Direction
        {
            get => _direction;
            set => _direction = value;
        }
        
        public Vector3 Origin
        {
            get => _origin;
            set => _origin = value;
        }
        
        public Color RayColor
        {
            get => _rayColor;
            set => _rayColor = value;
        }
        
        public float MaxDistance
        {
            get => _maxDistance;
            set => _maxDistance = value;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;
using System.Numerics;
using UnityEngine;

namespace Res.Scripts.Objects
{
    public class ObjectData : MonoBehaviour
    {
        public float absorptionCoef = 0.5f;
        public float surface = 0;
        private float _absorptionArea = 0;

        private void Start()
        {
            _absorptionArea = absorptionCoef * surface;
        }
        
        public float AbsorptionArea
        {
            get => _absorptionArea;
            set => _absorptionArea = value;
        }
    }
    
}
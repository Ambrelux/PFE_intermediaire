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
        public float width = 0;
        public float heigth = 0;
        public float depth = 0;
        private Renderer rend;
        private Vector3 objectSize;

        private void Start()
        {
            rend = GetComponent<Renderer>();
            objectSize = rend.bounds.size;
            width = objectSize.x;
            heigth = objectSize.y;
            depth = objectSize.z;
        }
    }
    
}
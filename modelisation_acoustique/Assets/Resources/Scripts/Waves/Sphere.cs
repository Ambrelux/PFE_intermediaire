using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using System.Collections.Generic;
using System.Collections;

namespace Resources.Scripts.Sphere

{
    public class Sphere : MonoBehaviour
    { 
        public GameObject sphereObject;
        private List<Vector3> waveCoordData = new List<Vector3>();
        private List<Vector3> waveDirectionData = new List<Vector3>();
        
        public List<Vector3> WaveCoordData
        {
            get => waveCoordData;
            set => waveCoordData = value;
        }

        public List<Vector3> WaveDirectionData
        {
            get => waveDirectionData;
            set => waveDirectionData = value;
        }

        void moveDirection()
        {
            transform.Translate(waveDirectionData[0]*(Time.deltaTime*5f));
        }

        public void Update()
        {
            moveDirection();
        }
    }
    
   
}
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
        private int nbBounce = 1;
        public float speed = 5;
        private float startTime;
        private float journeyLength;
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

        void move()
        {            
            if (nbBounce < waveCoordData.Count - 1)
            {
                UpdateMovementInformation();
                float distCovered = (Time.time - startTime) * speed;
                float fractionOfJourney = distCovered / journeyLength;
                transform.position =
                    Vector3.Lerp(waveCoordData[nbBounce - 1], waveCoordData[nbBounce], fractionOfJourney);
            }
        }

        void UpdateMovementInformation()
        {
            if (transform.position == waveCoordData[nbBounce])
            {
                nbBounce += 1;
                startTime = Time.time;
                journeyLength = Vector3.Distance(waveCoordData[nbBounce-1], waveCoordData[nbBounce]);
            }
        }


        public void Start()
        {
            startTime = Time.time;
            journeyLength = Vector3.Distance(waveCoordData[0], waveCoordData[1]);
        }

        public void Update()
        {
            move();
        }
        
        
    }
    
   
}
using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using System.Collections.Generic;
using System.Collections;

namespace Res.Scripts.Sphere

{
    public class Sphere : MonoBehaviour
    { 
        public GameObject sphereObject;
        private List<Vector3> waveCoordData = new List<Vector3>();
        private List<Vector3> waveDirectionData = new List<Vector3>(); 
        private List<Vector2> collidedObjectList = new List<Vector2>(); // surface alpha
        private int nbBounce = 1;
        public float speed = 0.00001f;
        private float absorptionArea;
        private float propConst = 0.163f;
        private float roomVolume = 50000f;
        private float propDist;
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

        public List<Vector2> CollidedObjectList
        {
            get => collidedObjectList;
            set => collidedObjectList = value;
        }

        // void move()
        // {            
        //     if (nbBounce < waveCoordData.Count - 1)
        //     {
        //         UpdateMovementInformation();
        //         float distCovered = (Time.time - startTime) * speed;
        //         float fractionOfJourney = distCovered / journeyLength;
        //         transform.position =
        //             Vector3.Lerp(waveCoordData[nbBounce - 1], waveCoordData[nbBounce], fractionOfJourney);
        //     }
        // }

        // void UpdateMovementInformation()
        // {
        //     if (transform.position == waveCoordData[nbBounce])
        //     {
        //         nbBounce += 1;
        //         startTime = Time.time;
        //         journeyLength = Vector3.Distance(waveCoordData[nbBounce-1], waveCoordData[nbBounce]);
        //     }
        // }
        
        IEnumerator MoveSphere()
        {
            Vector3 startCoord;
            Vector3 endCoord;
            float startTime;
            float journeyLength;
            float distCovered;
            float fractionOfJourney;
            
            if (waveCoordData.Count == 0)
            {
                yield break;
            }

            for (int i = 1; i < waveCoordData.Count; i++)
            {
                //Debug.Log(i);
                startTime = Time.time;
                startCoord = waveCoordData[i-1];
                endCoord = waveCoordData[i];
                //journeyLength = Vector3.Distance(startCoord, endCoord);
                fractionOfJourney = 0f;
                while (Vector3.Distance(transform.position, endCoord) > 0.05f)
                {
                    //journeyLength = Vector3.Distance(transform.position, endCoord);
                    // distCovered = (Time.time - startTime) * speed;
                    // fractionOfJourney = distCovered / journeyLength;
                    transform.position =
                        Vector3.Lerp(transform.position, endCoord, fractionOfJourney); 
                    fractionOfJourney += 0.0005f;
                    yield return null;
                }

            }
            
        }

        float CalculateAbsorptionArea()
        {
            float absorptionArea = 0;

            for (int i = 0; i < collidedObjectList.Count; i++)
            {
                absorptionArea = absorptionArea + collidedObjectList[i][0] * collidedObjectList[i][1];
            }

            return absorptionArea;
        }
        
        
        public void Start()
        {
            StartCoroutine(MoveSphere());
            absorptionArea = CalculateAbsorptionArea();
            propDist = 340 * propConst * roomVolume / absorptionArea;
        }

        // public void Update()
        // {
        //     move();
        // }
        

    }
    
   
}
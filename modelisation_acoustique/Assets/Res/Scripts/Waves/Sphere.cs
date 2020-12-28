using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using System.Collections.Generic;
using System.Collections;
using Res.Scripts.Objects;

namespace Res.Scripts.Sphere

{
    public class Sphere : MonoBehaviour
    { 
        public GameObject sphereObject;
        private List<Vector3> waveCoordData = new List<Vector3>();
        private AcousticCalculation _acousticCalculation = new AcousticCalculation();

        public List<Vector3> WaveCoordData
        {
            get => waveCoordData;
            set => waveCoordData = value;
        }
        
        IEnumerator MoveSphere()
        {
            Vector3 endCoord;
            Vector3 lastPosition;
            float distCovered = 0f;
            float fractionOfJourney;
            if (waveCoordData.Count == 0)
            {
                yield break;
            }

            for (int i = 1; i < waveCoordData.Count; i++)
            {
                endCoord = waveCoordData[i];
                fractionOfJourney = 0f;
                while (Vector3.Distance(transform.position, endCoord) > 0.05f)
                {
                    lastPosition = transform.position;
                    if (_acousticCalculation.GetReverbDistance() > distCovered)
                    {
                        transform.position =
                            Vector3.Lerp(transform.position, endCoord, fractionOfJourney);

                        distCovered += Vector3.Distance(lastPosition, transform.position);
                        fractionOfJourney += 0.0005f;
                        yield return null;
                    }
                    else
                    {
                        yield break;  
                    }
                }

            }
            
        }

        public void Start()
        {
            StartCoroutine(MoveSphere());
            
            Debug.Log(_acousticCalculation.GetReverbTime());
        }
    }
    
   
}
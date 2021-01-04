using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using System.Collections.Generic;
using System.Collections;
using Res.Scripts.Objects;

namespace Res.Scripts.Waves

{
    public class Sphere : MonoBehaviour
    { 
        public GameObject sphereObject;
        private List<Vector3> _waveCoordData = new List<Vector3>();
        private AcousticCalculation _acousticCalculation;

        public List<Vector3> WaveCoordData
        {
            get => _waveCoordData;
            set => _waveCoordData = value;
        }

        private IEnumerator MoveSphere()
        {
            Vector3 endCoord;
            Vector3 startCoord;
            Vector3 lastPosition;
            float distCovered = 0f;
            float fractionOfJourney;
            float startTime;
            float journeyLength;

            if (_waveCoordData.Count == 0)
            {
                yield break;
            }

            for (int i = 1; i < _waveCoordData.Count; i++)
            {
                startCoord = _waveCoordData[i - 1];
                endCoord = _waveCoordData[i];
                startTime = Time.time;
                journeyLength = Vector3.Distance(startCoord, endCoord);

                while (Vector3.Distance(transform.position, endCoord) > 0.05f)
                {
                    fractionOfJourney = (Time.time - startTime) * 10f / journeyLength;
                    lastPosition = transform.position;
                    if (_acousticCalculation.ReverbDistance > distCovered)
                    {
                        transform.position =
                            Vector3.Lerp(startCoord, endCoord, fractionOfJourney);

                        distCovered += Vector3.Distance(lastPosition, transform.position);
                        yield return null;
                    }
                    else
                    {
                        yield break;  
                    }
                }

            }
            
        }

        public void Awake()
        {
            _acousticCalculation = new AcousticCalculation();
        }

        public void Start()
        {
            StartCoroutine(MoveSphere());
        }
    }
    
   
}
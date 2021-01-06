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
        private Color _startColor = new Color32(71, 255, 78, 255);
        private Color _endColor = new Color32(255, 190, 13, 255);
        private Color _objectColor;
        private Renderer objectRenderer;

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
            float interColor = 0f;
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
                        interColor = distCovered / _acousticCalculation.ReverbDistance;
                        _objectColor = Color.Lerp(_startColor, _endColor, interColor);
                        objectRenderer.material.SetColor("_Color",_objectColor);
                        yield return null;
                    }
                    else
                    {
                        yield return new WaitForSeconds(5);
                        Destroy(sphereObject);  
                    }
                }

            }
            
        }

        public void Awake()
        {
            _acousticCalculation = new AcousticCalculation();
            objectRenderer = sphereObject.GetComponent<Renderer>();
            objectRenderer.material.SetColor("_Color",_startColor);
        }

        public void Start()
        {
            StartCoroutine(MoveSphere());
        }
    }
    
   
}
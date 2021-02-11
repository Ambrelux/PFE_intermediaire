using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using System.Collections.Generic;
using System.Collections;
using Random = UnityEngine.Random;
using Res.Scripts.Object;

namespace Res.Scripts.Waves

{
    public class Sphere : MonoBehaviour
    { 
        public GameObject sphereObject;
        private List<Vector3> _waveCoordData = new List<Vector3>();
        private AcousticCalculation _acousticCalculation;
        private readonly Color _startColor = new Color32(71, 255, 78, 255);
        private readonly Color _endColor = new Color32(255, 0,0 , 255);
        private Color _objectColor;
        private Renderer _objectRenderer;
        private int nbBounce = 0;
        private Color _rayColor;
        public Color RayColor => _rayColor;

        public int NbBounce
        {
            get => nbBounce;
        }

        public List<Vector3> WaveCoordData
        {
            get => _waveCoordData;
            set => _waveCoordData = value;
        }

        private IEnumerator MoveSphere()
        {
            if (_waveCoordData.Count == 0)
            {
                yield break;
            }

            var distCovered = 0f;
            
            for (int i = 1; i < _waveCoordData.Count; i++)
            {
                var startCoord = _waveCoordData[i - 1];
                var endCoord = _waveCoordData[i];
                var startTime = Time.time;
                var journeyLength = Vector3.Distance(startCoord, endCoord);
                
                while (Vector3.Distance(transform.position, endCoord) > 0.05f)
                {
                    var fractionOfJourney = (Time.time - startTime) * 20f / journeyLength;
                    var lastPosition = transform.position;
                    
                    if (_acousticCalculation.ReverbDistance > distCovered)
                    {
                        transform.position =
                            Vector3.Lerp(startCoord, endCoord, fractionOfJourney);

                        distCovered += Vector3.Distance(lastPosition, transform.position);
                        
                        var interColor = distCovered / _acousticCalculation.ReverbDistance;
                        _objectColor = Color.Lerp(_startColor, _endColor, interColor);
                        _objectRenderer.material.SetColor("_Color",_objectColor);
                        yield return null;
                    }
                    else
                    {
                        nbBounce = i;
                        yield return new WaitForSeconds(5);
                        Destroy(sphereObject);  
                    }
                }

            }
            
        }

        public void Awake()
        {
            _acousticCalculation = new AcousticCalculation();
            _objectRenderer = sphereObject.GetComponent<Renderer>();
            _objectRenderer.material.SetColor("_Color",_startColor);
            _rayColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        }

        public void Start()
        {
            StartCoroutine(MoveSphere());
            Debug.Log(_acousticCalculation.ReverbDistance);
        }
    } 
}
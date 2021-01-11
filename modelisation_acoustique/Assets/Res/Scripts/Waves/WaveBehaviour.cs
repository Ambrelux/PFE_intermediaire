using System;
using System.Collections.Generic;
using Res.Scripts.API;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;

namespace Res.Scripts.Waves
{
    public class WaveBehaviour : MonoBehaviour
    {
        private RaycastHit _hit;
        public int totalBounce = 10;
        public float lineOffset = 0.01f;
        [SerializeField] private LayerMask layers;
    
        private readonly List<Wave> _wavesList = new List<Wave>();
        private readonly List<GameObject> _spheresList = new List<GameObject>();
        public int nbWaves = 0;
        public GameObject sphereObject;
        public GameObject sphereParent;
        private readonly ApiRequest _api = new ApiRequest();


        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                InitWaves();
                InitSpheres();
                WaveData();
                StartCoroutine(ApiRequest.CreateSound(_spheresList, 0));
            }
            
            DrawRaycast();
        }

        private void InitWaves()
        {
            _wavesList.Clear();
        
            for (var i =0; i < nbWaves; i++)
            {
                var direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                var origin = transform.position + lineOffset * direction;
                _wavesList.Add(new Wave(direction.normalized, origin));
            }
        }

        private void WaveMovement()
        {
            foreach (var wave in _wavesList)
            {
                var direction = wave.Direction;
                var origin = wave.Origin;
                
                for (var i = 1; i <= totalBounce; i++)
                {
                    if (!Physics.Raycast(origin, direction, out _hit, wave.MaxDistance, layers)) continue;
                    Debug.DrawLine(origin, _hit.point, wave.RayColor);
                    direction = Vector3.Reflect(direction.normalized, _hit.normal);
                    origin = _hit.point + lineOffset * direction;
                }
            }
        }

        private void DrawRaycast()
        {
            for(int i = 0 ; i < _spheresList.Count; i++)
            {
                var sphereScript = _spheresList[i].GetComponent<Sphere>();

                if (sphereScript.NbBounce > 0)
                {
                    var direction = (sphereScript.WaveCoordData[1]-sphereScript.WaveCoordData[0]).normalized;
                    var origin = sphereScript.WaveCoordData[0];
                
                    for (var j = 1; j <= sphereScript.NbBounce; j++)
                    {
                        if (!Physics.Raycast(origin, direction, out _hit, 100, layers)) continue;
                        Debug.DrawLine(origin, _hit.point, sphereScript.RayColor);
                        direction = Vector3.Reflect(direction.normalized, _hit.normal);
                        origin = _hit.point + lineOffset * direction;
                    }    
                }
            }
        }
        
        private void WaveData()
        {
            for (var j = 0; j < _wavesList.Count; j++)
            {
                var direction = _wavesList[j].Direction;
                var origin = _wavesList[j].Origin;
                var sphereScript = _spheresList[j].GetComponent<Sphere>();
                
                for (var i = 1; i <= totalBounce; i++)
                {
                    if (!Physics.Raycast(origin, direction, out _hit, _wavesList[j].MaxDistance, layers)) continue;
                    direction = Vector3.Reflect(direction.normalized, _hit.normal);
                    origin = _hit.point + lineOffset * direction;
                    sphereScript.WaveCoordData.Add(origin);
                }
            }        
        
        }

        private void InitSpheres()
        {
            _spheresList.Clear();
        
            for (int i = 0; i < _wavesList.Count; i++)
            {
                var myNewSphere = Instantiate(sphereObject, _wavesList[i].Origin, Quaternion.identity);
                myNewSphere.transform.parent = sphereParent.transform;
                _spheresList.Add(myNewSphere);
                var sphereScript = _spheresList[i].GetComponent<Sphere>();
                sphereScript.WaveCoordData.Add(_wavesList[i].Origin);
            }
        }
    }
}


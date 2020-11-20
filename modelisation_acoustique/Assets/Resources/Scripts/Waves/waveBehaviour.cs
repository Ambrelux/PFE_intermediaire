using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Resources.Scripts.Waves;
using Vector3 = UnityEngine.Vector3;

public class waveBehaviour : MonoBehaviour
{
    private RaycastHit hit;
    public int totalBounce = 10;
    public float lineOffset = 0.01f;
    public GameObject raycastObject;
    public float maxDistance = 100f;
    [SerializeField] private LayerMask layers;
    
    private List<Wave> wavesList = new List<Wave>();
    public int nbWaves = 0;
    public int alpha = 15;
    
    private void Start()
    {
        initWaves();
    }

    void Update()
    {
        for (int i = 1; i <= totalBounce; i++)
        {
            for (int j = 0; j < wavesList.Count; j++)
            {
                Vector3 direction = wavesList[j].Direction;
                Vector3 origin = wavesList[j].Origin;
                if (Physics.Raycast(origin, direction, out hit, wavesList[j].MaxDistance, layers))
                {
                    // Debug.DrawRay(hit.point, Vector3.Reflect(direction.normalized, hit.normal), Color.cyan);  
                    Debug.DrawLine(origin, hit.point, wavesList[j].RayColor);
                    direction = Vector3.Reflect(direction.normalized, hit.normal);
                    origin = hit.point + lineOffset * direction;
                    // Debug.DrawRay(origin, hit.distance * direction, Color.blue);
                }                
            }
        }
    }

    void initWaves()
    {
        Vector3 direction;
        Vector3 origin;
        
        for (int i =0; i < nbWaves; i++)
        {
            direction = new Vector3(0, (alpha * i), 1);
            origin = transform.position + lineOffset * direction;
            wavesList.Add(new Wave(direction.normalized, origin));
        }
    }
}


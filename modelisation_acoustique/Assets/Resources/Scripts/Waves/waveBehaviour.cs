using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Resources.Scripts.Waves;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class waveBehaviour : MonoBehaviour
{
    private RaycastHit hit;
    public int totalBounce = 10;
    public float lineOffset = 0.01f;
    public GameObject raycastObject;
    [SerializeField] private LayerMask layers;
    
    private List<Wave> wavesList = new List<Wave>();
    public int nbWaves = 0;
    public float alpha = 15f;
    
    private void Start()
    {
        initWaves();
    }

    void Update()
    {
        waveMovement();
    }

    void initWaves()
    {
        Vector3 direction;
        Vector3 origin;
        float angleRad;
        float yCoord;
        float xCoord;
        float zCoord;
        
        for (int i =0; i < nbWaves; i++)
        {
            angleRad = alpha * i * Mathf.Deg2Rad;
            xCoord = Random.Range(-1f, 1f);
            yCoord = Random.Range(-1f, 1f);
            zCoord = Random.Range(-1f, 1f);
            //direction = new Vector3(Mathf.Cos(angleRad), yCoord, Mathf.Sin(angleRad));
            direction = new Vector3(xCoord, yCoord, zCoord);
            origin = transform.position + lineOffset * direction;
            wavesList.Add(new Wave(direction.normalized, origin));
        }
    }

    void waveMovement()
    {
        Vector3 direction;
        Vector3 origin;
        
        for (int j = 0; j < wavesList.Count; j++)
        {
            direction = wavesList[j].Direction;
            origin = wavesList[j].Origin;
            for (int i = 1; i <= totalBounce; i++)
            {
                if (Physics.Raycast(origin, direction, out hit, wavesList[j].MaxDistance, layers))
                {
                    Debug.DrawLine(origin, hit.point, wavesList[j].RayColor);
                    direction = Vector3.Reflect(direction.normalized, hit.normal);
                    origin = hit.point + lineOffset * direction;
                }                
            }
        }        
    }
}


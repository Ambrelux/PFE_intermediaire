using System;
using System.Collections.Generic;
using Res.Scripts.API;
using UnityEngine;
using Res.Scripts.Waves;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;

public class waveBehaviour : MonoBehaviour
{
    private RaycastHit hit;
    public int totalBounce = 10;
    public float lineOffset = 0.01f;
    [SerializeField] private LayerMask layers;
    
    private List<Wave> wavesList = new List<Wave>();
    private List<GameObject> spheresList = new List<GameObject>();
    public int nbWaves = 0;
    public GameObject sphereObject;
    public GameObject sphereParent;
    private APIRequest api = new APIRequest();
    
    // private void Start()
    // {
    //     InitWaves();
    //     InitSpheres();
    //     WaveData();
    // }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            InitWaves();
            InitSpheres();
            WaveData();
            StartCoroutine(api.CreateSound(spheresList, 0));
        }
    }

    void InitWaves()
    {
        Vector3 direction;
        Vector3 origin;
        float yCoord;
        float xCoord;
        float zCoord;
        
        wavesList.Clear();
        
        for (int i =0; i < nbWaves; i++)
        {
            xCoord = Random.Range(-1f, 1f);
            yCoord = Random.Range(-1f, 1f);
            zCoord = Random.Range(-1f, 1f);
            direction = new Vector3(xCoord, yCoord, zCoord);
            origin = transform.position + lineOffset * direction;
            wavesList.Add(new Wave(direction.normalized, origin));
        }
    }

    void WaveMovement()
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

    void WaveData()
    {
        Vector3 direction;
        Vector3 origin;
        Sphere sphereScript;
        for (int j = 0; j < wavesList.Count; j++)
        {
            direction = wavesList[j].Direction;
            origin = wavesList[j].Origin;
            sphereScript = spheresList[j].GetComponent<Sphere>();
            for (int i = 1; i <= totalBounce; i++)
            {
                if (Physics.Raycast(origin, direction, out hit, wavesList[j].MaxDistance, layers))
                {
                    direction = Vector3.Reflect(direction.normalized, hit.normal);
                    origin = hit.point + lineOffset * direction;
                    sphereScript.WaveCoordData.Add(origin);
                }                
            }
        }        
        
    }

    void InitSpheres()
    {
        spheresList.Clear();
        
        for (int i = 0; i < wavesList.Count; i++)
        {
            GameObject myNewSphere = Instantiate(sphereObject, wavesList[i].Origin, Quaternion.identity);
            myNewSphere.transform.parent = sphereParent.transform;
            spheresList.Add(myNewSphere);
            Sphere sphereScript = spheresList[i].GetComponent<Sphere>();
            sphereScript.WaveCoordData.Add(wavesList[i].Origin);
        }
    }
}


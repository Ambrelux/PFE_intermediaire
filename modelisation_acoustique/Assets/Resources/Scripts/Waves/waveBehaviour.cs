using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveBehaviour : MonoBehaviour
{
    private RaycastHit hit;
    public int totalBounce = 10;
    public float lineOffset = 0.01f;
    public GameObject raycastObject;
    public float maxDistance = 100f;
    [SerializeField] private LayerMask layers; 
    void Update()
    {

        Vector3 direction = transform.forward;
        Vector3 origin = transform.position + lineOffset * direction;
        
        for (int i = 1; i <= totalBounce; i++)
        {
            if (Physics.Raycast(origin, direction, out hit, maxDistance, layers))
            {
                // Debug.DrawRay(hit.point, Vector3.Reflect(direction.normalized, hit.normal), Color.cyan);  
                Debug.DrawLine(origin, hit.point, Color.red);
                direction = Vector3.Reflect(direction.normalized, hit.normal);
                origin = hit.point + lineOffset * direction;
                // Debug.DrawRay(origin, hit.distance * direction, Color.blue);

            }
        }
    }
}

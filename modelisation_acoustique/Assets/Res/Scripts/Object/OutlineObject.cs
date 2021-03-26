using UnityEngine;

namespace Res.Scripts.Object
{
    public class OutlineObject : MonoBehaviour
    {
        Material material;
    
        void Start () {
            material = GetComponentInChildren<Renderer>().material;
        }
    
        void OnMouseOver()
        {
            if (transform.CompareTag("Furniture"))
            {
                material.SetFloat("_OutlineWidth", 0.1f);
            }
    
        }
    
        void OnMouseExit()
        {
            if (transform.CompareTag("Furniture"))
            {
                material.SetFloat("_OutlineWidth", 0f);               
            }
        }
    }
}

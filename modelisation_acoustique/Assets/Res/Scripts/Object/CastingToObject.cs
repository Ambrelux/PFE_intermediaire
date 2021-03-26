using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Res.Scripts.Object.SelectedFlash;

namespace Res.Scripts.Object
{
    public class CastingToObject : MonoBehaviour
    {
        //public static GameObject[] objectsInScene;
        private GameObject _selectedObject;

        void Start()
        {
            //objectsInScene = GetAllObjectsOnlyInScene();
            /*objectsInScene = GameObject.FindGameObjectsWithTag("Furniture");
            foreach (GameObject obj in objectsInScene)
            {
                obj.AddComponent<SelectedFlash>();
            }

            Debug.Log(objectsInScene.Length);*/


        }

        private void Update()
        {
           
        }

        GameObject AttributeObjectToHighlight()
        {
            RaycastHit hit;
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.collider.gameObject.CompareTag("Furniture"))
                {
                    _selectedObject = hit.collider.gameObject;

                }
                else
                {
                    _selectedObject = null;
                }
                
            }
            return _selectedObject;
        }


/*
        List<GameObject> GetAllObjectsOnlyInScene()
        {
           

            foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[])
            {
                if (!EditorUtility.IsPersistent(go.transform.root.gameObject) &&
                    !(go.hideFlags == HideFlags.NotEditable || go.hideFlags == HideFlags.HideAndDontSave))
                    objectsInScene.Add(go);
            }

            return objectsInScene;
        }
    }*/
    }
}
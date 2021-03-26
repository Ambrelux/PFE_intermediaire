using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Res.Scripts.Object
{
    public class SelectedFlash : MonoBehaviour
    {
        public GameObject selectedObject;
        
        public int redCol;
        public int greenCol;
        public int blueCol;
        public bool lookingAtObject = false;
        public bool flashingIn = true;
        public bool startedFlashing = false;
        public List<Material> materialList;
        public List<Color32> materialsColors;

        private void Start()
        {
            AddMaterialInList(materialList, selectedObject);
            AddPreviousColorInList(materialsColors, selectedObject);
        }

        // Update is called once per frame
        void Update()
        {
            

            if (lookingAtObject == true)
            {
                
                for (int k = 0; k < materialList.Count; k++)
                {
                    
                   // materialList[k].color = new Color32((byte)redCol, (byte)greenCol, (byte)blueCol,255);
                   materialList[k].color = new Color32((byte)redCol, (byte)greenCol, (byte)blueCol,255);
                }
               
                //selectedObject.GetComponent<Renderer>().material.color = new Color32((byte)redCol, (byte)greenCol, (byte)blueCol,255);
            }
        }

        void AddMaterialInList(List<Material> list, GameObject obj)
        {
            list.Clear();

            Renderer rend = obj.GetComponent<Renderer>();
            
            for (int i = 0; i < rend.materials.Length; i++)
            {
                list.Add(rend.materials[i]);
                //colorList.Add(rend.materials[i].color);
            }
            

        }
        void AddPreviousColorInList(List<Color32> colorList, GameObject obj)
        {
            colorList.Clear();

            Renderer rend = obj.GetComponent<Renderer>();
            
            for (int i = 0; i < rend.materials.Length; i++)
            {
                
                colorList.Add(rend.materials[i].color);
            }

        }


       

        void OnMouseOver()
        {
            Debug.Log("dessus");
           // _selectedObject = AttributeObjectToHighlight();
            //AddMaterialInList(materialList, _selectedObject);
            //AddPreviousColorInList(materialsColors, _selectedObject);
            lookingAtObject = true;
            if (startedFlashing == false)
            {
                startedFlashing = true;
                StartCoroutine(FlashObject());
            }
        }

        void OnMouseExit()
        {
            startedFlashing = false;
            lookingAtObject = false;
            StopCoroutine(FlashObject());
            for (int k = 0; k < materialList.Count; k++)
            {
                //materialList[k].color = materialsColors[k];
                selectedObject.GetComponent<Renderer>().materials[k].color = materialsColors[k];
                //materialList[k].color = new Color32(255,255,255,255);
            }
            
        }
        

        IEnumerator FlashObject()
        {
            while (lookingAtObject)
            {
                yield return new WaitForSeconds(0.05f);
                if (flashingIn)
                {
                    if (blueCol <= 30)
                    {
                        flashingIn = false;
                    }
                    else
                    {
                        blueCol -= 25;
                        greenCol -= 1;
                    }
                }

                if (flashingIn == false)
                {
                    if (blueCol >= 250)
                    {
                        flashingIn = true;
                    }
                    else
                    {
                        blueCol += 25;
                        greenCol += 1;
                    }
                }
            }
        }

    }
}

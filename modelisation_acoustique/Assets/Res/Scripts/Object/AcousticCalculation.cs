using System.Collections.Generic;
using UnityEngine;

namespace Res.Scripts.Object
{
    public class AcousticCalculation
    {
        public AcousticCalculation()
        {
            var materialList = new List<GameObject>();
            var furnitureList= new List<GameObject>();  
            var personList= new List<GameObject>();
            AddGameObjectInList(materialList,"Material");
            AddGameObjectInList(furnitureList,"Furniture");
            AddGameObjectInList(personList,"Person");            
            var totalAbsorptionArea = GetAbsorptionArea(materialList) + GetAbsorptionArea(furnitureList)
                                                                      + GetAbsorptionArea(personList);
            Debug.Log(totalAbsorptionArea);
            const float roomVolume = 200f;
            var reverbTime = (0.16f * roomVolume) / totalAbsorptionArea;
            ReverbDistance = reverbTime * 340.29f;
        }
        
        
        private static float GetAbsorptionArea(List<GameObject> list)
        {
            var totalAbsorptionArea = 0f; 
            for(var i = 0; i< list.Count; i++)
            {
                totalAbsorptionArea += list[i].GetComponent<ObjectData>().surface * list[i].GetComponent<ObjectData>().absorptionCoef;
            }

            return totalAbsorptionArea;
        }

        private static void AddGameObjectInList(List<GameObject> list, string tag)
        {
            list.AddRange(GameObject.FindGameObjectsWithTag(tag));
        }
        
        public float ReverbDistance { get; }
    }
}
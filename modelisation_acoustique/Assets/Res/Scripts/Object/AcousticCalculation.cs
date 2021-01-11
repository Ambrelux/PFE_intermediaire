using System.Collections.Generic;
using System.Linq;
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
            const float roomVolume = 172.8f;
            var reverbTime = (0.16f * roomVolume) / totalAbsorptionArea;
            ReverbDistance = reverbTime * 340.29f;
        }
        
        
        private static float GetAbsorptionArea(List<GameObject> list)
        {
            return list.Select(gameObject => gameObject.GetComponent<ObjectData>()).Where(gameObjectData => gameObjectData != null).Sum(gameObjectData => gameObjectData.AbsorptionArea);
        }

        private static void AddGameObjectInList(List<GameObject> list, string tag)
        {
            list.AddRange(GameObject.FindGameObjectsWithTag(tag));
        }
        
        public float ReverbDistance { get; }
    }
}
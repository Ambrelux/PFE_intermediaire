using System.Collections.Generic;
using UnityEngine;

namespace Res.Scripts.Object
{
    public class AcousticCalculation : MonoBehaviour
    {
        private static AcousticCalculation _instance;
        public static AcousticCalculation Instance { get { return _instance; } }
        
        private List<GameObject> _materialList;
        private List<GameObject> _furnitureList;
        private List<GameObject> _personList;
        private float _reverbDistance;
        public float roomVolume=200f;
        
        void Awake()
        {
            _instance = this;
            _materialList = new List<GameObject>();
            _furnitureList= new List<GameObject>();  
            _personList= new List<GameObject>();
            UpdateAcousticCalculation();
        }

        public void UpdateAcousticCalculation()
        {
            AddGameObjectInList(_materialList,"Material");
            AddGameObjectInList(_furnitureList,"Furniture");
            AddGameObjectInList(_personList,"Person");            
            var totalAbsorptionArea = GetAbsorptionArea(_materialList) + GetAbsorptionArea(_furnitureList)
                                                                      + GetAbsorptionArea(_personList);
            var reverbTime = (0.16f * roomVolume) / totalAbsorptionArea;
            _reverbDistance = reverbTime * 340.29f;
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
            list.Clear();
            list.AddRange(GameObject.FindGameObjectsWithTag(tag));
        }

        public float ReverbDistance => _reverbDistance;
    }
}
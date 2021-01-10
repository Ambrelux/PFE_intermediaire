using System.Collections.Generic;
using UnityEngine;

namespace Res.Scripts.Objects
{
    public class AcousticCalculation
    {
        private List<GameObject> _materialList;
        private List<GameObject> _furnitureList;
        private List<GameObject> _personList;
        private float _totalAbsorptionArea;
        private float _reverbTime;
        private float _reverbDistance;
        private float _roomVolume;

        public AcousticCalculation()
        {
            // materialList = GameObject.FindGameObjectsWithTag("Material");
            // furnitureList = GameObject.FindGameObjectsWithTag("Furniture");
            // personList = GameObject.FindGameObjectsWithTag("Person");
            _materialList = new List<GameObject>();
            _furnitureList= new List<GameObject>();  
            _personList= new List<GameObject>();
            AddGameObjectInList(_materialList,"Material");
            AddGameObjectInList(_furnitureList,"Furniture");
            AddGameObjectInList(_personList,"Person");            
            _totalAbsorptionArea = GetAbsorptionArea(_materialList) + GetAbsorptionArea(_furnitureList)
                                                                  + GetAbsorptionArea(_personList);
            _roomVolume = 172.8f;
            _reverbTime = (0.16f * _roomVolume) / _totalAbsorptionArea;
            _reverbDistance = _reverbTime * 340.29f;
        }
        
        
        private float GetAbsorptionArea(List<GameObject> list)
        {
            float absorptionArea = 0;
            ObjectData gameObjectData;
            
            foreach (GameObject gameObject in list)
            {
                gameObjectData = gameObject.GetComponent<ObjectData>();

                if (gameObjectData != null)
                {
                    absorptionArea += gameObjectData.AbsorptionArea;
                }
            }

            return absorptionArea;
        }

        private void AddGameObjectInList(List<GameObject> list, string tag)
        {
            foreach (var gameObject in GameObject.FindGameObjectsWithTag(tag))
            {
                list.Add(gameObject);
            }
        }
        
        public float ReverbDistance => _reverbDistance;
    }
}
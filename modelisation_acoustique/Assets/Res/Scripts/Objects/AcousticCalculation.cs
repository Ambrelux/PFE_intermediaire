using UnityEngine;

namespace Res.Scripts.Objects
{
    public class AcousticCalculation
    {
        private GameObject[] materialList;
        private GameObject[] furnitureList;        
        private GameObject[] personList;
        private float roomVolume = 172.8f;

        public float GetReverbTime()
        {
            float reverbTime = 0;
            float totalAbsorptionArea = 0;
            materialList = GameObject.FindGameObjectsWithTag("Material");
            furnitureList = GameObject.FindGameObjectsWithTag("Furniture");
            personList = GameObject.FindGameObjectsWithTag("Person");

            totalAbsorptionArea = GetAbsorptionArea(materialList) + GetAbsorptionArea(furnitureList)
                                                                  + GetAbsorptionArea(personList);
            Debug.Log(totalAbsorptionArea);
            reverbTime = (0.16f * roomVolume) / totalAbsorptionArea;
            return reverbTime;
        }

        public float GetReverbDistance()
        {
            return GetReverbTime() * 340.29f;
        }
        public float GetAbsorptionArea(GameObject[] list)
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
        
    }
}
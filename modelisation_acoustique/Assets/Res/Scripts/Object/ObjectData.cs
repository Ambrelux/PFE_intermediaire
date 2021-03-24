using System;
using System.Collections;
using System.Collections.Generic;
using Vector3 = UnityEngine.Vector3;
using System.Numerics;
using UnityEngine;

namespace Res.Scripts.Object
{
    public class ObjectData : MonoBehaviour
    {
        public float absorptionCoef = 0.5f;
        public float surface = 0;
        private float _absorptionArea = 0;
        public string _materialName;

        private void Awake()
        {
            _materialName = "Default";
        }

        private void Start()
        {
            UpdateAbsorptionArea();
        }
        
        public float AbsorptionArea
        {
            get => _absorptionArea;
        }

        public void UpdateAbsorptionArea()
        {
            _absorptionArea = absorptionCoef * surface;
        }

        public void UpdateMaterial(string matName)
        {
            _materialName = matName;
            absorptionCoef = GetAbsorptionCoef();
        }

        private float GetAbsorptionCoef()
        {
            switch (SoundData.Instance.Frequency)
            {
                case 1:
                    Console.WriteLine("Case 1");
                    break;
                case 2:
                    Console.WriteLine("Case 2");
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
            return 0f;
        }
        
        // void OnMouseOver()
        // {
        //     if (transform.CompareTag("Furniture"))
        //     {
        //         Debug.Log("dessus");                
        //     }
        //
        // }
        //
        // void OnMouseExit()
        // {
        //     if (transform.CompareTag("Furniture"))
        //     {
        //         Debug.Log("pas dessus");                
        //     }
        // }
    }
    
}
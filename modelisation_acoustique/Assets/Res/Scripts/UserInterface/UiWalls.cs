using System;
using Res.Scripts.Object;
using UnityEngine;

namespace Res.Scripts.UserInterface
{
    public class UiWalls : MonoBehaviour
    {
        
        private static UiWalls _instance;
        public static UiWalls Instance { get { return _instance; } }
        public bool uiWallsState = false;
        public ObjectData objData;
        private void Awake()
        {
            _instance = this;
        }

        public void ChangeState()
        {
            uiWallsState = !uiWallsState;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(!child.gameObject.activeSelf);
            }
        }
        
        public void OnClickDropDown(string matName)
        {
            objData.UpdateMaterial(matName);
        }
    }
}
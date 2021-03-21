using System;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Res.Scripts.UserInterface
{
    public class UiManager : MonoBehaviour
    {
        public KeyCode key = KeyCode.LeftControl;
        private void ChangeState()
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(!child.gameObject.activeSelf);
            }
        }
        
        public void Update()
        {
            if (Input.GetKeyDown(key))
            {
                ChangeState();
            }
        }
    }
}
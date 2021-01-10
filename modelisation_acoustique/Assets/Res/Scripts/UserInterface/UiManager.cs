using System;
using UnityEngine;

namespace Res.Scripts.UserInterface
{
    public class UiManager : MonoBehaviour
    {
        private void ChangeState()
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(!child.gameObject.activeSelf);
            }
        }

        public void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                ChangeState();
            }
        }
    }
}
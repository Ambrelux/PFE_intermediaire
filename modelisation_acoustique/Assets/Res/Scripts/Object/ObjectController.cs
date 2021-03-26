﻿using UnityEngine;

namespace Res.Scripts.Object
{
    public class ObjectController : MonoBehaviour
    {
        public float speed = 5f;
        public GameObject _movableObject;
        private void Update()
        {
            OnClickSelectObject();
            Move();
        }

        private void Move()
        {
            if (_movableObject != null)
            {
                if (Input.GetKey(KeyCode.Keypad2))
                {
                    _movableObject.transform.Translate(Vector3.left * (Time.deltaTime * speed));
                }

                if (Input.GetKey(KeyCode.Keypad5))
                {
                    _movableObject.transform.Translate(Vector3.right * (Time.deltaTime * speed));
                }

                if (Input.GetKey(KeyCode.Keypad1))
                {
                    _movableObject.transform.Translate(Vector3.forward * (Time.deltaTime * speed));
                }

                if (Input.GetKey(KeyCode.Keypad3))
                {
                    _movableObject.transform.Translate(-Vector3.forward * (Time.deltaTime * speed));
                }
            }
        }

     

        private void OnClickSelectObject()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("testgyg");
                RaycastHit hit;
                Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    if (hit.collider.gameObject.CompareTag("Furniture"))
                    {
                        _movableObject = hit.collider.gameObject;
                        Debug.Log("test");
                    }
                }
            }
        }
        
    }
}

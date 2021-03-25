using System;
using UnityEngine;

namespace Res.Scripts.Camera
{
    public class CameraBehaviour : MonoBehaviour
    {
        public float speed = 15;
        public float rotationSpeed = 50;

        private void Update()
        {
            Rotate();
            Move();
            Raise();
        }

        private void Rotate()
        {
            if(Input.GetAxis("Horizontal") > 0)
            {
                transform.Rotate(Vector3.up * (Time.deltaTime * rotationSpeed));
            }
            else if(Input.GetAxis("Horizontal") < 0)
            {
                transform.Rotate(-Vector3.up * (Time.deltaTime * rotationSpeed));
            }
        }

        private void Move()
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                transform.Translate(Vector3.forward * (Time.deltaTime * speed));
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                transform.Translate(-Vector3.forward * (Time.deltaTime * speed));
            }
        }

        private void Raise()
        {
            if (Input.GetKey(KeyCode.H))
            {
                transform.Translate(Vector3.up * (Time.deltaTime * speed));
            }
            else if (Input.GetKey(KeyCode.B))
            {
                transform.Translate(-Vector3.up * (Time.deltaTime * speed));
            }
        }
    }
}
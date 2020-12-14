using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraBehaviour : MonoBehaviour
{
    public float speed = 15;
    public float rotationSpeed = 50;

    void Update()
    {
        Rotate();
        Move();
        Raise();
    }

    void Rotate()
    {
        if(Input.GetAxis("Horizontal") > 0)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            transform.Rotate(-Vector3.up * Time.deltaTime * rotationSpeed);
        }
    }

    void Move()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            transform.Translate(-Vector3.forward * Time.deltaTime * speed);
        }
    }

    void Raise()
    {
        if (Input.GetKey(KeyCode.H))
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        else if (Input.GetKey(KeyCode.B))
        {
            transform.Translate(-Vector3.up * Time.deltaTime * speed);
        }
    }
}


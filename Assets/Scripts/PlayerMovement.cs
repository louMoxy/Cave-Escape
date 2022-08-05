using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        ProcessRotation();
    }

    void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space)) {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Rotate(Vector3.back);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Rotate(Vector3.forward);
        }
    }

    private void Rotate(Vector3 rotation)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(rotation * rotationThrust * Time.deltaTime);
        rb.freezeRotation = false;
    }
}

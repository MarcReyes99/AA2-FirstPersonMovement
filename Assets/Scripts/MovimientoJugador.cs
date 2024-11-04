using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public float speed = 10f;
    public Transform cameraTransform;

    public float sprintMultiplier = 2f;
    public float jumpForce = 5f;
    public LayerMask layerMask;
    public bool grounded;

    private Rigidbody rb;    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        CheckGrounded();

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        float currentSpeed = speed;

        if(grounded)
        {
            if (z > 0)
            {
                currentSpeed = speed;
            }
            else if (z < 0)
            {
                currentSpeed = speed * 0.5f;
            }
            else if (x != 0)
            {
                currentSpeed = speed * 0.75f;
            }
            if (Input.GetKey(KeyCode.LeftShift) && z > 0)
            {
                currentSpeed *= sprintMultiplier;
            }
        }
        else
        {
            currentSpeed *= 0.5f;
        }

        Vector3 move = (forward * z + right * x).normalized * currentSpeed * Time.deltaTime;
        transform.Translate(move, Space.World);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void CheckGrounded()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, layerMask);
    }
}

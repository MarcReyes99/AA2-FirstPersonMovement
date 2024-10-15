using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public float speed = 12f;

    public LayerMask layerMask;
    public bool grounded;
    public Vector2[] points;
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, layerMask))
        {
            grounded = true; // Si el Raycast toca el suelo
        }
        else
        {
            grounded = false; // Si no toca el suelo
        }
    }
}

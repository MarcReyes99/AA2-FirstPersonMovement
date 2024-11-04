using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoPickUp : MonoBehaviour
{
    public Transform cameraTransform;
    public float pickupRange = 2f;
    public LayerMask pickupLayer;
    private GameObject pickedObject = null;
    private Rigidbody pickedObjectRb;

    public float minHoldDistance = 1f;   
    public float maxHoldDistance = 3f;     
    public float holdDistance = 1.5f;
    public float smoothSpeed = 10f;

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (pickedObject == null)
            {
                Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, pickupRange, pickupLayer))
                {
                    GameObject targetObject = hit.collider.gameObject;

                    Rigidbody rb = targetObject.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        pickedObject = targetObject;
                        pickedObjectRb = rb;

                        pickedObjectRb.useGravity = false;
                        pickedObjectRb.isKinematic = true;

                        pickedObject.layer = LayerMask.NameToLayer("Default");
                    }
                }
            }
        }
        else
        {
            if (pickedObject != null)
            {
                pickedObject.layer = LayerMask.NameToLayer("PickUp");

                pickedObjectRb.useGravity = true;
                pickedObjectRb.isKinematic = false;

                pickedObject = null;
                pickedObjectRb = null;
            }
        }
        if (pickedObject != null)
        {
            Vector3 targetPosition = cameraTransform.position + cameraTransform.forward * holdDistance;
            pickedObject.transform.position = Vector3.Lerp(pickedObject.transform.position, targetPosition, Time.deltaTime * smoothSpeed);
            pickedObject.transform.rotation = Quaternion.Slerp(pickedObject.transform.rotation, cameraTransform.rotation, Time.deltaTime * smoothSpeed);

            AdjustHoldDistance();
        }
    }

    void AdjustHoldDistance()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        holdDistance = Mathf.Clamp(holdDistance + scroll * 10, minHoldDistance, maxHoldDistance);
    }
}

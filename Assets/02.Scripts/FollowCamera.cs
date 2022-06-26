using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    //public Transform targetTransform;

    //private Transform cameraTransform;

    //[Range(-1.0f, 20.0f)]
    //public float distance = 10.0f;

    //[Range(0.0f, 10.0f)]
    //public float height = 2.0f;

    //public float moveDamping = 100f;
    //public float rotateDamping = 100f;

    //public float targetOffset = 2.0f;

    //void Start()
    //{
    //    cameraTransform = GetComponent<Transform>();
    //}

    //void Update()
    //{
    //    Vector3 pos = targetTransform.position
    //                  + (-targetTransform.forward * distance)
    //                  + (Vector3.up * height);

    //    cameraTransform.position = Vector3.Slerp(cameraTransform.position, pos, moveDamping * Time.deltaTime);

    //    cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, targetTransform.rotation, rotateDamping * Time.deltaTime);

    //    cameraTransform.LookAt(targetTransform.position + (targetTransform.up * targetOffset));
    //}
    [SerializeField]
    public float rotationSpeed = 200f;

    float mouseX;
    float mouseY;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Mouse X");
        float v = Input.GetAxisRaw("Mouse Y");

        mouseX += h *  rotationSpeed * Time.deltaTime;
        mouseY += v * rotationSpeed * Time.deltaTime;

        mouseY = Mathf.Clamp(mouseY, -20, -20);
        transform.eulerAngles = new Vector3(-mouseY, mouseX, 0);
    }


}

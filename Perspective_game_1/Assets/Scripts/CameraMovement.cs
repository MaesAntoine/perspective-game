using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement Instance; //camera direction will be needed for alignment checks
    public Camera cam;

    [SerializeField] float sensitivity = 180f;
    [SerializeField] float distance = 5f;

    private Vector3 previousPosition;
    private float clipLimit = 200f;


    private void Awake()
    {
        Instance = this;

        cam.nearClipPlane = -clipLimit;
        cam.farClipPlane = clipLimit;

        MoveCamera();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            MoveCamera();
        }

    }

    private void MoveCamera()
    {
        // get the direction and length of mouse drag input
        Vector3 direction = previousPosition - cam.ScreenToViewportPoint(Input.mousePosition);

        cam.transform.position = new Vector3();

        cam.transform.Rotate(new Vector3(1, 0, 0), direction.y * sensitivity);
        cam.transform.Rotate(new Vector3(0, 1, 0), -direction.x * sensitivity, Space.World);

        // make the camera rotate at a certain distance from the point (0,0,0)
        cam.transform.Translate(new Vector3(0, 0, -distance));

        previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float panSpeed = 30f;
	public float panBorderThickness = 10f;
	public float zoomSpeedMouse = 0.5f;

    [Header("Bounds")]
    public float[] boundsX = new float[] { -10f, 5f };
    public float[] boundsZ = new float[] { -100f, 0f };
    public float[] zoomBounds = new float[] { 8f, 40f };

    private Camera cam;
    private Vector3 lastPanPosition;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private bool doMovement = true;

    // Update is called once per frame
    void Update()
    {

        if (GameManager.GameIsOver)
        {
            this.enabled = false;
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            lastPanPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            PanCamera(Input.mousePosition);
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        ZoomCamera(scroll, zoomSpeedMouse);
    }

    void PanCamera(Vector3 newPanPosition)
    {
        // Determine how much to move the camera
        Vector3 offset = cam.ScreenToViewportPoint(lastPanPosition - newPanPosition);
        Vector3 move = - new Vector3(offset.x * panSpeed, 0, offset.y * panSpeed);

        // Perform the movement
        transform.Translate(move, Space.World);

        //Ensure the camera remains within bounds.
        Vector3 pos = transform.position;

       pos.x = Mathf.Clamp(transform.position.x, boundsX[0], boundsX[1]);
       pos.z = Mathf.Clamp(transform.position.z, boundsZ[0], boundsZ[1]);
        transform.position = pos;

        // Cache the position
        lastPanPosition = newPanPosition;
    }


    void ZoomCamera(float offset, float speed)
    {
        if (offset == 0)
        {
            return;
        }

        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView - (offset * speed), zoomBounds[0], zoomBounds[1]);
    }
}

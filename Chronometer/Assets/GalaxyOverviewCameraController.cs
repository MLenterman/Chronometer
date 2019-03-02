using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/*  TODO:
 *  - Implement Min&Max zoom constaints
 *  - Implement Min&Max top view movement
 *  - Abstract out the camera system
 *  - Fix fast zoom scroll overshooting
 *  - Implement zoom towards mouse position
 *  - Implement goto functionality
 *  - Implement mouse on edge of screen camera movement
 */


public class GalaxyOverviewCameraController : MonoBehaviour
{
    public Camera Cam;

    public float MinXYSpeed = 20.0f;
    public float MaxXYSpeed = 10000.0f;
    public float MinZoomSpeed = 75.0f;
    public float MaxZoomSpeed = 25000.0f;
    public float ZoomDampFactor = 0.9f;
    public float LowScaleDistance = 50.0f;
    public float HighScaleDistance = 6000.0f;

    private Vector3 target = Vector3.zero;
    private Vector3 value = Vector3.zero;
    private Vector3 velocity = Vector3.zero;

    private float distance = 0.0f;
    private float norm = 1.0f;
    private float zoomFactor = 1.0f;
    private float xzFactor = 1.0f;
	
	void Update ()
	{
	    distance = Cam.transform.position.y;
	    norm = (distance - LowScaleDistance) / (HighScaleDistance - LowScaleDistance);
	    norm = Mathf.Clamp01(norm);

	    zoomFactor = Mathf.Lerp(MinZoomSpeed, MaxZoomSpeed, norm);
        xzFactor = Mathf.Lerp(MinXYSpeed, MaxXYSpeed, norm);

        target.x = Cam.transform.position.x + Input.GetAxisRaw("Horizontal") * xzFactor;
	    target.z = Cam.transform.position.z + Input.GetAxisRaw("Vertical") * xzFactor;
	    target.y = Cam.transform.position.y + Input.GetAxisRaw("MouseScrollWheel") * -zoomFactor;

        Cam.transform.position = Vector3.SmoothDamp(Cam.transform.position, target, ref velocity, ZoomDampFactor);
    }
}

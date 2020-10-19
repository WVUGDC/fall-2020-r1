using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // All "public" variables can be seen and changed from the inspector

    [Tooltip("This is the game object that the camera will track")]
    public GameObject target;

    [Tooltip("This is the time it takes for the camera to zoom in and out. Lower values will result in snappier camera zooming")]
    public float sizeAdjustmentTime;// This is the time it takes for the camera to zoom in and out. Lower values will result in snappier camera movements  

    [Tooltip("Minimum orthographic size of camera")]
    public float minSize;

    [Tooltip("Maximum orthographic size of camera")]
    public float maxSize;

    [Tooltip("This is the time it takes for camera panning adjustments. Lower values will result in snappier camera panning")]
    public float panAdjustmentTime;

    [Tooltip("This is the degree to which the camera will pan ahead based on the target's velocity")]
    public float velocityFactor;


    // These are private variables that cannot be changed in the inspector window

    private Vector3 velocity = Vector3.zero; // This a velocity for the smooth damp movement
    private Rigidbody2D targetRigidBody;     // This is a reference to the RigidBody2D component of the target. This will be set on "Start"
    private Camera cam;                      // This is a reference to the "Camera" component which will have its orthographic size property adjusted 
    private float targetSize;                // This is the orthographic size that the camera is smoothly moving to

    // Start is called before the first frame update
    void Start()
    {
        targetRigidBody = target.GetComponent<Rigidbody2D>();
        cam = GetComponent<Camera>();
        cam.orthographicSize = maxSize;
    }

    // Update is called once per frame
    void Update()
    {
        PanCamera();
        AdjustCameraSize();
    }

    // Manipulates the camera's X and Y coordinates to track the target
    void PanCamera()
    {
        // Calculates the point that the camera will track to (target's position plus velocity to "look ahead")
        Vector3 trackPosition = target.transform.position + (Vector3)targetRigidBody.velocity * velocityFactor;

        // Smoothly move the camera towards that target position
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, trackPosition, ref velocity, panAdjustmentTime);
        targetPosition.z = transform.position.z; // This is a 2D game, so we don't want to be changing any Z values!
        transform.position = targetPosition; // Set the position of the camera to the targetPosition value we just made
    }

    // If you want to know how this works, please see https://docs.unity3d.com/ScriptReference/Mathf.Lerp.html
    void AdjustCameraSize()
    {
        targetSize = Mathf.Lerp(minSize, maxSize, targetRigidBody.velocity.magnitude);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, 1 / sizeAdjustmentTime * Time.deltaTime);
    }
}

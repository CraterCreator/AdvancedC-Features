using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArwingController : MonoBehaviour
{

    public enum Mode
    {
        CONFINED = 0,
        ALL_RANGE = 1
    }
    public Mode arwingMode = Mode.CONFINED;

    [Header("Camera")]
    public float cameraYSpeed = 0.5f;
    public float cameraMoveSpeed = 20f;
    public float cameraDistance = 5f;

    [Header("Arwing")]
    public Transform aimTarget;
    public Transform moveTarget;
    public float aimingSpeed = 15f;
    public float movementSpeed = 40f;
    public float rotationSpeed = 10f;

    private Camera parentCam;
    private float startDistance = 5f;
    private Vector3 up = Vector3.up;

    // Use this for initialization
    void Start()
    {
        parentCam = GetComponentInParent<Camera>();
        startDistance = Vector3.Distance(parentCam.transform.position, transform.position);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Moves the arwing to target gameobject
    void MoveArwingToMoveTarget()
    {
        // Get global aim target position
        Vector3 moveTargetPos = moveTarget.position;
        Vector3 arwingPos = transform.position;

        // Move the arwing towards aimtarget
        arwingPos = Vector3.MoveTowards(arwingPos, moveTargetPos, movementSpeed * Time.deltaTime);

        // Apply position to arwing
        transform.position = arwingPos;

        // reset z locally to startDistance
        Vector3 localArwingPos = transform.localPosition;
        localArwingPos.z = startDistance;
        transform.localPosition = localArwingPos;

    }

    // Moves the MoveTarget gameobject to arwing
    void MoveTargetToArwing()
    {
        // Get the aim target's local position
        Vector3 localAimTargetPos = aimTarget.localPosition;
        Vector3 localArwingPos = transform.localPosition;

        // Move aim target towards the arwing
        localAimTargetPos = Vector3.MoveTowards(localAimTargetPos, localArwingPos, movementSpeed * Time.deltaTime);

        // Apply aim targets local position
        aimTarget.localPosition = localAimTargetPos;
        transform.localPosition = localArwingPos;

    }

    // Rotating arwing to the aim target
    void RotateArwingToAimTarget()
    {
        // Get direction to aimTarget
        Vector3 direction = aimTarget.position - transform.position;

        // Get rotation to up
        Quaternion rotation = Quaternion.LookRotation(direction.normalized, up);

        // Lerp rotation for arwing
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, aimingSpeed * Time.deltaTime);
    }

    // Moves foward the camera
    void MoveForward()
    {
        parentCam.transform.position += parentCam.transform.forward * cameraMoveSpeed * Time.deltaTime;
    }

    // Get Camera to follow arwing only in AllRange mode
    void FollowArwing()
    {
        // Get camera's position & rotation
        Vector3 position = parentCam.transform.position;
        Quaternion rotation = parentCam.transform.rotation;

        // Get local position of target and arwing
        Vector3 localArwingPos = transform.localPosition;
        Vector3 localTargetPos = moveTarget.localPosition;

        // Calculate direction with LocalPos
        Vector3 direction = localTargetPos - localArwingPos;

        // Rotate the camera to direction
        rotation *= Quaternion.AngleAxis(direction.x * rotationSpeed * Time.deltaTime, Vector3.up);

        // Move the camera up at different speed
        position.y += direction.y * cameraYSpeed * Time.deltaTime;

        // Apply newly made position to camera
        parentCam.transform.position = position;
        parentCam.transform.rotation = rotation;
    }

    void MoveTarget(float inputH, float inputV)
    {
        // Get inputDir
        Vector3 inputDir = new Vector3(inputH, inputV, 0);

        // Calculate force by inputDir x movementSpeed
        Vector3 force = inputDir * movementSpeed;

        // Offset aimTarget by force
        moveTarget.localPosition += force * Time.deltaTime;
    }

    public void Move(float inputH, float inputV)
    {
        // Move the target
        MoveTarget(inputH, inputV);

        // Move Forward
        MoveForward();
        // Move based on arwing mode
        switch (arwingMode)
        {
            case Mode.CONFINED:
                break;
            case Mode.ALL_RANGE:
                FollowArwing();
                break;
            default:
                break;

        }
        // If no input was made move target to arwing
        if (inputH == 0 && inputV == 0)
        {
            MoveTargetToArwing();
        }

        // Move the arwing to move target
        MoveArwingToMoveTarget();

        // Rotate to aim target
        RotateArwingToAimTarget(); // NOT IMPLEMENTED
    }
}

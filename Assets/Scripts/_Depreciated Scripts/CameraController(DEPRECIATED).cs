using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Structure Used to Hold Data for aligning the camera
 * 
 */
struct CamPos
{
    private Vector3 pos;
    private Transform rotation;

    private Vector3 position;
    // Transform used for any rotation
    private Transform xForm;

    public Vector3 Position { get { return position; } set { position = value; } }
    public Transform XForm { get { return xForm; } set { xForm = value; } }

    public void Init(string camName, Vector3 pos, Transform transform, Transform parent)
    {
        position = pos;
        xForm = transform;
        xForm.name = camName;
        xForm.parent = parent;
        xForm.localPosition = Vector3.zero;
        xForm.localPosition = position;
    }
}

public class CameraController : MonoBehaviour
{
    public Transform cameraRotation;
    public float distanceFromPlayer;
    private float distanceMulti = 1.5f;
    public float cameraHeight;
    public float camHeightMulti = 5f;
    public Transform target;

    public float wideScreen = 0.2f;
    public float targetTime = 0.5f;
    public float firstPersonLookSpeed = 3.0f;
    public Vector2 firstPersonXClamp = new Vector2(-70f, 90f);
    public float firstPersonRotDegPerSec = 120f;
    public float firstPersonThresh = 0.5f;
    public float freeThresh = -.1f;

    public Vector2 minDistFromPlayer = new Vector2(1f,-0.5f);
    public float camStickThresh = 0.1f;
    public float freeRotDegPerSec = -5f;
    public float mouseWheelSensitivity = 3.0f;
    public float compensationOffset = 0.2f;
    public CamStates startingState = CamStates.Free;


    public Vector3 velCamSmooth = Vector3.zero;
    public float camSmoothDampTime = 0.1f;
    public Vector3 velLookDir = Vector3.zero;
    public float lookDirDampTime = 0.1f;

    private Vector3 lookDir;
    private Vector3 currentLookDIr;

    private CamStates camState = CamStates.Behind;
    private float XRot = 0.0f;
    private CamPos firstPersonCamPos;
    private float lookWeight;
    private const float TARGET_THRESHOLD = 0.01f;
    private Vector3 savedRigToGoal;
    private float distanceAwayFree;
    private float distanceUpFree;
    private Vector2 rightStickPrevFrame = Vector2.zero;
    private float lastStickMin = float.PositiveInfinity;
    private Vector3 nearClipDimensions = Vector3.zero;
    private Vector3 viewFrustum;
    private Vector3 playerOffset;
    private Vector3 targetPosition;

    public Vector3 RigToGoalDirection
    {
        get
        {
            // Move height and distance from character in separate parentRig transform since RotateAround has control of both position and rotation
            Vector3 rigToGoalDirection = Vector3.Normalize(playerOffset - this.transform.position);
            // Can't calculate distanceAway from a vector with Y axis rotation in it; zero it out
            rigToGoalDirection.y = 0f;

            return rigToGoalDirection;
        }
    }
    public CamStates CamState
    {
        get
        {
            return this.camState;
        }
    }

    public enum CamStates
    {
        Behind,         // Single analog stick, Japanese-style; character orbits around camera; default for games like Mario64 and 3D Zelda series
        FirstPerson,    // Traditional 1st person look around
        Target,         // L-targeting variation on "Behind" mode
        Free            // High angle; character moves relative to camera facing direction
    }
    // Start is called before the first frame update
    void Start()
    {
        cameraRotation = this.transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

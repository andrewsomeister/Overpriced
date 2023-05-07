using System;
using UnityEngine;
using UnityEngine.Serialization;

// follows tutorial https://www.youtube.com/watch?v=7bevpWbHKe4 
public class SwingingArms : MonoBehaviour
{
    public GameObject centerEyeCamera;
    public GameObject forwardDirection;
    public GameObject leftHand;
    public GameObject rightHand;
    public float speedup = 70;

    private float _handSpeed;

    private Vector3 _previousPlayerPosition;
    private Vector3 _previousLeftHandPosition;
    private Vector3 _previousRightHandPosition;

    private Vector3 _currentPlayerPosition;
    private Vector3 _currentLeftHandPosition;
    private Vector3 _currentRightHandPosition;

    private void Start()
    {
        // initialize previous positions
        _previousPlayerPosition = transform.position;
        _previousLeftHandPosition = leftHand.transform.position;
        _previousRightHandPosition = rightHand.transform.position;
    }

    private void Update()
    {
        // set forward direction around y-axis based on where camera/player is looking
        float yRotation = centerEyeCamera.transform.eulerAngles.y;
        forwardDirection.transform.rotation = Quaternion.AngleAxis(yRotation, Vector3.up);

        // update current positions
        _currentPlayerPosition = transform.position;
        _currentLeftHandPosition = leftHand.transform.position;
        _currentRightHandPosition = rightHand.transform.position;

        // compute hand travelled distances neglecting body movement between frames 
        var playerDistance =
            Vector3.Distance(_currentPlayerPosition, _previousPlayerPosition);
        var leftHandDistance =
            Vector3.Distance(_currentLeftHandPosition, _previousLeftHandPosition)
            - playerDistance;
        var rightHandDistance =
            Vector3.Distance(_currentRightHandPosition, _previousRightHandPosition)
            - playerDistance;
        _handSpeed = leftHandDistance + rightHandDistance;

        // update player position
        if (Time.timeSinceLevelLoad > 1f)
        {
            transform.position += forwardDirection.transform.forward *
                                  (_handSpeed * speedup * Time.deltaTime);
        }

        // update previous positions 
        _previousPlayerPosition = _currentPlayerPosition;
        _previousLeftHandPosition = _currentLeftHandPosition;
        _previousRightHandPosition = _currentRightHandPosition;
    }
}
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    [Header("Grasping Properties")] public float graspingRadius = 0.1f;

    // Store the hand controller this object will be attached to
    private HandController _handController;
    private bool _isAvailable = true;

    // Store initial transform parent
    private Transform _initialTransformParent;
    private Rigidbody _rigidbody; // optional
    private bool _hasRigidBody;

    void Start()
    {
        _initialTransformParent = transform.parent;
        _rigidbody = GetComponent<Rigidbody>();
        _hasRigidBody = _rigidbody != null;
    }

    // todo refactor & clean up: move/initialize expensive calls in Start(); comment/remove logs after done testing 
    public void throw_to(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        // Make sure that the object is not attached to a hand controller
        if (_handController != null) return;
        Debug.Log("inside throw_to () ");
        // Move the object to the given position
        if (this.GetComponent<Rigidbody>() != null)
        {
            Debug.Log("inside throw_to () if condition ");
            linearVelocity = new Vector3(10, 10, 10);
            angularVelocity = new Vector3(1, 1, 2);
            this.GetComponent<Rigidbody>().isKinematic = false;
            Debug.Log("kineamtic set to true ");

            this.GetComponent<Rigidbody>().AddForce(linearVelocity);
            Debug.Log("throw to called, two vectors {0} {1} " + linearVelocity.ToString() + angularVelocity.ToString());
        }
    }

    public void attach_to(HandController handController)
    {
        // Store the hand controller in memory
        _handController = handController;
        _isAvailable = false;

        // Set Rigidbody to kinematic so object moves with the parent (transform-based motion)
        // NOTE: seemingly has to be done before SetParent() to work
        if (_hasRigidBody)
        {
            _rigidbody.isKinematic = true;
        }

        // Set the object to be placed in the hand controller referential
        transform.SetParent(handController.transform);
    }

    public void detach_from(HandController handController)
    {
        // Make sure that the right hand controller ask for the release
        if (_handController != handController) return;

        // Detach the hand controller
        _handController = null;
        _isAvailable = true;

        // Set the object to be placed in the original transform parent
        transform.SetParent(_initialTransformParent);
        // No longer a kinematic Rigidbody after 1st grab
        // i.e. grabbable objects start to have collision effect & physics-based motion
        // after being moved away from initial position in scene
        if (_hasRigidBody)
        {
            _rigidbody.isKinematic = false;
        }
    }

    public bool is_available()
    {
        return _isAvailable;
    }

    public float get_grasping_radius()
    {
        return graspingRadius;
    }
}
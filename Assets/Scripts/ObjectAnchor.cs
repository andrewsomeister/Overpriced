using UnityEngine;

public class ObjectAnchor : MonoBehaviour
{
    [Header("Grasping Properties")] public float graspingRadius = 0.1f;

    // Store initial transform parent
    protected Transform initial_transform_parent;

    private Rigidbody _rigidbody; // optional
    private bool _hasRigidBody;

    void Start()
    {
        initial_transform_parent = transform.parent;
        _rigidbody = GetComponent<Rigidbody>();
        _hasRigidBody = _rigidbody != null;
    }


    // Store the hand controller this object will be attached to
    protected HandController hand_controller = null;

    public void throw_to(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        // Make sure that the object is not attached to a hand controller
        if (hand_controller != null) return;
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

    public void attach_to(HandController hand_controller)
    {
        // Store the hand controller in memory
        this.hand_controller = hand_controller;

        // Set Rigidbody to kinematic so object moves with the parent (transform-based motion)
        // NOTE: seemingly has to be done before SetParent() to work
        if (_hasRigidBody)
        {
            _rigidbody.isKinematic = true;
        }
        // Set the object to be placed in the hand controller referential
        transform.SetParent(hand_controller.transform);
    }

    public void detach_from(HandController hand_controller)
    {
        // Make sure that the right hand controller ask for the release
        if (this.hand_controller != hand_controller) return;

        // Detach the hand controller
        this.hand_controller = null;

        // Set the object to be placed in the original transform parent
        transform.SetParent(initial_transform_parent);
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
        return hand_controller == null;
    }

    public float get_grasping_radius()
    {
        return graspingRadius;
    }
}
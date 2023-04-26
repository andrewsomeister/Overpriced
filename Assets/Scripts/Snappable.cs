using UnityEngine;

public class Snappable : MonoBehaviour
{
    private bool _isSnappable;
    private bool _isSnapped;

    [Header("Snap Object")] public GameObject snapObject; // object that can snap onto this object
    private ObjectAnchor _grabbable; // mandatory
    private Rigidbody _rigidbody; // mandatory
    [Header("Snap Zone")] public GameObject snapZone;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == snapObject.name)
        {
            _isSnappable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == snapObject.name)
        {
            _isSnappable = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _grabbable = snapObject.GetComponent<ObjectAnchor>();
        _rigidbody = snapObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        Snap();
    }

    private void Snap()
    {
        if (!_isSnappable || _isSnapped || !_grabbable.is_available()) return;
        // Set Rigidbody to kinematic to use transform-based motion
        _rigidbody.isKinematic = true;
        // Update transform to snap to the center of Snap Zone
        var zoneTransform = snapZone.gameObject.transform;
        var objectTransform = snapObject.gameObject.transform;
        objectTransform.position = zoneTransform.position;
        objectTransform.rotation = zoneTransform.rotation;
        // Set parent so the pair moves together 
        objectTransform.SetParent(zoneTransform);
        // Avoid more snapping (can snap only once)
        _isSnapped = true;
        // todo box no longer grabbable 
        
        // todo bag becomes grabbable
    }
}
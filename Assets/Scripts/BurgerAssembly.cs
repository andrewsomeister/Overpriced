using UnityEngine;

/**
 * Snaps ingredients in order into a designated zone in burger box
 */
public class BurgerAssembly : MonoBehaviour
{
    private const int TotalIngredients = 4;
    private int _progress = 0;

    private bool _isSnappable = false;
    private GameObject _nextIngredient;
    private Grabbable _grabbable;

    public GameObject anchor;

    private void OnTriggerEnter(Collider other)
    {
        if ((_progress == 0 && other.gameObject.name == "bun")
            || (_progress == 1 && other.gameObject.name == "grilled_patty")
            || (_progress == 2 && other.gameObject.name == "chopped_tomato")
            || (_progress == 3 && other.gameObject.name == "bun"))
        {
            _nextIngredient = other.gameObject;
            _isSnappable = true;
            _progress++;
        }
    }

    public bool isReady()
    {
        return _progress == TotalIngredients;
    }

    private void OnTriggerExit(Collider other)
    {
        if (ReferenceEquals(other.gameObject, _nextIngredient))
        {
            _nextIngredient = null;
            _isSnappable = false;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        Snap();
    }

    private void Snap()
    {
        if (!_isSnappable || !_grabbable.is_available()) return;
        // Set Rigidbody to kinematic to use transform-based motion
        // _rigidbody.isKinematic = true; // todo ?
        // Update transform to snap to the center of Snap Zone
        var zoneTransform = anchor.gameObject.transform;
        var objectTransform = _nextIngredient.gameObject.transform;
        objectTransform.position = zoneTransform.position;
        objectTransform.rotation = zoneTransform.rotation;
        // Set parent so the pair moves together 
        objectTransform.SetParent(zoneTransform);
        // todo ingredients no longer grabbable 
    }
}
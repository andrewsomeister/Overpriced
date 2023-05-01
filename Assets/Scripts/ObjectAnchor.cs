using UnityEngine;

public class ObjectAnchor : MonoBehaviour {

	[Header( "Grasping Properties" )]
	public float graspingRadius = 0.1f;

	// Store initial transform parent
	protected Transform initial_transform_parent;
	void Start () {
		initial_transform_parent = transform.parent;
	}


	// Store the hand controller this object will be attached to
	protected HandController hand_controller = null;

	public void stop_moving (GameObject trashcan) {
		Vector3 zeroforce = new Vector3 (0,0,0);
		this.GetComponent<Rigidbody>().AddForce(zeroforce); 
		this.GetComponent<Rigidbody>().isKinematic = true;
		Debug.Log("Inside stop moving "); 
		transform.localScale = new Vector3(0.2F, 0.2F, 0.2F);
		transform.SetParent( trashcan.transform );
		transform.localPosition = new Vector3(0, 0.3F, 0);
		Debug.Log("stop moving parent is " + transform.parent.ToString()); 
		Debug.Log("current position is " + transform.position.ToString() );
		Debug.Log("current local position is " + transform.localPosition.ToString() );
	}

	public void throw_to ( Vector3 linearVelocity) {
		// Make sure that the object is not attached to a hand controller
		if ( hand_controller != null ) return;
		Debug.Log("inside throw_to () " + linearVelocity.ToString() ); 
		// Move the object to the given position
		if (this.GetComponent<Rigidbody>() != null) {
			// linearVelocity = new Vector3 (10,10,10); 
			// angularVelocity = new Vector3 (1,1,2);
			this.GetComponent<Rigidbody>().isKinematic = false; 
			this.GetComponent<Rigidbody>().AddForce(linearVelocity*10); 
		}
	}

	public void attach_to ( HandController hand_controller ) {
		// Store the hand controller in memory
		this.hand_controller = hand_controller;

		// Set the object to be placed in the hand controller referential
		transform.SetParent( hand_controller.transform );
	}

	public void detach_from ( HandController hand_controller ) {
		// Make sure that the right hand controller ask for the release
		if ( this.hand_controller != hand_controller ) return;

		// Detach the hand controller
		this.hand_controller = null;

		// Set the object to be placed in the original transform parent
		transform.SetParent( initial_transform_parent );
	}

	public bool is_available () { return hand_controller == null; }

	public float get_grasping_radius () { return graspingRadius; }
}
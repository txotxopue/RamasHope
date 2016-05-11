using UnityEngine;


/// <summary>
/// Handles the ship movement.
/// Currently it only can rotate around the tube.
/// </summary>
public class Movement : MonoBehaviour
{
    ///<summary>The velocity at which you rotate around the tube</summary>
    [SerializeField]
    private float _rotationSpeed = 10f;
    ///<summary>Reference to the ship axis</summary>
    private Transform _root;

    
    /// <summary>
    /// Called when the GameObject is created.
    /// </summary>
    void Awake ()
    {
        // Get the root object (this is the axis for the tube rotations)
        _root = transform.parent;
    }


    /// <summary>
    /// Rotate the ship around its axis.
    /// </summary>
    /// <param name="pRotation">Amount of rotation. Actually you can rotate only on Z axis.</param>
    public void Rotate (Vector3 pRotation)
    {
        _root.Rotate(pRotation * Time.deltaTime * _rotationSpeed);
    }

    
    /// <summary>
    /// Handle collisions with other objects.
    /// If this is a wall, the ship is destroyed.
    /// If this is a pickup, we call the pickup's method.
    /// </summary>
    /// <param name="other">Collider which we collided with</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            // First, we unparent the camera, otherwise it will get destroyed
            _root.GetComponentInChildren<Camera>().transform.parent = null;
            // Then deactivate the player ship
            _root.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.SendMessage("PickedUp", this.gameObject, SendMessageOptions.DontRequireReceiver);
        }
    }
}

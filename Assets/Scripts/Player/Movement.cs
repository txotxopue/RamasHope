using UnityEngine;


/// <summary>
/// Handles the ship movement.
/// Currently it only can rotate around the tube.
/// </summary>
public class Movement : MonoBehaviour
{
    ///<summary>The velocity at which you rotate around the tube</summary>
    [SerializeField]
    private float m_RotationSpeed = 10f;
    ///<summary>Reference to the ship axis</summary>
    private Transform m_root;

    //not needed at this moment
    //[SerializeField]
    //private float m_MovementSpeed = 10f;

    
    /// <summary>
    /// Called when the GameObject is created.
    /// </summary>
    void Awake ()
    {
        // Get the root object (this is the axis for the tube rotations)
        m_root = transform.parent;
    }


    /// <summary>
    /// Rotate the ship around its axis.
    /// </summary>
    /// <param name="pRotation">Amount of rotation. Actually you can rotate only on Z axis.</param>
    public void Rotate (Vector3 pRotation)
    {
        m_root.Rotate(pRotation * Time.deltaTime * m_RotationSpeed);
    }


    /* Not needed at the moment
    public void Move(Vector3 pMovement)
    {
        m_root.Translate(pMovement * Time.deltaTime * m_MovementSpeed);
    }
    */

    
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
            m_root.GetComponentInChildren<Camera>().transform.parent = null;
            // Then deactivate the player ship
            m_root.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.SendMessage("PickedUp", this.gameObject, SendMessageOptions.DontRequireReceiver);
        }
    }
}

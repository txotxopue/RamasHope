using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public float m_RotationSpeed = 10f;
    public float m_MovementSpeed = 10f;
    private Transform m_root;

    void Awake ()
    {
        m_root = transform.parent;
    }

    public void Rotate (Vector3 pRotation)
    {
        m_root.Rotate(pRotation * Time.deltaTime * m_RotationSpeed);
    }

    public void Move(Vector3 pMovement)
    {
        m_root.Translate(pMovement * Time.deltaTime * m_MovementSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        print("Triggered!!!");
        if (other.gameObject.CompareTag("Wall"))
        {
            print("Wall Triggered!!!");
        }
    }
}

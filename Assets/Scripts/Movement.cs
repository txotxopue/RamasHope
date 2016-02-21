using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public float m_RotationSpeed = 10f;
    public float m_MovementSpeed = 10f;

    public void Rotate (Vector3 pRotation)
    {
        transform.Rotate(pRotation * Time.deltaTime * m_RotationSpeed);
    }

    public void Move(Vector3 pMovement)
    {
        transform.Translate(pMovement * Time.deltaTime * m_MovementSpeed);
    }
}

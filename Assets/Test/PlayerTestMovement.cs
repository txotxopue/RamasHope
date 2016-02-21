using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerTestMovement : MonoBehaviour {
    public float m_MovementSpeed;
    public float m_RotationSpeed;
    private Vector3 movement = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    //private Rigidbody m_Rigidbody;

    void Awake()
    {
        Debug.Log(GameInstance.Instance.myGlobalVar);
    }

    // Use this for initialization
    void Start () {
        //m_Rigidbody = GetComponent<Rigidbody>();
	}
	
    void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("Test2");
        }
    }


	// Update is called once per frame
	void FixedUpdate () {
        movement.z = Input.GetAxis("Vertical") * Time.deltaTime * m_MovementSpeed;
        rotation.y = Input.GetAxis("Horizontal") * Time.deltaTime * m_RotationSpeed;
        //m_Rigidbody.velocity = movement;
        transform.Translate(movement);
        transform.Rotate(rotation);
    }

    void OnCollisionEnter(Collision collision)
    {
        print("Collided!!!");
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Wall"))
        {
            print("Wall Triggered!!!");
        }
    }
}

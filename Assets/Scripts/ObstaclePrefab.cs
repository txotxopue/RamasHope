using UnityEngine;
using System.Collections;

public class ObstaclePrefab : MonoBehaviour
{
    public int m_EndRadialOffset = 0;
    public Transform m_Pickups;

	// Use this for initialization
	void Awake ()
    {
        m_Pickups = transform.FindChild("Pickups");
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}


    public void InitObstacle()
    {
        if (m_Pickups == null)
        {
            print("no hay pickups!");
        }
        foreach (Transform child in m_Pickups)
        {
            child.gameObject.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        print("Killzoned!");
        ReturnToPool();
        //Destroy(other.gameObject);
    }


    public void ReturnToPool ()
    {
        gameObject.SetActive(false);
    }

    


}

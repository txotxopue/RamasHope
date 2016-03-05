using UnityEngine;
using System.Collections;

public class EnergyPickup : MonoBehaviour
{
    public int m_energyAmount = 5;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void PickedUp (GameObject pPicker)
    {
        //print("Energy recharged!");
        pPicker.SendMessage("AddEnergy", m_energyAmount, SendMessageOptions.DontRequireReceiver);
        //Destroy(this.gameObject);
        transform.parent.gameObject.SetActive(false);
        //parent.gameObject.SetActive(false);
    }
}

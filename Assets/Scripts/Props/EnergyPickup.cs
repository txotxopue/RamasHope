using UnityEngine;


/// <summary>
/// Controls energy pickups' interactions.
/// If the object is picked up, it adds the corresponding energy amount to the picker.
/// </summary>
public class EnergyPickup : MonoBehaviour
{
    ///<summary>Amount of energy this pickup provides</summary>
    [SerializeField]
    private int _energyAmount = 10;


    /// <summary>
    /// This procedure is called when someone picks up this object.
    /// Then we send the order to add this pickup's energy to the picker.
    /// </summary>
    /// <param name="pPicker">GameObject of who picked this pickup</param>
    public void PickedUp (GameObject pPicker)
    {
        pPicker.SendMessage("AddEnergy", _energyAmount, SendMessageOptions.DontRequireReceiver);
        transform.parent.gameObject.SetActive(false); //we deactivate the pickup (until further reusing of this section)
    }
}

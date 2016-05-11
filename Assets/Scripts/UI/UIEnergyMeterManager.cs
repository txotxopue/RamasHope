using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/// <summary>
/// This class handles the display of the remaining energy in the ship,
/// via a filled image of an energybar.
/// </summary>
public class UIEnergyMeterManager : MonoBehaviour
{
    /// <summary>The energy manager script which we retrieve the energy fill amount from</summary>
    [SerializeField]
    private EnergyManager _playerEnergyManager;

    /// <summary>Energymeter image that displays the remaining energy </summary>
    [SerializeField]
    private Image[] _energyMeter;


	// Update is called once per frame
	void Update ()
    {
        if (_energyMeter.Length != 0)
        {
            for (int i = 0; i < _energyMeter.Length; i++)
            {
                _energyMeter[i].fillAmount = _playerEnergyManager.Energy;
            }
        }
	}
}

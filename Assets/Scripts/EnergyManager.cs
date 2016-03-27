using UnityEngine;

/// <summary>
/// Controls ship's energy levels.
/// The maximum energy the ship can have is maxEnergy.
/// Energy decays at energyLossRate. 
/// When the energy reaches zero, the ship is dead.
/// </summary>
public class EnergyManager : MonoBehaviour
{

    ///<summary>Maximum energy of the ship</summary>
    [Tooltip("Maximum energy of the ship")]
    public float _maxEnergy = 100f;

    ///<summary>Current energy of the ship</summary>
    [Tooltip("Current energy of the ship (not editable)")]
    public float _currentEnergy;

    ///<summary>Energy losing rate in units per second</summary>
    [Tooltip("Energy losing rate in units per second")]
    public float _energyLossRate = 1f;

    ///<summary>If the energy should be decreasing or not.</summary>
    public bool _bEnergyDrain { get; set; }

    ///<summary>Is the ship destroyed?</summary>
    private bool _dead;


	void Start ()
    {
        _dead = false;
        _currentEnergy = _maxEnergy;
        _bEnergyDrain = false;
	}
	
	
	void Update ()
    {
        // Apply energy loss by time
        if (_bEnergyDrain)
        {
            _currentEnergy -= _energyLossRate * Time.deltaTime;
        }

        if (_currentEnergy <= 0 && !_dead)
        {
            // If there's not energy left, this is Game Over
            _dead = true;
            transform.parent.GetComponentInChildren<Camera>().transform.parent = null;
            transform.parent.gameObject.SetActive(false);
            //GameInstance.Instance.GameOver();
        }
	}


    /// <summary>
    /// Adds certain amount of energy to the current energy of the ship.
    /// Energy cannot surpass _maxEnergy.
    /// </summary>
    /// <param name="pEnergyAmount">Amount of energy to add</param>
    public void AddEnergy (int pEnergyAmount)
    {
        _currentEnergy += pEnergyAmount;
        if (_currentEnergy > _maxEnergy)
        {
            _currentEnergy = _maxEnergy;
        }
    }
}

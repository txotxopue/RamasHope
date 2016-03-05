using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Image m_EnergyMeter;
    private EnergyManager m_PlayerEnergyManager;
    private GameObject m_Player;

	// Use this for initialization
	void Start ()
    {
        m_EnergyMeter = GameObject.Find("EnergyMeter").GetComponent<Image>();
        m_Player = GameInstance.Instance.GetPlayer();
        /*
        if (m_Player == null)
        {
            print("no hay jugador");
        }
        */
        m_PlayerEnergyManager = m_Player.GetComponentInChildren<EnergyManager>();
        /*
        if (m_PlayerEnergyManager == null)
        {
            print("no hay EnergyMeter");
        }
        */
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (m_EnergyMeter != null && m_PlayerEnergyManager != null)
        {
            m_EnergyMeter.fillAmount = m_PlayerEnergyManager.m_currentEnergy / m_PlayerEnergyManager.m_maxEnergy;
        }
	}
}

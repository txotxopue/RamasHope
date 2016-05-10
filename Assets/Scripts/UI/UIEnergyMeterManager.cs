using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIEnergyMeterManager : MonoBehaviour
{
    [SerializeField]
    private Image[] m_EnergyMeter;
    private EnergyManager m_PlayerEnergyManager;
    private GameObject m_Player;
    //public GameManager m_GameManager;

	// Use this for initialization
	void Start ()
    {
        //m_EnergyMeter = GameObject.Find("EnergyMeter").GetComponent<Image>();
        //m_Player = GameInstance.Instance.GetPlayer();
        //m_PlayerEnergyManager = m_GameManager.m_PlayerManager.m_Instance.GetComponentInChildren<EnergyManager>();
        /*
        if (m_Player == null)
        {
            print("no hay jugador");
        }
        */
        //m_PlayerEnergyManager = m_Player.GetComponentInChildren<EnergyManager>();
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
        if (m_EnergyMeter.Length != 0)
        {
            m_PlayerEnergyManager = GameInstance.GetPlayer().GetComponentInChildren<EnergyManager>();
            for (int i = 0; i < m_EnergyMeter.Length; i++)
            {
                m_EnergyMeter[i].fillAmount = m_PlayerEnergyManager._currentEnergy / m_PlayerEnergyManager._maxEnergy;
            }
        }
        /*
        m_PlayerEnergyManager = GameInstance.Instance.GetPlayer().GetComponentInChildren<EnergyManager>();
        if (m_EnergyMeter != null && m_PlayerEnergyManager != null)
        {
            m_EnergyMeter.fillAmount = m_PlayerEnergyManager._currentEnergy / m_PlayerEnergyManager._maxEnergy;
        }
        */
	}
}

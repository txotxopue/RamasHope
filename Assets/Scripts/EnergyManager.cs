using UnityEngine;
using System.Collections;

public class EnergyManager : MonoBehaviour
{
    public float m_maxEnergy = 100f;
    public float m_currentEnergy;
    public float m_energyLossRate = 1f;
    private bool m_dead;
	// Use this for initialization
	void Start ()
    {
        m_dead = false;
        m_currentEnergy = m_maxEnergy;
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_currentEnergy -= m_energyLossRate * Time.deltaTime;
        if (m_currentEnergy <= 0 && !m_dead)
        {
            m_dead = true;
            GameInstance.Instance.GameOver();
        }
	}

    public void AddEnergy (int pEnergyAmount)
    {
        m_currentEnergy += pEnergyAmount;
        if (m_currentEnergy > m_maxEnergy)
        {
            m_currentEnergy = m_maxEnergy;
        }
    }
}

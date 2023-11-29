using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingBehaviour : MonoBehaviour 
{
    [Header("Primary stats for leveling system")]
    [SerializeField]
    private int m_Level;
    [SerializeField]
    private int m_LevelMax;
    [SerializeField]
    private int m_Experience;
    [SerializeField]
    private int m_ExpGivenOnDeath;
    [SerializeField]
    private float m_ConstantX; //Default 0.05
    [SerializeField]
    private float m_RatioY; // Default 2
    //Example table with the default values https://docs.google.com/spreadsheets/d/1uFed4cKE1BxxZ19BKuAbbo7Gk6_ezCDmFMV5fwCCxqw/edit#gid=1282610619

    private int m_ExperienceUntilNextLevel;

    //Getters
    public int Level => m_Level;
    public int LevelMax => m_LevelMax;
    public int Experience => m_Experience;
    public int ExpGivenOnDeath => m_ExpGivenOnDeath;

    //Components required to levelup
    private HealthBehaviour m_Health;
    private DamageableBehaviour m_Damage;
    private DefenseBehaviour m_Defense;
    private ManaBehaviour m_Mana;

    // Start is called before the first frame update
    void Start()
    {
        m_Health = GetComponent<HealthBehaviour>();
        m_Damage = GetComponentInChildren<DamageableBehaviour>();
        m_Defense = GetComponent<DefenseBehaviour>();
        m_Mana = GetComponent<ManaBehaviour>();
        //Player experience and level starts fresh. So player experience is 0 and level is 1.
        m_Level = 1;
        m_Experience = 0;
        m_ExperienceUntilNextLevel = CalculateNextLevelExperience();
    }

    //Level up formula, simple one (Level/Constant of XP)^Ratio of XP, where constant determines how much exp you need
    // by affecting the amount required and Ratio affecting how quickly you gain (higher the Y, higher the gaps between levels
    private int CalculateNextLevelExperience()
    {
        int nextLevelRequirement = (int)Mathf.Pow((m_Level / m_ConstantX), m_RatioY);
        return nextLevelRequirement;
    }

    private void CheckLevelUp()
    {
        if(m_Level < m_LevelMax)
        {
            if (m_Experience >= m_ExperienceUntilNextLevel)
            {
                LevelUp();
            }
        }
    }

    private void LevelUp()
    {
        m_Level++;
        m_ExperienceUntilNextLevel = CalculateNextLevelExperience();
        IncreaseStatsOnLevelUp();
    }
    
    public void AddExperience(int experience)
    {
        m_Experience += experience;
        CheckLevelUp();
    }

    public void IncreaseStatsOnLevelUp()
    {
        m_Health.AddMaxHealth(m_Level * 2);
        m_Mana.AddMaxMana(m_Level * 2);
        m_Damage.OnUpdateDamage(m_Level);
        m_Defense.OnAddDefense(m_Level);
    }

    public void SetLevelOnLoad(int level)
    {
        m_Level = 1;
        IncreaseStatsOnLevelUp();
        for(int i = 1; i < level; i++)
        {
            m_Level = i;
            IncreaseStatsOnLevelUp();
        }
    }

    public void OnSetExperienceOnDeath(int experience)
    {
        m_ExpGivenOnDeath = experience;
    }
}

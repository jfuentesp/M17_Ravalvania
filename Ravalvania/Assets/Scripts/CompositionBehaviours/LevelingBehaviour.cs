using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingBehaviour : MonoBehaviour 
{
    [SerializeField]
    GameEvent m_OnGUIEvent;


    [Header("Primary stats for leveling system")]
    [SerializeField]
    private int m_Level = 1;
    [SerializeField]
    private int m_LevelMax;
    [SerializeField]
    private int m_Experience;
    [SerializeField]
    private int m_ExpGivenOnDeath;
    [SerializeField]
    private float m_ConstantX = 0.05f; //Default 0.05
    [SerializeField]
    private float m_RatioY = 2; // Default 2
    //Example table with the default values https://docs.google.com/spreadsheets/d/1uFed4cKE1BxxZ19BKuAbbo7Gk6_ezCDmFMV5fwCCxqw/edit#gid=1282610619

    private int m_ExperienceUntilNextLevel;

    //Getters
    public int Level => m_Level;
    public int LevelMax => m_LevelMax;
    public int Experience => m_Experience;
    public int ExpGivenOnDeath => m_ExpGivenOnDeath;

    public int ExperienceUntilNextLevel => m_ExperienceUntilNextLevel;

    //Components required to levelup
    private HealthBehaviour m_Health;
    private DamageableBehaviour m_Damage;
    private DefenseBehaviour m_Defense;
    private ManaBehaviour m_Mana;

    private void Awake()
    {
        m_Level = 1;
        m_ConstantX = 0.05f;
        m_RatioY = 2;
        m_Health = GetComponent<HealthBehaviour>();
        m_Damage = GetComponentInChildren<DamageableBehaviour>();
        m_Defense = GetComponent<DefenseBehaviour>();
        m_Mana = GetComponent<ManaBehaviour>();
        //Player experience and level starts fresh. So player experience is 0 and level is 1.
        m_ExperienceUntilNextLevel = CalculateNextLevelExperience();
    }

    //Level up formula, simple one (Level/Constant of XP)^Ratio of XP, where constant determines how much exp you need
    // by affecting the amount required and Ratio affecting how quickly you gain (higher the Y, higher the gaps between levels
    private int CalculateNextLevelExperience()
    {
        float expRequiredCalculation = Mathf.Pow(((float)m_Level / m_ConstantX), m_RatioY);
        int nextLevelRequirement = (int) expRequiredCalculation;
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
        m_Experience = 0;
        IncreaseStatsOnLevelUp();
        Debug.Log("Entro en el levelup y en teoria estoy haciendo levelup");
    }
    
    public void AddExperience(int experience)
    {
        m_Experience += experience;
        CheckLevelUp();
        m_OnGUIEvent.Raise();
    }

    public void IncreaseStatsOnLevelUp()
    {
        m_Health.AddMaxHealth(m_Level * 2);
        m_Mana.AddMaxMana(m_Level * 2);
        m_Damage.OnAddDamage(m_Level);
        m_Defense.OnAddDefense(m_Level);
        Debug.Log("En teoria estoy haciendo subida de estadisticas.");
    }

    public void SetLevelOnLoad(int level)
    {
        for(int i = m_Level; i < level; i++)
        {
            Debug.Log("Entro en el level on load");
            LevelUp();
        }
        Debug.Log(string.Format("Level: {0} | ExpToNextLevel: {1} | Attack: {2} | Def: {3}", m_Level, m_ExperienceUntilNextLevel, m_Damage.AttackDamage, m_Defense.Defense));
    }

    public void OnSetExperienceOnDeath(int experience)
    {
        m_ExpGivenOnDeath = experience;
    }
}

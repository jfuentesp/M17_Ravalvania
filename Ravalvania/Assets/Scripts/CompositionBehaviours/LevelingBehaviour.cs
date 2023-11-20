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

    // Start is called before the first frame update
    void Start()
    {
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
    }
    
    public void AddExperience(int experience)
    {
        m_Experience += experience;
        CheckLevelUp();
    }
}

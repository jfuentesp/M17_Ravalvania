using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionBehaviour : MonoBehaviour
{
    private LevelManager m_LevelManager;

    [Header("Mission parameters")]
    [SerializeField]
    private EMission m_MissionType;
    [SerializeField]
    private int m_ValueRequired;
    [SerializeField]
    private string m_Tooltip;
    [SerializeField]
    private int m_CurrentValue;
    [SerializeField]
    private bool m_IsMissionCompleted;
    [SerializeField]
    private int m_ObjectiveType;
    [SerializeField]
    private string m_ObjectiveName;
    [SerializeField]
    private int m_CoinReward;
    [SerializeField]
    private int m_ExpReward;

    [SerializeField]
    private List<EnemyScriptableObject> m_Enemies;
    [SerializeField]
    private List<PickupScriptableObject> m_Pickups;

    public EMission MissionType => m_MissionType;
    public int ValueRequired => m_ValueRequired; 
    public string Tooltip => m_Tooltip;
    public int CurrentValue => m_CurrentValue;
    public bool IsMissionCompleted => m_IsMissionCompleted;
    public int ObjectiveType => m_ObjectiveType;
    public int CoinReward => m_CoinReward;
    public int ExpReward => m_ExpReward;

    private EconomyBehaviour m_Player1Economy;
    private LevelingBehaviour m_Player1Leveling;
    private EconomyBehaviour m_Player2Economy;
    private LevelingBehaviour m_Player2Leveling;

    private void Start()
    {
        m_LevelManager = LevelManager.LevelManagerInstance;
        m_Player1Economy = m_LevelManager.Player1.GetComponent<EconomyBehaviour>();
        m_Player1Leveling = m_LevelManager.Player1.GetComponent<LevelingBehaviour>();
        m_Player2Economy = m_LevelManager.Player2.GetComponent<EconomyBehaviour>();
        m_Player2Leveling = m_LevelManager.Player2.GetComponent<LevelingBehaviour>();
        m_MissionType = EMission.NONE;
    }

    public void OnSetMission()
    {
        //Sets a new random mission. 0 = KILL, 1 = PICK, 2 = JUMP, 3 = HIT, 4 = SHOOT
        m_IsMissionCompleted = false;
        int randomize = Random.Range(0, 5);
        m_CurrentValue = 0;
        m_CoinReward = 200;
        m_ExpReward = 300;
        switch (randomize)
        {
            case 0:
                m_MissionType = EMission.KILL;
                m_ValueRequired = 10;
                EnemyScriptableObject enemyrequirement = m_Enemies[Random.Range(0, m_Enemies.Count)];
                m_ObjectiveName = enemyrequirement.EnemyName;
                m_ObjectiveType = enemyrequirement.EnemyType;
                m_Tooltip = string.Format("Mission: Kill {0} {1}!", m_ValueRequired-m_CurrentValue, m_ObjectiveName);
                break;
            case 1:
                m_MissionType = EMission.PICK;
                m_ValueRequired = 10;
                PickupScriptableObject pickuprequirement = m_Pickups[Random.Range(0, m_Enemies.Count)];
                m_ObjectiveName = pickuprequirement.PickupName;
                m_ObjectiveType = pickuprequirement.PickupID;
                m_Tooltip = string.Format("Mission: Kill {0} {1}!", m_ValueRequired - m_CurrentValue, m_ObjectiveName);
                break;
            case 2:
                m_MissionType = EMission.JUMP;
                m_ValueRequired = 50;
                m_Tooltip = string.Format("Mission: Jump {0} times!", m_ValueRequired-m_CurrentValue);
                break;
            case 4:
                m_MissionType = EMission.HIT;
                m_ValueRequired = 35;
                m_Tooltip = string.Format("Mission: Hit enemies {0} times!", m_ValueRequired-m_CurrentValue);
                break;
            default:
                m_MissionType = EMission.SHOOT;
                m_ValueRequired = 25;
                m_Tooltip = string.Format("Mission: Shoot {0} bullets!", m_ValueRequired - m_CurrentValue);
                break;
        }
        Debug.Log(m_Tooltip);
    }

    public void OnObjectiveCountdown()
    {
        if (!m_IsMissionCompleted)
        {
            m_CurrentValue++;
            if (m_CurrentValue >= m_ValueRequired)
            {
                m_IsMissionCompleted = true;
                m_Tooltip = "Mission accomplished! Collect your rewards at the Safehouse";
            }
        }
    }

    public void OnGiveRewards()
    {
        m_Player1Leveling.AddExperience(m_ExpReward);
        m_Player2Leveling.AddExperience(m_ExpReward);
        m_Player1Economy.ChangeCoins(m_CoinReward);
        m_Player2Economy.ChangeCoins(m_CoinReward);
        m_MissionType = EMission.NONE;
    }
}

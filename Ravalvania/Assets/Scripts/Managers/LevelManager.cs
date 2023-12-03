using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MissionBehaviour))]
[RequireComponent(typeof(EconomyBehaviour))]
public class LevelManager : MonoBehaviour
{
    //Instance of the LevelManager. Refers to this own gameobject. It needs to be an instance if the prefabs should refer to this object. (As enemies, for example)
    private static LevelManager m_Instance;
    public static LevelManager LevelManagerInstance => m_Instance; //A getter for the instance of the manager. Similar to get { return m_Instance }. (Accessor)
   
    [SerializeField, Header("First spawn point")]
    private Transform spawnPoint;
    public Vector3 getSpawnPoint => spawnPoint.transform.position;

    [Header("Reference to the players")]
    [SerializeField]
    private PlayerBehaviour m_Player1;
    [SerializeField] 
    private PlayerBehaviour m_Player2;
    //Getters for player references
    public PlayerBehaviour Player1 => m_Player1;
    public PlayerBehaviour Player2 => m_Player2;

    private MissionBehaviour m_Mission;
    private EconomyBehaviour m_Money;

    public MissionBehaviour Mission => m_Mission;
    public EconomyBehaviour Money => m_Money;

    [Header("Reference to the Scene Doors")]
    [SerializeField]
    private List<DoorBehaviour> m_LevelDoors;
    public List<DoorBehaviour> LevelDoors => m_LevelDoors;

    private GameManager m_GameManager;

    private void Awake()
    {
        //First, we initialize an instance of Player. If there is already an instance, it destroys the element and returns.
        if (m_Instance == null)
        {
            m_Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        m_Mission = GetComponent<MissionBehaviour>();
        m_Money = GetComponent<EconomyBehaviour>();
    }

    private void Start()
    {
        m_GameManager = GameManager.GameManagerInstance;
    }
    
    public SaveData.GameData SaveGameData()
    {
        return new SaveData.GameData(m_Money.PlayerCoins, SceneManager.GetActiveScene().name, m_GameManager.DestinationDoor);
    }

    public void LoadGameData(SaveData.GameData _gameData)
    {
        m_GameManager.SetDestinationDoor(_gameData.doorDestination);
        m_Money.ChangeCoins(_gameData.money);
    }

    public void LoadDataPersistanceOnChangeScene(SaveData persistanceData)
    {
        m_Money.ChangeCoins(persistanceData.gameData.money);
        m_Player1.Load(persistanceData.player1, false);
        m_Player2.Load(persistanceData.player2, false);
        m_Mission.LoadMission(persistanceData.mission);
    }
}

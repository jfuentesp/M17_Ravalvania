using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;


[RequireComponent(typeof(SaveDataManager))]
public class GameManager : MonoBehaviour
{
    //Instance of the GameManager. Refers to this own gameobject.
    private static GameManager m_Instance;
    public static GameManager GameManagerInstance => m_Instance; //A getter for the instance of the game manager. Similar to get { return m_Instance }. (Accessor)

    private const string m_MainTitleScene = "MainTitleScene";
    private const string m_SafeHouseScene = "House";
    private const string m_CaveScene = "Cave";
    private const string m_WoodScene = "Forest";

    private EDoor m_DestinationDoor;
    public EDoor DestinationDoor => m_DestinationDoor;

    private LevelManager m_Levelmanager;
        

    [Header("GameEvents for the Game Mechanics")]
    [SerializeField]
    GameEvent m_OnNextWave;
    //[SerializeField]
    //GameEventVoid m_OnPlayerRespawn;
    [SerializeField]
    GameEvent m_OnGUIUpdate;

    //Player spawnpoints
    Vector3 m_Player1InitialSpawnPoint;
    Vector3 m_Player1SpawnPoint;
    Vector3 m_Player2InitialSpawnPoint;
    Vector3 m_Player2SpawnPoint;

    PlayerBehaviour m_Player1;
    PlayerBehaviour m_Player2;
    MissionBehaviour m_CurrentMission;
    int m_CurrentMoney;

    bool m_IsPositionSetAfterLoadingSaveGame;

    SaveDataManager m_SaveDataManager;
    SaveData m_LastDataPersistance;

    private void Awake()
    {
        //First, we initialize an instance of GameManager. If there is already an instance, it destroys the element and returns.
        if (m_Instance == null)
        {
            m_Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        m_DestinationDoor = EDoor.NONE; 
        SceneManager.sceneLoaded += OnSceneLoaded; //Everytime a new scene finishes at loading we execute this function by subscribing to the event
        m_SaveDataManager = GetComponent<SaveDataManager>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Levelmanager = LevelManager.LevelManagerInstance;
        //Gets the initial references, so on player death, it will inform this vector to LevelManager to spawn both players
        m_Player1InitialSpawnPoint = m_Levelmanager.Player1.transform.position;
        m_Player2InitialSpawnPoint = m_Levelmanager.Player2.transform.position;
        m_Player1SpawnPoint = m_Player1InitialSpawnPoint;
        m_Player2SpawnPoint = m_Player2InitialSpawnPoint;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            m_SaveDataManager.SaveData();

        if (Input.GetKeyDown(KeyCode.H))
            m_SaveDataManager.LoadData();
    }


    //Substracts all the money earned and respawns both players in the respawn
    public void OnPlayerDeath()
    {
        
    }

    public void ChangeScene(EDoor doorDestination)
    {
        KeepDataOnSceneChange();
        m_DestinationDoor = doorDestination;
        switch (m_DestinationDoor)
        {
            case EDoor.SAFEHOUSE:
                SceneManager.LoadScene(m_SafeHouseScene);
                break;
            case EDoor.CAVE:
                SceneManager.LoadScene(m_CaveScene);
                break;
            case EDoor.WOODHOUSE:
                SceneManager.LoadScene(m_WoodScene);
                break;
            case EDoor.WOODCAVE:
                SceneManager.LoadScene(m_WoodScene);
                break;
            default:
                SceneManager.LoadScene(m_WoodScene);
                break;
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode loadmode)
    {
        m_Levelmanager = LevelManager.LevelManagerInstance;
        DoorBehaviour destination = FindDestinationDoorOnLoad(m_DestinationDoor);
        if(m_LastDataPersistance != null)
            m_Levelmanager.LoadDataPersistanceOnChangeScene(m_LastDataPersistance);
        if(m_IsPositionSetAfterLoadingSaveGame)
        {
            m_IsPositionSetAfterLoadingSaveGame = false;
            return;
        }
        if(destination != null)
        {
            SetPlayersPosition(destination.transform.position);
        } else
        {
            SetPlayersPosition(m_Player1SpawnPoint);
        }
    }

    public void SetPlayersPosition(Vector3 position)
    {
        m_Levelmanager.Player1.transform.position = position;
        m_Levelmanager.Player2.transform.position = position + Vector3.right;
    }

    public void SetDestinationDoor(EDoor door)
    {
        m_DestinationDoor = door;
    }

    public void SetLoadBoolean(bool value)
    {
        m_IsPositionSetAfterLoadingSaveGame = value;
    }

    public void KeepDataOnSceneChange()
    {
        m_LastDataPersistance = m_SaveDataManager.DataPersistanceOnChangeScene();
    }

    //Finds the object reference in the scene to get the destination position
    public DoorBehaviour FindDestinationDoorOnLoad(EDoor doorDestination)
    {
        DoorBehaviour destination = null;
        switch (doorDestination)
        {
            case EDoor.SAFEHOUSE:
                destination = LevelManager.LevelManagerInstance.LevelDoors.FirstOrDefault(Door => Door.DoorName.Equals("SAFEHOUSE"));
                break;
            case EDoor.CAVE:
                destination = LevelManager.LevelManagerInstance.LevelDoors.FirstOrDefault(Door => Door.DoorName.Equals("CAVE"));
                break;
            case EDoor.WOODHOUSE:
                destination = LevelManager.LevelManagerInstance.LevelDoors.FirstOrDefault(Door => Door.DoorName.Equals("WOODHOUSE"));
                break;
            case EDoor.WOODCAVE:
                destination = LevelManager.LevelManagerInstance.LevelDoors.FirstOrDefault(Door => Door.DoorName.Equals("WOODCAVE"));
                break;
        }
        return destination;
    }
}


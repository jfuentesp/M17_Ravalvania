using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MissionBehaviour))]
[RequireComponent(typeof(EconomyBehaviour))]
public class LevelManager : MonoBehaviour
{
    //Instance of the LevelManager. Refers to this own gameobject. It needs to be an instance if the prefabs should refer to this object. (As enemies, for example)
    private static LevelManager m_Instance;
    public static LevelManager LevelManagerInstance => m_Instance; //A getter for the instance of the manager. Similar to get { return m_Instance }. (Accessor)

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
    }

    private void Start()
    {
        m_Mission = GetComponent<MissionBehaviour>();
        m_Money = GetComponent<EconomyBehaviour>();
    }

    public void OnLoadInfoNewScene(PlayerBehaviour p1, PlayerBehaviour p2, MissionBehaviour mission, int money)
    {

    }

    public void GetSaveData()
    {

    }
}

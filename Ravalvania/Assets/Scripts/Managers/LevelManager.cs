using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            
    }

    public void OnSavePlayerInfoBetweenScenes()
    {

    }

    public void OnLoadPlayerInfoBetweenScenes()
    {

    }
}

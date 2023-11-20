using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;


public class GameManager : MonoBehaviour
{
    //Instance of the GameManager. Refers to this own gameobject.
    private static GameManager m_Instance;
    public static GameManager GameManagerInstance => m_Instance; //A getter for the instance of the game manager. Similar to get { return m_Instance }. (Accessor)

    private const string m_MainTitleScene = "MainTitleScene";
    private const string m_GameScene = "GameScene";
    private const string m_GameOverScene = "GameOverScene";

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

    SpawnerBehaviour m_Spawner;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        m_Spawner = SpawnerBehaviour.SpawnerInstance;
        m_Levelmanager = LevelManager.LevelManagerInstance;
        //Gets the initial references, so on player death, it will inform this vector to LevelManager to spawn both players
        m_Player1InitialSpawnPoint = m_Levelmanager.Player1.transform.position;
        m_Player2InitialSpawnPoint = m_Levelmanager.Player2.transform.position;
    }

    //Substracts a Live and checks if the lives are more than 0. If not, loads the GameOver scene.
    public void OnPlayerDeath()
    {
            
    }

    public void OnSceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    /*private IEnumerator PlayerDeathCoroutine()
    {
        yield return new WaitForSeconds(2f);
        m_Player.transform.position = m_PlayerSpawnPoint;
        m_Player.gameObject.SetActive(true);
    }*/
}


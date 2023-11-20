using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class SpawnerBehaviour : MonoBehaviour
{
    //Instance of the Spawner. Refers to this own gameobject.
    private static SpawnerBehaviour m_Instance;
    public static SpawnerBehaviour SpawnerInstance => m_Instance; //A getter for the instance of the spawner. Similar to get { return m_Instance }. (Accessor)

    //Spawner will randomly select an enemy and spawn one of them for every spawnpoint in the list.

    [Header("Spawner settings")]
    [SerializeField]
    List<Transform> m_SpawnpointList;

    [Header("Pool references")]
    [SerializeField]
    private Pool m_RobberPool;
    [SerializeField]
    private Pool m_RangedPool;
    [SerializeField]
    private Pool m_ThiefPool;
    [SerializeField]
    private Pool m_GangPool;

    private void Awake()
    {
        //First, we initialize an instance of Spawner. If there is already an instance, it destroys the element and returns.
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

    // Start is called before the first frame update
    void Start()
    {
        OnSpawnEnemies();       
    }


    public void OnSpawnEnemies()
    {
        foreach(Transform spawnpoint in m_SpawnpointList)
        {
            SpawnEnemy(spawnpoint.position);
        }
    }

    private void SpawnEnemy(Vector2 spawnpoint)
    {
        GameObject enemy;
        int randomEnemy = Random.Range(0, 5);
        //A random enemy will spawn in a defined location due to the spawnpoint given
        switch (randomEnemy)
        {
            case 0:
                Pool RobberPool = m_RobberPool.GetComponent<Pool>();
                enemy = RobberPool.GetComponent<Pool>().GetElement();              
                enemy.transform.position = spawnpoint;
                break;
            case 1:
                Pool RangedPool = m_RangedPool.GetComponent<Pool>();
                enemy = RangedPool.GetComponent<Pool>().GetElement();
                enemy.transform.position = spawnpoint;
                break;
            case 2:
                Pool ThiefPool = m_ThiefPool.GetComponent<Pool>();
                enemy = ThiefPool.GetComponent<Pool>().GetElement();
                enemy.transform.position = spawnpoint;
                break;
            default:
                Pool GangPool = m_GangPool.GetComponent<Pool>();
                enemy = GangPool.GetComponent<Pool>().GetElement();
                enemy.transform.position = spawnpoint;
                break;
        }      
    }
}


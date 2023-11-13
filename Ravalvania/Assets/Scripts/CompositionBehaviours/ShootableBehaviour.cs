using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableBehaviour : MonoBehaviour
{
    [Header("Prefab of the bullet to instantiate")]
    [SerializeField]
    GameObject m_BulletPrefab;

    // Function that shoots a bullet
    public void Shoot(Vector2 spawnPosition)
    {
        GameObject prefab = Instantiate(m_BulletPrefab);
        prefab.transform.position = spawnPosition;
        prefab.GetComponent<BulletBehaviour>().InitBullet(transform.position);
    }
}

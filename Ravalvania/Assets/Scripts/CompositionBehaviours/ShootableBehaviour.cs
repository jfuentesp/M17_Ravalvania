using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableBehaviour : MonoBehaviour
{
    [Header("Prefab of the bullet to instantiate")]
    [SerializeField]
    private GameObject m_BulletPrefab;
    private MovableBehaviour m_Moving;

    private void Awake()
    {
        m_Moving = GetComponent<MovableBehaviour>();
    }

    // Function that shoots a bullet
    public void Shoot()
    {
        Vector2 direction = m_Moving.IsFlipped ? -Vector2.right : Vector2.right;
        GameObject prefab = Instantiate(m_BulletPrefab);
        prefab.transform.position = transform.position;
        prefab.GetComponent<BulletBehaviour>().InitBullet(direction);
    }
}

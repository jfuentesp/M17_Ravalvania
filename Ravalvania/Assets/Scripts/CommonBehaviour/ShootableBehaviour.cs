using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class sets the capability to shoot another objects to a target.
/// </summary>
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

    /// <summary>
    /// Function that shoots a bullet
    /// </summary>
    public void Shoot()
    {
        Vector2 direction = m_Moving.IsFlipped ? -Vector2.right : Vector2.right;
        GameObject prefab = Instantiate(m_BulletPrefab);
        prefab.transform.position = transform.position;
        prefab.GetComponent<BulletBehaviour>().InitBullet(direction);
    }

    /// <summary>
    /// Function that shoots a projectile to a target setting a parabolic movement.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="maxheight"></param>
    /// <param name="bulletspeed"></param>
    public void ParabolicShoot(GameObject target, float maxheight, float bulletspeed)
    {
        GameObject prefab = Instantiate(m_BulletPrefab);
        prefab.transform.position = transform.position;
        prefab.GetComponent<ParabolaBehaviour>().ShootingParabola(target, gameObject, maxheight, bulletspeed);
    }
}

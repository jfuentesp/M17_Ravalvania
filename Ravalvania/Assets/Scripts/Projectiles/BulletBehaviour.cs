using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamageableBehaviour))]
[RequireComponent(typeof(Rigidbody2D))]
public class BulletBehaviour : MonoBehaviour
{
    DamageableBehaviour m_Damageable;
    Rigidbody2D m_Rigidbody;

    [Header("Bullet parameters")]
    [SerializeField]
    private float m_BulletSpeed;
    [SerializeField]
    private float m_BulletLifeTime;
    [SerializeField]
    private bool m_IsDestroyedOnTime; // If the element gets destroyed or disabled on time
    private Transform m_SpawnPosition;

    private void Awake()
    {
        m_Damageable = GetComponent<DamageableBehaviour>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    // This function initializes the bullet and sets the parameters
    public void InitBullet(Vector2 direction)
    {
        m_Rigidbody.velocity = direction * m_BulletSpeed;
        transform.eulerAngles = direction.x < 0 ? new Vector3(0, 180, 0) : Vector3.zero;
        StartCoroutine(BulletAlive());
    }
    // Coroutine for making the bullet to destroy or disable on time
    private IEnumerator BulletAlive()
    {
        yield return new WaitForSeconds(m_BulletLifeTime);
        if (m_IsDestroyedOnTime)
        {
            Destroy(this.gameObject);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}

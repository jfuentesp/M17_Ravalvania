using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaBehaviour : MonoBehaviour
{
    [SerializeField]
    GameObject m_Target;
    [SerializeField]
    float m_MaxHeight;
    [SerializeField]
    float m_Time;

    private Rigidbody2D m_Rigidbody;

    [SerializeField]
    private GameObject m_ExplosionPrefab;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Scenario") || collision.gameObject.CompareTag("Player"))
        {
            Explosion();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitbox"))
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void ShootingParabola(GameObject target, GameObject shooter, float maxheight, float ballSpeed)
    {
        float targetX = target.transform.position.x;
        float shooterX = shooter.transform.position.x;
        float targetY = target.transform.position.y;
        float shooterY = shooter.transform.position.y + maxheight;


        float baseX = (targetX - shooterX) / ballSpeed; //X Direction. Our starting point minus our destination. All relative to the time we do expect to get the destination.
        float baseY = (targetY - shooterY + ((float) 4.9 * (ballSpeed * ballSpeed))) / ballSpeed;//Y dimension, it has gravity related so it has a deceleration and acceleration involved

        Debug.Log(baseX + " , " + baseY);

        m_Rigidbody.AddForce(new Vector2(baseX, baseY) * m_Rigidbody.mass, ForceMode2D.Impulse);
    }

    public void Explosion()
    {
        GameObject explosion = Instantiate(m_ExplosionPrefab);
        explosion.transform.position = transform.position;
        Destroy(this.gameObject);
    }
}

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

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ShootingParabola(m_Target, this.gameObject, m_MaxHeight, m_Time);
    }

    public void ShootingParabola(GameObject target, GameObject shooter, float maxheight, float time)
    {
        float targetX = target.transform.position.x;
        float shooterX = shooter.transform.position.x;
        float targetY = target.transform.position.y;
        float shooterY = shooter.transform.position.y;


        float baseX = (targetX - shooterX) / time; //X Direction. Our starting point minus our destination. All relative to the time we do expect to get the destination.
        float baseY = (float)(targetY - shooterY - 0.5 * 9.8 * time * time) / time;//Y dimension, it has gravity related so it has a deceleration and acceleration involved

        Debug.Log(baseX + " , " + baseY);

        m_Rigidbody.AddForce(new Vector2(baseX, baseY) * m_Rigidbody.mass, ForceMode2D.Impulse);
    }
}

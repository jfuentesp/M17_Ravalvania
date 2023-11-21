using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TirParabolic : MonoBehaviour
{
    [SerializeField]
    private Transform Llan�at;
    [SerializeField]
    private Transform Arribar;

    private Rigidbody2D Rigidbody;

    private float Altura = 5f;
    private float speed = 3f;
    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Llan�at.position = Input.mousePosition;
            
        } else if(Input.GetMouseButtonDown(1))
        {
            Arribar.position = Input.mousePosition;
        }
        else if (Input.GetKey(KeyCode.E)){
            GoParabolic();
        }
        
            
        
    }

    private void GoParabolic()
    {
        Altura += Arribar.position.y;
        Vector2 dist = Arribar.position - Llan�at.position;
        float nextX = Mathf.MoveTowards(transform.position.x, Arribar.position.x, speed * Time.deltaTime);
        float baseY = Mathf.Lerp(Llan�at.position.y, Arribar.position.y, (nextX - Llan�at.position.x) / dist.x);
        //float height = 2 * (nextX - Llan�at.position.x) * (nextX - Arribar.position.x) / (-0.25f * (dist.x * dist.x));
        Rigidbody.velocity = new Vector2(nextX, baseY+Altura);
    }
    private void FixedUpdate()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    Transform p1;
    Transform p2;
    void Start()
    {
        p1 = LevelManager.LevelManagerInstance.Player1.transform;
        p2 = LevelManager.LevelManagerInstance.Player2.transform;
    }

    // Update is called once per frame
    void Update()
    {


        float x = ((p1.position.x - p2.position.x) / 2) + p2.position.x;
        
        float y = ((p1.position.y - p2.position.y) / 2) + p2.position.y;
   
        transform.position = new Vector3(x,y,-10);
    }
}
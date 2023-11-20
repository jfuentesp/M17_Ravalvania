using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopping : MonoBehaviour
{
    public Action OnEntrar;
    public Action OnSortir;
    [SerializeField]
    private GameObject GUIShop;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        OnEntrar?.Invoke();
        Debug.Log("He entrado trigger");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        OnSortir?.Invoke();
    }
    public void EnableGUI()
    {
        if (GUIShop != null) {GUIShop.SetActive(true);} 
    }

    public void DisableGUI()
    {
        if (GUIShop != null) { GUIShop.SetActive(false); }
    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingBehaviour : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject P1Shop;
    [SerializeField]
    private GameObject P2Shop;

    public void interact(EPlayer player)
    {
        if(player == EPlayer.PLAYER1) 
        {
            if (!P1Shop.activeInHierarchy)
            {
                P1Shop?.SetActive(true);
                return;
            }
            P1Shop?.SetActive(false);          
        }     
        if(player == EPlayer.PLAYER2)
        {
            if (!P2Shop.activeInHierarchy)
            {
                P2Shop?.SetActive(true);
                return;
            }
            P2Shop?.SetActive(false);
        }
    }
}

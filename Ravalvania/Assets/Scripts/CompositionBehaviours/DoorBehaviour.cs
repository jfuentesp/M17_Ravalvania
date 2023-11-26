using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour, IInteractable
{
    [SerializeField]
    private EDoor m_DestinationDoor;
    [SerializeField]
    private string m_DoorName; //Should be the same name than the related EDoor e.g => EDoor.CAVE = "CAVE";

    public EDoor DestinationDoor => m_DestinationDoor;
    public string DoorName => m_DoorName;

    public void interact()
    {
        GameManager.GameManagerInstance.ChangeScene(m_DestinationDoor);
    }
}

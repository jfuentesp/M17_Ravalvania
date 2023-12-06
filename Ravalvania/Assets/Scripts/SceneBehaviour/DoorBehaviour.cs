using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour, IInteractable
{
    [SerializeField]
    private EDoor m_DestinationDoor; //Should be the name of the destination EDoor e.g => DestinationDoor = EDoor.CAVE 
    [SerializeField]
    private string m_DoorName; //Should be the name of the actual EDoor e.g => DoorName = "WOODCAVE" As it's the name of the gate where you appeared.

    public EDoor DestinationDoor => m_DestinationDoor;
    public string DoorName => m_DoorName;

    public void interact(EPlayer player)
    {
        GameManager.GameManagerInstance.ChangeScene(m_DestinationDoor);
    }
}

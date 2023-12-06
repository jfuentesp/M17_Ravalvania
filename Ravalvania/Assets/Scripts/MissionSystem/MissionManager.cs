using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour, IInteractable
{
    private MissionBehaviour m_CurrentMission;
    [SerializeField]
    private GameEvent m_OnGUIUpdate;

    //Getting the Mission component
    private void Start()
    {
        m_CurrentMission = LevelManager.LevelManagerInstance.GetComponent<MissionBehaviour>();
    }

    //Interact checks if there is a mission set. If it is, it check if its completed to give rewards. If not, it will set a new mission.
    public void interact(EPlayer player)
    {
        if(m_CurrentMission.MissionType == EMission.NONE)
        {
            m_CurrentMission.OnSetMission();
            m_OnGUIUpdate.Raise();
            return;
        }
        Debug.Log(m_CurrentMission.Tooltip);
        if (m_CurrentMission.IsMissionCompleted)
        {
            m_CurrentMission.OnGiveRewards();
            m_OnGUIUpdate.Raise();
        }
    }
}

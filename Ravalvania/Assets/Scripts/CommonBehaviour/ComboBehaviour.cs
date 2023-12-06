using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboBehaviour : MonoBehaviour
{
    private bool m_ComboAvailable;
    public bool ComboAvailable => m_ComboAvailable;

    //Combo implementates with a boolean and few functions
    //This public functions can be triggered from the clip events to trigger the begin and end of the combo frame and the end of the hit animation
    public void InitComboWindow()
    {
        m_ComboAvailable = true;
    }

    public void EndComboWindow()
    {
        m_ComboAvailable = false;
    }
}

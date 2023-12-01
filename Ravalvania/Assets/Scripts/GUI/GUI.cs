using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    //inside of GUI.
    [SerializeField]
    private TextMeshProUGUI m_QuestText;
    [SerializeField]
    private TextMeshProUGUI m_Coins;

    //P1
    [SerializeField, Header("Player 1")]
    private Image m_P1_HP;
    [SerializeField]
    private Image m_P1_Mana;
    [SerializeField]
    private Image m_P1_Exp;
    [SerializeField]
    private TextMeshProUGUI m_P1_Lvl;

    //P1
    [SerializeField, Header("Player 2")]
    private Image m_P2_HP;
    [SerializeField]
    private Image m_P2_Mana;
    [SerializeField]
    private Image m_P2_Exp;
    [SerializeField]
    private TextMeshProUGUI m_P2_Lvl;

    //Outside of GUI variables.
    HealthBehaviour m_HeatlhP1;
    HealthBehaviour m_HeatlhP2;
    ManaBehaviour m_ManaP1;
    ManaBehaviour m_ManaP2;
    LevelingBehaviour m_lvlP1;
    LevelingBehaviour m_lvlP2;
    int m_CurrentCoins;
    string m_MissionText;

    private void Start()
    {
        m_HeatlhP1 = LevelManager.LevelManagerInstance.Player1.GetComponentInChildren<HealthBehaviour>();
        m_HeatlhP2 = LevelManager.LevelManagerInstance.Player2.GetComponentInChildren<HealthBehaviour>();
        m_ManaP1 = LevelManager.LevelManagerInstance.Player1.GetComponent<ManaBehaviour>();
        m_ManaP2 = LevelManager.LevelManagerInstance.Player2.GetComponent<ManaBehaviour>();
        m_lvlP1 = LevelManager.LevelManagerInstance.Player1.GetComponent<LevelingBehaviour>();
        m_lvlP2 = LevelManager.LevelManagerInstance.Player2.GetComponent<LevelingBehaviour>();
        m_CurrentCoins = LevelManager.LevelManagerInstance.GetComponent<EconomyBehaviour>().PlayerCoins;
        m_MissionText = LevelManager.LevelManagerInstance.GetComponent<MissionBehaviour>().Tooltip;
    }

    public void updateUI()
    {
        Debug.Log(m_CurrentCoins);
        m_Coins.text = m_CurrentCoins.ToString();
        m_QuestText.text = m_MissionText;

        m_P1_HP.fillAmount = (100 * m_HeatlhP1.CurrentHealth / m_HeatlhP1.MaxHealth) * 0.01f;
        m_P1_Mana.fillAmount = (100 * m_ManaP1.CurrentMana / m_ManaP1.MaxMana) * 0.01f;
        m_P1_Exp.fillAmount = (100 * m_lvlP1.Experience / m_lvlP1.ExperienceUntilNextLevel) * 0.01f;

        m_P2_HP.fillAmount = (100 * m_HeatlhP2.CurrentHealth / m_HeatlhP2.MaxHealth) * 0.01f;
        m_P2_Mana.fillAmount = (100 * m_ManaP2.CurrentMana / m_ManaP2.MaxMana) * 0.01f;
        m_P2_Exp.fillAmount = (100 * m_lvlP2.Experience / m_lvlP2.ExperienceUntilNextLevel) * 0.01f;

        m_P1_Lvl.text = m_lvlP1.Level.ToString();
        m_P2_Lvl.text = m_lvlP2.Level.ToString();
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.M)) {
            m_lvlP2.AddExperience(2);
        }
    }
}
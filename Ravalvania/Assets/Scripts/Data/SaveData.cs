using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// A class that stores all the data objects and structs to save the game progress.
/// </summary>
[Serializable]
public class SaveData
{

    [Serializable]
    public struct PlayerData
    {
        public int level;
        public int experience;
        public float hp;
        public float mana;
        public Vector3 position;
        public Weapon weapon;
        public Armor armor;
        public Orb orb;

        public PlayerData(int PlayerLevel, int PlayerExperience, float PlayerHP, float PlayerMana, Vector3 Playerposition,
            Weapon EquippedWeapon, Armor EquippedArmor, Orb EquippedOrb)
        {
            level = PlayerLevel;
            experience = PlayerExperience;
            hp = PlayerHP;
            mana = PlayerMana;
            position = Playerposition;
            weapon = EquippedWeapon;
            armor = EquippedArmor;
            orb = EquippedOrb;
        }
    }
    [Serializable]
    public struct MissionData
    {
        public EMission missionType;
        public int valuerequired;
        public string tooltip;
        public int currentvalue;
        public bool isMissionCompleted;
        public int objectiveType;
        public int coinReward;
        public int expReward;

        public MissionData(EMission MissionType, int ValueRequired, string Tooltip, int CurrentValue, bool IsMissionCompleted, 
            int ObjectiveType, int CoinReward, int ExpReward)
        {
            missionType = MissionType;
            valuerequired = ValueRequired;
            tooltip = Tooltip;
            currentvalue = CurrentValue;
            isMissionCompleted = IsMissionCompleted;
            objectiveType = ObjectiveType;
            coinReward = CoinReward;
            expReward = ExpReward;
        }

    }

    [Serializable]
    public struct GameData
    {
        public int money;
        public string currentscene;
        public EDoor doorDestination;
        
        public GameData(int PlayerCoins, string CurrentScene, EDoor DoorDestination)
        {
            money = PlayerCoins;
            currentscene = CurrentScene;
            doorDestination = DoorDestination;
        }
    }

    public void SavePlayers()
    {
        player1 = LevelManager.LevelManagerInstance.Player1.SavePlayer();
        player2 = LevelManager.LevelManagerInstance.Player2.SavePlayer();
    }

    public void SaveGameData()
    {
        gameData = LevelManager.LevelManagerInstance.SaveGameData();
    }

    public void SaveMission()
    {
        mission = LevelManager.LevelManagerInstance.Mission.SaveMission();
    }

    public GameData gameData;
    public MissionData mission;
    public PlayerData player1;
    public PlayerData player2;

}

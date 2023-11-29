using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    //Name of the save game file
    private const string m_SaveFileName = "savedgame.json";

    public void SaveData()
    {
        SaveData data = new SaveData();
        data.SavePlayers(); //Sets the inner data vars (player1 and player2) with current player stats
        data.SaveMission(); //Sets the inner data var with the current mission parameters
        data.SaveScene(); //Sets the inner scene var with current scene name
        data.SaveMoney(); //Sets the inner money var with the current player coins
        string jsonData = JsonUtility.ToJson(data);

        try
        {
            File.WriteAllText(m_SaveFileName, jsonData);
        }
        catch(Exception e)
        {
            Debug.LogError($"Error while trying to save the current data in {Path.Combine(Application.persistentDataPath, m_SaveFileName)}. Error throws {e}");
        }

    }

    public void LoadData()
    {
        try
        {
            string savedData = File.ReadAllText(m_SaveFileName); //Reads all Json data in the filename

            SaveData data = new SaveData();
            JsonUtility.FromJsonOverwrite(savedData, data);
            GameManager.GameManagerInstance.ChangeScene(data.doorDestination);
            LevelManager.LevelManagerInstance.Money.ChangeCoins(data.money);
            LevelManager.LevelManagerInstance.Mission.LoadMission(data.mission);
            LevelManager.LevelManagerInstance.Player1.Load(data.player1);
            LevelManager.LevelManagerInstance.Player2.Load(data.player2);
        } 
        catch(Exception e)
        {
            Debug.LogError($"Error while trying to load current saved data in {Path.Combine(Application.persistentDataPath, m_SaveFileName)}. Error throws {e}");
        }
    }
}

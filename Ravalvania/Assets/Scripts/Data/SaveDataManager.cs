using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class holds all the SaveGame system logic
/// </summary>
public class SaveDataManager : MonoBehaviour
{
    //Name of the save game file
    private const string m_SaveFileName = "savedgame.json";

    /// <summary>
    /// Saves data and stores all data in a json file
    /// </summary>
    public void SaveData()
    {
        SaveData data = new SaveData();
        data.SavePlayers(); //Sets the inner data vars (player1 and player2) with current player stats
        data.SaveMission(); //Sets the inner data var with the current mission parameters
        data.SaveGameData(); //Sets the inner scene vars with current scene name and money var with the current player coins
        string jsonData = JsonUtility.ToJson(data);

        try
        {
            Debug.Log("Saving data.");
            Debug.Log(jsonData);

            File.WriteAllText(m_SaveFileName, jsonData);
        }
        catch(Exception e)
        {
            Debug.LogError($"Error while trying to save the current data in {Path.Combine(Application.persistentDataPath, m_SaveFileName)}. Error throws {e}");
        }
    }

    /// <summary>
    /// Data persistance on change scenes. Returns all the saved data.
    /// </summary>
    /// <returns></returns>
    public SaveData DataPersistanceOnChangeScene()
    {
        SaveData data = new SaveData();
        data.SavePlayers();
        data.SaveMission();
        data.SaveGameData();
        return data;
    }

    /// <summary>
    /// Reads data from a json file and calls the loading functions
    /// </summary>
    public void LoadData()
    {
        Debug.Log("Loading data.");
        try
        {
            string savedData = File.ReadAllText(m_SaveFileName); //Reads all Json data in the filename
            SaveData data = new SaveData();
            JsonUtility.FromJsonOverwrite(savedData, data);
            Debug.Log(data);

            GameManager.GameManagerInstance.SetLoadBoolean(true);
            GameManager.GameManagerInstance.ChangeScene(data.gameData.doorDestination);
            LevelManager.LevelManagerInstance.LoadGameData(data.gameData);
            LevelManager.LevelManagerInstance.Mission.LoadMission(data.mission);
            Debug.Log(string.Format("Player hp: {0} | Player exp: {1} | Player level: {2}", data.player2.hp, data.player2.experience, data.player2.level));
            LevelManager.LevelManagerInstance.Player1.Load(data.player1, true);
            LevelManager.LevelManagerInstance.Player2.Load(data.player2, true);
        } 
        catch(Exception e)
        {
            Debug.LogError($"Error while trying to load current saved data in {Path.Combine(Application.persistentDataPath, m_SaveFileName)}. Error throws {e}");
        }
    }
}

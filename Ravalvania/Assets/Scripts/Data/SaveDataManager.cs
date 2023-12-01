using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveDataManager : MonoBehaviour
{
    //Name of the save game file
    private const string m_SaveFileName = "savedgame.json";

    //Saves data and stores all data in a json file
    public void SaveData()
    {
        SaveData data = new SaveData();
        data.SavePlayers(); //Sets the inner data vars (player1 and player2) with current player stats
        data.SaveMission(); //Sets the inner data var with the current mission parameters
        data.SaveGameData(); //Sets the inner scene vars with current scene name and money var with the current player coins
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

    //Data persistance on change scenes. Returns all the saved data.
    public SaveData DataPersistanceOnChangeScene()
    {
        SaveData data = new SaveData();
        data.SavePlayers();
        data.SaveMission();
        data.SaveGameData();
        return data;
    }

    //Reads data from a json file and calls the loading functions
    public void LoadData()
    {
        try
        {
            string savedData = File.ReadAllText(m_SaveFileName); //Reads all Json data in the filename

            SaveData data = new SaveData();
            JsonUtility.FromJsonOverwrite(savedData, data);
            GameManager.GameManagerInstance.SetLoadBoolean(true);
            SceneManager.LoadScene(data.gameData.currentscene);
            LevelManager.LevelManagerInstance.LoadGameData(data.gameData);
            LevelManager.LevelManagerInstance.Mission.LoadMission(data.mission);
            LevelManager.LevelManagerInstance.Player1.Load(data.player1, true);
            LevelManager.LevelManagerInstance.Player2.Load(data.player2, true);
        } 
        catch(Exception e)
        {
            Debug.LogError($"Error while trying to load current saved data in {Path.Combine(Application.persistentDataPath, m_SaveFileName)}. Error throws {e}");
        }
    }
}

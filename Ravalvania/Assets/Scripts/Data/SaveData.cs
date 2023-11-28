using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{

    [Serializable]
    public struct PlayerData
    {
        public int level;
        public float hp;
        public float mana;
        public Vector3 position;
        public Weapon weapon;
        public Armor armor;
        public Orb orb;

        public PlayerData(int PlayerLevel, float PlayerHP, float PlayerMana, Vector3 Playerposition,
            Weapon EquippedWeapon, Armor EquippedArmor, Orb EquippedOrb)
        {
            level = PlayerLevel;
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

    }

    [Serializable]
    public struct InventoryData
    {

    }

    public string scene;
    public int money;
    public PlayerData player1;
    public PlayerData player2;

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SaveData;

public interface IDamageble
{
    public void damage(float damage);
    //Interficie per ser ferit per algun objecte
}
public interface IHealable
{
    public void heal(float heal);
    //Interficie per ser curat per algun objecte
}
public interface IPoisonable
{
    public void damage(float dmg, int time);
}
public interface IHealableOT
{
    public void healOverTime(float heal, float tick, float duration);
}
public interface IItem
{
    public string Id { get; set; }
    public string Description { get; set; }
    public Sprite Sprite { get; set; }

    public bool UsedBy(GameObject go);
}
public interface IInteractable
{
    public void interact();
}
public interface IObjectivable
{
    public void OnObjectiveCheck(EMission type);
}

public interface ISaveableObject
{
    public PlayerData SavePlayer();
    public void Load(PlayerData _playerData);
}

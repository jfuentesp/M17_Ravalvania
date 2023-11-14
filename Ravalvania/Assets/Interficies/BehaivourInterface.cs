using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public void heal(float heal, int time);
}
public interface IItem
{
    public string Id { get; set; }
    public string Description { get; set; }
    public Sprite Sprite { get; set; }

    public bool UsedBy(GameObject go);
}

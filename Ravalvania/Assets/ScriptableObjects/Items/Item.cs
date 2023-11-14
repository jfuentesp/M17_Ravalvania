using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject, IItem
{
    [Header("Dades Comunes")]
    [SerializeField]
    protected string m_Id = "";
    string IItem.Id { get => m_Id; set => m_Id = value; }
    [SerializeField]
    protected string m_Description = "";
    string IItem.Description { get => m_Description; set => m_Description = value; }
    [SerializeField]
    protected Sprite m_Sprite = null;
    Sprite IItem.Sprite { get => m_Sprite; set => m_Sprite = value; }

    public abstract bool UsedBy(GameObject go);
    
        
    
}

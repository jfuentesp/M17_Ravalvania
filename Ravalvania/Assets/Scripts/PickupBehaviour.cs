using System.Collections;
using System.Collections.Generic;
using streetsofraval;
using UnityEngine;

/* LIST OF PICKUPS REFERENCED */
/*
 * 0 = Coins + 100 coins
 * 1 = Diamond + 300 coins
 * 2 = Bandages
 * 3 = Mana Potion
 * 4 = Potion 
 * 5 = Treasure + 1000 score
 */

public class PickupBehaviour : MonoBehaviour
{
    [Header("Scriptable List References")]
    [SerializeField]
    List<PickupScriptableObject> m_ScriptablePickups;

    private Rigidbody2D m_Rigidbody;
    private CircleCollider2D m_Collider;

    private int m_PickupID;
    private string m_PickupName;
    private int m_PickupValue;
    private float m_PickupEffectDuration;
    private Color m_PickupColor;
    private Sprite m_PickupSprite;

    private SpriteRenderer m_SpriteRenderer;

    [Header("Time until the pickup disappears")]
    [SerializeField]
    private float m_PickupDuration;

    [Header("GameEvent references")]
    [SerializeField]
    private GameEventInt m_OnPickupCoins;
    [SerializeField]
    private GameEventItemPlayer m_OnPickupHealthPotion;
    [SerializeField]
    private GameEventItemPlayer m_OnPickupManaPotion;
    [SerializeField]
    private GameEventItemPlayer m_OnPickupBandage;

    [SerializeField]
    private Item m_Bandage;
    [SerializeField]
    private Item m_Potion;
    [SerializeField]
    private Item m_Mana;

    private void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_Collider = GetComponent<CircleCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {     
        PickupScriptableObject randomPickup = m_ScriptablePickups[Random.Range(0, m_ScriptablePickups.Count)];
        InitPickup(randomPickup);
        StartCoroutine(AliveCoroutine());
    }

    private void Update()
    {
        if(m_Rigidbody.velocity.y == 0)
        {
            m_Rigidbody.isKinematic = true;
        }
    }

    public void InitPickup(PickupScriptableObject pickupInfo)
    {
        m_PickupID = pickupInfo.PickupID;
        m_PickupName = pickupInfo.PickupName;
        m_PickupValue = pickupInfo.PickupValue;
        m_PickupEffectDuration = pickupInfo.PickupEffectDuration;
        m_PickupColor = pickupInfo.PickupColor;
        m_PickupSprite = pickupInfo.PickupSprite;
        m_SpriteRenderer.sprite = m_PickupSprite;
    }

    private IEnumerator AliveCoroutine()
    {
        yield return new WaitForSeconds(m_PickupDuration);
        Destroy(this.gameObject);
    }

    public void GetPickup(EPlayer player)
    {
        switch (m_PickupID)
        {
            case 0:
                m_OnPickupCoins.Raise(100);
                break;
            case 1:
                m_OnPickupCoins.Raise(500);
                break;
            case 2:
                m_OnPickupBandage.Raise(m_Bandage, player);
                break;
            case 3:
                m_OnPickupManaPotion.Raise(m_Mana, player);
                break;
            case 4:
                m_OnPickupHealthPotion.Raise(m_Potion, player);
                break;
            case 5:
                m_OnPickupCoins.Raise(1000);
                break;
            default:
                break;
        }
    }
}

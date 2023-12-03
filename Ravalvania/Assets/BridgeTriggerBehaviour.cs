using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeTriggerBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject m_BridgeObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(SetBridgeActive());
        }
    }

    private IEnumerator SetBridgeActive()
    {
        yield return new WaitForSeconds(1.5f);
        m_BridgeObject.SetActive(true);
        Destroy(this.gameObject);
    }
}

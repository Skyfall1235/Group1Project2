using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetter : MonoBehaviour
{
    [SerializeField] private GameObject m_playerPrefab;

    private void Start()
    {
        m_playerPrefab = FindObjectOfType<PlayerHealthManager>().gameObject;
        StartCoroutine(PlacePlayer());
    }

    private IEnumerator PlacePlayer()
    {
        yield return new WaitForSeconds(0.1f);
        m_playerPrefab.transform.position = transform.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetter : MonoBehaviour
{
    [SerializeField] private GameObject m_startPosition;
    [SerializeField] private GameObject m_playerPrefab;

    private void Start()
    {
        m_playerPrefab.transform.position = m_startPosition.transform.position;
    }
}

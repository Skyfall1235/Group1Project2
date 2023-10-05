using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBox : MonoBehaviour
{
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_clip;
    [SerializeField] bool m_playOnce;
    bool m_hasPlayed = false;
    

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }
        if (m_playOnce && !m_hasPlayed)
        {
            m_audioSource.PlayOneShot(m_clip);
            m_hasPlayed = true;
            return;
        }
        if(m_playOnce && m_hasPlayed)
        {
            return;
        }
        m_audioSource.PlayOneShot(m_clip);
        m_hasPlayed = true;

    }
}

using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager m_instance;
    AudioSource m_source;

    private void Awake()
    {
        m_source = GetComponent<AudioSource>();

        if (m_instance == null)
            m_instance = this;
        else
            Destroy(gameObject);
    }

    public void PlayClip(AudioClip clip)
    {
        m_source.clip = clip;
        m_source.Play();
    }
}

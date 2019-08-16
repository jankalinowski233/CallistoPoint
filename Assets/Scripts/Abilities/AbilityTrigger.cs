using UnityEngine;

public class AbilityTrigger : MonoBehaviour
{
    public Ability m_Ability;

    public Sprite m_Icon;
    public AudioClip m_Sound;

    public float m_fCooldown;
    protected float m_fNextReadyTime;

    protected bool m_bReady = true;

    public void Awake()
    {
        m_Ability.Initialize(gameObject);
    }

    public virtual void TriggerAbility()
    {
        //do basic stuff here, such as playing sound, playing particle system etc.
    }
}

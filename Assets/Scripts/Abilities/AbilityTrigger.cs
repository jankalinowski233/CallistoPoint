using UnityEngine;

public class AbilityTrigger : MonoBehaviour
{
    public Ability m_Ability;

    [HideInInspector] public Sprite m_Icon;
    [HideInInspector] public AudioClip m_Sound;
    [HideInInspector] public float m_fCooldown;
    [HideInInspector] protected float m_fNextReadyTime;
    [HideInInspector] protected float m_fCooldownLeft;

    protected bool m_bReady = true;

    public void Awake()
    {
        m_Ability.Initialize(gameObject);
    }

    private void Update()
    {
        m_bReady = (Time.time > m_fNextReadyTime);

        if (m_bReady == false)
        {
            Cooldown();
        }
        else
        {
            FinishCooldown();
        }
    }

    public virtual void TriggerAbility()
    {
        //do basic stuff here, such as playing sound, playing particle system etc.

        PlayVFX();
        StartCooldown();
    }

    public void PlayVFX()
    {
        //play vfx here
    }

    public void StartCooldown()
    {
        m_fNextReadyTime = Time.time + m_fCooldown;
        m_fCooldownLeft = m_fCooldown;

        //set UI here
    }

    public void Cooldown()
    {
        m_fCooldownLeft -= Time.deltaTime;

        //display cooldown UI updates (e.g. time)

    }

    public void FinishCooldown()
    {
        //set UI here
    }
}

using UnityEngine;

public abstract class Ability : ScriptableObject
{
    [Header("Basic variables")]
    [Space(5f)]
    public float m_fCooldown;
    public Sprite m_Icon;
    public AudioClip m_Sound;

    [Tooltip("If that is set to false, the object will spawn at player's position")]
    public bool m_CastRay;

    public abstract void Initialize(GameObject obj);
    public abstract void Cast();
}

using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public float m_fCooldown;
    public Sprite m_Icon;
    public AudioClip m_Sound;

    public bool m_CastRay;

    public abstract void Initialize(GameObject obj);
    public abstract void Cast();
}

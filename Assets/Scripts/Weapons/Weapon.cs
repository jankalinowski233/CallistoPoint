using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon stats")]
    [Space(7f)]
    [SerializeField] private float m_fShootingRange;
    [SerializeField] private float m_fWeaponDmg;
    public float m_fTimeBetweenShots;
    float m_fRemainingTimeBetweenShots;

    [Header("Weapon overheating")]
    [Space(7f)]
    public float m_fMaxWeaponTemperature;
    float m_fCurrentWeaponTemperature;
    [Range(5, 20)] public float m_fWeaponCooldownSpeed;
    bool m_bWeaponCooldown = false;

    [Header("Weapon FX")]
    [Space(7f)]
    public ParticleSystem m_shotParticles;
    public ParticleSystem m_gunParticles;
    public ParticleSystem m_hitParticles;
    public Light m_gunLight;

    LineRenderer m_lineRenderer;
    AudioSource m_audio;

    private void Awake()
    {
        m_shotParticles.transform.position = m_gunParticles.transform.position;
 
        m_fCurrentWeaponTemperature = 0;
        
        m_audio = GetComponent<AudioSource>();
        m_lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        RenderLine();
        ProcessKeyboardInput();

        if(m_bWeaponCooldown == true)
        {
            WeaponCooldown();
        }
    }

    void RenderLine()
    {
        m_lineRenderer.SetPosition(0, m_gunParticles.transform.position);
        m_lineRenderer.SetPosition(1, m_gunParticles.transform.position + transform.forward * m_fShootingRange);
    }

    void ProcessKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            m_bWeaponCooldown = true;
        }
    }

    public void Shoot()
    {
        if (m_fRemainingTimeBetweenShots <= 0)
        {
            if (m_fCurrentWeaponTemperature < m_fMaxWeaponTemperature)
            {
                if (m_bWeaponCooldown == false)
                {
                    //play weapon particle effects
                    m_gunParticles.Stop();
                    m_gunParticles.Play();

                    //enable lighting
                    m_gunLight.enabled = true;

                    //play sound
                    m_audio.Play();
                    m_shotParticles.Emit(1);

                    //cast ray
                    Ray ray = new Ray(transform.position, new Vector3(transform.forward.x, 0f, transform.forward.z));
                    RaycastHit weaponHit;

                    //check if it hit anything
                    if (Physics.Raycast(ray, out weaponHit, m_fShootingRange, LayerMask.GetMask("Damageable")))
                    {

                        if (weaponHit.collider.CompareTag("Enemy") || weaponHit.collider.CompareTag("Turret"))
                        {
                            //if it's an enemy, deal damage
                            Character character = weaponHit.collider.GetComponent<Character>();
                            character.TakeDamage(m_fWeaponDmg);
                        }

                    }

                    m_fCurrentWeaponTemperature++;

                    UIManager.m_instance.SetWeaponHeat(m_fCurrentWeaponTemperature, m_fMaxWeaponTemperature);

                    m_fRemainingTimeBetweenShots = m_fTimeBetweenShots;

                    //disable vfx
                    if (gameObject.activeInHierarchy == true)
                    {
                        StartCoroutine(DisableVFX());
                    }
                }
                else
                {
                    //TODO weapon is too hot, set UI (flashing or sth)
                }
            }            
        }
        else
        {
            m_fRemainingTimeBetweenShots -= Time.deltaTime;
        }           

    }

    public IEnumerator DisableVFX()
    {
        yield return new WaitForSeconds(0.05f);
        m_gunLight.enabled = false;
    }

    public void ResetTimeBetweenShots()
    {
        m_fRemainingTimeBetweenShots = 0f;
    }

    public void WeaponCooldown()
    {
        //cooldown
        if (m_fCurrentWeaponTemperature > 0)
        {
            m_fCurrentWeaponTemperature -= Time.deltaTime * m_fWeaponCooldownSpeed;
            UIManager.m_instance.SetWeaponHeat(m_fCurrentWeaponTemperature, m_fMaxWeaponTemperature);
        }
        else
            m_bWeaponCooldown = false;
    }

    private void OnEnable()
    {
        //make sure effects are disabled when switching weapon
        m_gunLight.enabled = false;
        UIManager.m_instance.SetWeaponHeat(m_fCurrentWeaponTemperature, m_fMaxWeaponTemperature);
    }
}

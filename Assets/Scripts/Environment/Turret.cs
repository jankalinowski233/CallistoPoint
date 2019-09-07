using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Interactable
{
    public enum TurretMode { Neutral, Friendly };
    public TurretMode m_TurretMode;

    public List<Enemy> m_enemies = new List<Enemy>();
    Enemy m_currentTarget = null;

    public float m_fTurretDamage;
    public float m_fTimeBetweenDamage;
    public float m_fRotationSpeed;
    float m_fRemainingTimeBetweenDamage;

    private void Update()
    {
        if(m_TurretMode == TurretMode.Friendly)
        {
            if (m_currentTarget == null)
            {
                SetCurrentTarget();
            }
            else
            {
                AttackCurrentTarget();
            }
        }
    }

    void SetCurrentTarget()
    {
        if(m_enemies.Count == 0)
        {
            if (m_fRemainingTimeBetweenDamage > 0)
                m_fRemainingTimeBetweenDamage = 0f;

            transform.rotation *= Quaternion.AngleAxis(Time.deltaTime * m_fRotationSpeed, Vector3.up);
            return;
        }

        if (m_enemies.Count == 1)
        {
            m_currentTarget = m_enemies[0];
        }
        else
        {
            for (int i = 1; i < m_enemies.Count; i++)
            {
                float distance = Vector3.Distance(transform.position, m_enemies[i].transform.position);
                float previousClosestDistance = Vector3.Distance(transform.position, m_enemies[i - 1].transform.position);

                //set target with the closest distance
                if (distance < previousClosestDistance)
                {
                    previousClosestDistance = distance;
                    m_currentTarget = m_enemies[i];
                    print(m_enemies[i].gameObject.name);
                }
            }
        }
    }

    public virtual void AttackCurrentTarget()
    {
        //rotate towards target
        Vector3 targetRotation = m_currentTarget.transform.position - transform.position;
        targetRotation.y = 0f;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetRotation), 1f);

        //measure time between attacks
        if(m_fRemainingTimeBetweenDamage <= 0)
        {
            //play vfx
            //play sound

            //deal damage
            DealDamage(m_currentTarget);

            m_fRemainingTimeBetweenDamage = m_fTimeBetweenDamage;
        }
        else
        {
            m_fRemainingTimeBetweenDamage -= Time.deltaTime;
        }
    }

    public void SetCurrentTarget(Enemy e)
    {
        m_currentTarget = e;
    }

    public Enemy GetCurrentTarget()
    {
        return m_currentTarget;
    }

    void DealDamage(Enemy enemy)
    {
        enemy.TakeDamage(m_fTurretDamage);
    }

    public void AddEnemy(Enemy enemy)
    {
        m_enemies.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        m_enemies.Remove(enemy);
    }

    public void SwitchMode()
    {
        m_TurretMode = TurretMode.Friendly;
    }
}

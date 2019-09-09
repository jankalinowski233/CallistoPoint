using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelController : MonoBehaviour
{
    public static LevelController m_instance;

    List<Enemy> m_enemies;

    public UnityEvent OnEnemiesClear;

    private void Awake()
    {
        if (m_instance == null) m_instance = this;
        else Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_enemies = new List<Enemy>();

        Enemy[] enemies = FindObjectsOfType<Enemy>();

        for (int i = 0; i < enemies.Length; i++)
        {
            AddToList(enemies[i]);
            enemies[i].m_EnemyDeathEvent += RemoveFromList;
        }
    }

    public void AddToList(Enemy e)
    {
        m_enemies.Add(e);
    }

    public void RemoveFromList(Enemy e)
    {
        m_enemies.Remove(e);

        if (m_enemies.Count == 0)
        {
            OnEnemiesClear.Invoke();
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelController : MonoBehaviour
{
    public static LevelController m_instance;

    List<GameObject> m_enemies;

    public UnityEvent OnEnemiesClear;

    private void Awake()
    {
        if (m_instance == null) m_instance = this;
        else Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_enemies = new List<GameObject>();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i++)
        {
            AddToList(enemies[i]);
        }
    }

    public void AddToList(GameObject go)
    {
        m_enemies.Add(go);
    }

    public void RemoveFromList(GameObject go)
    {
        m_enemies.Remove(go);

        if (m_enemies.Count == 0)
        {
            OnEnemiesClear.Invoke();
        }
    }
}

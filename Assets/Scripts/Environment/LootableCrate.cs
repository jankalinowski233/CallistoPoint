using System.Collections;
using UnityEngine;

public class LootableCrate : Interactable
{
    Animator m_anim;

    [Header("Spawning items")]
    public GameObject[] m_items;
    public Transform[] m_spawnTargets;

    private void Awake()
    {
        m_anim = GetComponent<Animator>();
    }

    public void OpenCrate()
    {
        m_anim.SetTrigger("CrateOpen");
    }

    void SpawnItems()
    {        
        for(int i = 0; i < m_spawnTargets.Length; i++)
        {
            int random = Random.Range(0, m_items.Length);
            GameObject go = Instantiate(m_items[random], transform.position, Quaternion.identity);

            Item item = go.GetComponent<Item>();
            StartCoroutine(item.MoveToTarget(m_spawnTargets[i].position, 10, 1));
            
        }
    }
}

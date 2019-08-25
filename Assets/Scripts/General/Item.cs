using UnityEngine;

public class Item : MonoBehaviour, ICollectable
{
    public virtual void Collect()
    {
        //basic behaviour, such as play sounds, particle system etc. goes here

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Collect();
    }


}

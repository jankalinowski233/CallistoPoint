using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour, ICollectable
{
    public UnityEvent OnCollection;

    public virtual void Collect()
    {
        //basic behaviour, such as play sounds, particle system etc. goes here
        OnCollection.Invoke();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Collect();
    }


}

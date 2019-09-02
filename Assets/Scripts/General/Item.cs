using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Item : MonoBehaviour, ICollectable
{
    public UnityEvent OnCollection;

    public virtual void Collect()
    {
        //basic behaviour, such as play sounds, particle system etc. goes here
        OnCollection.Invoke();
        gameObject.SetActive(false);
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Collect();
    }

    public IEnumerator MoveToTarget(Vector3 targetPos, float flightHeight, float flightDuration)
    {
        Vector3 startPos = transform.position;
        float normalizedTime = 0.0f;

        while (normalizedTime < 1.0f)
        {
            if (gameObject != null)
            {
                float yAxisOffset = flightHeight * (normalizedTime - normalizedTime * normalizedTime);
                transform.position = Vector3.Lerp(startPos, targetPos, normalizedTime) + yAxisOffset * Vector3.up;
                normalizedTime += Time.deltaTime / flightDuration;
                yield return null;
            }
            else break;
        }
    }


}

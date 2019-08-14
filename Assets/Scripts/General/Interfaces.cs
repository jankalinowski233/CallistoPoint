using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float dmg);
    void Kill();
}

public interface IInteractable
{
    void Interact();
}

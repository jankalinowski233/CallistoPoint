public interface IDamageable
{
    void TakeDamage(float dmg);
    void Kill();
}

public interface IInteractable
{
    void Interact();
}

public interface ICollectible
{
    void Collect();
}

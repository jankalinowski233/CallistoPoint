public interface IDamageable
{
    void TakeDamage(float dmg);
    void Die();
}

public interface IInteractable
{
    void Interact();
}

public interface ICollectable
{
    void Collect();
}

public interface IHackable
{
    void Hack(float time);
}

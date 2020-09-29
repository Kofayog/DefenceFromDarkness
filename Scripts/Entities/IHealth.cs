public interface IHealth
{
    float Health { get; set; }

    void RecieveDamage(float damageAmount);
    void RestoreHealth(float healthAmount);
}

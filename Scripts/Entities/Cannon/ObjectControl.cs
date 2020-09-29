using UnityEngine;

public abstract class ObjectControl: MonoBehaviour
{
    public Cannon cannon { get; set; }
    public virtual void Init(Cannon cannon)
    {
        this.cannon = cannon;
    }
    public abstract void Activate();
    public abstract void Deactivate();

    public abstract void Control(Vector3 direction);
}

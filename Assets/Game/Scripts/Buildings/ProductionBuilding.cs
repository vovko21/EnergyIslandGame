using UnityEngine;

public abstract class ProductionBuilding : MonoBehaviour
{
    [SerializeField] protected float _produced;

    public float Produced => _produced;

    public virtual float Gather()
    {
        var produced = _produced;
        _produced = 0;
        return produced;
    }
}

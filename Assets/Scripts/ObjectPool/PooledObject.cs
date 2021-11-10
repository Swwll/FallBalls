using UnityEngine;
using UnityEngine.Events;

public abstract class PooledObject : MonoBehaviour
{
    public abstract event UnityAction<PooledObject> Deactivated;
    public abstract void Activate();
}
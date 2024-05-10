using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected Transform _objectsContainer;
    [SerializeField] protected bool _autoExpand = true;
    [SerializeField] protected int _objectsCount;
}
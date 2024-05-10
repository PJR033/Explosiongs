using System.Collections.Generic;
using UnityEngine;

public class MonoPool<T> where T : MonoBehaviour
{
    private T _prefab;
    private Transform _container;
    private bool _autoExpand;
    private Queue<T> _pool = new Queue<T>();

    public MonoPool(T prefab, int count, Transform container, bool autoExpand)
    {
        _prefab = prefab;
        _container = container;
        _autoExpand = autoExpand;

        CreatePool(count);
    }

    public T GetFreeElement()
    {
        if (HasFreeElement(out T element))
        {
            return element;
        }
        else if (_autoExpand)
        {
            return CreateObject(true);
        }

        return null;
    }

    public void PutElement(T element)
    {
        _pool.Enqueue(element);
        element.gameObject.SetActive(false);
    }

    private bool HasFreeElement(out T element)
    {
        foreach (T mono in _pool)
        {
            if (mono.gameObject.activeInHierarchy == false)
            {
                element = mono;
                element.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }

    private void CreatePool(int count)
    {
        _pool = new Queue<T>();

        for (int i = 0; i < count; i++)
        {
            CreateObject();
        }
    }

    private T CreateObject(bool isDefaultActive = false)
    {
        T createdObject = Object.Instantiate(_prefab, _container);
        createdObject.gameObject.SetActive(isDefaultActive);
        _pool.Enqueue(createdObject);
        return createdObject;
    }
}

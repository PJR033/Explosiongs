using System;
using UnityEngine;

public class CubeSpawner : Spawner
{
    [SerializeField] private ExplosionCube _prefabCube;

    private MonoPool<ExplosionCube> _pool;

    public event Action<ExplosionCube> CubeSpawned;

    private void Awake()
    {
        _pool = new MonoPool<ExplosionCube>(_prefabCube, _objectsCount, _objectsContainer, _autoExpand);

        for (int i = 0; i < _objectsContainer.childCount; i++)
        {
            ExplosionCube cube = _objectsContainer.GetChild(i).GetComponent<ExplosionCube>();
            cube.NotExplosed.AddListener(SpawnCubes);
            cube.IsExplosed.AddListener(_pool.PutElement);
        }
    }

    private void SpawnCubes(ExplosionCube receivedCube)
    {
        int minCubesCount = 2;
        int maxCubesCount = 7;
        int cubesCount = UnityEngine.Random.Range(minCubesCount, maxCubesCount);
        float sizeCoefficent = 0.5f;
        Vector3 cubesScale = receivedCube.transform.localScale * sizeCoefficent;

        for (int i = 0; i < cubesCount; i++)
        {
            ExplosionCube newCube = _pool.GetFreeElement();
            MeshRenderer currentRenderer = newCube.Renderer;
            ChangeColor(currentRenderer);
            newCube.SpawnChance = receivedCube.SpawnChance * sizeCoefficent;
            newCube.transform.localScale = cubesScale;
            newCube.transform.position = transform.position;
            newCube.NotExplosed.AddListener(SpawnCubes);
            newCube.IsExplosed.AddListener(_pool.PutElement);
            CubeSpawned?.Invoke(newCube);
        }

        _pool.PutElement(receivedCube);
    }

    private void ChangeColor(MeshRenderer renderer)
    {
        string colorName = "_Color";
        float colorMaxValue = 1f;
        Color newColor = new Color(UnityEngine.Random.Range(0f, colorMaxValue), UnityEngine.Random.Range(0f, colorMaxValue), UnityEngine.Random.Range(0f, colorMaxValue));
        renderer.material.SetColor(colorName, newColor);
    }
}

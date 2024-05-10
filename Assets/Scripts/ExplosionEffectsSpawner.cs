using UnityEngine;

public class ExplosionEffectsSpawner : Spawner
{
    [SerializeField] CubeSpawner _cubeSpawner;
    [SerializeField] LifeTimeEffect _effectPrefab;
    [SerializeField] private Transform _cubesContainer;

    private MonoPool<LifeTimeEffect> _pool;

    private void Awake()
    {
        _pool = new MonoPool<LifeTimeEffect>(_effectPrefab, _objectsCount, _objectsContainer, _autoExpand);

        for (int i = 0; i < _cubesContainer.childCount; i++)
        {
            ExplosionCube cube = _cubesContainer.GetChild(i).GetComponent<ExplosionCube>();
            cube.IsExplosed.AddListener(SpawnEffect);
        }

        for (int i = 0; i < _objectsContainer.childCount; i++)
        {
            LifeTimeEffect effect = _objectsContainer.GetChild(i).GetComponent<LifeTimeEffect>();
            effect.IsLifeTimeEnd.AddListener(_pool.PutElement);
        }
    }

    private void OnEnable()
    {
        _cubeSpawner.CubeSpawned += CubeSubscribe;
    }

    private void OnDisable()
    {
        _cubeSpawner.CubeSpawned -= CubeSubscribe;
    }

    private void SpawnEffect(ExplosionCube cube)
    {
        LifeTimeEffect effect = _pool.GetFreeElement();
        effect.transform.position = cube.transform.position;
        effect.IsLifeTimeEnd.AddListener(_pool.PutElement);
    }

    private void CubeSubscribe(ExplosionCube cube)
    {
        cube.IsExplosed.AddListener(SpawnEffect);
    }
}

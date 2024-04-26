using TMPro;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(Rigidbody), typeof(Collider))]
public class ExplosionCube : MonoBehaviour
{
    [SerializeField] private CubeSpawner _spawner;
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    public float SpawnChance = 100;

    public MeshRenderer Renderer { get; private set; }

    private void Awake()
    {
        Renderer = GetComponent<MeshRenderer>();
    }

    public void TryExplose()
    {
        float maxChance = 100f;
        float currentChance = Random.Range(0, maxChance);

        if (currentChance <= SpawnChance)
        {
            _spawner.SpawnCubes(gameObject.GetComponent<ExplosionCube>());
            Destroy(gameObject);
        }
        else
        {
            Explose();
        }
    }

    public void SetSpawner(CubeSpawner spawner)
    {
        _spawner = spawner;
    }

    private void Explose()
    {
        float explosionCoefficent = 1f / transform.localScale.x;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider collider in hitColliders)
        {
            Rigidbody currentBody = collider.attachedRigidbody;

            if (currentBody != null)
            {
                currentBody.AddExplosionForce(_explosionForce * explosionCoefficent, transform.position, _explosionRadius * explosionCoefficent);
            }
        }

        Instantiate(_explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
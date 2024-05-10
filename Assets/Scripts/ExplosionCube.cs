using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer), typeof(Rigidbody), typeof(Collider))]
public class ExplosionCube : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    public float SpawnChance = 100;

    public UnityEvent<ExplosionCube> NotExplosed;
    public UnityEvent<ExplosionCube> IsExplosed;

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
            NotExplosed?.Invoke(this);
        }
        else
        {
            Explose();
        }
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

        IsExplosed?.Invoke(this);
    }
}
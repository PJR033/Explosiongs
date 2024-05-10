using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[RequireComponent(typeof(ParticleSystem))]
public class LifeTimeEffect : MonoBehaviour
{
    private ParticleSystem _particles;
    private WaitForSeconds _delay;

    public UnityEvent<LifeTimeEffect> IsLifeTimeEnd;

    private void Awake()
    {
        _particles = GetComponent<ParticleSystem>();
        _delay = new WaitForSeconds(_particles.main.startLifetime.constant);
    }

    private void OnEnable()
    {
        StartCoroutine(Disappearing());
    }

    private IEnumerator Disappearing()
    {
        yield return _delay;
        IsLifeTimeEnd?.Invoke(this);
    }
}

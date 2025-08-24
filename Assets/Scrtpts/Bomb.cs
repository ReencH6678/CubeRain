using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ColorChanger))]
public class Bomb : MonoBehaviour , IPoolable
{

    [SerializeField] private float _explodeRadius;
    [SerializeField] private float _explodeForce;

    private float _deley;

    private float _maxLifeTime = 5;
    private float _minLifeTime = 2;

    private ColorChanger _colorChanger;

    public event UnityAction<IPoolable> DeactivationRequested;

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
    }

    private void OnEnable()
    {
        StartCoroutine(Explode());
        _deley = Random.Range(_minLifeTime, _maxLifeTime + 1);
    }

    public void ResetObject()
    {
       _colorChanger.ResetColor();
    }

    private IEnumerator Explode()
    {
        var waitForSeconds = new WaitForSeconds(_deley);

        StartCoroutine(_colorChanger.SetAlpha(0, _deley));

        yield return waitForSeconds;

        foreach (Rigidbody collider in GetExlodableObjects())
        {
            collider.AddExplosionForce(_explodeForce, transform.position, _explodeRadius);
        }

        DeactivationRequested?.Invoke(this);
    }

    private List<Rigidbody> GetExlodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explodeRadius);

        List<Rigidbody> cubes = new List<Rigidbody>();

        foreach (Collider cube in hits)
            if (cube.gameObject.TryGetComponent<Cube>(out _) && cube.attachedRigidbody != null)
                cubes.Add(cube.attachedRigidbody);

        return cubes;
    }
}

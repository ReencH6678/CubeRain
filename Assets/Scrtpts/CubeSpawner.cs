using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private BoxCollider _boxCollider;

    private float _spawnRate = 0.3f;
    private bool _isOn = true;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        var waitForSecond = new WaitForSeconds(_spawnRate);

        while (_isOn)
        {
            _pool.Get();

            yield return waitForSecond;
        }
    }

    protected override Vector3 GetSpawnPosition()
    {
        Bounds bounds = _boxCollider.bounds;

        return new Vector3(Random.Range(bounds.min.x, bounds.max.x), transform.position.y, Random.Range(bounds.min.z, bounds.max.z));
    }
}
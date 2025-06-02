using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefabe;
    [SerializeField] private BoxCollider _boxCollider;

    private int _poolCapacity = 100;
    private int _poolMaxSize = 100;

    private ObjectPool<Cube> _pool;

    private float _spawnRate = 0.3f;
    private bool _isOn = true;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>
            (
                createFunc: () => Instantiate(_prefabe, GetSpawnPosition(), Quaternion.identity),
                actionOnGet: (obj) => ActionOnGet(obj),
                actionOnRelease: (obj) => obj.gameObject.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj),
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaxSize);
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        var waitForSecond = new WaitForSeconds(_spawnRate);

        while (_isOn)
        {
            GetCube();
            yield return waitForSecond;
        }
    }

    private void GetCube()
    {
        _pool.Get();
    }

    private void ActionOnGet(Cube obj)
    {
        obj.Collised += ReleaseCube;
        obj.transform.position = GetSpawnPosition();
        obj.gameObject.SetActive(true);
    }

    private void ReleaseCube(Cube obj)
    {
        obj.ResetObject();
        obj.Collised -= ReleaseCube;
        _pool.Release(obj);
    }

    private Vector3 GetSpawnPosition()
    {
        Bounds bounds = _boxCollider.bounds;

        return new Vector3(Random.Range(bounds.min.x, bounds.max.x), transform.position.y, Random.Range(bounds.min.z, bounds.max.z));
    }
}
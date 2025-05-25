using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefabe;
    [SerializeField] private BoxCollider _boxCollider;

    private int _poolCapacity = 100;
    private int _poolMaxSize = 100;

    private ObjectPool<GameObject> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>
            (
                createFunc: () => Instantiate(_prefabe, GetSpawnPosition(), Quaternion.identity),
                actionOnGet: (obj) => ActionOnGet(obj),
                actionOnRelease: (obj) => obj.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj),
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaxSize);    
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0, 0.3f);
    }

    private void GetCube()
    {
        _pool.Get();
    }

    private void ActionOnGet(GameObject obj) 
    {
        obj.GetComponent<Cube>().Collised += ActionOnRelease;
        obj.transform.position = GetSpawnPosition();
        obj.SetActive(true);
    }

    private void ActionOnRelease(GameObject obj) 
    {
        obj.GetComponent<MeshRenderer>().material = _prefabe.GetComponent<MeshRenderer>().sharedMaterial;
        _pool.Release(obj);
        obj.GetComponent<Cube>().Collised -= ActionOnRelease;
    }
    private Vector3 GetSpawnPosition()
    {
        Bounds bounds = _boxCollider.bounds;

        return new Vector3(Random.Range(bounds.min.x, bounds.max.x), transform.position.y, Random.Range(bounds.min.z, bounds.max.z));
    }
}

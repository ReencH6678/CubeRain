using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour, IPoolable
{
    [SerializeField] private T _prefabe;

    public ObjectPool<T> _pool;

    private int _poolCapacity = 100;
    private int _poolMaxSize = 100;

    public int CreatedObjectsCount { get; private set; }
    public int ActivObjectsCount { get; private set; }
    public int SpawnedObjectsCount { get; private set; }

    public event UnityAction Released;
    public event UnityAction Actived;
    public event UnityAction Created;

    private void Awake()
    {
        _pool = new ObjectPool<T>
            (
                createFunc: () => CreateFunc(),
                actionOnGet: (obj) => ActionOnGet(obj),
                actionOnRelease: (obj) => obj.gameObject.SetActive(false),
                actionOnDestroy: (obj) => Destroy(obj),
                collectionCheck: true,
                defaultCapacity: _poolCapacity,
                maxSize: _poolMaxSize);
    }

    private T CreateFunc()
    {
        Created?.Invoke();
        ++CreatedObjectsCount;

        return Instantiate(_prefabe, GetSpawnPosition(), Quaternion.identity);
    }

    public void ActionOnGet(T obj)
    {
        Actived?.Invoke();

        ++ActivObjectsCount;
        ++SpawnedObjectsCount;

        obj.DeactivationRequested += Release;
        obj.transform.position = GetSpawnPosition();
        obj.gameObject.SetActive(true);
    }

    public virtual void Release(IPoolable obj)
    {
        Released?.Invoke();
        --ActivObjectsCount;

        obj.ResetObject();
        obj.DeactivationRequested -= Release;
        _pool.Release((T)obj);
    }

    protected virtual Vector3 GetSpawnPosition()
    {
        return new Vector3(0, 0, 0);
    }
}
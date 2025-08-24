using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    private Transform _spawnPoint;

    public void Spawn(Transform spawnPoint)
    {
       _spawnPoint = spawnPoint;
        _pool.Get();
    }

    protected override Vector3 GetSpawnPosition()
    {
        return _spawnPoint.position;
    }
}

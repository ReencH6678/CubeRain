using TMPro;
using UnityEngine;

public class SpawnerInfoViewer<T> : MonoBehaviour where T : MonoBehaviour, IPoolable
{
    private const string CreateadObjects = "Created Objects ";
    private const string ActivObjects = "Activ Objects ";
    private const string SpawnObjects = "Spawn Objects ";

    [SerializeField] private Spawner<T> _spawner;

    [SerializeField] private TextMeshProUGUI _activeObjectsCount;
    [SerializeField] private TextMeshProUGUI _createdObjectsCount;
    [SerializeField] private TextMeshProUGUI _spawnedObjectsCount;

    private void OnEnable()
    {
        _spawner.Released += UpdateActiveText;
        _spawner.Actived += UpdateActiveText;
        _spawner.Created += UpdateCreateText;
        _spawner.Actived += UpdateSpawnText;
    }

    private void OnDisable()
    {
        _spawner.Released -= UpdateActiveText;
        _spawner.Actived -= UpdateActiveText;
        _spawner.Created -= UpdateCreateText;
        _spawner.Actived -= UpdateSpawnText;
    }

    private void UpdateCreateText()
    {
        _createdObjectsCount.text = CreateadObjects + _spawner.CreatedObjectsCount.ToString();
    }

    private void UpdateActiveText()
    {
        _activeObjectsCount.text = ActivObjects + _spawner.ActivObjectsCount.ToString();
    }

    private void UpdateSpawnText()
    {
        _spawnedObjectsCount.text = SpawnObjects + _spawner.SpawnedObjectsCount.ToString();
    }
}

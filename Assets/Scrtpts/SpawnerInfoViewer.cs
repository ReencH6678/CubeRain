using TMPro;
using UnityEngine;

public class SpawnerInfoViewer<T> : MonoBehaviour where T :MonoBehaviour, IPoolable
{
    private const string CreateadObjects = "Created Objects ";
    private const string ActivObjects = "ActivObjects ";
    private const string SpawnObjects = "Spawn Objects ";

    [SerializeField] private Spawner<T> _spawner;

    [SerializeField] private TextMeshProUGUI _activeObjectsCount;
    [SerializeField] private TextMeshProUGUI _createdObjectsCount;
    [SerializeField] private TextMeshProUGUI _spawnedObjectsCount;

    private void OnEnable()
    {
        _spawner.VelueChanged += UpdateText;
    }

    private void OnDisable()
    {
        _spawner.VelueChanged -= UpdateText;
    }

    private void UpdateText()
    {
        _createdObjectsCount.text = CreateadObjects + _spawner.CreatedObjectsCount.ToString();
        _activeObjectsCount.text = ActivObjects + _spawner.ActivObjectsCount.ToString();
        _spawnedObjectsCount.text = SpawnObjects + _spawner.SpawnedObjectsCount.ToString();
    }
}

using TMPro;
using UnityEngine;

public class SpawnerInfoViewer<T> : MonoBehaviour where T :MonoBehaviour, IPoolable
{
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
        _createdObjectsCount.text = "Created Objects " + _spawner.CreatedObjectsCount.ToString();
        _activeObjectsCount.text = "ActivObjects " + _spawner.ActivObjectsCount.ToString();
        _spawnedObjectsCount.text = "Spawn Objects " + _spawner.SpawnedObjectsCount.ToString();
    }
}

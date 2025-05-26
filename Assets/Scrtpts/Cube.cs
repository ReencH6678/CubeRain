using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    private float _maxLifeTime = 5;
    private float _minLifeTime = 2;

    private bool _haveColorChanged = false;

    private MeshRenderer _meshRenderer;

    public event UnityAction<Cube> Collised;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(DeteteObject());
    }

    private IEnumerator DeteteObject()
    {
        float minHue = 0, maxHue = 1;
        float minSaturation = 0, maxSaturation = 1;
        float minValue = 0, maxValue = 1;

        if (_haveColorChanged == false)
        {
            _meshRenderer.material.color = Random.ColorHSV(minHue, maxHue, minSaturation, maxSaturation, minValue, maxValue);
            _haveColorChanged = true;
        }

        yield return new WaitForSeconds(Random.Range(_minLifeTime, _maxLifeTime));

        _haveColorChanged = false;
        Collised?.Invoke(this);
    }
}



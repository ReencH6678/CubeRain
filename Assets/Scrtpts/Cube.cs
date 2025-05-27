using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{
    private const string PlatformTag = "Platform";

    [SerializeField] private Color _defoultColor;

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
        if(collision.gameObject.tag == PlatformTag)
        StartCoroutine(DeteteObject());
    }

    public void ResetColor()
    {
        _meshRenderer.material.color = _defoultColor;
    }

    private IEnumerator DeteteObject()
    {
        float minHue = 0;
        float maxHue = 1;
        float minSaturation = 0;
        float maxSaturation = 1;
        float minValue = 0;
        float maxValue = 1;

        _defoultColor = _meshRenderer.material.color;

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



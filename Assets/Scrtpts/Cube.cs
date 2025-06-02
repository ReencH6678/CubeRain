using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MeshRenderer), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private const string PlatformTag = "Platform";

    [SerializeField] private Color _defoultColor;
    [SerializeField] private Transform _defoultTransform;

    private float _maxLifeTime = 5;
    private float _minLifeTime = 2;

    private bool _haveColorChanged = false;

    private MeshRenderer _meshRenderer;
    private Rigidbody _rigidbody;

    public event UnityAction<Cube> Collised;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == PlatformTag)
        StartCoroutine(DeteteObject());
    }

    public void ResetObject()
    {
        _meshRenderer.material.color = _defoultColor;
        _rigidbody.velocity = Vector3.zero; 
        transform.rotation = _defoultTransform.rotation;
    }

    private IEnumerator DeteteObject()
    {
        float minHue = 0;
        float maxHue = 1;
        float minSaturation = 0;
        float maxSaturation = 1;
        float minValue = 0;
        float maxValue = 1;

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



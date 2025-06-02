using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ColorChanger), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private float _maxLifeTime = 5;
    private float _minLifeTime = 2;

    private bool _haveColorChanged = false;

    private ColorChanger _colorChanger;
    private Rigidbody _rigidbody;

    public event UnityAction<Cube> Collised;

    private void Awake()
    {
        _colorChanger = GetComponent<ColorChanger>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Platform>(out Platform platform))
            StartCoroutine(DeteteObject());
    }

    public void ResetObject()
    {
        _colorChanger.ResetColor();
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    private IEnumerator DeteteObject()
    {
        if (_haveColorChanged == false)
        {
            _colorChanger.ChangeColor();       
            _haveColorChanged = true;
        }

        yield return new WaitForSeconds(Random.Range(_minLifeTime, _maxLifeTime));

        _haveColorChanged = false;
        Collised?.Invoke(this);
    }
}



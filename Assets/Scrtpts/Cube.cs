using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ColorChanger), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    private float _maxLifeTime = 5;
    private float _minLifeTime = 2;

    private bool _haveCollised = false;

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
        if (collision.gameObject.TryGetComponent<Platform>(out Platform platform) && _haveCollised == false)
        {
            StartCoroutine(DeteteObject());
            _haveCollised = true;
        }
    }

    public void ResetObject()
    {
        _colorChanger.ResetColor();

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    private IEnumerator DeteteObject()
    {
        _colorChanger.ChangeColor();

        yield return new WaitForSeconds(Random.Range(_minLifeTime, _maxLifeTime));

        _haveCollised = false;
        Collised?.Invoke(this);
    }
}



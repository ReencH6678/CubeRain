using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Cube : MonoBehaviour
{
    public event UnityAction<GameObject> Collised;

    private float _maxLifeTime = 5;
    private float _minLifeTime = 2;

    private bool _haveColorChanged = false;

    private IEnumerator DeteteObject()
    {
        float minHue = 0, maxHue = 1;
        float minSaturation = 0, maxSaturation = 1;
        float minValue = 0, maxValue = 1;

        Material material = new Material(Shader.Find("Standard"));

        if (_haveColorChanged == false)
        {
            material.color = Random.ColorHSV(minHue, maxHue, minSaturation, maxSaturation, minValue, maxValue);
            gameObject.GetComponent<MeshRenderer>().material = material;
            _haveColorChanged = true;
        }

        yield return new WaitForSeconds(Random.Range(_minLifeTime, _maxLifeTime + 1));

        _haveColorChanged = false;
        Collised?.Invoke(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(DeteteObject());
    }
}



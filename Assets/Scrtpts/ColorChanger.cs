using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Color _defoultColor;

    private MeshRenderer _meshRenderer;

    private float _minHue = 0;
    private float _maxHue = 1;
    private float _minSaturation = 0;
    private float _maxSaturation = 1;
    private float _minValue = 0;
    private float _maxValue = 1;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    
    public void ChangeColor()
    {
        _meshRenderer.material.color = Random.ColorHSV(_minHue, _maxHue, _minSaturation, _maxSaturation, _minValue, _maxValue);
    }

    public void ResetColor()
    {
        _meshRenderer.material.color = _defoultColor;
    }
}

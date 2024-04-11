using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(IHealth))]
public class LifebarController : MonoBehaviour
{
    [SerializeField] private Canvas _lifebarPrefab;
    [SerializeField] private Vector3 _offset;
    private Transform _lifebarTransform;
    private IHealth _health;
    private Slider _slider;

    private void Start()
    {
        Vector3 totalOffset = transform.position + _offset;
        _lifebarTransform = Instantiate(_lifebarPrefab, totalOffset, Quaternion.identity, this.transform).transform;
        _health = GetComponent<IHealth>();
        _slider = _lifebarTransform.GetComponentInChildren<Slider>();
        _slider.maxValue = _health.MaxHealth;
        _slider.value = _health.CurrentHealth;
        _health.OnHealthChange += SetSlider;
        _health.OnDeath += DisableSlider;
    }

    private void OnDestroy()
    {
        _health.OnHealthChange -= SetSlider;
        _health.OnDeath -= DisableSlider;
    }

    private void LateUpdate() =>
        _lifebarTransform.LookAt(Camera.main.transform.position);

    private void SetSlider(int currentLife) =>
        _slider.value = currentLife;

    private void DisableSlider() =>
        _slider.gameObject.SetActive(false);
}

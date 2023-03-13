using UnityEngine;
using UnityEngine.UI;

public class Environment : MonoBehaviour
{
    [SerializeField] private float expGet;
    [SerializeField] private float _healthMax;
    private float _health;

    [SerializeField] private float _sliderCooldownMax;
    private float _sliderCooldownTimer;
    private bool _sliderCooldown;

    [SerializeField] private Image _healthSlider;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _devided;

    [HideInInspector]
    public bool Life = true;

    private void Start()
    {
        _health = _healthMax;
        UpdateSlider();
    }

    private void Update()
    {
        UpdateSlider();

        if(_sliderCooldown == true)
        {
            _canvas.SetActive(true);
            _sliderCooldownTimer++; 

            if(_sliderCooldownTimer >= _sliderCooldownMax)
            {
                _sliderCooldown = false;
                _sliderCooldownTimer = 0;
            }
        }
        else
        {
            _canvas.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>()) { }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>()) { }
    } 

    public void Damage(int damage, GrowMonster monster)
    {
        _sliderCooldown = true;

        _health -= damage;
        UpdateSlider();

        if (_health > 0 || Life == false) { return; }

        monster.Grow(expGet);
        Die();
    }

    private void UpdateSlider()
    {
        float yVelocity = 0.0f;
        _healthSlider.fillAmount = Mathf.SmoothDamp(_healthSlider.fillAmount, _health / _healthMax, ref yVelocity, 0.025f);
    } 

    private void Die()
    {
        Life = false;
        GameObject clone = Instantiate(_devided, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }
}

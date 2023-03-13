using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.UI;

public class GrowMonster : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _damageCooldownMax;

    [Space(10)]
    [SerializeField] private float _growLevelMax;
    public float _growLevel;

    [Space(10)]
    [SerializeField] private float _growMultiple;
    [SerializeField] private float _growSpeed;

    [Space(10)]
    [SerializeField] private float _expMultiple;
    [SerializeField] private float _levelUpExp;
    public float ExpForEnemy;
    [HideInInspector] public float _exp;

    [SerializeField] private int _score;
    [SerializeField] public Text _scoreText;
    [SerializeField] public Text _scoreTextWorld;

    [SerializeField] private SkinnedMeshRenderer _meshRenderer;
    private float _blendShapeGrowValue;
    private float _blendShapeValue;

    [SerializeField] private UnityEvent OnLoseEvents;

    [HideInInspector] public bool BeatsSomething;

    private List<Environment> _objects = new List<Environment>();
    private Vector3 _scaleMultiple = new Vector3(1, 1, 1);

    private bool _triggered;
    private bool _damageCooldown;

    private float _damageCooldownTimer;

    private void Start()
    {
        _scoreText.text = _score.ToString();
        _scoreTextWorld.text = _score.ToString();
        _blendShapeGrowValue = (_growLevel / _growLevelMax) * 100;
        _blendShapeValue = _meshRenderer.GetBlendShapeWeight(0);
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, _scaleMultiple, _growSpeed * Time.deltaTime);

        if (_objects.Count <= 0) { BeatsSomething = false; }
        else { BeatsSomething = true; }

        if (_triggered == false) { return; }

        if (_damageCooldown == true) 
        {
            if (_damageCooldownTimer >= _damageCooldownMax) { _damageCooldown = false; _damageCooldownTimer = 0; }
            _damageCooldownTimer++;
            return; 
        }

        _damageCooldown = true;
        foreach (Environment environment in _objects)
        {
            environment.Damage(_damage, this); 

            if(environment.Life == false)
                _objects.Remove(environment);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<GrowMonster>() == false) { return; }

        GrowMonster enemy = collision.gameObject.GetComponent<GrowMonster>();

        if (enemy._growLevel > _growLevel) { Lose(enemy); return; }
        if (enemy._growLevel < _growLevel) { enemy.Lose(this); return; }

        if (enemy._exp > _exp) { Lose(enemy); return; }
        if (enemy._exp < _exp) { enemy.Lose(this); return; }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Environment>() == false) { return; }

        Environment collisionHealth = other.GetComponent<Environment>();
        _objects.Add(collisionHealth);
        _triggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Environment>() == false) { return; }

        Environment collisionHealth = other.GetComponent<Environment>();
        foreach (Environment health in _objects)
        {
            if (health == collisionHealth) { _objects.Remove(health); }
        }
    }

    public void Grow(float exp)
    {
        _score += 20;
        _scoreText.text = _score.ToString();
        _scoreTextWorld.text = _score.ToString();

        if (_growLevel >= _growLevelMax) { return; }

        _exp += exp;

        if (_exp < _levelUpExp) { return; }

        _blendShapeValue -= _blendShapeGrowValue;
        _meshRenderer.SetBlendShapeWeight(0, _blendShapeValue);

        _growLevel++;
        _exp = 0;
        _levelUpExp *= _expMultiple;
        ExpForEnemy *= _expMultiple;

        _scaleMultiple *= _growMultiple;
        _damage *= 2;
    }

    public void Lose(GrowMonster enemyMonster)
    {
        enemyMonster._exp += ExpForEnemy; 
        OnLoseEvents.Invoke();
        gameObject.SetActive(false);
    }
}

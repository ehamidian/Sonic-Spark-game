using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class EnemyBar : MonoBehaviour
{
    Slider _enemyHealthSlider;

    private void Start()
    {
        _enemyHealthSlider = GetComponent<Slider>();
    }

    public void SetMaxHealth(float maxHealth)
    {
        if(_enemyHealthSlider == null)
            _enemyHealthSlider = GetComponent<Slider>();
            
        _enemyHealthSlider.maxValue = maxHealth;
        _enemyHealthSlider.value = maxHealth;
    }

    public void SetHealth(float health)
    {
        _enemyHealthSlider.value -= health;
    }

    public void ResetHealth(float health)
    {
        _enemyHealthSlider.value = health;
    }

    public bool IsEnemyDead()
    {
        return _enemyHealthSlider.value <= 0;
    }
}
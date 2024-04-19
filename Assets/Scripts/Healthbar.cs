using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    Slider _healthSlider;

    private void Start()
    {
        _healthSlider = GetComponent<Slider>();
    }

    public void SetMaxHealth(float maxHealth)
    {
        // _healthSlider.maxValue = maxHealth;
        _healthSlider.value = maxHealth;
    }

    // Collision with box
    public void SetHealthOnBox(float health)
    {
        _healthSlider.value -= health;

        if (_healthSlider.value == _healthSlider.maxValue)
        {
            SoundEFManager.instance.PlaySoundEffect("health");
        }
    }

    // Collision with flame
    public void SetHealthOnFlame(float health)
    {
        _healthSlider.value += health;
    }

    // Collision with enemy
    public void SetHealthOnEnemy(float health, bool hitPlayer)
    {
        if (hitPlayer)
            _healthSlider.value -= health;
        else
            _healthSlider.value += health;
    }

    public int GetHealthCount()
    {
        return (int)_healthSlider.value;
    }
}
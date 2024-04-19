using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class BulletBar : MonoBehaviour
{
    Slider _bulletSlider;

    private void Start()
    {
        _bulletSlider = GetComponent<Slider>();
    }

    public void SetMaxBullet(float maxBullet)
    {
        _bulletSlider.value = maxBullet;
    }

    public void SetBullet(float bullet, bool isShooting = false)
    {
        if (isShooting)
            _bulletSlider.value -= bullet;
        else
            _bulletSlider.value += bullet;
    }

    public int GetBulletCount()
    {
        return (int)_bulletSlider.value;
    }
}
using UnityEngine;

public class ProjectileAddon : MonoBehaviour
{
    Rigidbody rb;
    bool targetHit;
    int damage = 40;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Make sure only to stick to the first target you hit
        if (targetHit)
            return;
        else
            targetHit = true;

        // Check if you hit an enemy
        if (collision.gameObject.GetComponent<HitEnemy>())
        {
            HitEnemy damage = collision.gameObject.GetComponent<HitEnemy>();

            // Damage enemy
            damage.TakeDamage(this.damage);

            // Destroy projectile after 0.1 seconds
            Invoke(nameof(DestroyProjectile), 0.1f);
        }


        // Make sure projectile sticks to surface
        rb.isKinematic = true;

        // Make sure projectile moves with target
        transform.SetParent(collision.transform);
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
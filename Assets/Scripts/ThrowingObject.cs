using UnityEngine;

public class ThrowingObject : MonoBehaviour
{

    [Header("References")]
    public Transform attackPoint;
    public GameObject objectToThrow;
    [SerializeField] BulletBar bulletBar;
    public PlayerScore playerScore;

    [Header("Settings")]
    public float throwCooldown = 0.5f;

    [Header("Throwing")]
    public float throwForce = 70f;
    public float throwUpwardForce = 10f;

    bool readyToThrow;
    GameObject[] enemies;
    float distanceToTarget;
    int totalThrows = 0;

    void Start()
    {
        readyToThrow = true;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length > 0 && !playerScore.IsGameOver())
        {
            GetDistanceToThrow(enemies);

            if(bulletBar != null)
            {
                totalThrows = bulletBar.GetBulletCount();
            }
        }
    }

private void GetDistanceToThrow(GameObject[] p_enemies)
    {
        float throwDistanceThreshold = 150f;

        // Loop through all enemies
        foreach (GameObject enemy in p_enemies)
        {
            if (enemy != null)
            {
                // Get distance to the target
                distanceToTarget = Vector3.Distance(attackPoint.position, enemy.transform.position);
                Vector3 throwerToEnemy = enemy.transform.position - attackPoint.position;
                float angle = Vector3.Angle(attackPoint.forward, throwerToEnemy);

                if (distanceToTarget <= throwDistanceThreshold && readyToThrow && totalThrows > 0)
                {
                    // Check if the target is tagged as enemy
                    if (enemy.CompareTag("Enemy") && angle < 90.0f) // Check if the enemy is in front of the thrower
                    {
                        Throw(enemy);
                    }
                }
            }
        }
    }

    private void Throw(GameObject p_gameObject)
    {
        readyToThrow = false;

        // Instantiate object to throw
        GameObject thrownObject = Instantiate(objectToThrow, attackPoint.position, attackPoint.rotation);

        // Get rigidbody component
        Rigidbody rb = thrownObject.GetComponent<Rigidbody>();

        // Calculate direction
        Vector3 forceDirection = attackPoint.forward;

        if (Physics.Raycast(attackPoint.position, attackPoint.forward, out RaycastHit raycastHit, 500f))
        {
            forceDirection = (raycastHit.point - attackPoint.position).normalized;
        }

        // Add force to object
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        rb.AddForce(forceToAdd, ForceMode.Impulse);

        //totalThrows--;
        bulletBar.SetBullet(1, true);

        SoundEFManager.instance.PlaySoundEffect("fire");

        // Destroy the thrown object
        Destroy(thrownObject, 2f);

        // Implement cooldown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    private void ResetThrow()
    {
        readyToThrow = true;
    }
}
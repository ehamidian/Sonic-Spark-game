
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GameMechanic : MonoBehaviour
{
    private Rigidbody rb;
    public PlayerScore playerScore;
    public FollowPlayer followPlayer;
    public float forwardSpeed;
    public float scaleFactor = 3.0f;
    public ParticleSystem leftParticleSystem;
    public ParticleSystem rightParticleSystem;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Ensure rotation is frozen
        rb.useGravity = true;

        if (leftParticleSystem == null || rightParticleSystem == null)
            Debug.LogError("Particle systems not assigned!");

        leftParticleSystem.Stop();
        rightParticleSystem.Stop();
    }

    void Update()
    {
        if (!playerScore.IsGameOver())
        {
            int loudness = AudioLoudnessDetection.Instance.audioLoudness;
            forwardSpeed = SpeedController.Instance.GetCurrentSpeed();

            MovePlayer(loudness);
        }
    }

    // Move the player based on voice direction
    void MovePlayer(int loudness)
    {
        float targetLateralVelocity = 0f;
        float lateralSpeed = 100f;
        int[] leftDirections = { 1, 2, 3 };
        int[] rightDirections = { 4, 5, 6 };

        foreach (int direction in leftDirections)
        {
            if (loudness == direction)
            {
                leftParticleSystem.Play();
                rightParticleSystem.Stop();
                break;
            }
        }

        foreach (int direction in rightDirections)
        {
            if (loudness == direction)
            {
                rightParticleSystem.Play();
                leftParticleSystem.Stop();
                break;
            }
        }

        switch (loudness)
        {
            case 1:
                targetLateralVelocity = -lateralSpeed;
                break;
            case 2:
                targetLateralVelocity = -lateralSpeed * 2;
                break;
            case 3:
                targetLateralVelocity = -lateralSpeed * 3;
                break;
            case 4:
                targetLateralVelocity = lateralSpeed;
                break;
            case 5:
                targetLateralVelocity = lateralSpeed * 2;
                break;
            case 6:
                targetLateralVelocity = lateralSpeed * 3;
                break;
            default:
                targetLateralVelocity = 0;
                leftParticleSystem.Stop();
                rightParticleSystem.Stop();
                break;
        }

        if (loudness != 0)
        {
            SoundEFManager.instance.PlaySoundEffect("move");
        }

        // Use Mathf.Sign to ensure the correct sign for the lateral velocity
        targetLateralVelocity *= Mathf.Sign(transform.localScale.x);

        // Gradually interpolate between current and target velocities
        Vector3 targetVelocity = new Vector3(targetLateralVelocity, 0, forwardSpeed);
        rb.velocity = Vector3.Lerp(rb.velocity, targetVelocity, Time.deltaTime * scaleFactor);
    }

    void GameOver()
    {
        if (followPlayer == null)
        {
            Debug.LogError("followPlayer is null!");
            return;
        }

        playerScore.ShowGameOver();
        followPlayer.GameOver();  // Call the GameOver method on the FollowPlayer script
        StopPlayerAndCamera();
    }

    void StopPlayerAndCamera()
    {
        // Stop the player
        rb.velocity = Vector3.zero;

        // Stop the camera follow
        followPlayer.enabled = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (playerScore == null)
        {
            Debug.LogError("playerScore is null!");
            return;
        }

        if (collision.gameObject.CompareTag("Mountain"))
        {
            SoundEFManager.instance.PlaySoundEffect("mountain");
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerScore.CollideWithEnemy();
            SoundEFManager.instance.PlaySoundEffect("hit");
            Destroy(collision.gameObject);

             if(playerScore.IsGameOver())
            {
                SoundEFManager.instance.PlaySoundEffect("lose");
                GameOver();
            }
        }

        if (collision.gameObject.CompareTag("Candle"))
        {
            //Debug.Log("Collision with Candle!");
            playerScore.CollectCandle();
            SoundEFManager.instance.PlaySoundEffect("flame");
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Box"))
        {
            //Debug.Log("Collision with Obstacle!");
            playerScore.CollideWithBox();
            SoundEFManager.instance.PlaySoundEffect("hit");
            Destroy(collision.gameObject);

            // Check for game over after colliding with the box
            if (playerScore.IsGameOver())
            {
                SoundEFManager.instance.PlaySoundEffect("lose");
                GameOver();
            }
        }

        // if (collision.gameObject.CompareTag("Road")) // Assuming you've tagged your road objects as "Road"
        // {
        //     // Option 2: Prevent Movement
        //     // To prevent the player from moving off the road, you can freeze certain axes of movement (e.g., Y-axis if the road is flat).
        //     rb.constraints = RigidbodyConstraints.FreezePositionY;
        // }
    }
}

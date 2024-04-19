using UnityEngine;

public class SpeedController : MonoBehaviour
{
    public static SpeedController Instance;
    private readonly float initialSpeed = 40f;
    private readonly float timeToSpeedUp = 20f;
    private readonly float speedIncreaseAmount = 2f;
    private float elapsedTime = 0f;
    private float currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // Set the instance
        if (Instance == null)
            Instance = this;

        currentSpeed = initialSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the elapsed time
        elapsedTime += Time.deltaTime;

        // Check if it's time to speed up
        if (elapsedTime >= timeToSpeedUp)
        {
            // Increase the speed
            currentSpeed += speedIncreaseAmount;

            // Reset the elapsed time
            elapsedTime = 0f;
        }
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
}
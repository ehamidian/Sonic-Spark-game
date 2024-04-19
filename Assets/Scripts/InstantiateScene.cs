using UnityEngine;
using UnityEngine.SceneManagement;

public class InstantiateScene : MonoBehaviour
{
    public GameObject scenePrefab; // Reference to the scene prefab

    Transform player;
    readonly float triggerDistance = 100f; // Distance from the end to trigger the scene instantiation
    float distanceToTrigger;
    GameObject previousScene; // Reference to the previously instantiated scene object

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Check the distance between player and the trigger zone
        distanceToTrigger = Vector3.Distance(transform.position, player.position);

        if (distanceToTrigger < triggerDistance)
        {
            // Destroy the previous scene if it exists
            if (previousScene != null)
            {
                Destroy(previousScene);
            }

            // Instantiate a new scene prefab
            GameObject sceneObject = Instantiate(scenePrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z + triggerDistance * 5), Quaternion.identity);

            // Set the reference to the new scene object
            previousScene = sceneObject;

            // Move the trigger zone ahead
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + triggerDistance * 40);

            Debug.Log("Scene instantiated at " + transform.position);
        }
    }
}
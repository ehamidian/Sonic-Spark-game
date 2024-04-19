using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game") // Replace YourThirdSceneName with the actual name of your third scene
        {
            Destroy(gameObject); // Stops the music by destroying the GameObject
        }
    }
}

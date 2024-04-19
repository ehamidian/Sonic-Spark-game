using UnityEngine;

public class SoundEFManager : MonoBehaviour
{
    public static SoundEFManager instance;
    public AudioClip hitClip;
    public AudioClip flameClip;
    public AudioClip loseClip;
    public AudioClip moveClip;
    public AudioClip mountainClip;
    public AudioClip healthClip;
    public AudioClip throwingClip;
    public AudioClip enemyClip;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
    }

    public void PlaySoundEffect(string filter)
    {
        AudioClip audioClip = null;

        switch (filter)
        {
            case "hit":
                audioClip = hitClip;
                break;
            case "flame":
                audioClip = flameClip;
                break;
            case "lose":
                audioClip = loseClip;
                break;
            case "move":
                audioClip = moveClip;
                break;
            case "mountain":
                audioClip = mountainClip;
                break;
            case "health":
                audioClip = healthClip;
                break;
            case "fire":
                audioClip = throwingClip;
                break;
            case "enemy":
                audioClip = enemyClip;
                break;
        }

        if (audioClip!=null)
        {
            audioSource.PlayOneShot(audioClip);
        }
        else
        {
            Debug.LogError("AudioClip is null!");
        }
    }
}
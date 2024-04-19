using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InstructionController : MonoBehaviour
{
    public GameObject[] instructionPanels;
    public Button nextButton1;
    public Button nextButton2;
    public Button nextButton3;
    public Button nextButton4;
    public Button playButton;
    private int currentPanelIndex = 0;

    private void Start()
    {
        // Freeze the game initially
        Time.timeScale = 0;

        ShowPanel(currentPanelIndex);
        playButton.interactable = false;
        nextButton1.onClick.AddListener(() => NextButtonClicked(1));
        nextButton2.onClick.AddListener(() => NextButtonClicked(2));
        nextButton3.onClick.AddListener(() => NextButtonClicked(3));
        nextButton4.onClick.AddListener(() => NextButtonClicked(4));
        playButton.onClick.AddListener(PlayButtonClicked);
    }

    private void NextButtonClicked(int nextIndex)
    {
        HidePanel(currentPanelIndex);
        currentPanelIndex = nextIndex;

        if (currentPanelIndex < instructionPanels.Length)
        {
            ShowPanel(currentPanelIndex);

            // Check if it's the last instruction panel
            if (currentPanelIndex == instructionPanels.Length - 1)
            {
                playButton.interactable = true;
                nextButton4.gameObject.SetActive(false); // Hide the next button on the last panel
            }
            else
            {
                nextButton1.gameObject.SetActive(currentPanelIndex == 0);
                nextButton2.gameObject.SetActive(currentPanelIndex == 1);
                nextButton3.gameObject.SetActive(currentPanelIndex == 2);
                nextButton4.gameObject.SetActive(currentPanelIndex == 3);
            }
        }
    }

    private void PlayButtonClicked()
    {
        // Hide all instruction panels
        foreach (var panel in instructionPanels)
        {
            panel.SetActive(false);
        }

        // Unfreeze the game and start the game logic
        Time.timeScale = 1;
        SceneManager.LoadScene("Game"); // Replace with your game scene name

        Debug.Log("Play button clicked, loading game scene.");
    }

    private void ShowPanel(int index)
    {
        if (index >= 0 && index < instructionPanels.Length)
        {
            instructionPanels[index].SetActive(true);
        }
    }

    private void HidePanel(int index)
    {
        if (index >= 0 && index < instructionPanels.Length)
        {
            instructionPanels[index].SetActive(false);
        }
    }
}

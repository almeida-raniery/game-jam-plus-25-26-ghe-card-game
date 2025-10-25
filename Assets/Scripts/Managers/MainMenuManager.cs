using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] PlayableAsset beginGameTimeline;
    [SerializeField] PlayableDirector director;

    [SerializeField] Button beginGameButton;
    [SerializeField] Button creditsButton;
    [SerializeField] Button exitGameButton;

    public void BeginGameSequence() 
    {
        director.Play();

        beginGameButton.interactable = false;
        creditsButton.interactable = false;
        exitGameButton.interactable = false;
    }

    public void BeginGame()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void ExitGame() 
    {
        Application.Quit();
    }
}

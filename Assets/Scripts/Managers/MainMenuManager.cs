using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] PlayableAsset beginGameTimeline;
    [SerializeField] PlayableAsset AfterIntroTimeline;
    [SerializeField] PlayableDirector director;

    [SerializeField] Button beginGameButton;
    [SerializeField] Button creditsButton;
    [SerializeField] Button exitGameButton;


    private void Start()
    {
        director.Play();
    }

    public void BeginGameSequence()
    {
        director.playableAsset = beginGameTimeline;
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

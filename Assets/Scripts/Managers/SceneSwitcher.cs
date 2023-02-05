using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public static SceneSwitcher Instance;
    [SerializeField]
    private Animator transition;

    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameMusic;
    [SerializeField] private AudioClip[] shingSounds;
    
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            SoundManager.Instance.PlayMusic(menuMusic);
        }
        else
        {
            SoundManager.Instance.PlayMusic(gameMusic);
        }
    }

    public void OnButtonClickedSound()
    {
        SoundManager.Instance.PlayRandom(shingSounds);
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadSceneCo(sceneIndex));
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadSceneCo(SceneManager.GetActiveScene().buildIndex + 1));
    }
    
    public void LoadPreviousScene()
    {
        StartCoroutine(LoadSceneCo(SceneManager.GetActiveScene().buildIndex - 1));
    }
    
    public void ReloadScene()
    {
        StartCoroutine(LoadSceneCo(SceneManager.GetActiveScene().buildIndex));
    }
    
    public void Quit()
    {
        Application.Quit();
    }
    
    
    
    IEnumerator LoadSceneCo(int sceneNum)
    {
        if(sceneNum < 0)
            sceneNum = 0;
        if(sceneNum >= SceneManager.sceneCountInBuildSettings)
            sceneNum = SceneManager.sceneCountInBuildSettings - 1;
        
        transition.Play("SceneEnd");
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene(sceneNum);
    }
}
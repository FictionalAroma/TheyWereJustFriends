using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    private static PauseMenuManager _instance;
    [SerializeField] private GameObject pauseScreen;

    private bool isPaused;

    public static PauseMenuManager Instance => _instance;

    void Start()
    {
        _instance = this;
        Resume();
    }

    public void OpenPauseMenu()
    {
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
        isPaused = true;
    }
    public void Resume()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
        pauseScreen.SetActive(false);

    }
    public void QuitApplication()
    {
        Application.Quit();
        Debug.Log("Escaping");
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Resume();
    }


    //this is for other scripts potentially
    //i know theres probably loads of safer and simplers ways to do this but ehhhh
    public void TogglePause() 
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            OpenPauseMenu();
        }
    }



}

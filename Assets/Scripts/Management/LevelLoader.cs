using UnityEngine;
using UnityEngine.SceneManagement;

namespace Management
{
    public class LevelLoader : MonoBehaviour
    {
        private int _currentSceneIndex;
        public static void LoadStartScreen()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public static void GoToMainGame()
        {
            SceneManager.LoadScene("MainGame");
        }

        private void Start()
        {
            _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        }

        internal static void ExitGame()
        {
            Application.Quit();
        }
    }
}

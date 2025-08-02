using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsToMainMenu : MonoBehaviour
{
    [SerializeField] KeyCode[] keys = { KeyCode.Escape, KeyCode.Mouse1};
    [SerializeField] string mainMenuScene = "MainMenu";

    private void Update()
    {
        foreach (var key in keys)
        {
            if (Input.GetKeyDown(key))
            {
                SceneManager.LoadScene(mainMenuScene);
            }
        }
    }
}


using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    static bool freshScene = true;

    [SerializeField] GameObject BgImage;
    [SerializeField] GameObject[] mainMenuObjects;
    [SerializeField] SelectWithKeys selectScript;
    [SerializeField] Sprite Bg;
    [SerializeField] Sprite BgBlurred;
    [SerializeField] AudioClip selectSound;
    public TMPro.TMP_Text pressAnyButton;
    [SerializeField] float startDelay = 0.5f;

    private AudioSource source;

    void Start()
    {
        selectScript = GetComponent<SelectWithKeys>();
        pressAnyButton.enabled = true;
        BgImage.GetComponent<Image>().sprite = BgBlurred;
        foreach(GameObject obj in mainMenuObjects)
        {
            obj.SetActive(false);
        }
        selectScript.enabled = false;
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!freshScene)
        {
            StartCoroutine(pressAnyButtonToPlay(0));
        }
        else if (Input.anyKeyDown)
        {
            StartCoroutine(pressAnyButtonToPlay(startDelay));
        }
    }

    IEnumerator pressAnyButtonToPlay(float delay)
    {
        pressAnyButton.enabled = false;
        yield return new WaitForSeconds(delay);
        BgImage.GetComponent<Image>().sprite = Bg;
        foreach (GameObject obj in mainMenuObjects)
        {
            obj.SetActive(true);
            yield return new WaitForSeconds(delay);
        }
        selectScript.enabled = true;
        freshScene = false;
    }


    // Button Function
    public void start()
    {
        Debug.Log("start");
        source.PlayOneShot(selectSound);
        StartCoroutine(loadSceneAfterDelay());
    }
    public void settings()
    {
        Debug.Log("settings");
        source.PlayOneShot(selectSound);
    }
    public void quit()
    {
        Debug.Log("quit");
        source.PlayOneShot(selectSound);
        StartCoroutine(quitAfterDelay());
    }

    IEnumerator loadSceneAfterDelay()
    {
        yield return new WaitForSeconds(selectSound.length);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator quitAfterDelay()
    {
        yield return new WaitForSeconds(selectSound.length);
        Application.Quit();
    }
}

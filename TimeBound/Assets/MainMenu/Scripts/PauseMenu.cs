using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Adjustments")]
    [SerializeField] KeyCode pauseKey = KeyCode.Escape;
    [SerializeField] bool paused = false;

    [Header("References")]
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SFXSlider;
    [SerializeField] Slider NarratorSlider;
    [SerializeField] Button quitButton;
    [SerializeField] Button resetButton;
    [SerializeField] GameObject cross;
    [SerializeField] GameObject reset;

    private Canvas canvas;
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        cross.SetActive(false);
        reset.SetActive(false);
        musicSlider.value = AudioData.Instance.musicVolume;
        SFXSlider.value = AudioData.Instance.sfxVolume;
        NarratorSlider.value = AudioData.Instance.narratorVolume;
    }

    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            paused = !paused;
            canvas.enabled = paused;
        }
        if(InputState.currentState == ControlState.Keyboard)
        {
            if (EventSystem.current.currentSelectedGameObject == quitButton.gameObject)
            {
                EnableDisablecross(true);
            }
            else
            {
                EnableDisablecross(false);
            }

            if (EventSystem.current.currentSelectedGameObject == resetButton.gameObject)
            {
                EnableDisableReset(true);
            }
            else
            {
                EnableDisableReset(false);
            }
        }
    }

    public void ChangeMusicVolume()
    {
        AudioData.Instance.musicVolume = musicSlider.value;
    }

    public void ChangeSFXVolume()
    {
        AudioData.Instance.sfxVolume = SFXSlider.value;
    }
    public void ChangeNarratorVolume()
    {
        AudioData.Instance.narratorVolume = NarratorSlider.value;
    }

    public void quit()
    {
        Application.Quit();
    }

    public void ResetLvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EnableDisablecross(bool enable)
    {
        cross.SetActive(enable);
    }
    public void EnableDisableReset(bool enable)
    {
        reset.SetActive(enable);
    }
}

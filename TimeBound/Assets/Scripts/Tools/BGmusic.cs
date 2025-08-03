using UnityEngine;
using UnityEngine.SceneManagement;

public class BGmusic : MonoBehaviour
{
    public static BGmusic instance;
    [SerializeField] string menu = "MainMenu";
    [SerializeField] string startScene = "BRP Sample Scene";
    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip BGMusic;

    private AudioSource source;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == menu)
        {
            if(source.clip != menuMusic)
            {
                source.clip = menuMusic;
                source.Play();
            }
        }
        else if (SceneManager.GetActiveScene().name == startScene)
        {
            if (source.clip != BGMusic)
            {
                source.clip = BGMusic;
                source.Play();
            }
        }
    }
}

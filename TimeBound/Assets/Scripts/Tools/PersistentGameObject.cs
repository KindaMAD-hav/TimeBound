using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentGameObject : MonoBehaviour
{
    private void Awake()
    {
            DontDestroyOnLoad(gameObject);
    }
}

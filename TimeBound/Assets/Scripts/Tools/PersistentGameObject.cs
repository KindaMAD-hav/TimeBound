using UnityEngine;

public class PersistentGameObject : MonoBehaviour
{
    public static PersistentGameObject instance;

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
    }
}

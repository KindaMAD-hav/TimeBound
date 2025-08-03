using UnityEngine;

public class VineAnimData : MonoBehaviour
{
    [SerializeField] Material mat;
    [Range(-0.15f, 1f)]
    [SerializeField] float GrowVal;

    private void Update()
    {
        mat.SetFloat("Grow",GrowVal);
    }
}

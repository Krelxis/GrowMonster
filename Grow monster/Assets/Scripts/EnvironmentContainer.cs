using UnityEngine;

public class EnvironmentContainer : MonoBehaviour
{
    public static EnvironmentContainer Instance;

    public GameObject[] Environments = new GameObject[3];

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}

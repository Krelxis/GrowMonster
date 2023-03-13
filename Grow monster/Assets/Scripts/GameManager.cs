using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private UnityEvent _onStartEvents;
    [SerializeField] private UnityEvent _onFightEvents; 

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void StartGame()
    {
        _onStartEvents.Invoke();
    }
    public void StartFight()
    {
        _onFightEvents.Invoke();
    }
}

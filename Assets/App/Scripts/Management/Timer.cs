using UnityEngine;
using UnityEngine.SceneManagement;
public class Timer : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float _time = 10f;

    [Header("References")]
    public RSO_Timer RSO_Timer;

    private void Start()
    {
        RSO_Timer.Value = _time;
    }

    private void Update()
    {
        RSO_Timer.Value -= Time.deltaTime;

        if (RSO_Timer.Value <= 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
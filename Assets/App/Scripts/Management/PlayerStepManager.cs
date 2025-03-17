using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStepManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] int maxStepPossibleCount;
    int currentStepCount;

    [SerializeField] string sceneToLoadOnLose;

    //[Header("References")]

    //[Space(10)]
    // RSO
    // RSF
    // RSP

    [Header("Input")]
    [SerializeField] RSE_OnPlayerMove rseOnPlayerMove;

    [Header("Output")]
    [SerializeField] RSE_UpdateStepCountVisual rseUpdateStepCountVisual;

    private void OnEnable()
    {
        rseOnPlayerMove.Action += CheckPlayerStepCount;
    }
    private void OnDisable()
    {
        rseOnPlayerMove.Action -= CheckPlayerStepCount;
    }

    private void Start()
    {
        rseUpdateStepCountVisual.Call(currentStepCount, maxStepPossibleCount);
    }

    void CheckPlayerStepCount(Vector3Int playerPosition)
    {
        currentStepCount++;
        rseUpdateStepCountVisual.Call(currentStepCount, maxStepPossibleCount);

        if(currentStepCount >= maxStepPossibleCount)
        {
            SceneManager.LoadScene(sceneToLoadOnLose);
        }
    }
}
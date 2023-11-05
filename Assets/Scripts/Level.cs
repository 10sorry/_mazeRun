using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private Player player;
    [SerializeField] private Exit exitFromLevel;

    private bool gameIsEnded;
    private int currentLevel;

    private void Start()
    {
        Application.targetFrameRate = 360;
        QualitySettings.vSyncCount = 0;
        exitFromLevel.Close();
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        if (gameIsEnded)
            return;

        LookAtPlayerHealth();
        LookAtPlayerInventory();
        TryCompleteLevel();
        
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void TryCompleteLevel()
    {
        if (!exitFromLevel.IsOpen)
            return;

        var position = exitFromLevel.transform.position;
        var flatExitPosition = new Vector2(position.x, position.z);
        var position1 = player.transform.position;
        var flatPlayerPosition = new Vector2(position1.x, position1.z);

        if (Mathf.Approximately(flatExitPosition.x, flatPlayerPosition.x) && Mathf.Approximately(flatExitPosition.y, flatPlayerPosition.y))
            Victory();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void LookAtPlayerHealth()
    {
        if (player.IsAlive)
            return;

        Lose();
    }

    public void LookAtPlayerInventory()
    {
        if (player.HasKey)
            exitFromLevel.Open();
    }

    private void Victory()
    {
        MusicControllerScript.Instance.PlaySound("victory");
        UnlockLevel();
        SceneManager.LoadScene(currentLevel + 1);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void UnlockLevel()
    {
        if (currentLevel < PlayerPrefs.GetInt("levels")) return;
        PlayerPrefs.SetInt("levels", currentLevel);
        Debug.Log(PlayerPrefs.GetInt("levels"));
    }

    public void Lose()
    {
        MusicControllerScript.Instance.PlaySound("loose");
        gameIsEnded = true;
        SceneManager.LoadScene(currentLevel);
    }
}

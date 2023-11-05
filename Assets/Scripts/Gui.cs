
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Gui : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private bool timerIsOn;
    [SerializeField] private float timerValue;
    [SerializeField] private TextMeshProUGUI timerView;
    private float _timer = 0;

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI keyPoint;
    [SerializeField] private GameObject keey;
    
    

    [SerializeField] private Level level;
    [SerializeField] private Player player;
    [SerializeField] private Key key;
    
    public int KeyPoint { get; set; } = 0;
    
 
    
    private void Awake()
    {

        keey.SetActive(true);
        keyPoint.text = KeyPoint.ToString();
       
        _timer = timerValue;

    }
    

    private void Update()
    {
        
        TimerTick();
        if (player.HasKey)
        {
            keyPoint.text = KeyPoint.ToString();
            
        }
    }



    public void sceneChanger()
    {
        SceneManager.LoadScene("Level menu");
    }

    public void returnToMainMenu()
    {
        SceneManager.LoadScene("Main menu");
    }

    
    private void TimerTick()
    {
        if(timerIsOn == false)
            return;
        
        _timer -= Time.deltaTime;
        timerView.text = $"{_timer:F1}";

        if (_timer <= 0)
            level.Lose();
    }
    
    public void ExitGame()
    {
        // Выход из игры
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}



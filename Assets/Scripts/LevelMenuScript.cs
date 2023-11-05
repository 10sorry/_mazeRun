using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelMenuScript : MonoBehaviour
{
    public static LevelMenuScript Instance;
    private AudioSource levelMusic;
    private bool isPlaying = false;
    
    private int level;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Удаляем дублирующиеся экземпляры
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        levelMusic = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }

    private void Update()
    {
        level = SceneManager.GetActiveScene().buildIndex;
        
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (IsInLevelRange(scene.name))
        {
            if (!isPlaying)
            {
                PlayMusic();
            }
        }
        else
        {
            StopMusic();
        }
    }

    private bool IsInLevelRange(string sceneName)
    {
        int levelIndex;
        if (int.TryParse(sceneName.Replace("level", ""), out levelIndex))
        {
            return levelIndex >= 2 && levelIndex <= 12;
        }
        return false;
    }

    private void PlayMusic()
    {
        if (!levelMusic.isPlaying)
        {
            levelMusic.Play();
            isPlaying = true;
        }
    }

    private void StopMusic()
    {
        levelMusic.Stop();
        isPlaying = false;
    }
}
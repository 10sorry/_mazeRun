
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicControllerScript : MonoBehaviour
{
    public static MusicControllerScript Instance;

    [SerializeField] private AudioSource musicMenuSource, levelMusic, sfxSource;
    private int currentLevel;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Удаляем дублирующиеся экземпляры
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }

    private void Update()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(currentLevel);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Main menu" && scene.name != "Level menu")
        {
            StopBackgroundMusic(musicMenuSource);
            PlayBackgroundMusic(levelMusic);
            
        }

        else if (!musicMenuSource.isPlaying)
        {
            StopBackgroundMusic(levelMusic);
            PlayBackgroundMusic(musicMenuSource);
        }
    }
    


    private void PlayBackgroundMusic(AudioSource audioSource)
    {
        audioSource.Play();
    }

    private void StopBackgroundMusic(AudioSource audioSource)
    {
        audioSource.Stop();
    }
    


    public void PlaySound(string clip)
    {
        sfxSource.PlayOneShot((AudioClip)Resources.Load(clip));
    }
    
}    

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    int _levelUnlock;
    int firstLevel = 1;
    [SerializeField] private Button[] buttons;

    private void Start()
    {
        _levelUnlock = PlayerPrefs.GetInt("levels", firstLevel);
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        for(int i = 0; i < _levelUnlock; i++)
        {
            buttons[i].interactable = true;
        }
    }
    

    public void ResetPrefs()
    {
        PlayerPrefs.DeleteAll();
        firstLevel = 1;
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}

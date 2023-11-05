using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelText : MonoBehaviour
{
    public TextMeshProUGUI tmpro;

    private void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        tmpro.text = sceneName;
    }
}

using System;
using UnityEngine;

public class WrongPlatforms : MonoBehaviour
{
    [SerializeField] Level _level;
    [SerializeField] private bool isOnLevel3;
    
    [SerializeField] private Color newColor; // цвет, на который нужно изменить материал платформы.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Renderer platformRenderer = GetComponent<Renderer>();

            if (platformRenderer != null)
            {
                Material platformMaterial = platformRenderer.material;

                if (platformMaterial != null)
                {
                    platformMaterial.color = newColor;
                    _level.Lose();// Меняем цвет материала на новый.
                }
            }
        }
    }
}

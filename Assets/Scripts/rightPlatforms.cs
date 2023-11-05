using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightPlatforms : MonoBehaviour
{
    [SerializeField] private Color newColor; // цвет, на который нужно изменить материал платформы
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
                    platformMaterial.color = newColor; // Меняем цвет материала на новый.
                }
            }
        }
    }
}

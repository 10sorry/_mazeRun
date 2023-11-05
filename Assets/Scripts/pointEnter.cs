using UnityEngine;

public class pointEnter : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public void PointClick()
    {
        audioSource.PlayOneShot((AudioClip)Resources.Load("pointer"));
    }
}

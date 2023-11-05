using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private Gui gui;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            MusicControllerScript.Instance.PlaySound("key");
            gui.KeyPoint += 1;
            Debug.Log(gui.KeyPoint);
            player.PickUpKey();
            Destroy(gameObject);
        }
    }
}

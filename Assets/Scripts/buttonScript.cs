using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public bool _buttonIsActivated = false;

    public void ActivateButton()
    {
        _buttonIsActivated = true;
    }
}

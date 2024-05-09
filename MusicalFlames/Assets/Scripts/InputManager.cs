using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [HideInInspector]
    public bool inputAllowed = false;

    public static InputManager Instance { get { return _instance; } }
    private static InputManager _instance;

    private void Awake()
    {
        _instance = this;
    }

    public void KeyPressed(int keyCode)
    {
        if (inputAllowed)
        {
            FlameManager.Instance.StartCoroutine("DisplayInput", keyCode);
        }
    }
}
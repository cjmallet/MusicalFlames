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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KeyPressed(int keyCode)
    {
        if (inputAllowed)
        {
            FlameManager.Instance.StartCoroutine("DisplayInput", keyCode);
        }
    }
}
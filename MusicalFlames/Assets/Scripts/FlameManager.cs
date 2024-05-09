using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlameManager : MonoBehaviour
{
    public static FlameManager Instance { get { return _instance; } }
    private static FlameManager _instance;

    [SerializeField]
    private float waitTimeBetweenFlames;
    private List<Transform> flames = new List<Transform>();

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            flames.Add(child);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator DisplayCandle(int flameToDisplay)
    {
        switch (flameToDisplay)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
                flames[flameToDisplay].GetComponent<Image>().enabled = true;
                yield return new WaitForSeconds(waitTimeBetweenFlames);
                flames[flameToDisplay].GetComponent<Image>().enabled = false;
                break;

            case 5:

                break;
            case 6:

                break;
            case 7:

                break;
            case 8:

                break;
        }
    }
}

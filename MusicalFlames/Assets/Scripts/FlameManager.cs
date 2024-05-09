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

    public IEnumerator DisplayCandle(int flamesToDisplay)
    {
        if (flamesToDisplay<5)
        {
            flames[flamesToDisplay].GetComponent<Image>().enabled = true;
            yield return new WaitForSeconds(waitTimeBetweenFlames);
            flames[flamesToDisplay].GetComponent<Image>().enabled = false;
        }
        else
        {
            flames[flamesToDisplay - 5].GetComponent<Image>().enabled = true;
            flames[flamesToDisplay - 4].GetComponent<Image>().enabled = true;
            yield return new WaitForSeconds(waitTimeBetweenFlames);
            flames[flamesToDisplay - 5].GetComponent<Image>().enabled = false;
            flames[flamesToDisplay - 4].GetComponent<Image>().enabled = false;
        }
    }
}

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

    [SerializeField]
    private float waitTimeBetweenInputs;

    private List<Transform> flames = new List<Transform>();

    private void Awake()
    {
        _instance = this;

        foreach (Transform child in transform)
        {
            if (child.CompareTag("Candle"))
            {
                flames.Add(child);
            }
        }
    }

    public IEnumerator DisplayQueue(Queue<int> flamesToDisplay)
    {
        InputManager.Instance.inputAllowed = false;

        foreach (int flame in flamesToDisplay)
        {
            if (flame < 5)
            {
                if (GameManager.Instance.currentPhase!=GameManager.Phases.NoCandles)
                {
                    flames[flame].GetComponent<Image>().enabled = true;
                }
                
                flames[flame].GetComponent<AudioSource>().Play();
                yield return new WaitForSeconds(waitTimeBetweenFlames);

                if (GameManager.Instance.currentPhase != GameManager.Phases.NoCandles)
                {
                    flames[flame].GetComponent<Image>().enabled = false;
                }

                flames[flame].GetComponent<AudioSource>().Stop();
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                if (GameManager.Instance.currentPhase != GameManager.Phases.NoCandles)
                {
                    flames[flame - 5].GetComponent<Image>().enabled = true;
                    flames[flame - 4].GetComponent<Image>().enabled = true;
                }
                
                flames[flame - 5].GetComponent<AudioSource>().Play();
                flames[flame - 4].GetComponent<AudioSource>().Play();
                yield return new WaitForSeconds(waitTimeBetweenFlames);

                if (GameManager.Instance.currentPhase != GameManager.Phases.NoCandles)
                {
                    flames[flame - 5].GetComponent<Image>().enabled = false;
                    flames[flame - 4].GetComponent<Image>().enabled = false;
                }
                
                flames[flame - 5].GetComponent<AudioSource>().Stop();
                flames[flame - 4].GetComponent<AudioSource>().Stop();
                yield return new WaitForSeconds(0.1f);
            }
        }
        InputManager.Instance.inputAllowed = true;
    }

    public IEnumerator DisplayInput(int flame)
    {
        InputManager.Instance.inputAllowed = false;
        if (flame < 5)
        {
            flames[flame].GetComponent<Image>().enabled = true;
            flames[flame].GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(waitTimeBetweenInputs);
            flames[flame].GetComponent<Image>().enabled = false;
            flames[flame].GetComponent<AudioSource>().Stop();
            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            flames[flame - 5].GetComponent<Image>().enabled = true;
            flames[flame - 5].GetComponent<AudioSource>().Play();

            flames[flame - 4].GetComponent<Image>().enabled = true;
            flames[flame - 4].GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(waitTimeBetweenInputs);

            flames[flame - 5].GetComponent<Image>().enabled = false;
            flames[flame - 5].GetComponent<AudioSource>().Stop();

            flames[flame - 4].GetComponent<Image>().enabled = false;
            flames[flame - 4].GetComponent<AudioSource>().Stop();
            yield return new WaitForSeconds(0.1f);
        }

        GameManager.Instance.CheckInput(flame);
    }
}

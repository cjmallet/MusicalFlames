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
                ActivateFlame(flame, false);

                yield return new WaitForSeconds(waitTimeBetweenFlames);

                DeactivateFlame(flame, false);
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                ActivateFlame(flame - 4, false);
                ActivateFlame(flame - 5, false);

                yield return new WaitForSeconds(waitTimeBetweenFlames);

                DeactivateFlame(flame - 4, false);
                DeactivateFlame(flame - 5, false);
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
            ActivateFlame(flame, true);
            yield return new WaitForSeconds(waitTimeBetweenInputs);

            DeactivateFlame(flame, true);
            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            ActivateFlame(flame - 4, true);
            ActivateFlame(flame - 5, true);
            yield return new WaitForSeconds(waitTimeBetweenInputs);

            DeactivateFlame(flame - 4, true);
            DeactivateFlame(flame - 5, true);
            yield return new WaitForSeconds(0.1f);
        }

        GameManager.Instance.CheckInput(flame);
    }

    private void ActivateFlame(int flame, bool isPlayerInput)
    {
        if (GameManager.Instance.currentPhase != GameManager.Phases.NoCandles || isPlayerInput)
        {
            flames[flame].GetComponent<Image>().enabled = true;
        }

        flames[flame].GetComponent<AudioSource>().Play();
        flames[flame].GetComponent<SpawnMusic>().SpawnMusicNote();
    }

    private void DeactivateFlame(int flame, bool isPlayerInput)
    {
        if (GameManager.Instance.currentPhase != GameManager.Phases.NoCandles || isPlayerInput)
        {
            flames[flame].GetComponent<Image>().enabled = false;
        }

        flames[flame].GetComponent<SpawnMusic>().DeleteMusicNote();
        flames[flame].GetComponent<AudioSource>().Stop();
    }
}

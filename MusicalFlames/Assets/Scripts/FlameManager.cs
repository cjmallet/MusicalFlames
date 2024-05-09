using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the activation and deactivation of all candles 
/// and other corresponding actions.
/// </summary>
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

    //Displays the current sequence on screen
    public IEnumerator DisplayQueue(Queue<int> flamesToDisplay)
    {
        InputManager.Instance.inputAllowed = false;

        foreach (int flame in flamesToDisplay)
        {
            //If the flame code is below 5 only one flame will be activated
            if (flame < 5)
            {
                ActivateFlame(flame, false);

                yield return new WaitForSeconds(waitTimeBetweenFlames);

                DeactivateFlame(flame, false);
                yield return new WaitForSeconds(0.1f);
            }
            //If the flame code is 5 or higher two flames will be activated
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

    //Displays the current input from the player
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
        //Checks if the game is currently in need to display the flame
        if (GameManager.Instance.currentPhase != GameManager.Phases.NoCandles || isPlayerInput)
        {
            flames[flame].GetComponent<Image>().enabled = true;
        }

        flames[flame].GetComponent<AudioSource>().Play();
        flames[flame].GetComponent<SpawnMusic>().SpawnMusicNote();
    }

    private void DeactivateFlame(int flame, bool isPlayerInput)
    {
        //Checks if the game is currently displaying a flame
        if (GameManager.Instance.currentPhase != GameManager.Phases.NoCandles || isPlayerInput)
        {
            flames[flame].GetComponent<Image>().enabled = false;
        }

        flames[flame].GetComponent<SpawnMusic>().DeleteMusicNote();
        flames[flame].GetComponent<AudioSource>().Stop();
    }
}

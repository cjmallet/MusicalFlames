using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return _instance; } }
    private static GameManager _instance;

    [SerializeField]
    private GameObject failedScreen;

    private Queue<int> currentOrder = new Queue<int>();
    private Queue<int> correctOrder = new Queue<int>();
    private int amountCompleted;

    [HideInInspector]
    public Phases currentPhase= Phases.TwoNotes;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateRandomOrder();
    }

    private void CreateRandomOrder()
    {
        int max = 0;

        switch (currentPhase)
        {
            case Phases.Base:
            case Phases.NoCandles:
                max = 4;
                break;
            
            case Phases.TwoNotes:
                max = 8;
                break;
        }

        int randomCandle = Random.Range(0, max);

        correctOrder.Enqueue(randomCandle);

        foreach (int flame in correctOrder)
        {
            currentOrder.Enqueue(flame);
        }
        StartRound();
    }

    private void StartRound()
    {
        FlameManager.Instance.StartCoroutine("DisplayQueue", currentOrder);
    }

    private void GameOver()
    {
        failedScreen.gameObject.SetActive(true);
    }

    private void Succes()
    {
        amountCompleted++;

        if (amountCompleted == 5)
        {
            ClearOrder();
            amountCompleted = 0;

            if (currentPhase == Phases.Base)
            {
                currentPhase = Phases.NoCandles;
            }
            else
            {
                currentPhase = Phases.TwoNotes;
            }
        }

        CreateRandomOrder();
    }

    private void ClearOrder()
    {
        currentOrder.Clear();
        correctOrder.Clear();
    }

    public void CheckInput(int input)
    {
        int correctValue = currentOrder.Dequeue();
        if (input != correctValue)
        {
            GameOver();
            return;
        }

        if (currentOrder.Count==0)
        {
            Succes();
            return;
        }

        InputManager.Instance.inputAllowed = true;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Retry()
    {
        ClearOrder();
        currentPhase = Phases.Base;
        failedScreen.gameObject.SetActive(false);
        CreateRandomOrder();
    }

    public enum Phases
    {
        Base,
        NoCandles,
        TwoNotes
    }
}
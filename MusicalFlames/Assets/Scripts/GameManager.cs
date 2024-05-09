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
    public Phases currentPhase= Phases.Base;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateRandomOrder();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    private void Quit()
    {
        Application.Quit();
    }

    private void Succes()
    {
        amountCompleted++;

        if (amountCompleted==5)
        {
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

    public void Retry()
    {
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
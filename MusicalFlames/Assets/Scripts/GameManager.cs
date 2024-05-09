using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return _instance; } }
    private static GameManager _instance;

    public float waitTimeBetweenFlames;

    private Queue<int> currentOrder = new Queue<int>();
    private int amountCompleted;
    private Phases currentPhase;

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
        int min = 0;
        int max = 0;
        currentOrder.Clear();
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

        for (int x = 0; x < amountCompleted + 1; x++)
        {
            int randomCandle = Random.Range(0, 4);
            currentOrder.Enqueue(randomCandle);
        }
        StartRound();
    }

    private void StartRound()
    {
        Queue<int> currentOrderCopy = currentOrder;
        for (int x = 0; x < currentOrderCopy.Count; x++)
        {
            int flameToDisplay = currentOrderCopy.Dequeue();

            FlameManager.Instance.StartCoroutine("DisplayCandle", flameToDisplay);
        }
    }

    private enum Phases
    {
        Base,
        NoCandles,
        TwoNotes
    }
}
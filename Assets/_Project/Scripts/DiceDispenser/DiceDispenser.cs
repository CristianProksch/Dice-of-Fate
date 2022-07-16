using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceDispenser : MonoBehaviour
{
    [Header("Movement")]
    public Transform startPosition;
    public Transform endPosition;
    public float moveSpeed;

    [Header("Spawn")]
    public Die diePrefab;
    public int dicePerTurn;

    private Vector3 direction;
    private int spawnedThisTurn;
    private int finishedDiceThisTurn;

    // Start is called before the first frame update
    void Start()
    {
        InputController.AddMouseUpListener(() => SpawnDie());
        TurnController.AddStartActionListener(() => OnNewTurn());
        direction = Vector3.right;
    }

    void OnDestroy()
    {
        InputController.RemoveMouseUpListener(() => SpawnDie());
        TurnController.RemoveStartActionListener(() => OnNewTurn());
    }

    // Update is called once per frame
    void Update()
    {
        var dispenserPosition = transform.position;

        if (dispenserPosition.x <= startPosition.transform.position.x)
        {
            direction = Vector3.right;
        }
        if (dispenserPosition.x >= endPosition.transform.position.x)
        {
            direction = Vector3.left;
        }
        dispenserPosition += direction * moveSpeed * Time.deltaTime;
        transform.position = dispenserPosition;
    }

    void SpawnDie()
    {
        if (TurnController.GetCurrentPhase() != TurnPhase.Action)
        {
            return;
        }

        if (InputController.IsMouseOverUI())
        {
            return;
        }

        if (spawnedThisTurn >= dicePerTurn)
        {
            return;
        }

        var rotation = Quaternion.Euler(new Vector3(0f, 0f, Random.Range(0f, 360f)));
        var die = Instantiate(diePrefab, transform.position, rotation);
        die.AddDieFinishedListener(() => OnDieFinished());

        spawnedThisTurn++;
    }

    void OnDieFinished()
    {
        finishedDiceThisTurn++;

        if (finishedDiceThisTurn >= dicePerTurn)
        {
            TurnController.NextPhase();
        }
    }

    void OnNewTurn()
    {
        spawnedThisTurn = 0;
        finishedDiceThisTurn = 0;
    }
}

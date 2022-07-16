using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceDispenser : MonoBehaviour
{
    public Transform startPosition;
    public Transform endPosition;
    public float moveSpeed;

    public GameObject die;

    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.right;
    }

    // Update is called once per frame
    void Update()
    {
        var dispenserPosition = transform.position;

        if (dispenserPosition.x <= startPosition.transform.position.x )
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
        Instantiate(die);
    }
}

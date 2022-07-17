using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieSelector : MonoBehaviour
{
    [SerializeField] private List<Sprite> possibleSprites;
    [SerializeField] private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        int random = Random.Range(0, possibleSprites.Count);
        spriteRenderer.sprite = possibleSprites[random];
    }
}

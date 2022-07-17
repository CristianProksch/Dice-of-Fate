using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCollectionPreview : MonoBehaviour
{
    [SerializeField]
    private Image _collectionImage;

    public void SetSprite(Sprite sprite)
    {
        _collectionImage.sprite = sprite;
    }
}

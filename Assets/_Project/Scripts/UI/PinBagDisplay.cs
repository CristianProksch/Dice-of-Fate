using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinBagDisplay : MonoBehaviour
{
    [SerializeField]
    private PinCollectionPreview _previewPrefab;
    [SerializeField]
    private Transform _container;

    public void UpdatePreviews(List<ActionPinCollection> collections)
    {
        Clear();

        foreach (var collection in collections)
        {
            var preview = Instantiate(_previewPrefab, _container);
            preview.SetSprite(collection.UiSprite);
        }
    }

    private void Clear()
    {
        foreach(Transform child in _container)
        {
            Destroy(child.gameObject);
        }
    }
}

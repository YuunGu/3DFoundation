using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public string GetInteractaPrompt();
    public void OnInteract();
}
public class ItemObject : MonoBehaviour , IInteractable
{
    public ItemData itemData;

    public string GetInteractaPrompt()
    {
        string str = $"{itemData.displayName} \n {itemData.description}";
        return str;
    }

    public void OnInteract()
    {
        CharacterManager.Instance.Player.itemData = itemData;
        CharacterManager.Instance.Player.addItem?.Invoke();
        Destroy(gameObject);
    }
}

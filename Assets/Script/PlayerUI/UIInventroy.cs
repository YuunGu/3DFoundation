using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIInventroy : MonoBehaviour
{
    public ItemSlot[] slots;

    public GameObject inventoryWindow;
    public Transform slotPanel;
    public Transform dropPosition;
    [Header("Select Item")]
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;

    public GameObject useBtn;
    public GameObject equipBtn;
    public GameObject unequipBtn;
    public GameObject dropBtn;

    private PlayerController controller;
    private PlayerState state;

    ItemData selectItem;
    int selectdItemindex;
    void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        state = CharacterManager.Instance.Player.state;
        dropPosition = CharacterManager.Instance.Player.dropItem;

        controller.inventory += Toggle;
        CharacterManager.Instance.Player.addItem += AddItem;

        inventoryWindow.SetActive(false);
        slots = new ItemSlot[slotPanel.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
        }
        ClearSelctedItemWindow();
    }

    void ClearSelctedItemWindow()
    {
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;

        useBtn.SetActive(false);
        equipBtn.SetActive(false);
        unequipBtn.SetActive(false);
        dropBtn.SetActive(false);
    }

    public void Toggle()
    {
        if (IsOpen())
        {
            inventoryWindow.SetActive(false);
        }
        else
        {
            inventoryWindow.SetActive(true);
        }
    }

    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

    void AddItem()
    {
        ItemData data = CharacterManager.Instance.Player.itemData;

        if (data.canStack)
        {
            ItemSlot slot = GetItemStack(data);
            if(slot != null)
            {
                slot.quantity++;
                UpdateUI();
                CharacterManager.Instance.Player.itemData = null;
                return;
            }
        }
        ItemSlot emptySlot = GetEmptySlot();
        if (emptySlot !=null)
        {
            emptySlot.item = data;
            emptySlot.quantity = 1;
            UpdateUI();
            CharacterManager.Instance.Player.itemData = null;
            return;
        }

        ThrowItem(data);
        CharacterManager.Instance.Player.itemData = null;

    }
    void UpdateUI()
    {
        for(int i = 0; i<slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                slots[i].Set();
            }
            else
            {
                slots[i].Clear();
            }
        }
    }

    ItemSlot GetItemStack(ItemData data)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == data && slots[i].quantity < data.maxStackAmount)
            {
                return slots[i];
            }
        }
        return null;
    }

    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return slots[i];
            }
        }
        return null;
    }

    void ThrowItem(ItemData data)
    {
        Instantiate(data.dropPrefab , dropPosition.position , Quaternion.Euler(Vector3.one * Random.value * 360));
    }

    public void SelectItem(int index)
    {
        if (slots[index].item == null) return;
        selectItem = slots[index].item;
        selectdItemindex = index;

        selectedItemName.text = selectItem.displayName;
        selectedItemDescription.text = selectItem.description; 

        
        useBtn.SetActive(true);
        dropBtn.SetActive(true);
    }

    public void OnUseBtn()
    {
        if(selectItem.type == ItemType.Consumable)
        {
            for(int i = 0; i< selectItem.consumables.Length; i++) 
            { 
                switch (selectItem.consumables[i].type)
                {
                    case ConsumableType.Health:
                        Debug.Log("아무일도..일어나지않았습니다");
                        break;
                    case ConsumableType.Dash:
                        float j = selectItem.consumables[i].value;
                        CharacterManager.Instance.Player.state.uiState.dash.AddDashCount(j);
                        break;
                }
            }
            RemoveSelectedItem();
        }
        if (selectItem.type == ItemType.TimeLimit)
        {
            if(selectItem.displayName == "점프2배")
            {
                StartCoroutine(JumpItem());
            }
            else if(selectItem.displayName == "스피드아이템")
            {
                StartCoroutine(SpeedItem());
            }
            RemoveSelectedItem();
        }

        if (selectItem.type == ItemType.OneUse)
        {
            if (selectItem.displayName == "트로피")
            {
                SceneManager.LoadScene(1);
            }
            RemoveSelectedItem();
        }

    }
    public void OnDropBtn()
    {
        ThrowItem(selectItem);
        RemoveSelectedItem();
    }

    void RemoveSelectedItem()
    {
        slots[selectdItemindex].quantity--;
        if (slots[selectdItemindex].quantity <= 0)
        {
            selectItem = null;
            slots[selectdItemindex].item = null;
            selectdItemindex = -1;
            ClearSelctedItemWindow();
        }
        UpdateUI();
    }
    IEnumerator SpeedItem()
    {
        float originaloveSpeed = CharacterManager.Instance.Player.controller.moveSpeed;


        // 점프력 두 배로 증가
        CharacterManager.Instance.Player.controller.moveSpeed = originaloveSpeed * 2;


        // 5초 동안 대기
        yield return new WaitForSeconds(5f);


        // 원래 점프력으로 복원
        CharacterManager.Instance.Player.controller.moveSpeed = originaloveSpeed;
    }
    IEnumerator JumpItem()
    {
        float originalJumpPower = CharacterManager.Instance.Player.controller.jumpPower;


        // 점프력 두 배로 증가
        CharacterManager.Instance.Player.controller.jumpPower = originalJumpPower * 2;


        // 5초 동안 대기
        yield return new WaitForSeconds(5f);


        // 원래 점프력으로 복원
        CharacterManager.Instance.Player.controller.jumpPower = originalJumpPower;


    }
}

using UnityEngine;
using UnityEngine.UI;

public class InventorySlotScript : MonoBehaviour
{
    #region Fields
    [SerializeField] private Image _icon;
    [SerializeField] private Image _frame;
    [SerializeField] private InventoryUI _inventoryUI;
    #endregion

    private Item _item;

    #region Logic
    public void AddItem(Item item)
    {
        _item = item;
        UpdateView();
    }

    public void RemoveItem()
    {
        _inventoryUI.UnEquipItemFromInventory(_item);
        _item = null;
        UpdateView();
    }

    private void UpdateView()
    {
        if(_item != null)
        {
            _icon.sprite = _item.Icon;
            _icon.enabled = true;
            _frame.sprite = _item.Frame;
            _frame.enabled = true;
        }
        else
        {
            _icon.sprite = null;
            _icon.enabled = false;
            _frame.sprite = null;
            _frame.enabled = false;
        }
    }

    public void SlotButtonAction()
    {
        if(_item == null) return;
        RemoveItem();
    }
    #endregion
}
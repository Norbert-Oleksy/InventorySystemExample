using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackpackSlotScript : MonoBehaviour
{
    [SerializeField] private Image icon,frame;
    [SerializeField] private TextMeshProUGUI statsText;

    private InventoryUI _inventoryUI;
    private Item _item;

    #region Logic
    public void EquipItem()
    {
        _inventoryUI.EquipItemFromBackpack(_item);
        Destroy(gameObject);
    }

    public void Initialize(InventoryUI inventoryUI, Item item)
    {
        _inventoryUI = inventoryUI;
        _item = item;
        icon.sprite = _item.Icon;
        frame.sprite = _item.Frame;
        statsText.text = $"Health: {_item.Data.HealthPoints} Defense: {_item.Data.Defense} \nDamage: {_item.Data.Damage} Speed: {_item.Data.MovementSpeed}";
    }
    #endregion
}

using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    #region SerializeField
    [SerializeField] private GameObject _loadingScreen;
    [Header("Stats")]
    [SerializeField] private TextMeshProUGUI _statsHealthValue;
    [SerializeField] private TextMeshProUGUI _statsDefenseValue;
    [SerializeField] private TextMeshProUGUI _statsDamageValue;
    [SerializeField] private TextMeshProUGUI _statsSpeedValue;
    [Header("BackPack")]
    [SerializeField] private Transform _backpackContent;
    [SerializeField] private GameObject _backpackSlotPrefab;
    [Header("Inventory")]
    [SerializeField] private InventorySlotScript _inventoryHelmetSlot;
    [SerializeField] private InventorySlotScript _inventoryArmorSlot;
    [SerializeField] private InventorySlotScript _inventoryBootsSlot;
    [SerializeField] private InventorySlotScript _inventoryNecklaceSlot;
    [SerializeField] private InventorySlotScript _inventoryRingSlot;
    [SerializeField] private InventorySlotScript _inventoryWeaponSlot;
    #endregion
    private Player _player;
    private string _currentCategory;
    #region Logic
    public void OpenInventory(Player player)
    {
        _player = player;
        _loadingScreen.SetActive(false);
        UpdateStatsDisplay();
        DisplayBackpackContent();
    }

    private void UpdateStatsDisplay()
    {
        _statsHealthValue.text = _player.MaxHealth.ToString();
        _statsDefenseValue.text = _player.Defense.ToString();
        _statsDamageValue.text = _player.Damage.ToString();
        _statsSpeedValue.text = _player.MovementSpeed.ToString();
    }

    public void DisplayBackpackContent(string category = "")
    {
        foreach (Transform child in _backpackContent) Destroy(child.gameObject);

        List<Item> itemsList = string.IsNullOrEmpty(category)
            ? _player.Items
            : _player.Items.Where(item => item.Data.Category == category).ToList();

        _currentCategory = category;

        foreach (var item in itemsList)
        {
            GameObject slot = Instantiate(_backpackSlotPrefab, _backpackContent);
            slot.GetComponent<BackpackSlotScript>().Initialize(this,item);
        }
    }

    public void EquipItemFromBackpack(Item item)
    {
        _player.EquipItem(item);


        switch (item.Data.Category)
        {
            case "Helmet":
                _inventoryHelmetSlot.AddItem(item);
                break;
            case "Armor":
                _inventoryArmorSlot.AddItem(item);
                break;
            case "Boots":
                _inventoryBootsSlot.AddItem(item);
                break;
            case "Necklace":
                _inventoryNecklaceSlot.AddItem(item);
                break;
            case "Ring":
                _inventoryRingSlot.AddItem(item);
                break;
            case "Weapon":
                _inventoryWeaponSlot.AddItem(item);
                break;
        }

        UpdateStatsDisplay();
    }

    public void UnEquipItemFromInventory(Item item)
    {
        _player.UnEquipItem(item);
        UpdateStatsDisplay();

        if(_currentCategory == "" || _currentCategory == item.Data.Category)
        {
            GameObject slot = Instantiate(_backpackSlotPrefab, _backpackContent);
            slot.GetComponent<BackpackSlotScript>().Initialize(this, item);
        }
    }
    #endregion
}

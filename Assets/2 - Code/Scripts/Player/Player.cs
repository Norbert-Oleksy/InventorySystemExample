using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Inventory
    public List<Item> Items { get; private set; }
    public Dictionary<string, Item> Equipment { get; private set; }
    #endregion

    #region Stats
    [SerializeField] private int _baseHealthPoints;
    [SerializeField] private float _baseMovementSpeed;

    private int _equipmentHealthPoints, _equipmentDefense, _equipmentDamage;
    private float _equipmentMovementSpeed;

    public int MaxHealth => _baseHealthPoints + _equipmentHealthPoints;
    [HideInInspector]public int Health;
    public float MovementSpeed => _baseMovementSpeed + (1 + _equipmentMovementSpeed / 100f);
    public int Defense => _equipmentDefense;
    public int Damage => _equipmentDamage;
    #endregion

    #region Logic
    public void LoadItemsFromSever()
    {
        NetworkManager.Instance.DownloadPlayerInventory(this, () => { UIManager.Instance.OpenIventory(this); });
    }

    public void EquipItem(Item item)
    {
        if(!Items.Contains(item)) return;

        if (Equipment[item.Data.Category] != null) UnEquipItem(item.Data.Category);

        Equipment[item.Data.Category] = item;

        _equipmentHealthPoints += item.Data.HealthPoints;
        _equipmentMovementSpeed += item.Data.MovementSpeed;
        _equipmentDefense += item.Data.Defense;
        _equipmentDamage += item.Data.Damage;
    }

    public void UnEquipItem(Item item) {
        if (Equipment[item.Data.Category] == null) return;

        Items.Add(item);
        Equipment[item.Data.Category] = null;

        _equipmentHealthPoints -= item.Data.HealthPoints;
        _equipmentMovementSpeed -= item.Data.MovementSpeed;
        _equipmentDefense -= item.Data.Defense;
        _equipmentDamage -= item.Data.Damage;
    }

    public void UnEquipItem(string category)
    {
        if (Equipment[category] == null) return;

        Items.Add(Equipment[category]);
        Equipment[category] = null;
    }
    #endregion

    #region Unity-API
    private void Awake()
    {
        Items = new List<Item>();

        Equipment = new Dictionary<string, Item>
        {
            { "Helmet", null },
            { "Armor", null },
            { "Boots", null },
            { "Necklace", null },
            { "Ring", null },
            { "Weapon", null }
        };

        Health = _baseHealthPoints;
    }

    private void Start()
    {
        TestManager.Instance.OnGameStart += ()=> { Health = MaxHealth; };
    }

    private void OnDisable()
    {
        TestManager.Instance.OnGameStart -= () => { Health = MaxHealth; };
    }
    #endregion
}

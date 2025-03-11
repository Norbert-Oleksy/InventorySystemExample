using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Coroutine _attack;

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

    public void TakeDamage(int ammount)
    {
        Health -= ammount - Defense/10;
        if(Health < 0) Death();
    }

    private void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Attack(InputAction.CallbackContext context)
    {
        if (TestManager.Instance != null && TestManager.Instance.Stage != TestManager.TestStage.Running) return;
        if (_attack!=null) return;

        _attack = StartCoroutine(PlayerAttack());
    }

    private IEnumerator PlayerAttack()
    {
        float attackRange = 2f;
        float attackRadius = 2f;

        Vector3 attackPosition = transform.position + transform.forward * attackRange;

        Collider[] hitColliders = Physics.OverlapSphere(attackPosition, attackRadius);
        foreach (Collider collider in hitColliders)
        {
            Ghost ghost = collider.GetComponent<Ghost>();
            if (ghost != null)
            {
                ghost.TakeDamage(Damage);
            }
        }

        yield return new WaitForSeconds(1f);
        _attack = null;
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
        InputManager.Instance.attack.performed += Attack;
    }

    private void OnDisable()
    {
        TestManager.Instance.OnGameStart -= () => { Health = MaxHealth; };
    }
    #endregion
}

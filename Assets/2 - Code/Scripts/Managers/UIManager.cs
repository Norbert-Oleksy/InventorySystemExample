using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    #region UI-Elements
    [SerializeField] private InventoryUI _inventoryUI;
    #endregion

    #region Unity-API
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    #region Logic
    public void OpenIventory(Player player)
    {
        _inventoryUI.OpenInventory(player);
    }

    public void OpenPlayerInterface(Player player)
    {
        //TODO
    }
    #endregion
}

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance { get; private set; }
    private GameServerMock _gameServerMock = new GameServerMock();

    #region Logic
    public async void DownloadPlayerInventory(Player player, Action onComplete = null)
    {
        List<Item> items = new List<Item>();

        try
        {
            string data = await _gameServerMock.GetItemsAsync(new CancellationToken());
            var jsonObject = JsonConvert.DeserializeObject<JObject>(data);
            var dataList = jsonObject["Items"].ToObject<List<ItemData>>();

            foreach (ItemData itemData in dataList)
            {
                Item item = new Item
                {
                    Data = itemData,
                    Icon = Resources.Load<Sprite>($"Textures/Items/{itemData.Category}/{itemData.Name}"),
                    Frame = Resources.Load<Sprite>($"Textures/Items/Rarity/{GetFrameName(itemData.Rarity)}")
                };
                player.Items.Add(item);
            }
        }catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        onComplete?.Invoke();
    }        
    
    private string GetFrameName(int rarity)
    {
            switch(rarity)
            {
                case 0:
                    return "ItemRarityCommon";
                case 1:
                    return "ItemRarityUncommon";
                case 2:
                    return "ItemRarityRare";
                case 3:
                    return "ItemRarityEpic";
                case 4:
                    return "ItemRarityLegendary";
                default:
                    return "ItemRarityCommon";
            }
    }
    #endregion

    #region Unity-API
    private void Awake()
    {
        Instance = this;
    }
    #endregion
}

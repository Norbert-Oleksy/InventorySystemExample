using System;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public static TestManager Instance { get; private set; }
    public TestStage Stage { get; private set; }

    public Action OnGameStart;

    private Player _player;

    #region Logic
    public void ChangeStage(TestStage stage)
    {
        Stage = stage;
        if(Stage == TestStage.Running) OnGameStart?.Invoke();
    }
    #endregion

    #region Unity-API
    private void Awake()
    {
        Instance = this;
        Stage = TestStage.Menu;
        _player = FindFirstObjectByType<Player>();
    }

    private void Start()
    {
        _player.LoadItemsFromSever();
    }
    #endregion

    public enum TestStage
    {
        Menu,
        Running
    }
}
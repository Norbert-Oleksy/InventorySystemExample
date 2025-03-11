using UnityEngine;

public class TestManager : MonoBehaviour
{
    public static TestManager Instance { get; private set; }
    public TestStage Stage { get; private set; }

    #region Logic
    public void ChangeStage(TestStage stage)
    {
        Stage = stage;
    }
    #endregion

    #region Unity-API
    private void Awake()
    {
        Instance = this;
        Stage = TestStage.Menu;
    }
    #endregion

    public enum TestStage
    {
        Menu,
        Running
    }
}
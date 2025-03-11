using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayButtonAction()
    {
        TestManager.Instance.ChangeStage(TestManager.TestStage.Running);
        gameObject.SetActive(false);
    }
}

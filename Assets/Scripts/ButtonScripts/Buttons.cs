using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public void OnClickMainMenu() // ���� �޴� ��ư
    {
        SceneManager.LoadScene("MainMenu"); // MainMenu���� ��ȯ
    }
    public void OnClickGameStart() // ���� ���� ��ư
    {
        SceneManager.LoadScene("Stage1Explan"); // Stage1���� ��ȯ
    }
    public void OnClickSetting() // ���� ��ư
    {
        SceneManager.LoadScene("Setting"); // Setting���� ��ȯ
    }
    public void OnClickGameDescript() // ���� ���� ��ư
    {
        SceneManager.LoadScene("GameDescript"); // GameDescript���� ��ȯ
    }

    //private bool isPaused = false; // ���� �Ͻ� ���� ����
    //public void OnClickContinue() // ����ϱ� ��ư
    //{
    //    GameObject.FindObjectOfType<SubMenu>().CheckPause();
    //}
}

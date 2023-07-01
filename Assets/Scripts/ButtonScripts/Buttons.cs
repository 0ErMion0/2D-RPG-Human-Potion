using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public void OnClickMainMenu() // 메인 메뉴 버튼
    {
        SceneManager.LoadScene("MainMenu"); // MainMenu으로 전환
    }
    public void OnClickGameStart() // 게임 시작 버튼
    {
        SceneManager.LoadScene("Stage1Explan"); // Stage1으로 전환
    }
    public void OnClickSetting() // 설정 버튼
    {
        SceneManager.LoadScene("Setting"); // Setting으로 전환
    }
    public void OnClickGameDescript() // 게임 설명 버튼
    {
        SceneManager.LoadScene("GameDescript"); // GameDescript으로 전환
    }

    //private bool isPaused = false; // 게임 일시 정지 상태
    //public void OnClickContinue() // 계속하기 버튼
    //{
    //    GameObject.FindObjectOfType<SubMenu>().CheckPause();
    //}
}

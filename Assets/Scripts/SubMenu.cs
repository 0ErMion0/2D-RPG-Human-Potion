using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubMenu : MonoBehaviour
{
    public GameObject subMenu; // 서브메뉴 오브젝트

    public bool isPaused = false; // 게임 일시 정지 상태
    //private int count = 0; // esc로 게임 일시 정지 위해 카운트해주는 변수

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // esc 버튼 누르면
        {
           CheckPause();
        }
    }
    public void CheckPause()
    {
        if (isPaused == false) // 일시 정지 상태가 아니면
        {
            PauseGame(); // 서브 메뉴를 열고 게임을 일시 정지
        }
        else
        {
            ResumeGame(); // 서브 메뉴가 이미 열려있을 때는 닫음
        }
    }

    // 게임 멈춤
    void PauseGame()
    {
        isPaused = true; // false->true로 바꿔 나중에 게임 재개 가능하도록
        //count = 1;
        Time.timeScale = 0f; // 게임 일시 정지
        subMenu.SetActive(true); // 서브 메뉴 창 띄움
    }

    // 게임 재개
    void ResumeGame()
    {
        isPaused = false; // true->false로 바꿔 나중에 게임 멈출 수 있도록
        //count = 0;
        Time.timeScale = 1f; // 게임 재개
        subMenu.SetActive(false); // 서브 메뉴 창 닫음
    }

    public void OnClickContinue() // 계속하기 버튼
    {
        //isPaused = false; // 멈춤 상태 거짓으로 설정
        //count = 0; // 카운트 1로 설정, 계속하기로 창 닫고 esc로 창 열 수 있도록
        ResumeGame(); // 게임 재개
        //print("계속하기" + count);
    }

    public void OnClickMainMenu() // 메인 메뉴 버튼
    {
        //isPaused = false; // 멈춤 상태 거짓으로 설정
        ResumeGame(); // 게임 재개 함수 호출하여 메인메뉴 돌아왔다가 게임 정지되어 있는 상태가 안 되도록 함
        SceneManager.LoadScene("MainMenu"); // MainMenu으로 전환
    }
}

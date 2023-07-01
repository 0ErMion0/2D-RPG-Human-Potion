using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubMenu : MonoBehaviour
{
    public GameObject subMenu; // ����޴� ������Ʈ

    public bool isPaused = false; // ���� �Ͻ� ���� ����
    //private int count = 0; // esc�� ���� �Ͻ� ���� ���� ī��Ʈ���ִ� ����

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // esc ��ư ������
        {
           CheckPause();
        }
    }
    public void CheckPause()
    {
        if (isPaused == false) // �Ͻ� ���� ���°� �ƴϸ�
        {
            PauseGame(); // ���� �޴��� ���� ������ �Ͻ� ����
        }
        else
        {
            ResumeGame(); // ���� �޴��� �̹� �������� ���� ����
        }
    }

    // ���� ����
    void PauseGame()
    {
        isPaused = true; // false->true�� �ٲ� ���߿� ���� �簳 �����ϵ���
        //count = 1;
        Time.timeScale = 0f; // ���� �Ͻ� ����
        subMenu.SetActive(true); // ���� �޴� â ���
    }

    // ���� �簳
    void ResumeGame()
    {
        isPaused = false; // true->false�� �ٲ� ���߿� ���� ���� �� �ֵ���
        //count = 0;
        Time.timeScale = 1f; // ���� �簳
        subMenu.SetActive(false); // ���� �޴� â ����
    }

    public void OnClickContinue() // ����ϱ� ��ư
    {
        //isPaused = false; // ���� ���� �������� ����
        //count = 0; // ī��Ʈ 1�� ����, ����ϱ�� â �ݰ� esc�� â �� �� �ֵ���
        ResumeGame(); // ���� �簳
        //print("����ϱ�" + count);
    }

    public void OnClickMainMenu() // ���� �޴� ��ư
    {
        //isPaused = false; // ���� ���� �������� ����
        ResumeGame(); // ���� �簳 �Լ� ȣ���Ͽ� ���θ޴� ���ƿԴٰ� ���� �����Ǿ� �ִ� ���°� �� �ǵ��� ��
        SceneManager.LoadScene("MainMenu"); // MainMenu���� ��ȯ
    }
}

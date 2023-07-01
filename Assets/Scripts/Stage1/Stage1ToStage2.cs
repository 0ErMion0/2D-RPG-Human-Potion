using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1ToStage2 : MonoBehaviour
{
    private GameObject player; // �÷��̾� ������Ʈ

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");  // Player �±׸� ���� ������Ʈ ã��
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene("Stage2"); // �浹�ϸ� Stage2�� ��ȯ
        player.transform.position = new Vector2(-10.52f, -5.02f); // �÷��̾��� ��ġ ����
    }
}

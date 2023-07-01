using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1ToStage2 : MonoBehaviour
{
    private GameObject player; // 플레이어 오브젝트

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");  // Player 태그를 가진 오브젝트 찾음
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene("Stage2"); // 충돌하면 Stage2로 전환
        player.transform.position = new Vector2(-10.52f, -5.02f); // 플레이어의 위치 설정
    }
}

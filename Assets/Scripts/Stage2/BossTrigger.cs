using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject BringerOfDeath; // 보스 오브젝트
    public GameObject HowToUI; // 설명 UI 오브젝트
    public GameObject BossHpBar; // 보스 hp바
   

    // 부딪히면 보스, 보스 hp바 나타나게 하고 설명 UI창은 꺼지도록
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BringerOfDeath.SetActive(true); // 보스 나타나게 함
        BossHpBar.SetActive(true); // 보스의 hp바를 나타나게 함
        HowToUI.SetActive(false); // 스테이지 설명창 끔
        Destroy(gameObject); // 자기자신 없앰
    }
}

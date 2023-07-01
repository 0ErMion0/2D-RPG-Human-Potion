using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S2ToS3_2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") // 부딪힌게 플레이어라면
            SceneManager.LoadScene("Stage3_2"); // 충돌하면 Stage3_@으로 전환
    }
}

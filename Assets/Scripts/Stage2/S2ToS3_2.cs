using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S2ToS3_2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player") // �ε����� �÷��̾���
            SceneManager.LoadScene("Stage3_2"); // �浹�ϸ� Stage3_@���� ��ȯ
    }
}

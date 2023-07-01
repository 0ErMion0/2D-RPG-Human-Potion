using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCast : MonoBehaviour
{
    public float bossCastDmg = 20; // ������ �ִ� ���� ���� ������
    // 0.2�� �ڿ� �ݶ��̴� ��������
    private BoxCollider2D prefabCollider; // ��ȯ ���� �������� �ݶ��̴�

    private void Start()
    {
        prefabCollider = GetComponent<BoxCollider2D>(); // �������� �ݶ��̴� ������
        prefabCollider.enabled = false; // �ݶ��̴� ����
        Invoke("ActivateCollider", 0.2f); // 0.2�� �ڿ� �ݶ��̴� Ŵ
    }

    private void ActivateCollider()
    {
        prefabCollider.enabled = true; // �ݶ��̴� Ŵ
    }

    //private void OnCollisionEnter2D(Collision2D collision) // ������ -> �̷����ϸ� ���ڸ��� ����Ʈ �� �� �����ְ� �����
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        collision.gameObject.GetComponent<S2PlayerController>().Damaged(bossCastDmg);
    //        // ���� ������Ʈ�� �÷��̾� ��Ʈ�� ��ũ��Ʈ�� Damaged�Լ� ȣ���Ͽ� �÷��̾�� ������ ��
    //        Destroy(gameObject); // ������� ������Ʈ ����
    //    }
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().Damaged(bossCastDmg);
            // ���� ������Ʈ�� �÷��̾� ��Ʈ�� ��ũ��Ʈ�� Damaged�Լ� ȣ���Ͽ� �÷��̾�� ������ ��
            Destroy(gameObject); // ������� ������Ʈ ����
        }
    }
}

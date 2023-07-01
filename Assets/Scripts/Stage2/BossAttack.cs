using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    Vector2 move; // Į ���� �߻� ����
    public float bossAttackDmg = 30; // ������ �ִ� Į ���� ������

    private void Start()
    {
        move = GameObject.FindObjectOfType<BossController>().attackObjMove; // BossController ��ũ��Ʈ���� Į ���� �߻� ���� ������
    }

    private void Update()
    {
        transform.Translate(move * Time.deltaTime); // Į ���� �ش� �������� �̵�
    }

    private void OnCollisionEnter2D(Collision2D collision) // ������
    {
       if(collision.gameObject.tag == "Player")
        {
            print("����");
            collision.gameObject.GetComponent<PlayerController>().Damaged(bossAttackDmg);
            // ���� ������Ʈ�� �÷��̾� ��Ʈ�� ��ũ��Ʈ�� Damaged�Լ� ȣ���Ͽ� �÷��̾�� ������ ��
            Destroy(gameObject); // ������� ������Ʈ ����
        }
    }
}

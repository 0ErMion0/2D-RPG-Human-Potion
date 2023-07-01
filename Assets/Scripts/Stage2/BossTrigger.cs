using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject BringerOfDeath; // ���� ������Ʈ
    public GameObject HowToUI; // ���� UI ������Ʈ
    public GameObject BossHpBar; // ���� hp��
   

    // �ε����� ����, ���� hp�� ��Ÿ���� �ϰ� ���� UIâ�� ��������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BringerOfDeath.SetActive(true); // ���� ��Ÿ���� ��
        BossHpBar.SetActive(true); // ������ hp�ٸ� ��Ÿ���� ��
        HowToUI.SetActive(false); // �������� ����â ��
        Destroy(gameObject); // �ڱ��ڽ� ����
    }
}

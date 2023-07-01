using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarriorAttack : MonoBehaviour
{
    Vector3 move; // Į ���� �߻� ����
    public float playerAttackDmg = 20; // �÷��̾ ������ �� �ִ� ������
    // private GameObject redSlime; // �÷��̾� ������Ʈ
    //private Animator redSlimeAnimator; // Red Slime�� �ִϸ�����

    // Start is called before the first frame update
    void Start()
    {
        //Scene scene = SceneManager.GetActiveScene();
        //if (scene.name == "Stage1") // ��������1�� ���
            move = GameObject.FindObjectOfType<PlayerController>().objMove; // PlayerController ��ũ��Ʈ���� Į ���� �߻� ���� ������
        //else // ��������2�� ���
            //move = GameObject.FindObjectOfType<S2PlayerController>().objMove; // S2PlayerController ��ũ��Ʈ���� Į ���� �߻� ���� ������
        //redSlime = GameObject.FindGameObjectWithTag("RedSlime");  // RedSlime �±׸� ���� ������Ʈ ã��
        // redSlimeAnimator = redSlime.GetComponent<Animator>(); // RedSlime �±׸� ���� ������Ʈ�� Animator�� ������
        playerAttackDmg = 20; // 20/60 �Ǵ� 20/10
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(move * Time.deltaTime); // Į ���� �ش� �������� �̵�
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Destroy(collision.gameObject); // ��� �ε����°� �� �׿�
        //Destroy(gameObject);


        // ������ ���� �� ü�� ����
        //GameObject.FindObjectOfType<PlayerController>().Attack();
        if (collision.gameObject.tag == "RedSlime") // ���� ������Ʈ�� �±װ� RedSlime�� ���
        {
            //GameObject.FindObjectOfType<RedSlimeController>().Damaged(playerAttackDmg); // Red Slime�� Damaged �Լ� ȣ��.. �̷��� �ϸ�.. ������ �������϶� �ƹ� �������̳� ȣ���ع�����..
            collision.gameObject.GetComponent<RedSlimeController>().Damaged(playerAttackDmg);
        }
        if (collision.gameObject.tag == "GreenSlime") // ���� ������Ʈ�� �±װ� GreenSlime�� ���
        {
            //ameObject.FindObjectOfType<GreenSlimeController>().Damaged(playerAttackDmg); // Green Slime�� Damaged �Լ� ȣ��
            collision.gameObject.GetComponent<GreenSlimeController>().Damaged(playerAttackDmg);
        }
        if(collision.gameObject.tag=="Boss") // ���� ������Ʈ�� �±װ� Boss�� ���
        {
            collision.gameObject.GetComponent<BossController>().Damaged(playerAttackDmg);
        }

        Destroy(gameObject); // �ε����� ������
        //redSlimeAnimator.SetBool("Red Hurt - Animation", false);
    }
}

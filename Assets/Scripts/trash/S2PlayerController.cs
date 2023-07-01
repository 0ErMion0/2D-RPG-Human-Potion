using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class S2PlayerController : MonoBehaviour
{
    Rigidbody2D playerRigidbody; // �̵��� ����� ������ٵ� ������Ʈ
    Animator animator; // �÷��̾� �ִϸ��̼�

    // �÷��̾� �̵�
    private Vector2 velocity; // �÷��̾� �ӵ�
    private float horizontal; // �÷��̾� �¿� ������
    float jumpForce = 15.0f; // ������ �� ���� ��
    float moveSpeed = 8.0f; // �÷��̾� �̵� �ӵ�
    //float runForce = 30.0f;
    //float maxRunSpeed = 2.0f;
    private bool isGrounded; // ���� ���� ���� ���� ���� ��Ҵ��� ���� �Ǵ�
    public int key; // �÷��̾� �¿���� ���� ����(��: -1, ��: 1)

    // �÷��̾� ü�°� ������
    public static int playerMaxHp = 100; // �÷��̾� ���� �ִ�ġ
    public static float playerCurrentHp; // �÷��̾� ���� ����
    //public float playerAttackDmg = 20; // �÷��̾ ������ �� �ִ� ������

    // �÷��̾� hp��
    //public GameObject pHpBar; // Player�� Hp ���α׷��� ��
    //public GameObject canvas; // hp�ٰ� ����ִ� ĵ����
    //RectTransform hpBar; // Player�� Hp ���α׷��� ��
    public GameObject currentHpBar; // hp���� ���� hp��
    public Text currentHpText; // hp�� ǥ�����ִ� text

    // �÷��̾��� ����
    GameObject obj; // ������ ���� ������Ʈ
    public Vector3 objMove; // �������� �̵� ����
    float waitingTime; // ���� �߻� ������ �ð�
    [SerializeField]
    GameObject attackPrefab; // ������ �� ������ �ٶ�

    void Start()
    {
        this.playerRigidbody = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        isGrounded = true; // ���� ��� ������ ������ ǥ��

        // �÷��̾� ü��, ����, ������ ����
        playerMaxHp = 100; // 100
        playerCurrentHp = playerMaxHp;
        //playerAttackDmg = 20; // 20/60 �Ǵ� 20/10

        waitingTime = 0.4f; // ���� ������ �ð�

        // ���α׷��� �� ����
        currentHpBar = GameObject.Find("playerHpBar");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCurrentHp <= 0) // ���� - �׾��� �� �� �����̰� �ϱ� ����
        {
            animator.SetTrigger("Die"); // ���� ��� �����ִٰ� ���� ��� �ִϸ��̼� ������ Die()�Լ� ȣ���ϸ� �� ��ȯ
        }
        else // ����ִٸ�
        {
            // �¿��̵��� �̹��� ���� ���� ������ ���� ����
            // �¿��̵�, GetAxis ���
            horizontal = Input.GetAxis("Horizontal"); // ���۰� ���� ���� ���, ����: -1.0, ������: 1.0, �ƹ��͵� �� ����:0 �� �Է� ����
            animator.SetFloat("DirX", Mathf.Abs(horizontal)); // horizontal�� 0.02���� ũ�� Idle->Run���� �ٲ��ֱ� ���� ���
            playerRigidbody.velocity = new Vector2(horizontal * moveSpeed, playerRigidbody.velocity.y); // �÷��̾� �ӵ�

            int key = 0; // �¿� ������ ���� ������ �� ����
            if (Input.GetKey(KeyCode.RightArrow)) { key = 1; } // ��: 5
            if (Input.GetKey(KeyCode.LeftArrow)) { key = -1; } // ��: -5

            // ����
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true) // �����̽� ������ �ٴڿ� �پ��ִٸ�
            {
                this.playerRigidbody.velocity = Vector2.up * jumpForce; // ���� ����
                animator.SetBool("isJump", true); // isJump�� true�� �÷��̾� �÷��̾� ���� �ִϸ��̼� ����
                                                  //animator.speed = 1.0f;

                isGrounded = false;
            }
            else
            {
                animator.SetBool("isJump", false); // �������� �ʴ´ٸ� ���� �ִϸ��̼� �ߴ�
            }

            // �����̴� ���� ���� ����
            if (key != 0) // ������ �ִ� ���°� �ƴ� ��
            {
                transform.localScale = new Vector3(key * 5, 5, 1);
                // ���������� �����̰� ����: (5, 5, 1) ������ ��
                // �������� �����̰� ����: (-5, 5, 1) ���� ��
            }

            // ���� - Į
            if (Input.GetKeyDown(KeyCode.Alpha1)) // ���� 1Ű ������
            {
                animator.SetTrigger("Attack"); // ���� �ִϸ��̼� �����ϰ� ����
                //Invoke("SwordTrail", waitingTime); // ������ �ð� ���� Į ���� �߻�
            }
            //else
            //{
            //    animator.SetBool("isAttack", false); // �����ϰ� ���� �ʴٸ� isAttack ���� ���� �ִϸ��̼� ����
            //}
        }

        if (playerCurrentHp < 0) // �÷��̾��� hp�� 0���� ������ 0���� ����
        {
            playerCurrentHp = 0;
        }

        //if(playerCurrentHp )

        currentHpBar.GetComponent<Image>().fillAmount = (float)playerCurrentHp / playerMaxHp; // Player�� Hp�ٸ� ���� ü��/�ִ� ü������ ǥ��
        currentHpText.text = "Hp " + playerCurrentHp + "/100"; // player�� hp�� ��Ÿ���ִ� �۾� ����

        // �÷��̾ ȭ�� ������ ������ hp 30 ���̰� ���� ��ġ�� ���ƿ�
        if (transform.position.y < -10)
        {
            print("�߶�");
            playerCurrentHp -= 30;
            transform.position = new Vector2(-10.52f, -5.02f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���� ��Ұų� ������ ���� �ö󰡸� �ٽ� ���� ����
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true; // ���� ��Ҵٸ� true�� �ٲ�
        }

        //// ���� ����(�����Ӱ� �浹���� ���)
        //if (collision.gameObject.tag == "RedSlime" || collision.gameObject.tag == "GreenSlime")
        //{
        //    Damaged(); // ���� ���� �Լ� ȣ��
        //}
    }

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    // ���� ����(�����Ӱ� �浹���� ���)
    //    if (collision.gameObject.tag == "RedSlime" || collision.gameObject.tag == "GreenSlime")
    //    {
    //        Damaged(); // ���� ���� �Լ� ȣ��
    //    }
    //}

    // �÷��̾ �״´ٸ� 
    public void Die()
    {
        // ���� ȭ�� 2�� ��ȯ
        SceneManager.LoadScene("Fail2");
    }

    // Į ���� �߻�
    public void SwordTrail()
    {
        // Į ���� �߻�
        if (transform.localScale == new Vector3(5, 5, 1))
        {
            obj = Instantiate(attackPrefab, transform.position + Vector3.right, transform.rotation); // ������, ������ ��ġ, ������ ��������Ʈ�� ȸ����
            objMove = new Vector3(15, 0, 0); // ���������� �߻�
            obj.transform.localScale = new Vector3(5, 5, 1);
        }
        else
        {
            obj = Instantiate(attackPrefab, transform.position + Vector3.left, transform.rotation); // ������, ������ ��ġ, ������ ��������Ʈ�� ȸ����
            objMove = new Vector3(-15, 0, 0); // �������� �߻�
            obj.transform.localScale = new Vector3(-5, 5, 1);
        }

        Destroy(obj, 1f); // 1�� �ڿ� ������
    }

    // ���� ����(�Ű������� �������� ���� ����)(redSlime: 30, greenSlime: 10)
    public void Damaged(float gotDamage)
    {
        animator.SetTrigger("Damaged"); // �÷��̾� ���ݹ��� �ִϸ��̼� ����
        playerCurrentHp -= gotDamage;
        print("�÷��̾� ���ݹ���");
        print(gotDamage);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreenSlimeController : MonoBehaviour
{
    public Rigidbody2D rb; // Green Slime�� Rigidbody2D
    Animator animator; // Green Slime�� �ִϸ�����
    public GameObject greenSlime; // ������ ��ü�� ������(���x)

    private GameObject player; // �÷��̾� ������Ʈ
    private Animator playerAnimator; // �÷��̾��� �ִϸ�����

    public float greenSlimeMaxHp = 10; // Red Slime�� �ִ� ü��
    public float greenSlimeCurrentHp; // Red Slime�� ���� ü��
    public float greenSlimeDmg = 10; // Red Slime�� �ִ� ������

    public Transform target; // Ÿ���� �÷��̾�
    public float distance; // �÷��̾�� ������ ������ �Ÿ�
    public float attackDelay; // ���� ������
    public float delay; // ���� ���� ���� ������ �ɸ��� �ð�

    public float playerHp; // �÷��̾��� hp
    public float playerAttackDamage; // �÷��̾ �ִ� ������

    public GameObject gsHpBar; // Red Slime�� Hp ���α׷��� ��
    public GameObject canvas; // hp�ٰ� ����ִ� ĵ����
    RectTransform hpBar; // Red Slime�� Hp ���α׷��� ��
    public float height = 0.5f; // hp�ٰ� ��ġ�ϰ� �� ����
    Image currentHpBar; // hp���� ���� hp��

    public AudioSource slimeAudioSource; // ������ ������ �԰� ���� �� ���� �����

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Green Slime�� Rigidbody2D�� ������
        animator = this.GetComponent<Animator>(); // Green Slime�� Animator ������
        player = GameObject.FindGameObjectWithTag("Player");  // Player �±׸� ���� ������Ʈ ã��
        playerAnimator = player.GetComponent<Animator>(); // Player �±׸� ���� ������Ʈ�� Animator�� ������

        // ������ ü��, ������
        greenSlimeMaxHp = 10;
        greenSlimeCurrentHp = greenSlimeMaxHp;
        greenSlimeDmg = 10;

        // ���� ������
        attackDelay = 0;
        delay = 0.78f;

        hpBar = Instantiate(gsHpBar, canvas.transform).GetComponent<RectTransform>(); // ü�¹� ����(ĵ������ �ڽ����� �����ϰ� ��ġ ���� ���� hpBar�� �Ҵ�)
        currentHpBar = hpBar.transform.GetChild(0).GetComponent<Image>(); // �ڽ� ������Ʈ ������
    }

    // Update is called once per frame
    void Update()
    {
        attackDelay -= Time.deltaTime; // ���� ������ �ð� ī��Ʈ�ٿ�
        if (attackDelay < 0) { attackDelay = 0; } // ���� ������ �ð� 0 �̸� �Ǹ� 0���� ����

        // �������� �÷��̾��� ������
        if (player.transform.position.x < transform.position.x) // �÷��̾ Green Slime�� ���ʿ� �ִٸ�
        {
            transform.localScale = new Vector3(-1f, 1f, 1); // �������� ������ �ٶ󺸵��� ����
        }
        else // �÷��̾ Green Slime�� �����ʿ� �ִٸ�
        {
            transform.localScale = new Vector3(1f, 1f, 1); // �������� �������� �ٶ󺸵��� ����
        }

        if (hpBar != null)
        {
            Vector3 hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
            hpBar.position = hpBarPos; // hp���� ��ġ�� ������ �������� ��ġ�� ����
        }

        currentHpBar.fillAmount = (float)greenSlimeCurrentHp / greenSlimeMaxHp; // green Slime�� Hp�ٸ� ���� ü��/�ִ� ü������ ǥ��

        if (greenSlimeCurrentHp <= 0) // Green Slime�� ü���� 0���ϰ� �Ǹ�
        {
            Die(); // ���� �Լ� ȣ��
        }
    }


    // ���� ����
    public void Damaged(float gotDamage)
    {
        greenSlimeCurrentHp -= gotDamage; // �÷��̾�� ���� �޾��� ��, ���� �������� ü�� ����
        slimeAudioSource.Play(); // �������� ������ ��� �Ҹ� ���
    }

    // ����
    void Die()
    {
        animator.SetTrigger("Die"); // ���� �ִϸ��̼� ����
        GetComponent<Collider2D>().enabled = false; // �浹 ��Ȱ�� - �������� ���� �޷���� �÷��̾� ���� �޴� ��� �۵��� ���� �־ ����
        Destroy(hpBar.gameObject, 1f); // hp�ٵ� 1f�� �� ����
        Destroy(gameObject, 1f); // �ڱ��ڽ� 1f�� �� ����
    }

    // ������Ʈ�� ������
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���� ������Ʈ�� player�� ���: ������ ������ ����, ������ ���� ���� ��ȯ, �÷��̾� ����
        if (collision.gameObject.tag == "Player") // �÷��̾�� ��Ҵٸ� ������ ������ ����
        {
            attackDelay = delay; // ������ ����

            if (player.transform.position.x < this.transform.position.x) // �÷��̾ Green Slime�� ���ʿ� �ִٸ�
            {
                transform.localScale = new Vector3(-1f, 1f, 1); // �������� ������ �ٶ󺸵��� ����
                player.transform.localScale = new Vector3(5, 5, 1); // �÷��̾ ������ ���� �ٶ󺸵���(�´� �ִϸ��̼��� ������)
            }
            else // �÷��̾ Red Slime�� �����ʿ� �ִٸ�
            {
                transform.localScale = new Vector3(1f, 1f, 1); // �������� �������� �ٶ󺸵��� ����
                player.transform.localScale = new Vector3(-5, 5, 1); // �÷��̾ ������ ���� �ٶ󺸵���(�´� �ִϸ��̼��� ������)
            }

            playerAnimator.SetBool("isSearch", true); // �÷��̾� ������ �Դ� �ִϸ��̼� ����
            //playerAnimator.SetTrigger("Damaged");

            GameObject.FindObjectOfType<PlayerController>().Damaged(greenSlimeDmg); // �÷��̾��� Damaged �Լ� ȣ��

        }
    }

    // �÷��̾�� ��� ���°� ���ӵǸ� ��� ����
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (attackDelay == 0) // ���� ������ �ð��� 0�� �Ǿ� ���� �簳
            {
                GameObject.FindObjectOfType<PlayerController>().Damaged(greenSlimeDmg); // �÷��̾ greenSlimeDmg��ŭ ������ �Ե��� �Լ� ȣ��
                attackDelay = delay; // ���� ������ �ٽ� ä��
            }
        }
    }

    // �÷��̾�� ������ �����ڸ��� �÷��̾��� ���ݹ޴� �ִϸ��̼� ����
    private void OnCollisionExit2D(Collision2D collision)
    {
        playerAnimator.SetBool("isSearch", false);
    }
}

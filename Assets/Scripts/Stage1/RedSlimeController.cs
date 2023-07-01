using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedSlimeController : MonoBehaviour
{
    Rigidbody2D rb; // Red Slime�� Rigidbody2D
    public Transform[] moveArr; // �������� �̵��� ��ġ �迭
    public float speed = 0.8f; // �������� �̵� �ӵ�
    private int randSpot; // ���� ���õ� ��ġ
    Animator animator; // Red Slime�� �ִϸ�����

    private GameObject player; // �÷��̾� ������Ʈ
    //private PlayerController playerSc; // �÷��̾� ��ũ��Ʈ
    private Animator playerAnimator; // �÷��̾��� �ִϸ�����

    private float redSlimeMaxHp;// = 60; // Red Slime�� �ִ� ü��
    public float redSlimeCurrentHp;// = 60; // Red Slime�� ���� ü��
    private float redSlimeDmg;// = 30; // Red Slime�� �ִ� ������


    public Transform target; // Ÿ���� �÷��̾�
    public float distance; // �÷��̾�� ������ ������ �Ÿ�
    public float attackDelay = 0; // ���� ������
    public float delay = 0.78f; // ���� ���� ���� ������ �ɸ��� �ð�

    private float playerHp; // �÷��̾��� hp
    private float playerAttackDamage; // �÷��̾ �ִ� ������

    public GameObject rsHpBar; // Red Slime�� Hp ���α׷��� ��
    public GameObject canvas; // hp�ٰ� ����ִ� ĵ����
    RectTransform hpBar; // Red Slime�� Hp ���α׷��� ��
    private float height = 1.4f; // hp�ٰ� ��ġ�ϰ� �� ����
    Image currentHpBar; // hp���� ���� hp��

    public AudioSource slimeAudioSource; // ������ ������ �԰� ���� �� ���� �Ҹ�

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Red Slime�� Rigidbody2D�� ������
        animator = this.GetComponent<Animator>(); // Red Slime�� Animator ������
        player = GameObject.FindGameObjectWithTag("Player");  // Player �±׸� ���� ������Ʈ ã��
        //playerSc = GameObject.FindObjectOfType<PlayerController>();
        playerAnimator = player.GetComponent<Animator>(); // Player �±׸� ���� ������Ʈ�� Animator�� ������
        randSpot = 0; // ���� ���õ� ��ġ 0���� ����

        // ������ ü��, ������
        redSlimeMaxHp = 60; //60/60
        redSlimeCurrentHp = redSlimeMaxHp;
        redSlimeDmg = 30; // -30/100

        //currentHpBar.value = (float)redSlimeCurrentHp / redSlimeMaxHp; // Red Slime�� Hp�ٸ� ���� ü��/�ִ� ü������ ǥ��
        // ���� ������
        attackDelay = 0;
        delay = 0.78f;

        // �÷��̾� ����
        //playerHp = GameObject.FindObjectOfType<PlayerController>().playerCurrentHp; // PlayerController ��ũ��Ʈ���� �÷��̾��� ���� ���� ������
        //playerAttackDamage = GameObject.FindObjectOfType<PlayerController>().playerAttackDmg; // PlayerController ��ũ��Ʈ���� �÷��̾ �ִ� ������ ������

        // ���α׷��� �� ����
        hpBar = Instantiate(rsHpBar, canvas.transform).GetComponent<RectTransform>(); // ü�¹� ����(ĵ������ �ڽ����� �����ϰ� ��ġ ���� ���� hpBar�� �Ҵ�)
        currentHpBar = hpBar.transform.GetChild(0).GetComponent<Image>(); // �ڽ� ������Ʈ ������
    }

    // Update is called once per frame
    void Update()
    {
        attackDelay -= Time.deltaTime; // ���� ������ ī��Ʈ�ٿ�
        if (attackDelay < 0) { attackDelay = 0; } // ���� ������ 0 �̸��̸� 0���� �ʱ�ȭ

        distance = Vector3.Distance(transform.position, target.position); // �����Ӱ� �÷��̾� ���� �Ÿ�

        if(distance > 1.142868) // �����Ӱ� �÷��̾� ���� �Ÿ��� 1.142868���� Ŭ �� Move
        {
            Move(); // ������ �����̴� �Լ�
        }
        else
        {
            // ������Ʈ�� ���� �ʴ��� �� �Ÿ� ���� ������ �÷��̾� �� �����ֱ�
            if (player.transform.position.x < this.transform.position.x) // �÷��̾ Red Slime�� ���ʿ� �ִٸ�
            {
                transform.localScale = new Vector3(-1.5f, 1.5f, 1); // �������� ������ �ٶ󺸵��� ����
                player.transform.localScale = new Vector3(5, 5, 1); // �÷��̾ ������ ���� �ٶ󺸵���(�´� �ִϸ��̼��� ������)
            }
            else // �÷��̾ Red Slime�� �����ʿ� �ִٸ�
            {
                transform.localScale = new Vector3(1.5f, 1.5f, 1); // �������� �������� �ٶ󺸵��� ����
                player.transform.localScale = new Vector3(-5, 5, 1); // �÷��̾ ������ ���� �ٶ󺸵���(�´� �ִϸ��̼��� ������)
            }
        }

        if (hpBar != null)
        {
            Vector3 hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
            hpBar.position = hpBarPos; // hp���� ��ġ�� ������ �������� ��ġ��
        }

        currentHpBar.fillAmount = (float)redSlimeCurrentHp/redSlimeMaxHp; // Red Slime�� Hp�ٸ� ���� ü��/�ִ� ü������ ǥ��
        //currentHpBar.value = (float)redSlimeCurrentHp / redSlimeMaxHp; // Red Slime�� Hp�ٸ� ���� ü��/�ִ� ü������ ǥ��

        if (redSlimeCurrentHp <= 0) // Red Slime�� ü���� 0���ϰ� �Ǹ�
        {
            Die(); // ���� �Լ� ȣ��
        }
    }

    // ������ �̵�
    void Move()
    { 
            speed = 0.8f; // ������

            transform.position = Vector2.MoveTowards( // ���� ��ġ���� moveArr[randSpot] ��ġ�� ������ �ӵ� �̵�
                transform.position, moveArr[randSpot].position, speed * Time.deltaTime
            );

            if (Vector2.Distance(transform.position, moveArr[randSpot].position) < 0.2f) // �Ÿ����� 0.2���� ������ ���� ��ġ�� �̵�
            {
                randSpot = Random.Range(0, moveArr.Length); // �� ���� WayPoint�� �ִµ� ���ʿ� �ٴٸ��� �ٸ������� ��
            }

            // �������� �÷��̾��� ������
            if (player.transform.position.x < this.transform.position.x) // �÷��̾ Red Slime�� ���ʿ� �ִٸ�
            {
                transform.localScale = new Vector3(-1.5f, 1.5f, 1); // �������� ������ �ٶ󺸵��� ����
                //player.transform.localScale = new Vector3(5, 5, 1); // �÷��̾ ������ ���� �ٶ󺸵���(�´� �ִϸ��̼��� ������)
            }
            else // �÷��̾ Red Slime�� �����ʿ� �ִٸ�
            {
                transform.localScale = new Vector3(1.5f, 1.5f, 1); // �������� �������� �ٶ󺸵��� ����
                //player.transform.localScale = new Vector3(-5, 5, 1); // �÷��̾ ������ ���� �ٶ󺸵���(�´� �ִϸ��̼��� ������)
            }

        // �÷��̾� �ִϸ��̼� Damaged �ߴ�
        //playerAnimator.SetBool("isSearch", false); // isSearch�� false�� �÷��̾� ���� �޴� �ִϸ��̼� �ߴ�
    }

    // ���� ����
    public void Damaged(float damage)
    {
        //print("���� ������ ���� ����");
        redSlimeCurrentHp -= damage; // �÷��̾�� ���� �޾��� ��, ���� �������� ü�� ����
        slimeAudioSource.Play(); // ����� ���
    }

    // ����
    void Die()
    {
        speed = 0; // ������ ����
        animator.SetTrigger("Die"); // ���� �ִϸ��̼� ����
        GetComponent<Collider2D>().enabled = false; // �浹 ��Ȱ��
        //Destroy(GetComponent<Rigidbody2D>()); // �߷� ����
        Destroy(hpBar.gameObject, 1f); // hp�ٵ� 1f�� �� ����
        Destroy(gameObject, 1f); // 1f�� �� ����
    }

    // ������Ʈ�� ������
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���� ������Ʈ�� player�� ���: ������ ������ ����, ������ ���� ���� ��ȯ, �÷��̾� ����
        if(collision.gameObject.tag == "Player") // �÷��̾�� ��Ҵٸ� ������ ������ ����
        {
            speed = 0; // �������� ����
            attackDelay = delay; // ������ ����

            if (player.transform.position.x < this.transform.position.x) // �÷��̾ Red Slime�� ���ʿ� �ִٸ�
            {
                transform.localScale = new Vector3(-1.5f, 1.5f, 1); // �������� ������ �ٶ󺸵��� ����
                player.transform.localScale = new Vector3(5, 5, 1); // �÷��̾ ������ ���� �ٶ󺸵���(�´� �ִϸ��̼��� ������)
            }
            else // �÷��̾ Red Slime�� �����ʿ� �ִٸ�
            {
                transform.localScale = new Vector3(1.5f, 1.5f, 1); // �������� �������� �ٶ󺸵��� ����
                player.transform.localScale = new Vector3(-5, 5, 1); // �÷��̾ ������ ���� �ٶ󺸵���(�´� �ִϸ��̼��� ������)
            }

            playerAnimator.SetBool("isSearch", true); // �÷��̾� ������ �Դ� �ִϸ��̼� ����
            //playerAnimator.SetTrigger("Search");
            //GameObject.FindObjectOfType<PlayerController>().Damaged();

            //if (attackDelay == 0) // �̷��� �ϸ� ������ �ð� 0�Ǳ� ���� Enter ����� �� ������ �� ��
            //{
                GameObject.FindObjectOfType<PlayerController>().Damaged(redSlimeDmg); // �÷��̾��� Damaged �Լ� ȣ��
               // attackDelay = 2.5f; // ���� ������ �ٽ� ä��
            //}

        }
        // ���� ������Ʈ�� player attack�� ���: ���� ���� �ִϸ��̼� ����
        if (collision.gameObject.tag == "PlayerAttack")
        {
            animator.SetTrigger("isHurt"); // ���� ������ ������ �Դ� �ִϸ��̼� ����
            //Damaged(); // Red Slime�� Damaged �Լ� ȣ��
        }
    }

    // �÷��̾�� ��� ���°� ���ӵǸ� ��� ����
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (attackDelay == 0) // ���� ������ �ð��� 0�� �Ǿ� ���� �簳
            {
                GameObject.FindObjectOfType<PlayerController>().Damaged(redSlimeDmg); // �÷��̾�� redSlimeDmg��ŭ ������ �������� �Լ� ȣ��
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

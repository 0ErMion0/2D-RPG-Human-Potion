using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    Rigidbody2D rb; // ������ Rigidbody2D
    Animator animator; // ������ animator
    public GameObject player; // �÷��̾� ������Ʈ
    //private Animator playerAnimator; // �÷��̾��� �ִϸ�����

    public float bossMaxHp = 1000; // ������ �ִ� ü��
    public float bossCurrentHp; // ������ ���� ü��
    //public float bossAttackDmg = 30; // ������ �ִ� Į ���� ������
    //public float bossCastDmg = 20; // ������ �ִ� ���� ���� ������

    GameObject attackObj; // Į ���� ������ ���� ������Ʈ
    public Vector3 attackObjMove; // Į ���� �������� �̵� ���� - �� �ִ��� �ٽ� ����
    GameObject castObj; // ��ȯ ���� ������ ���� ������Ʈ
    
    public float attackDelay; // ���� ������ ������ �ɸ��� �ð�
    public float castDelay; // ���� ��ȯ ������ ������ �ɸ��� �ð�
    private float attackDelta; // Į ���� ���� �ð� üũ
    private float castDelta; // ��ȯ ���� �ð� üũ


    [SerializeField]
    GameObject attackPrefab; // ������ �� ������ Į ����
    [SerializeField]
    GameObject castPrefab; // ��ȯ ������ �� �������� ��

    public GameObject currentHpBar; // hp���� ���� hp��
    public Text currentHpText; // hp�� ǥ�����ִ� text

    Vector3 xyplus; // ������ ���� ��ġ ����
    Vector3 xyminus;

    public GameObject Eye; // ���� ������
    public GameObject NextStageUI; // ���� �������� ���̵� UI
    public GameObject NextStagePotal; // ���� �������� ��Ż
    //public GameObject RunAwayUI; // ���� ���̵� UI
    //public GameObject RunAwayPotal; // ������ �� �ֵ��� ���ִ� ��Ż

    // �����
    public AudioSource swordSound; // Į �ֵθ��� �Ҹ�
    public AudioSource castSound; // ��ȯ�ϴ� �Ҹ�
    public AudioSource damagedSound; // ������ �Դ� �Ҹ�

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // ������ Rigidbody2D�� ������
        animator = this.GetComponent<Animator>(); // ������ Animator ������
        player = GameObject.FindGameObjectWithTag("Player");  // Player �±׸� ���� ������Ʈ ã��
        //playerAnimator = player.GetComponent<Animator>(); // Player �±׸� ���� ������Ʈ�� Animator�� ������

        // ������ ü��, ���� ������
        bossMaxHp = 1000;
        bossCurrentHp = bossMaxHp;
        //bossAttackDmg = 30;
        //bossCastDmg = 20;

        attackDelay = 7f; // ���� ������ �ð�
        castDelay = 3f; // ��ȯ ���� ������ �ð�


        // ���α׷��� �� ����
        currentHpBar = GameObject.Find("BossHpBar");

        xyplus = new Vector3(-5, -1.3f, 0);  // ������ �߽������� ������ ������ �� �ֵ��� �����ִ� ����
        xyminus = new Vector3(5, -1.3f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        attackDelta += Time.deltaTime; // Į ���� ���� �ð� ī��Ʈ
        castDelta += Time.deltaTime; // ��ȯ ���� �ð� ī��Ʈ

        if (attackDelta > attackDelay) // Į ���� ������ �ð����� ���� �ð��� ũ�ٸ�
        {
            attackDelta = 0; // �帥 �ð� ī��Ʈ 0���� �ʱ�ȭ
            animator.SetTrigger("Attack"); // ���� �ִϸ��̼� ����
            swordSound.Play(); // Į �ֵθ��� �Ҹ� ���
            //Invoke("SwordTrail", 0.4f); // Į ���� �߻� �Լ� 0.4�� �� ȣ��
        }
        if (castDelta > castDelay) // ��ȯ ���� ������ �ð����� ���� �ð��� ũ�ٸ�
        {
            castDelta = 0; // �帥 �ð� ī��Ʈ 0���� �ʱ�ȭ
            animator.SetTrigger("Cast"); // ��ȯ ���� �ִϸ��̼� ����
            castSound.Play(); // ��ȯ�ϴ� �Ҹ� ���
            //Invoke("Cast", 0.5f); // ��ȯ ���� �Լ� 0.5�� �� ȣ��
            //Invoke("Cast", 0.5f); // ��ȯ ���� �Լ� 0.5�� �� ȣ��
        }

        // ������ ���� ���� ����
        if (player.transform.position.x < this.transform.position.x) // �÷��̾��� ��ġ�� ������ ���ʿ� ���� ��
        {
            this.gameObject.transform.localScale = new Vector3(10, 10, 1); // ������ ���� ��
            this.gameObject.transform.position = new Vector3(25, 2.55f, 1); // ������ ��ġ(�ǹ��� ������ ����� �ִ°� �ƴ϶�)
        }
        else // �÷��̾��� ��ġ�� ������ �����ʿ� ���� ��
        {
            this.gameObject.transform.localScale = new Vector3(-10, 10, 1); // ������ ������ ��
            this.gameObject.transform.position = new Vector3(18.15f, 2.55f, 1); // ������ �� ��ġ(�ǹ��� ������ ����� �ִ°� �ƴ϶�)
        }

        if (bossCurrentHp <= 0) // ������ ü���� 0���� �۴ٸ�
        {
            animator.SetTrigger("Die"); // ���� ��� �����ִٰ� ���� ��� �ִϸ��̼� ������ Die()�Լ� ȣ���ϸ� �� ��ȯ
        }

        currentHpBar.GetComponent<Image>().fillAmount = (float)bossCurrentHp / bossMaxHp; // Player�� Hp�ٸ� ���� ü��/�ִ� ü������ ǥ��
        currentHpText.text = bossCurrentHp + "/1000"; // player�� hp�� ��Ÿ���ִ� �۾� ����
    }

    // Į ���� �߻�
    public void SwordTrail()
    {
        // Į ���� �߻�
        if (player.transform.position.x<this.transform.position.x) // �÷��̾��� ��ġ�� ������ ���ʿ� ���� �� ���������� �߻�
        {
            attackObj = Instantiate(attackPrefab, transform.position + Vector3.left+ xyplus, transform.rotation); // ������, ������ ��ġ, ������ ��������Ʈ�� ȸ����
            attackObjMove = new Vector3(-15, 0, 0); // �������� �߻�
            attackObj.transform.localScale = new Vector3(7, 7, 1); // ������ �¿���� ����(���� ��)
        }
        else
        {
            attackObj = Instantiate(attackPrefab, transform.position + Vector3.right + xyminus, transform.rotation); // ������, ������ ��ġ, ������ ��������Ʈ�� ȸ����
            attackObjMove = new Vector3(15, 0, 0); // ���������� �߻�
            attackObj.transform.localScale = new Vector3(-7, 7, 1); // ������ �¿���� ����(������ ��)
        }

        Destroy(attackObj, 1f); // 1�� �ڿ� ������
    }

    // ��ȯ ���� ȣ��
    public void Cast()
    {
        castObj = Instantiate(castPrefab); // ���������� ������Ʈ ����
        int position = Random.Range(-10, 36); // �� ���� �ȿ��� �������� ������Ʈ ����
        castObj.transform.position = new Vector3(position, 1.45f, 0); // �������� ���� ��ġ ����
        Destroy(castObj, 1f); // 1�� �ڿ� ������
    }

    // ���� ����
    public void Damaged(float gotDamage)
    {
        damagedSound.Play(); // ������ �Դ� �Ҹ� ���
        bossCurrentHp -= gotDamage; // �÷��̾�� ���� �޾��� ��, ������ ü�� ����
    }

    // ����
    void Die()
    {
        //animator.SetTrigger("Die"); // ���� �ִϸ��̼� ����
        //GetComponent<Collider2D>().enabled = false; // �浹 ��Ȱ�� - �������� ���� �޷���� �÷��̾� ���� �޴� ��� �۵��� ���� �־ ����
        //Destroy(currentHpBar.gameObject, 1f); // hp�ٵ� 1f�� �� ����
        //Destroy(gameObject, 1f); // �ڱ��ڽ� 1f�� �� ����

        Destroy(gameObject, 1f); // �ڱ� �ڽ� 1�� �� ����

        Eye.SetActive(true); // ���� ������ ��Ÿ��
        NextStageUI.SetActive(true); // ���� �������� ���̵� UI ��Ÿ��
        NextStagePotal.SetActive(true); // ���� �������� �̵� ��Ż ��Ÿ��

        //// runaway���� ui ��Ÿ���� ���� ���������� �÷��̾ ���� ���δٸ� ���� �������� ���̵� ui�� ���ļ� ��Ÿ���� ���� �ذ��
        //RunAwayUI.SetActive(false); // ���� ���̵� UI�� �����������
        //RunAwayPotal.SetActive(false); // ���� ��Ż ��������� ��

        // ���� ȭ�� 2�� ��ȯ
        //SceneManager.LoadScene("Clear2");
    }
}

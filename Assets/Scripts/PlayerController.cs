using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
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
    public int playerMaxHp = 100; // �÷��̾� ���� �ִ�ġ
    public float playerCurrentHp =100; // �÷��̾� ���� ����
    public int playerMaxMp; // �÷��̾� ���� �ִ�ġ
    //public int playerCurrentMp; // �÷��̾� ���� ����
    //public float playerAttackDmg = 20; // �÷��̾ ������ �� �ִ� ������
    //public int playerMagicDmg; // �÷��̾ ���� �� �� �ִ� ������

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

    // ���� ����
    [SerializeField]
    GameObject potionExplan; // ü�� 30���ϰ� �κ��丮�� ������ ������ �����ִ� ���� ���� ���� ���̵�
    //public int redPotionCount; // �κ��丮 ����, ������ ����
    //public int bluePotionCount; // �κ��丮 ����, �Ķ��� ����
    int redPotionCount = 3; // ������ ���� ���� - stage2���� �̿�
    int bluePotionCount = 3; // �Ķ��� ���� ���� - stage2���� �̿�
    public GameObject UseItem; // �κ��丮�� ������ ���� ����
    bool upArrow = false; // �� Ű �������� ����
    bool downArrow = false; // �� Ű �������� ����

    Scene scene; // ���� �� �ľ�

    public GameObject RunAwayUI; // ���� ���̵� UI
    public GameObject RunAwayPotal; // ������ �� �ֵ��� ���ִ� ��Ż
    public GameObject boss; // ���� ������Ʈ

    // �ڷ�ƾ�� - �������� ���� ���� 5�ʵ��� �÷��̾� �� �����̵���
    bool startTimer = false; // Ÿ�̸� ���� ����
    float delayTime = 5f; // 5�� ������ �ð�

    // �����
    public AudioSource swardAudioSource; // Į �ֵη��
    public AudioSource jumpAudioSource; // ����
    public AudioSource damagedAudioSource; // ����
    public AudioSource dieAudioSource; // ����
    public AudioSource getItemAudioSource; // ������ ����
    public AudioSource createPotionAudioSource; // ���� ������

    void Start()
    {
        this.playerRigidbody = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        isGrounded = true; // ���� ��� ������ ������ ǥ��

        // �÷��̾� ü��, ����, ������ ����
        playerMaxHp = 100; // 100
        playerCurrentHp = playerMaxHp;
        //playerAttackDmg = 20; // 20/60 �Ǵ� 20/10
        //playerMagicDmg = 50;

        waitingTime = 0.4f; // ���� ������ �ð�

        // ���α׷��� �� ����
        //hpBar = Instantiate(pHpBar, canvas.transform).GetComponent<RectTransform>(); // ü�¹� ����(ĵ������ �ڽ����� �����ϰ� ��ġ ���� ���� hpBar�� �Ҵ�)
        //currentHpBar = transform.GetChild(0).GetComponent<Image>(); // �ڽ� ������Ʈ ������
        //currentHpBar.fillAmount = (float)playerMaxHp/ playerCurrentHp;

        //currentHpBar.type = Image.Type.Filled;
        //currentHpBar.fillMethod = Image.FillMethod.Horizontal;
        //currentHpBar.fillOrigin = (int)Image.OriginHorizontal.Left;

        currentHpBar = GameObject.Find("playerHpBar");
        UseItem = GameObject.FindObjectOfType<Inventory>().gameObject;

        //redPotionCount = GameObject.FindObjectOfType<Inventory>().redPotionCount;
        //bluePotionCount = GameObject.FindObjectOfType<Inventory>().bluePotionCount;

        scene = SceneManager.GetActiveScene();
        StartCoroutine(StartTimerWithDelay()); // ������ �ִ� �ڷ�ƾ
    }

    // Update is called once per frame
    void Update()
    {
        scene = SceneManager.GetActiveScene(); // ������ �� ������ ������

        // (x)�κ��丮�� redPotion�� bluePotion ������ ������ ������
        //redPotionCount = GameObject.FindObjectOfType<Inventory>().redPotionCount;
        //bluePotionCount  GameObject.FindObjectOfType<Inventory>().bluePotionCount;
        
        if(playerCurrentHp > 0 && startTimer == true) // ����ְ� 5�� ������ �ð��� ������
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
                jumpAudioSource.Play(); // ���� �Ҹ�
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
                potionExplan.transform.localScale = new Vector3(key*1, 1, 1); // ���� ���� ���̵�� �׻� ������ ������
            }

            // ���� - Į
            if (Input.GetKeyDown(KeyCode.Alpha1)) // ���� 1Ű ������
            {
                animator.SetTrigger("Attack"); // ���� �ִϸ��̼� �����ϰ� ����
                //animator.SetBool("isAttack", true); // �����ϰ� �ִٸ� isAttack true�� ���� ���� �ִϸ��̼� ����
                //Invoke("SwordTrail", waitingTime); // ������ �ð� ���� Į ���� �߻�
            }
            //else
            //{
            //    animator.SetBool("isAttack", false); // �����ϰ� ���� �ʴٸ� isAttack ���� ���� �ִϸ��̼� ����
            //}
        }
        else if (playerCurrentHp <= 0 && startTimer == true) // ���� - �׾��� �� �� �����̰� �ϱ� ����
        {
            animator.SetTrigger("Die"); // ���� ��� �����ִٰ� ���� ��� �ִϸ��̼� ������ Die()�Լ� ȣ���ϸ� �� ��ȯ
            dieAudioSource.Play(); // �״� �Ҹ�
        }

        if (playerCurrentHp < 0) // �÷��̾��� hp�� 0���� ������ 0���� ����
        {
            playerCurrentHp = 0;
        }

        if(scene.name == "Stage2") // Stage2�� ���
        {
            boss = GameObject.Find ("Bringer-of-Death"); // ������ ������Ʈ ã�Ƽ� �Ҵ�
            if (playerCurrentHp <= 30 && playerCurrentHp > 0 && bluePotionCount > 0 && redPotionCount > 0) // �÷��̾��� hp�� 30�����̰� �������� ��� �����ϸ� ���Ϲ�ư ���� ���� ����
            {
                potionExplan.SetActive(true); // ���� ���� ��� �˷��ִ� ������Ʈ ������
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    upArrow = true; // ���� Ű ������ upArrow ��
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    downArrow = true; // �Ʒ��� Ű ������ downArrow ��
                }
                if (upArrow == true && downArrow == true) // ����Ű ��� �������� ���
                {
                    createPotionAudioSource.Play(); // ���� ���� �����

                    playerCurrentHp += 100; // �÷��̾� ü�� 100 ����

                    // �÷��̾��� ������ ü���� max ü���� 100�� ������ ü���� 100���� ����
                    if (playerCurrentHp > playerMaxHp)
                        playerCurrentHp = playerMaxHp;

                    UseItem.GetComponent<Inventory>().UseItem("Blue Potion"); // Blue Potion�� ���� ���ҽ�Ŵ
                    UseItem.GetComponent<Inventory>().UseItem("Red Potion"); // Red Potion�� ���� ���ҽ�Ŵ

                    bluePotionCount-=1; redPotionCount-=1; // blue, red ������ ���� ī��Ʈ �ϳ��� ����
                    potionExplan.SetActive(false); // �����ִ°� ��
                    upArrow = false; downArrow = false; // ���� ���� �Ϸ��ؼ� ������ Ű false��
                }
            }

            if(playerCurrentHp <= 50 && playerCurrentHp >0 && bluePotionCount == 0 && redPotionCount == 0 && boss!=null) // �÷��̾��� hp�� 50���ϰ� ���� ���� ������ �Ұ����ϴٸ�
            {
                RunAwayUI.SetActive(true); // ���� ���̵� UI�� ��Ÿ������ ��
                RunAwayPotal.SetActive(true); // ���� ��Ż ��Ÿ������ ��
            }
            if (boss == null) // ������ ������ٸ�
            {
                RunAwayUI.SetActive(false); // ���� ���̵� UI�� �����������
                RunAwayPotal.SetActive(false); // ���� ��Ż ��������� ��
            }
        }

        currentHpBar.GetComponent<Image>().fillAmount = (float)playerCurrentHp / playerMaxHp; // Player�� Hp�ٸ� ���� ü��/�ִ� ü������ ǥ��
        currentHpText.text = "Hp " + playerCurrentHp + "/100"; // player�� hp�� ��Ÿ���ִ� �۾� ����

        // �÷��̾ ȭ�� ������ ������ hp 30 ���̰� ���� ��ġ�� ���ƿ�
        if (transform.position.y < -10) {
            print("�߶�");
            playerCurrentHp -= 30;
            transform.position = new Vector2(-10.52f, -5.02f);
            //SceneManager.LoadScene("GameScene");
         }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���� ��Ұų� ������ ���� �ö󰡸� �ٽ� ���� ����
        if(collision.gameObject.tag == "Ground"||collision.gameObject.tag=="RedSlime"||collision.gameObject.tag=="GreenSlime")
        {
            isGrounded = true; // ���� ��Ҵٸ� true�� �ٲ�
        }

        //// ���� ����(�����Ӱ� �浹���� ���)
        //if (collision.gameObject.tag == "RedSlime" || collision.gameObject.tag == "GreenSlime")
        //{
        //    Damaged(); // ���� ���� �Լ� ȣ��
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ������ ȹ���ϸ�
        if(collision.gameObject.tag == "Item")
        {
            getItemAudioSource.Play(); // ������ ȹ�� ����� ���
        }
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
        swardAudioSource.Play(); // Į �ֵθ��� �Ҹ�
        Destroy(obj, 1f); // 1�� �ڿ� ������
    }

    // ���� ����(�Ű������� �������� ���� ����)(redSlime: 30, greenSlime: 10)
    public void Damaged(float gotDamage)
    {
        //animator.SetBool("isSearch", true); // �÷��̾� ���� �޴� �ִϸ��̼� ����
        animator.SetTrigger("Damaged");
        damagedAudioSource.Play(); // ������ �޴� �Ҹ�

        playerCurrentHp -= gotDamage;
        print("�÷��̾� ���ݹ���");
        print(gotDamage);
    }

    // �ڷ�ƾ-> ������ ���Ŀ� Ÿ�̸� ����
    IEnumerator StartTimerWithDelay()
    {
        yield return new WaitForSeconds(delayTime);
        startTimer = true;
    }
}

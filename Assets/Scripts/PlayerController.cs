using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRigidbody; // 이동에 사용할 리지드바디 컴포넌트
    Animator animator; // 플레이어 애니메이션
    
    // 플레이어 이동
    private Vector2 velocity; // 플레이어 속도
    private float horizontal; // 플레이어 좌우 움직임
    float jumpForce = 15.0f; // 점프할 때 가할 힘
    float moveSpeed = 8.0f; // 플레이어 이동 속도
    //float runForce = 30.0f;
    //float maxRunSpeed = 2.0f;
    private bool isGrounded; // 연속 점프 금지 위해 땅에 닿았는지 여부 판단
    public int key; // 플레이어 좌우반전 위한 변수(좌: -1, 우: 1)

    // 플레이어 체력과 데미지
    public int playerMaxHp = 100; // 플레이어 생명 최대치
    public float playerCurrentHp =100; // 플레이어 현재 생명
    public int playerMaxMp; // 플레이어 마나 최대치
    //public int playerCurrentMp; // 플레이어 현재 마나
    //public float playerAttackDmg = 20; // 플레이어가 공격할 때 주는 데미지
    //public int playerMagicDmg; // 플레이어가 마법 쓸 때 주는 데미지

    // 플레이어 hp바
    //public GameObject pHpBar; // Player의 Hp 프로그래스 바
    //public GameObject canvas; // hp바가 담겨있는 캔버스
    //RectTransform hpBar; // Player의 Hp 프로그래스 바
    public GameObject currentHpBar; // hp바의 실제 hp바
    public Text currentHpText; // hp를 표시해주는 text

    // 플레이어의 공격
    GameObject obj; // 프리팹 넣을 오브젝트
    public Vector3 objMove; // 프리팹의 이동 방향
    float waitingTime; // 궤적 발사 딜레이 시간
    [SerializeField]
    GameObject attackPrefab; // 공격할 때 나가는 바람

    // 포션 제조
    [SerializeField]
    GameObject potionExplan; // 체력 30이하고 인벤토리에 아이템 있으면 보여주는 생명 포션 제조 가이드
    //public int redPotionCount; // 인벤토리 접근, 빨간색 포션
    //public int bluePotionCount; // 인벤토리 접근, 파란색 포션
    int redPotionCount = 3; // 빨간색 포션 개수 - stage2에서 이용
    int bluePotionCount = 3; // 파란색 포션 개수 - stage2에서 이용
    public GameObject UseItem; // 인벤토리의 아이템 개수 줄임
    bool upArrow = false; // 상 키 눌렀는지 여부
    bool downArrow = false; // 하 키 눌렀는지 여부

    Scene scene; // 현재 씬 파악

    public GameObject RunAwayUI; // 도망 가이드 UI
    public GameObject RunAwayPotal; // 도망갈 수 있도록 해주는 포탈
    public GameObject boss; // 보스 오브젝트

    // 코루틴용 - 스테이지 시작 전에 5초동안 플레이어 못 움직이도록
    bool startTimer = false; // 타이머 시작 여부
    float delayTime = 5f; // 5초 딜레이 시간

    // 오디오
    public AudioSource swardAudioSource; // 칼 휘두루는
    public AudioSource jumpAudioSource; // 점프
    public AudioSource damagedAudioSource; // 맞음
    public AudioSource dieAudioSource; // 죽음
    public AudioSource getItemAudioSource; // 아이템 얻음
    public AudioSource createPotionAudioSource; // 포션 제작함

    void Start()
    {
        this.playerRigidbody = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        isGrounded = true; // 땅에 닿고 있음이 맞음을 표시

        // 플레이어 체력, 마나, 데미지 관련
        playerMaxHp = 100; // 100
        playerCurrentHp = playerMaxHp;
        //playerAttackDmg = 20; // 20/60 또는 20/10
        //playerMagicDmg = 50;

        waitingTime = 0.4f; // 공격 딜레이 시간

        // 프로그래스 바 관련
        //hpBar = Instantiate(pHpBar, canvas.transform).GetComponent<RectTransform>(); // 체력바 생성(캔버스의 자식으로 생성하고 위치 변경 위해 hpBar에 할당)
        //currentHpBar = transform.GetChild(0).GetComponent<Image>(); // 자식 오브젝트 가져옴
        //currentHpBar.fillAmount = (float)playerMaxHp/ playerCurrentHp;

        //currentHpBar.type = Image.Type.Filled;
        //currentHpBar.fillMethod = Image.FillMethod.Horizontal;
        //currentHpBar.fillOrigin = (int)Image.OriginHorizontal.Left;

        currentHpBar = GameObject.Find("playerHpBar");
        UseItem = GameObject.FindObjectOfType<Inventory>().gameObject;

        //redPotionCount = GameObject.FindObjectOfType<Inventory>().redPotionCount;
        //bluePotionCount = GameObject.FindObjectOfType<Inventory>().bluePotionCount;

        scene = SceneManager.GetActiveScene();
        StartCoroutine(StartTimerWithDelay()); // 딜레이 주는 코루틴
    }

    // Update is called once per frame
    void Update()
    {
        scene = SceneManager.GetActiveScene(); // 현재의 씬 정보를 가져옴

        // (x)인벤토리의 redPotion과 bluePotion 아이템 개수를 가져옴
        //redPotionCount = GameObject.FindObjectOfType<Inventory>().redPotionCount;
        //bluePotionCount  GameObject.FindObjectOfType<Inventory>().bluePotionCount;
        
        if(playerCurrentHp > 0 && startTimer == true) // 살아있고 5초 딜레이 시간이 끝나면
        {
            // 좌우이동시 이미지 반전 위해 다음과 같이 구현
            // 좌우이동, GetAxis 사용
            horizontal = Input.GetAxis("Horizontal"); // 조작감 개선 위해 사용, 왼쪽: -1.0, 오른쪽: 1.0, 아무것도 안 누름:0 값 입력 들어옴
            animator.SetFloat("DirX", Mathf.Abs(horizontal)); // horizontal이 0.02보다 크면 Idle->Run으로 바꿔주기 위해 사용
            playerRigidbody.velocity = new Vector2(horizontal * moveSpeed, playerRigidbody.velocity.y); // 플레이어 속도

            int key = 0; // 좌우 반전을 위한 스케일 값 조절
            if (Input.GetKey(KeyCode.RightArrow)) { key = 1; } // 우: 5
            if (Input.GetKey(KeyCode.LeftArrow)) { key = -1; } // 좌: -5

            // 점프
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true) // 스페이스 눌렀고 바닥에 붙어있다면
            {
                this.playerRigidbody.velocity = Vector2.up * jumpForce; // 위로 점프
                animator.SetBool("isJump", true); // isJump가 true면 플레이어 플레이어 점프 애니메이션 시작
                                                  //animator.speed = 1.0f;
                jumpAudioSource.Play(); // 점프 소리
                isGrounded = false;
            }
            else
            {
                animator.SetBool("isJump", false); // 점프하지 않는다면 점프 애니메이션 중단
            }

            // 움직이는 방향 따라 반전
            if (key != 0) // 가만히 있는 상태가 아닐 시
            {
                transform.localScale = new Vector3(key * 5, 5, 1);
                // 오른쪽으로 움직이고 있음: (5, 5, 1) 오른쪽 봄
                // 왼쪽으로 움직이고 있음: (-5, 5, 1) 왼쪽 봄
                potionExplan.transform.localScale = new Vector3(key*1, 1, 1); // 포션 제조 가이드는 항상 오른쪽 보도록
            }

            // 공격 - 칼
            if (Input.GetKeyDown(KeyCode.Alpha1)) // 숫자 1키 누르면
            {
                animator.SetTrigger("Attack"); // 공격 애니메이션 시작하고 공격
                //animator.SetBool("isAttack", true); // 공격하고 있다면 isAttack true로 만들어서 공격 애니메이션 시작
                //Invoke("SwordTrail", waitingTime); // 딜레이 시간 이후 칼 퀘적 발사
            }
            //else
            //{
            //    animator.SetBool("isAttack", false); // 공격하고 있지 않다면 isAttack 끄고 공격 애니메이션 종료
            //}
        }
        else if (playerCurrentHp <= 0 && startTimer == true) // 죽음 - 죽었을 때 못 움직이게 하기 위해
        {
            animator.SetTrigger("Die"); // 죽은 모습 보여주다가 죽은 모습 애니메이션 끝나면 Die()함수 호출하며 씬 전환
            dieAudioSource.Play(); // 죽는 소리
        }

        if (playerCurrentHp < 0) // 플레이어의 hp가 0보다 작으면 0으로 수정
        {
            playerCurrentHp = 0;
        }

        if(scene.name == "Stage2") // Stage2일 경우
        {
            boss = GameObject.Find ("Bringer-of-Death"); // 보스의 오브젝트 찾아서 할당
            if (playerCurrentHp <= 30 && playerCurrentHp > 0 && bluePotionCount > 0 && redPotionCount > 0) // 플레이어의 hp가 30이하이고 아이템이 모두 존재하면 상하버튼 눌러 포션 제작
            {
                potionExplan.SetActive(true); // 포션 제조 방법 알려주는 오브젝트 보여줌
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    upArrow = true; // 윗쪽 키 누르면 upArrow 참
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    downArrow = true; // 아랫쪽 키 누르면 downArrow 참
                }
                if (upArrow == true && downArrow == true) // 상하키 모두 눌러졌을 경우
                {
                    createPotionAudioSource.Play(); // 포션 제작 오디오

                    playerCurrentHp += 100; // 플레이어 체력 100 더함

                    // 플레이어의 더해진 체력이 max 체력인 100을 넘으면 체력을 100으로 설정
                    if (playerCurrentHp > playerMaxHp)
                        playerCurrentHp = playerMaxHp;

                    UseItem.GetComponent<Inventory>().UseItem("Blue Potion"); // Blue Potion의 개수 감소시킴
                    UseItem.GetComponent<Inventory>().UseItem("Red Potion"); // Red Potion의 개수 감소시킴

                    bluePotionCount-=1; redPotionCount-=1; // blue, red 포션의 개수 카운트 하나씩 줄임
                    potionExplan.SetActive(false); // 보여주는거 끔
                    upArrow = false; downArrow = false; // 포션 제조 완료해서 눌렀던 키 false로
                }
            }

            if(playerCurrentHp <= 50 && playerCurrentHp >0 && bluePotionCount == 0 && redPotionCount == 0 && boss!=null) // 플레이어의 hp가 50이하고 생명 포션 제조가 불가능하다면
            {
                RunAwayUI.SetActive(true); // 도망 가이드 UI가 나타나도록 함
                RunAwayPotal.SetActive(true); // 도망 포탈 나타나도록 함
            }
            if (boss == null) // 보스가 사라졌다면
            {
                RunAwayUI.SetActive(false); // 도망 가이드 UI가 사라지도록함
                RunAwayPotal.SetActive(false); // 도망 포탈 사라지도록 함
            }
        }

        currentHpBar.GetComponent<Image>().fillAmount = (float)playerCurrentHp / playerMaxHp; // Player의 Hp바를 현재 체력/최대 체력으로 표현
        currentHpText.text = "Hp " + playerCurrentHp + "/100"; // player의 hp를 나타내주는 글씨 수정

        // 플레이어가 화면 밖으로 나가면 hp 30 깎이고 원래 위치로 돌아옴
        if (transform.position.y < -10) {
            print("추락");
            playerCurrentHp -= 30;
            transform.position = new Vector2(-10.52f, -5.02f);
            //SceneManager.LoadScene("GameScene");
         }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 땅에 닿았거나 슬라임 위에 올라가면 다시 점프 가능
        if(collision.gameObject.tag == "Ground"||collision.gameObject.tag=="RedSlime"||collision.gameObject.tag=="GreenSlime")
        {
            isGrounded = true; // 땅에 닿았다면 true로 바뀜
        }

        //// 공격 받음(슬라임과 충돌했을 경우)
        //if (collision.gameObject.tag == "RedSlime" || collision.gameObject.tag == "GreenSlime")
        //{
        //    Damaged(); // 공격 받음 함수 호출
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 아이템 획득하면
        if(collision.gameObject.tag == "Item")
        {
            getItemAudioSource.Play(); // 아이템 획득 오디오 재생
        }
    }

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    // 공격 받음(슬라임과 충돌했을 경우)
    //    if (collision.gameObject.tag == "RedSlime" || collision.gameObject.tag == "GreenSlime")
    //    {
    //        Damaged(); // 공격 받음 함수 호출
    //    }
    //}

    // 플레이어가 죽는다면 
    public void Die()
    {
        // 실패 화면 2로 전환
        SceneManager.LoadScene("Fail2");
    }

    // 칼 궤적 발사
    public void SwordTrail()
    {
        // 칼 궤적 발사
        if (transform.localScale == new Vector3(5, 5, 1))
        {
            obj = Instantiate(attackPrefab, transform.position + Vector3.right, transform.rotation); // 프리팹, 생성될 위치, 생성될 오브제젝트의 회전값
            objMove = new Vector3(15, 0, 0); // 오른쪽으로 발사
            obj.transform.localScale = new Vector3(5, 5, 1);
        }
        else
        {
            obj = Instantiate(attackPrefab, transform.position + Vector3.left, transform.rotation); // 프리팹, 생성될 위치, 생성될 오브제젝트의 회전값
            objMove = new Vector3(-15, 0, 0); // 왼쪽으로 발사
            obj.transform.localScale = new Vector3(-5, 5, 1);
        }
        swardAudioSource.Play(); // 칼 휘두르는 소리
        Destroy(obj, 1f); // 1초 뒤에 없어짐
    }

    // 공격 받음(매개변수로 데미지값 전달 받음)(redSlime: 30, greenSlime: 10)
    public void Damaged(float gotDamage)
    {
        //animator.SetBool("isSearch", true); // 플레이어 공격 받는 애니메이션 시작
        animator.SetTrigger("Damaged");
        damagedAudioSource.Play(); // 데미지 받는 소리

        playerCurrentHp -= gotDamage;
        print("플레이어 공격받음");
        print(gotDamage);
    }

    // 코루틴-> 딜레이 이후에 타이머 시작
    IEnumerator StartTimerWithDelay()
    {
        yield return new WaitForSeconds(delayTime);
        startTimer = true;
    }
}

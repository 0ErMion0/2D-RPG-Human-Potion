using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class S2PlayerController : MonoBehaviour
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
    public static int playerMaxHp = 100; // 플레이어 생명 최대치
    public static float playerCurrentHp; // 플레이어 현재 생명
    //public float playerAttackDmg = 20; // 플레이어가 공격할 때 주는 데미지

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

    void Start()
    {
        this.playerRigidbody = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        isGrounded = true; // 땅에 닿고 있음이 맞음을 표시

        // 플레이어 체력, 마나, 데미지 관련
        playerMaxHp = 100; // 100
        playerCurrentHp = playerMaxHp;
        //playerAttackDmg = 20; // 20/60 또는 20/10

        waitingTime = 0.4f; // 공격 딜레이 시간

        // 프로그래스 바 관련
        currentHpBar = GameObject.Find("playerHpBar");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCurrentHp <= 0) // 죽음 - 죽었을 때 못 움직이게 하기 위해
        {
            animator.SetTrigger("Die"); // 죽은 모습 보여주다가 죽은 모습 애니메이션 끝나면 Die()함수 호출하며 씬 전환
        }
        else // 살아있다면
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
            }

            // 공격 - 칼
            if (Input.GetKeyDown(KeyCode.Alpha1)) // 숫자 1키 누르면
            {
                animator.SetTrigger("Attack"); // 공격 애니메이션 시작하고 공격
                //Invoke("SwordTrail", waitingTime); // 딜레이 시간 이후 칼 퀘적 발사
            }
            //else
            //{
            //    animator.SetBool("isAttack", false); // 공격하고 있지 않다면 isAttack 끄고 공격 애니메이션 종료
            //}
        }

        if (playerCurrentHp < 0) // 플레이어의 hp가 0보다 작으면 0으로 수정
        {
            playerCurrentHp = 0;
        }

        //if(playerCurrentHp )

        currentHpBar.GetComponent<Image>().fillAmount = (float)playerCurrentHp / playerMaxHp; // Player의 Hp바를 현재 체력/최대 체력으로 표현
        currentHpText.text = "Hp " + playerCurrentHp + "/100"; // player의 hp를 나타내주는 글씨 수정

        // 플레이어가 화면 밖으로 나가면 hp 30 깎이고 원래 위치로 돌아옴
        if (transform.position.y < -10)
        {
            print("추락");
            playerCurrentHp -= 30;
            transform.position = new Vector2(-10.52f, -5.02f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 땅에 닿았거나 슬라임 위에 올라가면 다시 점프 가능
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true; // 땅에 닿았다면 true로 바뀜
        }

        //// 공격 받음(슬라임과 충돌했을 경우)
        //if (collision.gameObject.tag == "RedSlime" || collision.gameObject.tag == "GreenSlime")
        //{
        //    Damaged(); // 공격 받음 함수 호출
        //}
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

        Destroy(obj, 1f); // 1초 뒤에 없어짐
    }

    // 공격 받음(매개변수로 데미지값 전달 받음)(redSlime: 30, greenSlime: 10)
    public void Damaged(float gotDamage)
    {
        animator.SetTrigger("Damaged"); // 플레이어 공격받음 애니메이션 시작
        playerCurrentHp -= gotDamage;
        print("플레이어 공격받음");
        print(gotDamage);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class S3ItemButton : MonoBehaviour
{
    // Stage3 포션 제조
    public List<Button> buttons; // 버튼 리스트
    public GameObject HumanPotion; // human potion 이미지

    private int clickedButtonCount; // 버튼이 몇개 클릭되었는지 카운트

    Scene scene; // 현재 씬 확인

    public AudioSource getItemAudioSource; // human potion 제작 완료 오디오

    private void Start()
    {
        clickedButtonCount = 0;

        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => OnClickItemButton(button)); // 버튼 클릭될 때마다 이벤트 리스너 추가, 함수 호출
        }
    }

    private void Update()
    {
        scene = SceneManager.GetActiveScene(); // 현재의 씬 정보를 가져옴
    }

    private void OnClickItemButton(Button clickedButton)
    {
        clickedButton.interactable = false; // 클릭된 버튼 비활성화
        clickedButtonCount++; // 버튼 눌린 횟수 증가

        if (clickedButtonCount == buttons.Count) // 클랙된 버튼의 수가 리스트에 저장된 버튼의 수와 동일하다면
        {
            HumanPotion.SetActive(true); // human potion 활성화
            getItemAudioSource.Play(); // 포션 제작 알리는 오디오 재생
        }
    }

    public void OnClickGetPotion()
    {
        if (scene.name == "Stage3") // 스테이지3라면
        {
            SceneManager.LoadScene("Clear2"); // Clear2로 전환
        }
        if (scene.name == "Stage3_2") // 스테이지3_2라면
        {
            SceneManager.LoadScene("Clear1"); // Clear1로 전환
        }
    }
}

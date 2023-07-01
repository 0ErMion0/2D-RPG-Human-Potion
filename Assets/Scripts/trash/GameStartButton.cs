using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameStartButton : MonoBehaviour
{
    AudioSource audioSoure;
    private void Start()
    {
        audioSoure = GetComponent<AudioSource>();
    }
    public void OnClick()
    {
        //audioSoure.Play(); // 클릭음 시작
        SceneManager.LoadScene("Stage1Explan"); // Stage1으로 전환
    }
}

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
        //audioSoure.Play(); // Ŭ���� ����
        SceneManager.LoadScene("Stage1Explan"); // Stage1���� ��ȯ
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TalkPanel : MonoBehaviour
{
    public GameObject talkPanel; // 대화 UI 패널 오브젝트
    public Text text; // 대화 텍스트
    int clickCount;// = 0; // 버튼 누른 횟수
    //int getGem = false;
    
    // Start is called before the first frame update
    void Start()
    {
        clickCount = 0;
        text.text = "평화롭던 나날들..."; // 초기 text
    }

    // Update is called once per frame
    void Update()
    {
        //switch (clickCount) // 잘 됐는데 갑자기 안됨.. clickCount가 늘어나야되는데 0, 1, ...의 상태가 공존해서 케이스0 텍스트만 보여짐
        //{
        //    case 0:
        //        text.text = "평화롭던 나날들...";
        //        print("0");
        //        break;
        //    case 1:
        //        text.text = "몬스터들이 하나둘 나타나며 ...";
        //        print("1");
        //        break;
        //    case 2:
        //        text.text = "인간들은 몬스터로 변하기 시작하였고 ...";
        //        print("2");
        //        break;
        //    case 3:
        //        text.text = "인간으로써 남기 위해서는 재료를 모아 포션을 만들어야된다는 소문이 퍼졌다 ...";
        //        break;
        //    case 4:
        //        text.text = "어서 재료를 모아 포션을 만들어라!!";
        //        break;
        //    case 5:
        //        text.text = "";
        //        SceneManager.LoadScene("Stage1"); // 스테이지1으로 씬 전환
        //        break;

        //}
    }

    public void OnClick()
    {
        clickCount+=1; // Front 버튼 누르면 다음 말풍선으로 넘어감
        switch (clickCount)
        {
            case 0:
                text.text = "평화롭던 나날들...";
                print("0");
                break;
            case 1:
                text.text = "몬스터들이 하나둘 나타나며 ...";
                print("1");
                break;
            case 2:
                text.text = "인간들은 몬스터로 변하기 시작하였고 ...";
                print("2");
                break;
            case 3:
                text.text = "인간으로써 남기 위해서는 재료를 모아 포션을 만들어야된다는 소문이 퍼졌다 ...";
                break;
            case 4:
                text.text = "어서 재료를 모아 포션을 만들어라!!";
                break;
            case 5:
                text.text = "";
                SceneManager.LoadScene("Stage1"); // 스테이지1으로 씬 전환
                break;
        }
    }

    public void Skip()
    {
        SceneManager.LoadScene("Stage1"); // 스테이지1으로 씬 전환
    }
}

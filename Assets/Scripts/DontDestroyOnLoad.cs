using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    private static DontDestroyOnLoad s_Instance = null;
    //Scene scene;

    private void Awake()
    {
        //scene = SceneManager.GetActiveScene();

        //if (scene.name == "Stage1" || scene.name == "Stage2")
        //{
        //    if (s_Instance)
        //    {
        //        DestroyImmediate(this.gameObject); // s_Instance가 있으면 현재 게임 오브젝트 파괴
        //                                           // -> 다른 오브젝트가 DontDestroyOnLoad로 설정되어 있을 때 중복 생성 방지 위해 넣음
        //    }

        //    //현재 게임 오브젝트 할당하고 게임 오브젝트를 씬 전환 시에도 파괴되지 않도록 함
        //    s_Instance = this;
        //    DontDestroyOnLoad(this.gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}

        if (s_Instance)
        {
            DestroyImmediate(this.gameObject); // s_Instance가 있으면 현재 게임 오브젝트 파괴
                                               // -> 다른 오브젝트가 DontDestroyOnLoad로 설정되어 있을 때 중복 생성 방지 위해 넣음
        }

        //현재 게임 오브젝트 할당하고 게임 오브젝트를 씬 전환 시에도 파괴되지 않도록 함
        s_Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    //private void OnEnable()
    //{
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //}

    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{

    //}

    //private void OnDisable()
    //{
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //}

    //private void Update()
    //{
    //    DestroyCheck();
    //}

}


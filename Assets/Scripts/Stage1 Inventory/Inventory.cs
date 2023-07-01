using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    // 아이템 최소 획득 개수
    public int eggReqCount = 1;
    public int featherReqCount = 1;
    public int boneReqCount = 3;
    public int bluePotionReqCount = 3;
    public int redPotionReqCount = 3;

    // 요구되는 아이템 개수
    public int requiredEggCount;
    public int requiredFeatherCount;
    public int requiredBoneCount;
    public int requiredBluePotionCount;
    public int requiredRedPotionCount;

    // 획득한 아이템 개수 체크용
    public int eggCount;// = 0;
    public int featherCount;// = 0;
    public int boneCount;// = 0;
    public int bluePotionCount;// = 0;
    public int redPotionCount;// = 0;


    [SerializeField]
    private GameObject go_SlotsParent; // Slot들의 부모인 Grid Setting 

    private Slot[] slots;  // 슬롯들 배열

    [SerializeField]
    private GameObject stageClear; // 스테이지 클리어시 나타나는 글씨
    [SerializeField]
    private GameObject potal; // 스테이지 클리어시 나타나는 포탈

    Scene scene;
    private Transform[] slotTransforms; // 슬롯 배열 업데이트용

    private void Awake()
    {
        slotTransforms = go_SlotsParent.GetComponentsInChildren<Transform>(); // go_SlotsParent의 자식인 모든 Transform 가져옴
        slots = new Slot[slotTransforms.Length - 1]; // 슬롯 배열의 크기를 슬롯 개수에 맞게 설정 (부모 Transform도 포함되는거라 -1 해줌)
        for (int i = 1; i < slotTransforms.Length; i++)
        {
            slots[i - 1] = slotTransforms[i].GetComponent<Slot>(); // 각 슬롯의 컴포넌트 할당
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //instance = this; // (추가)인스턴스 설정
        slots = go_SlotsParent.GetComponentsInChildren<Slot>(); // 슬롯 컴포넌트 가져옴
        SetRequiredItemCount(); // 요구되는 아이템 개수를 최소 획득 개수로 초기화하는 함수 호출
        scene = SceneManager.GetActiveScene(); // 현재 씬 정보 가져옴

        // 현재 획득한 아이템 개수 0으로 초기화
        eggCount = 0;
        featherCount = 0;
        boneCount = 0;
        bluePotionCount = 0;
        redPotionCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>(); // 슬롯의 내용 계속 가져올 수 있도록 함
        // (x)이렇게 안하면 나중에 포션 제조하고 red, blue 포션의 개수가 감소하지 않아, CheckRequiredItemCount() 함수에서 아이템 개수 변수에 반영하지 못해서
        // 근데 이게 슬롯 배열인 slots를 실제로 슬롯의 개수와 일치하도록 업데이트하지 않음..
        // => slots 배열의 크기를 업데이트하는 작업 필요..?

        if (CheckRequiredItemCount()==true && scene.name=="Stage1") // 필요한 아이템 개수 모두 충족시킨다면 Stage2로 넘어감
        {
            //SceneManager.LoadScene("Stage2");
            stageClear.SetActive(true); // 스테이지 클리어 UI 나타내기
            potal.SetActive(true); // 포탈 나타냄
        }
    }


    public void AcquireItem(Item _item, int _count = 1) // 같은 종류의 아이템 이미 있는지 검사
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)  // null 이라면 slots[i].item.itemName 할 때 런타임 에러 나서
            {
                if (slots[i].item.itemName == _item.itemName)
                {
                    slots[i].SetSlotCount(_count); // -> (있다면)아이템 갯수만 더해줌
                    return;
                }
            }
            else
            {
                slots[i].AddItem(_item, _count); // -> (없다면)빈 슬롯 찾아서 Slot.cs의 AddItem() 호출
                return;
            }
        }

        
    }

    // 요구되는 아이템 개수를 아이템 최소 획득 개수로 초기화
    private void SetRequiredItemCount()
    {
        requiredEggCount = eggReqCount;
        requiredFeatherCount = featherReqCount;
        requiredBoneCount = boneReqCount;
        requiredBluePotionCount = bluePotionReqCount;
        requiredRedPotionCount = redPotionReqCount;
    }

    private bool CheckRequiredItemCount() // 필요한 아이템 개수 모두 충족했는지 확인
    {
        //int eggCount = 0;
        //int featherCount = 0;
        //int boneCount = 0;
        //int bluePotionCount = 0;
        //int redPotionCount = 0;

        for (int i = 0; i < slots.Length; i++) // 슬롯의 길이만큼 돌면서
        {
            if (slots[i].item != null) // 슬롯 자리에 아이템이 들어있다면
            {
                if (slots[i].item.itemName == "Egg") // 아이템의 이름이 Egg라면
                {
                    eggCount = slots[i].itemCount; // Egg의 개수를 더해줌
                }
                if (slots[i].item.itemName == "Feather")
                {
                    featherCount = slots[i].itemCount;
                }
                if (slots[i].item.itemName == "Bone")
                {
                    boneCount = slots[i].itemCount;
                }
                if (slots[i].item.itemName == "Blue Potion")
                {
                    bluePotionCount = slots[i].itemCount;
                }
                if (slots[i].item.itemName == "Red Potion")
                {
                    redPotionCount = slots[i].itemCount;
                }
            }
        }

        // 필요한 아이템을 모두 얻으면 true 반환
        if(eggCount >= requiredEggCount && featherCount >= requiredFeatherCount && boneCount >= requiredBoneCount && redPotionCount >= requiredRedPotionCount && bluePotionCount>= requiredBluePotionCount)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    // Stage2에서 아이템 사용을 위한 함수 추가 - 인벤토리에서 해당 아이템 찾아 개수 감소시킴
    public void UseItem(string itemName)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null && slots[i].item.itemName == itemName) // 슬롯에 들어있는 아이템의 이름이 받아온 아이템 이름과 일치한다면
            {
                print(itemName);
                slots[i].SetSlotCount(-1); // 아이템 갯수 1 감소
            }
           //redPotionCount--; bluePotionCount--;
        }
    }
}

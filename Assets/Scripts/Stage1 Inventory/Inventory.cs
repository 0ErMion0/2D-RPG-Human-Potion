using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    // ������ �ּ� ȹ�� ����
    public int eggReqCount = 1;
    public int featherReqCount = 1;
    public int boneReqCount = 3;
    public int bluePotionReqCount = 3;
    public int redPotionReqCount = 3;

    // �䱸�Ǵ� ������ ����
    public int requiredEggCount;
    public int requiredFeatherCount;
    public int requiredBoneCount;
    public int requiredBluePotionCount;
    public int requiredRedPotionCount;

    // ȹ���� ������ ���� üũ��
    public int eggCount;// = 0;
    public int featherCount;// = 0;
    public int boneCount;// = 0;
    public int bluePotionCount;// = 0;
    public int redPotionCount;// = 0;


    [SerializeField]
    private GameObject go_SlotsParent; // Slot���� �θ��� Grid Setting 

    private Slot[] slots;  // ���Ե� �迭

    [SerializeField]
    private GameObject stageClear; // �������� Ŭ����� ��Ÿ���� �۾�
    [SerializeField]
    private GameObject potal; // �������� Ŭ����� ��Ÿ���� ��Ż

    Scene scene;
    private Transform[] slotTransforms; // ���� �迭 ������Ʈ��

    private void Awake()
    {
        slotTransforms = go_SlotsParent.GetComponentsInChildren<Transform>(); // go_SlotsParent�� �ڽ��� ��� Transform ������
        slots = new Slot[slotTransforms.Length - 1]; // ���� �迭�� ũ�⸦ ���� ������ �°� ���� (�θ� Transform�� ���ԵǴ°Ŷ� -1 ����)
        for (int i = 1; i < slotTransforms.Length; i++)
        {
            slots[i - 1] = slotTransforms[i].GetComponent<Slot>(); // �� ������ ������Ʈ �Ҵ�
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //instance = this; // (�߰�)�ν��Ͻ� ����
        slots = go_SlotsParent.GetComponentsInChildren<Slot>(); // ���� ������Ʈ ������
        SetRequiredItemCount(); // �䱸�Ǵ� ������ ������ �ּ� ȹ�� ������ �ʱ�ȭ�ϴ� �Լ� ȣ��
        scene = SceneManager.GetActiveScene(); // ���� �� ���� ������

        // ���� ȹ���� ������ ���� 0���� �ʱ�ȭ
        eggCount = 0;
        featherCount = 0;
        boneCount = 0;
        bluePotionCount = 0;
        redPotionCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        slots = go_SlotsParent.GetComponentsInChildren<Slot>(); // ������ ���� ��� ������ �� �ֵ��� ��
        // (x)�̷��� ���ϸ� ���߿� ���� �����ϰ� red, blue ������ ������ �������� �ʾ�, CheckRequiredItemCount() �Լ����� ������ ���� ������ �ݿ����� ���ؼ�
        // �ٵ� �̰� ���� �迭�� slots�� ������ ������ ������ ��ġ�ϵ��� ������Ʈ���� ����..
        // => slots �迭�� ũ�⸦ ������Ʈ�ϴ� �۾� �ʿ�..?

        if (CheckRequiredItemCount()==true && scene.name=="Stage1") // �ʿ��� ������ ���� ��� ������Ų�ٸ� Stage2�� �Ѿ
        {
            //SceneManager.LoadScene("Stage2");
            stageClear.SetActive(true); // �������� Ŭ���� UI ��Ÿ����
            potal.SetActive(true); // ��Ż ��Ÿ��
        }
    }


    public void AcquireItem(Item _item, int _count = 1) // ���� ������ ������ �̹� �ִ��� �˻�
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)  // null �̶�� slots[i].item.itemName �� �� ��Ÿ�� ���� ����
            {
                if (slots[i].item.itemName == _item.itemName)
                {
                    slots[i].SetSlotCount(_count); // -> (�ִٸ�)������ ������ ������
                    return;
                }
            }
            else
            {
                slots[i].AddItem(_item, _count); // -> (���ٸ�)�� ���� ã�Ƽ� Slot.cs�� AddItem() ȣ��
                return;
            }
        }

        
    }

    // �䱸�Ǵ� ������ ������ ������ �ּ� ȹ�� ������ �ʱ�ȭ
    private void SetRequiredItemCount()
    {
        requiredEggCount = eggReqCount;
        requiredFeatherCount = featherReqCount;
        requiredBoneCount = boneReqCount;
        requiredBluePotionCount = bluePotionReqCount;
        requiredRedPotionCount = redPotionReqCount;
    }

    private bool CheckRequiredItemCount() // �ʿ��� ������ ���� ��� �����ߴ��� Ȯ��
    {
        //int eggCount = 0;
        //int featherCount = 0;
        //int boneCount = 0;
        //int bluePotionCount = 0;
        //int redPotionCount = 0;

        for (int i = 0; i < slots.Length; i++) // ������ ���̸�ŭ ���鼭
        {
            if (slots[i].item != null) // ���� �ڸ��� �������� ����ִٸ�
            {
                if (slots[i].item.itemName == "Egg") // �������� �̸��� Egg���
                {
                    eggCount = slots[i].itemCount; // Egg�� ������ ������
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

        // �ʿ��� �������� ��� ������ true ��ȯ
        if(eggCount >= requiredEggCount && featherCount >= requiredFeatherCount && boneCount >= requiredBoneCount && redPotionCount >= requiredRedPotionCount && bluePotionCount>= requiredBluePotionCount)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    // Stage2���� ������ ����� ���� �Լ� �߰� - �κ��丮���� �ش� ������ ã�� ���� ���ҽ�Ŵ
    public void UseItem(string itemName)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null && slots[i].item.itemName == itemName) // ���Կ� ����ִ� �������� �̸��� �޾ƿ� ������ �̸��� ��ġ�Ѵٸ�
            {
                print(itemName);
                slots[i].SetSlotCount(-1); // ������ ���� 1 ����
            }
           //redPotionCount--; bluePotionCount--;
        }
    }
}

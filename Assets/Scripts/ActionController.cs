using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private Inventory theInventory;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item") // 부딪힌게 아이템이라면
        {
            // 인벤토리에 아이템 뜨게 만듦
            theInventory.AcquireItem(collision.transform.GetComponent<ItemPickUp>().item);
            collision.gameObject.SetActive(false); // 아이템 사라지게 만듦
        }
    }
}

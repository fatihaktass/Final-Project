using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectibleSword : MonoBehaviour
{
    [SerializeField] GameObject swordInHand;
    [SerializeField] LayerMask playerLayer;
    bool nearTheSword;

    void Update()
    {
        SwordItem();
    }

    void SwordItem()
    {
        nearTheSword = Physics.CheckSphere(transform.position, 2f, playerLayer);
        if (nearTheSword)
        {
            Debug.Log("Yakýn");
            if (Input.GetKeyDown(KeyCode.F))
            {
                gameObject.SetActive(false);
                swordInHand.SetActive(true);
            }
        }
    }
}

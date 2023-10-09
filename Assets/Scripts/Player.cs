using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void Start()
    {
        Knapsack.Instance.StoreItem(1);
        for(int i = 0;i < 8;i++)
            Knapsack.Instance.StoreItem(2);
        Knapsack.Instance.StoreItem(2,0,3);
        Knapsack.Instance.StoreItem(2,0,3);
        Knapsack.Instance.StoreItem(2,0,3);
    }
    void Update()
    {
        //随机将一个物体放到背包里面
        if (Input.GetKeyDown(KeyCode.G))
        { 
            int id = Random.Range(1,18);
            float random = Random.Range(0.0f, 1.0f);
            if (random < 0.5)
                Knapsack.Instance.StoreItem(id);
            else
                Knapsack.Instance.AddItem(id,1);
        }
    }
}

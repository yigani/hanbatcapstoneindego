using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    public static ItemDB instance;
    public GameObject fieldItemPrefab;
    public List<Item> itemDB = new List<Item>();
    void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        
    }
    public void Generate_Item(Vector3 pos,int type = -1,int cost = 0)
    {
        GameObject go  = Instantiate(fieldItemPrefab,pos,Quaternion.identity);
        if(type == -1)
            go.GetComponent<FieldItem>().SetItem(itemDB[Random.Range(0,itemDB.Count)]);
        else
            go.GetComponent<FieldItem>().SetItem(itemDB[type], cost);
    }
}

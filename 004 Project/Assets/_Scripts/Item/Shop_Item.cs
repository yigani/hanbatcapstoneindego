using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro 네임스페이스

public class Shop_Item : MonoBehaviour
{
    public List<GameObject> shopItem_pos = new List<GameObject>(3);
    

    void Start()
    {
        for (int i = 0; i < shopItem_pos.Count; i++)
        {
            // 아이템 생성
            ItemDB.instance.Generate_Item(shopItem_pos[i].transform.position, i, 100);

            // 텍스트 생성
            CreateText(shopItem_pos[i].transform.position + Vector3.up * 2, "100");
        }
    }

    void CreateText(Vector3 position, string textContent)
    {
        // 새 게임 오브젝트 생성
        GameObject textObj = new GameObject("ShopItemText");

        // 위치 설정
        textObj.transform.position = position;

        // TextMeshPro 컴포넌트 추가
        TextMeshPro textMesh = textObj.AddComponent<TextMeshPro>();
        
        // 텍스트 설정
        textMesh.text = textContent;
        textMesh.fontSize = 2f; // 텍스트 크기 조정
        textMesh.alignment = TextAlignmentOptions.Center; // 가운데 정렬
        textMesh.color = Color.white; // 텍스트 색상 설정
        

        // 텍스트를 카메라에 따라 보이게 고정 (옵션)
        textObj.transform.LookAt(Camera.main.transform);
        textObj.transform.Rotate(0, 180, 0); // 방향 수정
    }
}

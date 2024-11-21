using UnityEngine;
using UnityEngine.UI;

public class MapUIController : MonoBehaviour
{
    [SerializeField] private RawImage mapImage; // 맵 이미지 UI
    [SerializeField] private RectTransform playerMarker; // 플레이어 위치 마커
    [SerializeField] private Transform playerTransform; // 플레이어 Transform
    [SerializeField] private Tile_Map_Create tileMapCreate; // Tile_Map_Create 참조
    private Texture2D mapTexture;
    private bool isMapVisible = false;

    private void Start()
    {
        MapToImageConverter converter = GetComponent<MapToImageConverter>();
        mapTexture = converter.ConvertMapToImage();
        mapImage.texture = mapTexture;

        // 초기 UI 비활성화
        mapImage.gameObject.SetActive(false);
        playerMarker.gameObject.SetActive(false);
    }

    private void Update()
    {
        // M 키로 맵 토글
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMap();
        }

        // 맵이 활성화된 경우 플레이어 위치 업데이트
        if (isMapVisible)
        {
            UpdatePlayerMarker();
        }
    }

    private void ToggleMap()
    {
        isMapVisible = !isMapVisible;
        mapImage.gameObject.SetActive(isMapVisible);
        playerMarker.gameObject.SetActive(isMapVisible);
    }

    private void UpdatePlayerMarker()
    {
        Vector3 playerPos = playerTransform.position;
        Vector3Int cellPosition = tileMapCreate.Tilemap.WorldToCell(playerPos);

        // 맵 좌표 계산
        int width = tileMapCreate.horizontal;
        int height = tileMapCreate.vertical;
        Vector2 normalizedPosition = new Vector2(
            (float)cellPosition.x / width,
            (float)cellPosition.y / height
        );

        // UI에 반영
        playerMarker.anchoredPosition = new Vector2(
            normalizedPosition.x * mapImage.rectTransform.rect.width,
            normalizedPosition.y * mapImage.rectTransform.rect.height
        );
    }
}
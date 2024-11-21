using UnityEngine;

public class MapToImageConverter : MonoBehaviour
{
    [SerializeField] private Tile_Map_Create tileMapCreate; // Tile_Map_Create 참조
    [SerializeField] private Color wallColor = Color.black; // 벽 색상
    [SerializeField] private Color floorColor = Color.white; // 바닥 색상
    [SerializeField] private Color defaultColor = Color.clear; // 빈 공간 색상

    public Texture2D ConvertMapToImage()
    {
        int[,] tileData = tileMapCreate.GetTileData();
        int width = tileMapCreate.horizontal;
        int height = tileMapCreate.vertical;

        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color color;
                switch (tileData[x, y])
                {
                    case 0: // 빈 공간
                        color = defaultColor;
                        break;
                    case 10: // 바닥
                        color = floorColor;
                        break;
                    default: // 벽
                        color = wallColor;
                        break;
                }
                texture.SetPixel(x, y, color);
            }
        }

        texture.Apply();
        return texture;
    }
}
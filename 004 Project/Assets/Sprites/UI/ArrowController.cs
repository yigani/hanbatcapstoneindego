using UnityEngine;
using UnityEngine.UI;

public class ArrowController : MonoBehaviour
{
    [SerializeField] private RectTransform arrowImage; // 화살표 이미지 (UI)
    [SerializeField] private Text distanceText;        // 남은 거리 텍스트 UI
    [SerializeField] private float hideDistance = 5.0f; // 화살표와 텍스트가 사라질 거리
    private Transform playerTransform;                // Player의 Transform
    private Transform portalTransform;                // Portal의 Transform
    private Camera mainCamera;                        // 메인 카메라 참조

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (playerTransform == null || portalTransform == null) return;

        // 1. Player와 Portal 간 거리 계산
        float distanceToPortal = Vector3.Distance(playerTransform.position, portalTransform.position);

        // 2. 거리 조건에 따라 화살표 및 텍스트 UI 표시/숨김 처리
        if (distanceToPortal <= hideDistance)
        {
            arrowImage.gameObject.SetActive(false); // 특정 거리 이내면 숨김
            distanceText.gameObject.SetActive(false);
            return;
        }
        else
        {
            arrowImage.gameObject.SetActive(true); // 거리 바깥이면 표시
            distanceText.gameObject.SetActive(true);
        }

        // 3. Player 머리 위 위치 설정
        Vector3 playerHeadPosition = playerTransform.position + Vector3.up * 2.0f;
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(playerHeadPosition);

        if (screenPosition.z > 0) // Player가 카메라 앞에 있을 때만
        {
            arrowImage.position = screenPosition;
            distanceText.transform.position = screenPosition + new Vector3(0, -30, 0); // 화살표 아래 텍스트 배치
        }
        else
        {
            arrowImage.gameObject.SetActive(false); // 카메라 뒤에 있으면 숨김
            distanceText.gameObject.SetActive(false);
            return;
        }

        // 4. Portal 방향 계산 및 회전
        Vector3 directionToPortal = portalTransform.position - playerTransform.position;

        // 월드 좌표계 방향 -> 화면 좌표계 방향 변환
        Vector2 screenDirection = new Vector2(directionToPortal.x, directionToPortal.z).normalized;

        // 회전 각도 계산
        float angle = Mathf.Atan2(screenDirection.y, screenDirection.x) * Mathf.Rad2Deg;

        // 화살표 이미지 회전 (Z축 기준)
        arrowImage.rotation = Quaternion.Euler(0, 0, angle - 0); // 기본 방향 보정

        // 5. 거리 텍스트 업데이트
        distanceText.text = $"{distanceToPortal:F1}m"; // 거리 소수점 한 자리까지 표시
    }

    // Player Transform 설정
    public void SetPlayerTransform(Transform player)
    {
        playerTransform = player;
        Debug.Log("ArrowController: Player Transform이 설정되었습니다.");
    }

    // Portal Transform 설정
    public void SetPortalTransform(Transform portal)
    {
        portalTransform = portal;
        Debug.Log("ArrowController: Portal Transform이 설정되었습니다.");
    }
}
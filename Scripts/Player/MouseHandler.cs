using UnityEngine;

public class MouseHandler : MonoBehaviour
{
    public Transform cursor;
    public Vector2 mousePos;
    private Camera mainCamera;

    public Transform chPosition;
    public Transform crosshair;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mainCamera.nearClipPlane));

        cursor.position = worldPosition;

        crosshair.position = chPosition.position;
    }
}

using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    private Vector2 touchDelta;
    private bool isTouching;

    public void HandleLook()
    {
        if (Input.touchCount == 0) return; // Нет касаний — ничего не делаем

        Touch touch = Input.GetTouch(0);

        // Проверяем, чтобы касание было НЕ на джойстике
        if (touch.position.x < Screen.width * 0.3f) return;

        if (touch.phase == TouchPhase.Began)
        {
            isTouching = true;
        }
        else if (touch.phase == TouchPhase.Moved && isTouching)
        {
            touchDelta = touch.deltaPosition;
            transform.Rotate(0, touchDelta.x * 0.1f, 0);
            cameraTransform.Rotate(-touchDelta.y * 0.1f, 0, 0);
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            isTouching = false;
        }
    }
}

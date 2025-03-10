using UnityEngine;
using UnityEngine.LowLevel;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private Transform _cameraTransform;
    private CharacterController characterController;
    private PlayerLook playerLook;
    private PlayerInteractor playerInteractor;

    [Inject]
    private void Construct(PlayerLook look, PlayerInteractor interactor)
    {
        playerLook = look;
        playerInteractor = interactor;
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (characterController == null)
        {
            Debug.LogError("CharacterController is missing!");
            return;
        }

        if (_joystick == null)
        {
            Debug.LogError("Joystick is not assigned!");
            return;
        }

        if (playerLook == null)
        {
            Debug.LogError("playerLook is not assigned!");
            return;
        }

        if (playerInteractor == null)
        {
            Debug.LogError("playerInteractor is not assigned!");
            return;
        }

        Move();
        playerLook.HandleLook();
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            playerInteractor.TryPickUpObject();
        }
    }

    private void Move()
    {
        Vector3 moveDirection = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
        moveDirection = transform.TransformDirection(moveDirection);
        characterController.Move(moveDirection * _moveSpeed * Time.deltaTime);
    }
}
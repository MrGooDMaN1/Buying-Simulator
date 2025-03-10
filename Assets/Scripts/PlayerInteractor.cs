using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Transform holdPoint;
    [SerializeField] private GameObject dropButton;
    private GameObject heldObject;

    private void Start()
    {
        dropButton.SetActive(false);
    }

    public void TryPickUpObject()
    {
        if (heldObject != null) return;

        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 222222f, interactableLayer))
        {
            Debug.Log("Объект найден: " + hit.collider.gameObject.name); // Отладка

            heldObject = hit.collider.gameObject;
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.isKinematic = true; // Отключаем физику, чтобы предмет не падал
            }
            else
            {
                Debug.LogError("На объекте нет Rigidbody!");
            }

            heldObject.transform.SetParent(holdPoint);
            heldObject.transform.localPosition = Vector3.zero;
            dropButton.SetActive(true);
        }
        else
        {
            Debug.Log("Ничего не найдено!"); // Отладка
        }
    }

    public void DropObject()
    {
        if (heldObject == null) return;

        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        heldObject.transform.SetParent(null);
        rb.isKinematic = false;
        rb.AddForce(cameraTransform.forward * 5f, ForceMode.Impulse);
        heldObject = null;
        dropButton.SetActive(false);
    }
}


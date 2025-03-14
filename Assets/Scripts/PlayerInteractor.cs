using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Transform holdPoint;
    [SerializeField] private Button dropButton;
    private GameObject heldObject;

    private void Start()
    {
       dropButton.onClick.AddListener(() => DropObject());
       //dropButton.SetActive(false);
    }

    public void TryPickUpObject()
    {
        if (heldObject != null) return;

        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 2f, interactableLayer))
        {
            Debug.Log("������ ������: " + hit.collider.gameObject.name); // �������

            heldObject = hit.collider.gameObject;
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.isKinematic = true; // ��������� ������, ����� ������� �� �����
            }
            else
            {
                Debug.LogError("�� ������� ��� Rigidbody!");
            }

            heldObject.transform.SetParent(holdPoint);
            heldObject.transform.localPosition = Vector3.zero;
        }
        else
        {
            Debug.Log("������ �� �������!"); // �������
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
    }
}


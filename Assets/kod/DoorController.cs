using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float openAngle = 90f; // Kapının açılacağı açı
    public float openSpeed = 2f; // Kapının açılma hızı

    private Quaternion initialRotation; // Kapının başlangıç rotasyonu
    private Quaternion targetRotation; // Kapının hedef rotasyonu
    private bool isOpen = false; // Kapının açık olup olmadığını belirten flag

    private void Start()
    {
        initialRotation = transform.rotation;
        targetRotation = initialRotation * Quaternion.Euler(0f, openAngle, 0f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isOpen)
                CloseDoor();
            else
                OpenDoor();
        }
    }

    public void OpenDoor()
    {
        StopAllCoroutines();
        StartCoroutine(OpenDoorCoroutine());
    }

    public void CloseDoor()
    {
        StopAllCoroutines();
        StartCoroutine(CloseDoorCoroutine());
    }

    private IEnumerator OpenDoorCoroutine()
    {
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, openSpeed * Time.deltaTime);
            yield return null;
        }

        isOpen = true;
    }

    private IEnumerator CloseDoorCoroutine()
    {
        while (Quaternion.Angle(transform.rotation, initialRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, openSpeed * Time.deltaTime);
            yield return null;
        }

        isOpen = false;
    }
}


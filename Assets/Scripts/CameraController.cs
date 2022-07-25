using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float panSpeed = 30;
    [SerializeField] float panThreshold = 10;

    [SerializeField] float scrollSpeed = 5;
    [SerializeField] float minY = 10;
    [SerializeField] float maxY = 50;

    void Update()
    {
        var direction = Vector3.zero;

        if (Input.GetKey("w") || Math.Abs(Screen.height - Input.mousePosition.y) <= panThreshold)
            direction = Vector3.forward;
        else if (Input.GetKey("s") || Math.Abs(Input.mousePosition.y) <= panThreshold)
            direction = Vector3.back;
        else if (Input.GetKey("a") || Math.Abs(Input.mousePosition.x) <= panThreshold)
            direction = Vector3.left;
        else if (Input.GetKey("d") ||
                 Math.Abs(Screen.width - Input.mousePosition.x) <= panThreshold)
            direction = Vector3.right;

        if (direction != Vector3.zero)
        {
            var angle = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            var distance = panSpeed * Time.deltaTime;
            transform.Translate(angle * direction * distance, Space.World);
        }

        var scroll = Input.GetAxis("Mouse ScrollWheel");

        if ((scroll > 0 && transform.position.y > minY) ||
            (scroll < 0 && transform.position.y < maxY))
            transform.Translate(
                scroll * 1000 * scrollSpeed * Time.deltaTime * Vector3.forward
            );
    }
}
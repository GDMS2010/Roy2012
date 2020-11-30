using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 20f;
    public float rotSpeed = 50f;
    public float zoomSpeed = 50f;

    // Update is called once per frame
    void Update()
    {
        // For Camera moving Forward and backwards
        if(Input.GetButton("Forward") && PauseMenuManager.gamePause == false)
        {
            int dir = ((Input.GetAxisRaw("Forward") > 0) ? 1 : -1);

            Vector3 direction = transform.forward;
            direction.y = 0;
            direction.Normalize();

            transform.Translate(direction * Time.deltaTime * panSpeed * dir, Space.World);
        }
        else if(Input.mousePosition.y >= Screen.height * 0.95 && PauseMenuManager.gamePause == false)
        {
            int dir = 1;

            Vector3 direction = transform.forward;
            direction.y = 0;
            direction.Normalize();

            transform.Translate(direction * Time.deltaTime * panSpeed * dir, Space.World);
        }
        else if(Input.mousePosition.y <= Screen.height * 0.05 && PauseMenuManager.gamePause == false)
        {
            int dir = -1;

            Vector3 direction = transform.forward;
            direction.y = 0;
            direction.Normalize();

            transform.Translate(direction * Time.deltaTime * panSpeed * dir, Space.World);
        }

        // For Camera Moving left and right
        if (Input.GetButton("Horizontal") && PauseMenuManager.gamePause == false)
        {
            int dir = ((Input.GetAxisRaw("Horizontal") > 0) ? 1 : -1);

            Vector3 direction = transform.right;
            direction.y = 0;
            direction.Normalize();

            transform.Translate(direction * Time.deltaTime * panSpeed * dir, Space.World);
        }
        else if (Input.mousePosition.x >= Screen.width * 0.95 && PauseMenuManager.gamePause == false)
        {
            int dir = 1;

            Vector3 direction = transform.right;
            direction.y = 0;
            direction.Normalize();

            transform.Translate(direction * Time.deltaTime * panSpeed * dir, Space.World);
        }
        else if (Input.mousePosition.x <= Screen.width * 0.05 && PauseMenuManager.gamePause == false)
        {
            int dir = -1;

            Vector3 direction = transform.right;
            direction.y = 0;
            direction.Normalize();

            transform.Translate(direction * Time.deltaTime * panSpeed * dir, Space.World);
        }

        // For Camera Rotation
        if (Input.GetButton("Rotate") && PauseMenuManager.gamePause == false)
        {
            int dir = ((Input.GetAxisRaw("Rotate") > 0) ? 1 : -1);
            transform.RotateAround(transform.position, Vector3.up, (dir * rotSpeed * Time.deltaTime));
        }

        // For Camera Zoom
        if(Input.GetAxis("Mouse ScrollWheel") != 0 && PauseMenuManager.gamePause == false)
        {
            int dir = ((Input.GetAxisRaw("Mouse ScrollWheel") > 0) ? 1 : -1);
            transform.Translate((Vector3.forward * dir) * Time.deltaTime * zoomSpeed * 15f);
        }
    }
}

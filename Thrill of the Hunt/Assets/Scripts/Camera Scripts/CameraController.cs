using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 20f;
    public float rotSpeed = 50f;
    public float zoomSpeed = 50f;
    BoxCollider BoxCollider;
    public GameObject boundry;

    private void Start()
    {
        BoxCollider = boundry.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenuManager.gamePause == false)
        {
            // For Camera moving Forward and backwards
            if (Input.GetButton("Forward"))
            {
                int dir = ((Input.GetAxisRaw("Forward") > 0) ? 1 : -1);

                Vector3 direction = transform.forward;
                direction.y = 0;
                direction.Normalize();

                transform.Translate(direction * Time.deltaTime * panSpeed * dir, Space.World);
            }
            else if (Input.mousePosition.y >= Screen.height * 0.95)
            {
                int dir = 1;

                if (transform.position.z < BoxCollider.bounds.max.z)
                {
                    Vector3 direction = transform.forward;
                    direction.y = 0;
                    direction.Normalize();

                    transform.Translate(direction * Time.deltaTime * panSpeed * dir, Space.World);
                }
            }
            else if (Input.mousePosition.y <= Screen.height * 0.05)
            {
                int dir = -1;

                if (transform.position.z > BoxCollider.bounds.min.z)
                {
                    Vector3 direction = transform.forward;
                    direction.y = 0;
                    direction.Normalize();

                    transform.Translate(direction * Time.deltaTime * panSpeed * dir, Space.World);
                }
            }

            // For Camera Moving left and right
            if (Input.GetButton("Horizontal"))
            {
                int dir = ((Input.GetAxisRaw("Horizontal") > 0) ? 1 : -1);

                Vector3 direction = transform.right;
                direction.y = 0;
                direction.Normalize();

                transform.Translate(direction * Time.deltaTime * panSpeed * dir, Space.World);
            }
            else if (Input.mousePosition.x >= Screen.width * 0.95)
            {
                int dir = 1;

                if (transform.position.x < BoxCollider.bounds.max.x)
                {
                    Vector3 direction = transform.right;
                    direction.y = 0;
                    direction.Normalize();

                    transform.Translate(direction * Time.deltaTime * panSpeed * dir, Space.World);
                }
            }
            else if (Input.mousePosition.x <= Screen.width * 0.05)
            {
                int dir = -1;
                if (transform.position.x > BoxCollider.bounds.min.x)
                {
                    Vector3 direction = transform.right;
                    direction.y = 0;
                    direction.Normalize();

                    transform.Translate(direction * Time.deltaTime * panSpeed * dir, Space.World);
                }
            }

            // For Camera Rotation
            if (Input.GetButton("Rotate"))
            {
                int dir = ((Input.GetAxisRaw("Rotate") > 0) ? 1 : -1);
                transform.RotateAround(transform.position, Vector3.up, (dir * rotSpeed * Time.deltaTime));
            }

            // For Camera Zoom
            if (Input.GetAxis("Mouse ScrollWheel") != 0 && transform.position.y > 1 && transform.position.y < 10)
            {
                int dir = ((Input.GetAxisRaw("Mouse ScrollWheel") > 0) ? 1 : -1);
                transform.Translate((Vector3.forward * dir) * Time.deltaTime * zoomSpeed * 15f);

                if (transform.position.y < 1)
                    transform.Translate(0.0f, 1.0f, 0.0f);
                else if (transform.position.y > 10)
                    transform.Translate(0.0f, -1.0f, 0.0f);
            }
        }
    }
}

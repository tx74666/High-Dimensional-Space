using UnityEngine;
using Unity.Netcode;

public class Player : NetworkBehaviour
{
    public float speed = 5f;
    public bool floor;
    private Vector2 turn;
    public Camera playerCamera;
    [SerializeField] private GameObject bulletPrefab;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerCamera.enabled = IsOwner;
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.normal.y > 0.9)
            {
                floor = true;
                break;
            }
        }
    }

    private void Update()
    {
        if (!IsOwner) return;

        HandleMovement();
        HandleRotation();
        HandleActions();
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = speed * Time.deltaTime * new Vector3(horizontalInput, 0.0f, verticalInput);
        transform.Translate(movement);
    }

    private void HandleRotation()
    {
        turn.x += Input.GetAxis("Mouse X");
        turn.y += Input.GetAxis("Mouse Y");
        turn.y = Mathf.Clamp(turn.y, -60f, 60f);
        transform.localRotation = Quaternion.Euler(0, turn.x, 0);
        playerCamera.transform.localEulerAngles = new Vector3(-turn.y, 0, 0);
    }

    private void HandleActions()
    {
        if (Input.GetKeyDown(KeyCode.Space) && floor)
        {
            floor = false;
            GetComponent<Rigidbody>().AddForce(Vector3.up * 10, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetMouseButtonDown(0))
        {
            FireServerRpc(transform.position, Quaternion.LookRotation(transform.forward, Vector3.up));
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            Vector3 playerPosition = transform.position;
            Debug.Log("Player Position: " + playerPosition);
        }
    }

    [ServerRpc]
    private void FireServerRpc(Vector3 position, Quaternion rotation)
    {
        GameObject bullet = Instantiate(bulletPrefab, position, rotation);
        NetworkObject bulletNetObj = bullet.GetComponent<NetworkObject>();
        bulletNetObj.Spawn();
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetOwner(gameObject); // 将当前玩家实例作为子弹的发射者
        }
    }
}

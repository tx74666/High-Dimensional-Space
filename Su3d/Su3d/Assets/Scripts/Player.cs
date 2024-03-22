using UnityEngine;
using Unity.Netcode;
using System;

public class Player : NetworkBehaviour
{
    public float speed =5f;
    public bool floor;
    private Vector2 turn;
    public Camera playerCamera;
    [SerializeField] private GameObject bulletPrefab;
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
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (IsOwner)
        {
            playerCamera.enabled = true;
        }
        else
        {
            playerCamera.enabled = false;
        }
    }
    [ServerRpc]
    void FireServerRpc(Vector3 position, Quaternion rotation)
    {
        GameObject bullet = Instantiate(bulletPrefab, position, rotation);
        NetworkObject bulletNetObj = bullet.GetComponent<NetworkObject>();
        bulletNetObj.Spawn();
    }
    void Update()
    {
        if(!IsOwner) return;
        HandleMovement();
        HandleRotation();

        if ((Input.GetKeyDown(KeyCode.Space)) && (floor == true))
        {
            floor = false;
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
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
}

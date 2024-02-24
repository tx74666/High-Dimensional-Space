using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public static float rowSpeed = 5.0f;
    public float newSpeed = 1.5f * rowSpeed;
    public bool floor;
    public Vector2 turn;
    public Camera playerCamera;

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
        speed = rowSpeed;
    }
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = speed * Time.deltaTime * new Vector3(horizontalInput, 0.0f, verticalInput);
        transform.Translate(movement);
        turn.x += Input.GetAxis("Mouse X");
        turn.y += Input.GetAxis("Mouse Y");
        turn.y = Mathf.Clamp(turn.y,-60f,60f);
        transform.localRotation=Quaternion.Euler(0,turn.x,0);
        playerCamera.transform.localEulerAngles = new Vector3(-turn.y,0,0);
        if ((Input.GetKeyDown(KeyCode.Space))&&(floor == true))
        {
            floor = false;
            GetComponent<Rigidbody>().AddForce(new Vector3(0,10,0),ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState= CursorLockMode.None;
        }
        if (Input.GetMouseButtonDown(0))
        {
            var bullet = Resources.Load<GameObject>("bullet");
            GameObject.Instantiate(bullet,transform.position, Quaternion.LookRotation(transform.forward,Vector3.up));
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            speed = newSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            speed = rowSpeed;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            Vector3 playerPosition = transform.position;
            Debug.Log("Player Position: " + playerPosition);
        }
    }
}
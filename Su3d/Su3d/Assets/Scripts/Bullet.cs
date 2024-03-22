using UnityEngine;
using Unity.Netcode;

public class Bullet : NetworkBehaviour
{
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private float speed = 40f;

    private void Start()
    {
        if (IsServer)
        {
            Invoke(nameof(DestroySelf), lifetime);
        }
    }

    private void Update()
    {
        if (IsServer)
        {
            transform.Translate(speed * Time.deltaTime * Vector3.forward);
        }
    }

    private void DestroySelf()
    {
        // Ensure that we only call Despawn on the server
        if (IsServer)
        {
            GetComponent<NetworkObject>().Despawn();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        DestroySelf();
    }
}

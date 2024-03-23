using UnityEngine;
using Unity.Netcode;

public class Bullet : NetworkBehaviour
{
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private float speed = 40f;
    private GameObject owner;

    public void SetOwner(GameObject newOwner)
    {
        owner=newOwner;
    }

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
        if (IsServer)
        {
            NetworkObject networkObject = GetComponent<NetworkObject>();
            networkObject.Despawn(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != owner)
        {
            DestroySelf();
        }
    }
}

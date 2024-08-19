using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    private Rigidbody2D _rb2D;
    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _input;

    public void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (!isLocalPlayer) return;

        _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("VErtical")).normalized;
    }

    public void FixedUpdate()
    {
        _rb2D.velocity = _input * _speed * Time.deltaTime;
    }
}
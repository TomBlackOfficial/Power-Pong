using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private GameManager manager;

    public bool isPlayer1;
    public float speed = 5;

    private Vector3 startPosition;
    private Rigidbody2D rb;
    private float movement;

    private bool paralized = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    private void Start()
    {
        manager = GameManager._instance;
    }

    private void Update()
    {
        if (isPlayer1)
        {
            movement = Input.GetAxisRaw("Vertical_P1");
        }
        else
        {
            movement = Input.GetAxisRaw("Vertical_P2");
        }
        rb.velocity = new Vector2(0, movement * speed);
    }

    public void Reset()
    {
        paralized = true;
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
    }

    public void StartRound()
    {
        paralized = false;
    }
}

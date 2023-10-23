using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private GameManager manager;

    public bool isPlayer1;

    [SerializeField] private GameObject sprite;

    private float speed = 5;
    private int height = 20;

    private Vector2 minMaxSpeed = new Vector2(1, 20);
    private Vector2 minMaxHeight = new Vector2(2, 40);

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
        SetSpeed(manager.gamemode.startingPaddleSpeed);
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

    public void SetSpeed(float newSpeed)
    {
        speed = Mathf.Clamp(newSpeed, minMaxSpeed.x, minMaxSpeed.y);
    }

    public void SetPaddleHeight(int newHeight)
    {
        height = (int)Mathf.Clamp(newHeight, minMaxHeight.x, minMaxHeight.y);
        sprite.transform.localScale = new Vector3(1, height, 1);
    }
}

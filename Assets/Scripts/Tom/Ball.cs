using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private GameManager manager;

    public float speed { get; private set; }
    private Rigidbody2D rb;
    private Vector3 startPosition;
    private Vector2 minMaxSpeed = new Vector2(1, 10);

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    private void Start()
    {
        manager = GameManager.instance;
        StartCoroutine(LaunchDelay());
    }

    public void SetBallSpeed(float newSpeed)
    {
        speed = Mathf.Clamp(newSpeed, minMaxSpeed.x, minMaxSpeed.y);
    }

    public void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        rb.velocity = new Vector2(speed * x, speed * y);
    }

    IEnumerator LaunchDelay()
    {
        yield return new WaitForSeconds(0.5f);

        Launch();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            SetBallSpeed(speed + 0.5f);
        }
    }
}

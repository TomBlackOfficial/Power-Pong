using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private GameManager manager;

    private float speed;
    private Rigidbody2D rb;
    private Vector3 startPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    private void Start()
    {
        manager = GameManager._instance;
        StartCoroutine(LaunchDelay());
    }

    public void SetBallSpeed(float newSpeed)
    {
        speed = newSpeed;
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
}

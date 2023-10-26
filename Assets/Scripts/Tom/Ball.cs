using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private GameManager manager;

    public float speed { get; private set; }
    public float size { get; private set; }
    private Rigidbody2D rb;
    private Vector3 startPosition;
    private Vector2 minMaxSpeed = new Vector2(1, 10);
    private Vector2 minMaxSize = new Vector2(0.5f, 5);
    private List<BallModifier> normalModifiers = new List<BallModifier>();
    private Dictionary<BallModifier, bool> activateModifiers = new Dictionary<BallModifier, bool>();

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
        rb.velocity = rb.velocity.normalized * speed;
    }

    public void SetBallSize(float newSize)
    {
        size = Mathf.Clamp(newSize, minMaxSize.x, minMaxSize.y);
    }

    public void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        rb.velocity = new Vector2(speed * x, speed * y);
        gameObject.transform.localScale = new Vector3(size, size, 1);
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
            SetBallSpeed(speed + collision.gameObject.GetComponent<Paddle>().knockback);
        }
    }

    public void AddModifier(GameObject modifier, Paddle paddle)
    {
        GameObject thisObject = Instantiate(modifier, this.transform);
        thisObject.transform.parent = this.gameObject.transform;
        BallModifier mod;
        if (thisObject.TryGetComponent<BallModifier>(out mod))
        {
            if (mod.activateable)
            {
                activateModifiers.Add(mod, paddle.isPlayer1);
            }
            else
            {
                normalModifiers.Add(mod);
            }
            mod.StartModifierEffect();
        }
        else
        {
            Destroy(thisObject);
            Debug.LogError("Non player modifier was trying to be added as a modifier.");
        }
    }
}

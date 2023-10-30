using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private GameManager manager;

    public float speed { get; private set; }
    public float size { get; private set; } = 1;

    private Rigidbody2D rb;
    private Vector3 startPosition;
    [SerializeField] private Vector2 minMaxSpeed = new Vector2(1, 10);
    [SerializeField] private Vector2 minMaxSize = new Vector2(0.5f, 5);
    private List<BallModifier> allModifiers = new List<BallModifier>();
    private List<BallModifier> normalModifiers = new List<BallModifier>();
    private Dictionary<BallModifier, bool> activateModifiers = new Dictionary<BallModifier, bool>();

    private ParticleSystem myPS;
    [SerializeField] private Vector2Int particlesToEmitOnBurst = new Vector2Int(50, 75);
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myPS = GetComponentInChildren<ParticleSystem>();
        startPosition = transform.position;
    }

    private void Start()
    {
        manager = GameManager.instance;
        StartCoroutine(LaunchDelay());
        myPS.Pause();
        myPS.Clear();
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

    public Vector2 GetBallMinMixSpeed()
    {
        return minMaxSpeed;
    }

    public void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        rb.velocity = new Vector2(speed * x, speed * y);
        gameObject.transform.localScale = new Vector3(size, size, 1);
        //AudioManager.instance.PlayLaunchSound();
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
            AudioManager.instance.PlayHitPaddleSound();
            for (int m = 0; m < allModifiers.Count; m++)
            {
                allModifiers[m].BallHitPlayer(collision.gameObject.GetComponent<Paddle>());
            }
            collision.gameObject.GetComponent<Paddle>().TriggerParticleEffect(gameObject.transform.position);
        }
        else
        {
            AudioManager.instance.PlayHitSidesSound();
        }
        TriggerParticleEffect();
    }

    public void AddModifier(GameObject modifier, Paddle paddle = null)
    {
        GameObject thisObject = Instantiate(modifier, this.transform);
        thisObject.transform.parent = this.gameObject.transform;
        BallModifier mod;
        if (thisObject.TryGetComponent<BallModifier>(out mod))
        {
            if (mod.needsPlayerAssignment)
            {
                if (paddle == null)
                {
                    Debug.LogError("Trying to add a modifier with an activatable ability without assigning a player.");
                    Destroy(thisObject);
                    return;
                }
                mod.AssignPlayer(paddle);
                if (mod.activateable)
                {
                    activateModifiers.Add(mod, paddle.isPlayer1);
                }
                else
                {
                    normalModifiers.Add(mod);
                }
            }
            else
            {
                normalModifiers.Add(mod);
            }
            allModifiers.Add(mod);
            mod.InitializeValues();
            mod.StartModifierEffect();
        }
        else
        {
            Destroy(thisObject);
            Debug.LogError("Non Ball modifier was trying to be added as a modifier.");
        }
    }

    public void TriggerParticleEffect()
    {
        myPS.Emit(Random.Range(particlesToEmitOnBurst.x, particlesToEmitOnBurst.y + 1));
        myPS.Play();
    }
}

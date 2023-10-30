using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private GameManager manager;

    public bool isPlayer1;

    [SerializeField] private GameObject sprite;

    public float speed { get; private set; } = 5;
    public float height { get; private set; } = 1;
    public float knockback { get; private set; } = 0.5f;
    [SerializeField] private Vector2 minMaxSpeed = new Vector2(1, 20);
    [SerializeField] private Vector2 minMaxHeight = new Vector2(0.25f, 2.5f);
    [SerializeField] private Vector2 minMaxKnockback = new Vector2(0.25f, 5f);
    private bool flipControls = false;
    private bool lastActivate = false;
    private bool slidingControls = false;
    private float slideAmount = 0.1f;

    private Vector3 startPosition;
    private Rigidbody2D rb;
    private float movement;

    private bool paralized = true;

    private List<PlayerModifier> normalModifiers = new List<PlayerModifier>();
    private List<ModifierParent> activateModifiers = new List<ModifierParent>();

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
        SetSpeed(manager.gamemode.startingPaddleSpeed);
        myPS.Pause();
        myPS.Clear();
    }

    private void Update()
    {
        bool activate = false;
        if (isPlayer1)
        {
            movement = Input.GetAxisRaw("Vertical_P1");
            if (Input.GetAxisRaw("Action_P1") != 0)
            {
                activate = true;
            }
        }
        else
        {
            movement = Input.GetAxisRaw("Vertical_P2");
            if (Input.GetAxisRaw("Action_P2") != 0)
            {
                activate = true;
            }
        }
        if (flipControls)
        {
            movement = -movement;
        }
        if (slidingControls)
        {
            if (movement != 0)
            {
                rb.velocity = new Vector2(0, movement * speed);
            }
            else
            {
                rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(0, 0), slideAmount);
            }
        }
        else
        {
            rb.velocity = new Vector2(0, movement * speed);
        }

        if (activate)
        {
            if (activateModifiers.Count > 0)
            {
                for (int a = 0; a < activateModifiers.Count; a++)
                {
                    activateModifiers[a].ActivateAbility();
                }
            }
        }
        else if (activate != lastActivate)
        {
            for (int a = 0; a < activateModifiers.Count; a++)
            {
                activateModifiers[a].DeactivateAbility();
            }
        }
        lastActivate = activate;
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
    public void FlipControls()
    {
        flipControls = !flipControls;
    }

    public void SlidingControls(float slipAmount)
    {
        slideAmount = slipAmount;
        slidingControls = true;
    }
    public void SetPaddleHeight(float newHeight)
    {
        height = Mathf.Clamp(newHeight, minMaxHeight.x, minMaxHeight.y);
        transform.localScale = new Vector3(1, height, 1);
    }

    public void SetKnockBack(float newKnockback)
    {
        knockback = Mathf.Clamp(newKnockback, minMaxKnockback.x, minMaxKnockback.y);
    }
    public void AddModifier(GameObject modifier)
    {
        GameObject thisObject = Instantiate(modifier, this.transform);
        thisObject.transform.parent = this.gameObject.transform;
        PlayerModifier mod;
        if (thisObject.TryGetComponent<PlayerModifier>(out mod))
        {
            if (mod.activateable)
            {
                activateModifiers.Add(mod);
            }
            else
            {
                normalModifiers.Add(mod);
            }
            mod.InitializeValues();
            mod.StartModifierEffect();
        }
        else
        {
            Destroy(thisObject);
            Debug.LogError("Non player modifier was trying to be added as a modifier.");
        }
    }

    public void AddBallActive(BallModifier mod)
    {
        activateModifiers.Add(mod);
    }

    public void TriggerParticleEffect(Vector3 position)
    {
        myPS.transform.position = new Vector3(gameObject.transform.position.x, position.y, gameObject.transform.position.z);
        myPS.Emit(Random.Range(particlesToEmitOnBurst.x, particlesToEmitOnBurst.y + 1));
        myPS.Play();
    }
}

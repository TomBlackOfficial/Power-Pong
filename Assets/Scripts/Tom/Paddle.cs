using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private GameManager manager;

    public bool isPlayer1;

    [SerializeField] private GameObject sprite;

    public float speed { get; private set; } = 5;
    public int height { get; private set; } = 20;

    private Vector2 minMaxSpeed = new Vector2(1, 20);
    private Vector2 minMaxHeight = new Vector2(2, 40);

    private Vector3 startPosition;
    private Rigidbody2D rb;
    private float movement;

    private bool paralized = true;

    private List<PlayerModifier> normalModifiers = new List<PlayerModifier>();
    private List<PlayerModifier> activateModifiers = new List<PlayerModifier>();

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    private void Start()
    {
        manager = GameManager.instance;
        SetSpeed(manager.gamemode.startingPaddleSpeed);
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
        rb.velocity = new Vector2(0, movement * speed);

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
            mod.StartModifierEffect();
        }
        else
        {
            Destroy(thisObject);
            Debug.LogError("Non player modifier was trying to be added as a modifier.");
        }
    }
}

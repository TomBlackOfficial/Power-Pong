using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour
{
    public enum ButtonStates
    {
        normal,
        highlighted,
        pressed,
        selected,
        disabled
    }

    [HideInInspector] public ButtonStates state;

    public bool interactable = true;
    public bool affectText = true;
    public bool affectOutline = true;

    public Color normalColor = Color.white;
    public Color highlightedColor = new Color(220, 220, 220, 1f);
    public Color pressedColor = new Color(170, 170, 170, 1f);
    public Color selectedColor = Color.white;
    public Color disabledColor = new Color(170, 170, 170, 0.5f);

    [Space(15)]

    public UnityEvent onClickDown;
    public UnityEvent onClickUp;

    private bool highlighted;
    private bool pressed;
    private bool selected;

    private SpriteRenderer spriteRenderer;
    private TextMesh textMesh;
    private Animator anim;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        textMesh = GetComponentInChildren<TextMesh>();
        TryGetComponent(out anim);

        if (!interactable)
            SetState(ButtonStates.disabled);
    }

    private void Update()
    {
        if (!interactable)
            return;
        if (GameManager.instance != null)
        {
            if (GameManager.instance.loser.isPlayer1)
            {
                if (Input.GetButtonDown("Action_P1") && highlighted)
                {
                    onClickDown.Invoke();
                    pressed = true;
                }
                else if (Input.GetButtonUp("Action_P1") && highlighted)
                {
                    onClickUp.Invoke();
                    pressed = false;
                    selected = true;

                    if (AudioManager.instance)
                    {
                        AudioManager.instance.PlayLaunchSound();
                    }
                }
            }
            else
            {
                if (Input.GetButtonDown("Action_P2") && highlighted)
                {
                    onClickDown.Invoke();
                    pressed = true;
                }
                else if (Input.GetButtonUp("Action_P2") && highlighted)
                {
                    onClickUp.Invoke();
                    pressed = false;
                    selected = true;

                    if (AudioManager.instance)
                    {
                        AudioManager.instance.PlayLaunchSound();
                    }
                }
            }
        }
        else
        {
            if (Input.GetButtonDown("Action") && highlighted)
            {
                onClickDown.Invoke();
                pressed = true;                
            }
            else if (Input.GetButtonUp("Action") && highlighted)
            {
                onClickUp.Invoke();
                pressed = false;
                selected = true;

                if (AudioManager.instance)
                {
                    AudioManager.instance.PlayLaunchSound();
                }
            }
        }
        CalculateCurrentState();
    }

    private void CalculateCurrentState()
    {
        if (pressed)
        {
            SetState(ButtonStates.pressed);
        }
        //else if (selected)
        //{
        //    SetState(ButtonStates.selected);
        //}
        else if (highlighted)
        {
            SetState(ButtonStates.highlighted);
        }
        else if (interactable)
        {
            SetState(ButtonStates.normal);
        }

        if (anim != null)
            UpdateAnimator();
    }

    private void SetState(ButtonStates newState)
    {
        if (state == newState)
            return;

        state = newState;

        switch (state)
        {
            case ButtonStates.normal:
                ChangeColor(normalColor);
                break;
            case ButtonStates.highlighted:
                ChangeColor(highlightedColor);
                break;
            case ButtonStates.pressed:
                ChangeColor(pressedColor);
                break;
            case ButtonStates.selected:
                ChangeColor(selectedColor);
                break;
            case ButtonStates.disabled:
                ChangeColor(disabledColor);
                break;
        }
    }

    private void UpdateAnimator()
    {
        anim.SetBool("Highlighted", IsState(ButtonStates.highlighted));
        anim.SetBool("Pressed", IsState(ButtonStates.pressed));
        anim.SetBool("Selected", IsState(ButtonStates.selected));
    }

    private bool IsState(ButtonStates currentState)
    {
        if (currentState == state)
            return true;
        else
            return false;
    }

    private void ChangeColor(Color newColor)
    {
        if (affectOutline)
            spriteRenderer.color = newColor;

        if (affectText)
            textMesh.color = newColor;
    }

    public void SetHighlighted(bool newValue)
    {
        highlighted = newValue;

        if (!highlighted && selected)
            selected = false;
    }
}

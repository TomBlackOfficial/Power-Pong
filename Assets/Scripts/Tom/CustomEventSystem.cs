using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomEventSystem : MonoBehaviour
{
    public static CustomEventSystem instance;

    public enum SortingModes
    {
        Horizontal,
        Vertical
    }

    public bool isActive = true;
    public bool loopSelection;

    public SortingModes sorting = SortingModes.Vertical;

    public CustomButton[] buttons;

    private CustomButton selectedButton;

    private bool started;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            Destroy(this);
        }
        else
        {
            if (buttons.Length <= 0)
            {
                buttons = GetComponentsInChildren<CustomButton>();
            }
        }
    }

    private void Start()
    {
        if (!isActive)
            return;

        StartEventSystem();
    }

    private void Update()
    {
        if (!isActive)
            return;

        if (!started)
        {
            StartEventSystem();
        }

        if (sorting == SortingModes.Horizontal)
            UpdateHorizontal();
        else if (sorting == SortingModes.Vertical)
            UpdateVertical();
    }

    public void StartEventSystem()
    {
        SetSelectedButton(buttons[0]);

        if (!selectedButton.interactable)
            MoveNext();

        started = true;
    }

    private void UpdateHorizontal()
    {
        if (GameManager.instance.loser.isPlayer1)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (AudioManager.instance)
                    AudioManager.instance.PlayMenuSound();
                MoveBack();
                return;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (AudioManager.instance)
                    AudioManager.instance.PlayMenuSound();
                MoveNext();
                return;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (AudioManager.instance)
                    AudioManager.instance.PlayMenuSound();
                MoveBack();
                return;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (AudioManager.instance)
                    AudioManager.instance.PlayMenuSound();
                MoveNext();
                return;
            }
        }
    }

    private void UpdateVertical()
    {
        if (GameManager.instance.loser.isPlayer1)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (AudioManager.instance)
                    AudioManager.instance.PlayMenuSound();
                MoveBack();
                return;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (AudioManager.instance)
                    AudioManager.instance.PlayMenuSound();
                MoveNext();
                return;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (AudioManager.instance)
                    AudioManager.instance.PlayMenuSound();
                MoveBack();
                return;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (AudioManager.instance)
                    AudioManager.instance.PlayMenuSound();
                MoveNext();
                return;
            }
        }
    }

    private void MoveNext()
    {
        if (AudioManager.instance)
        {
            AudioManager.instance.PlayMenuSound();
        }

        if (selectedButton != buttons[buttons.Length - 1])
        {
            SetSelectedButton(buttons[Array.IndexOf(buttons, selectedButton) + 1]);
        }
        else if (loopSelection)
        {
            SetSelectedButton(buttons[0]);
        }
        else
        {
            BadInput();
            return;
        }

        if (!selectedButton.interactable)
            MoveNext();
    }

    private void MoveBack()
    {
        if (AudioManager.instance)
        {
            AudioManager.instance.PlayMenuSound();
        }

        if (selectedButton != buttons[0])
        {
            SetSelectedButton(buttons[Array.IndexOf(buttons, selectedButton) - 1]);
        }
        else if (loopSelection)
        {
            SetSelectedButton(buttons[buttons.Length - 1]);
        }
        else
        {
            BadInput();
            return;
        }

        if (!selectedButton.interactable)
            MoveBack();
    }

    public void SetSelectedButton(CustomButton newButton)
    {
        if (selectedButton != null)
            selectedButton.SetHighlighted(false);

        selectedButton = newButton;

        if (selectedButton != null)
            selectedButton.SetHighlighted(true);
    }

    private void BadInput()
    {

    }
}

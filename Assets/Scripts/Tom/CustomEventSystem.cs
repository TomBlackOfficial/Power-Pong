using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomEventSystem : MonoBehaviour
{
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
        if (buttons.Length <= 0)
        {
            buttons = GetComponentsInChildren<CustomButton>();
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
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveBack();
            return;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveNext();
            return;
        }
    }

    private void UpdateVertical()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveNext();
            return;
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveBack();
            return;
        }
    }

    private void MoveNext()
    {
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

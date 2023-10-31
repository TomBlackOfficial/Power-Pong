using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHealthHelperScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] heartSprites;
    private int health = 0;
    [SerializeField] private bool player1 = true;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite halfHeart;

    private void Awake()
    {
        UpdateHealth(3);
    }

    public bool GetPlayer()
    {
        return player1;
    }

    public void UpdateHealth(int h)
    {
        if (h < 0)
        {
            h = 0;
        }
        if (h > 10)
        {
            h = 10;
        }
        health = h;
        UpdateSprites();
    }

    private void UpdateSprites()
    {
        int tempHP = health;
        for (int s = 0; s < heartSprites.Length; s++)
        {
            heartSprites[s].gameObject.SetActive(true);
            heartSprites[s].flipX = false;
            if (tempHP >= 2)
            {
                heartSprites[s].sprite = fullHeart;
            }
            else if (tempHP == 1)
            {
                heartSprites[s].sprite = halfHeart;
                if (!player1)
                {
                    heartSprites[s].flipX = true;
                }
            }
            else
            {
                heartSprites[s].gameObject.SetActive(false);
            }
            tempHP -= 2;
        }
    }
}

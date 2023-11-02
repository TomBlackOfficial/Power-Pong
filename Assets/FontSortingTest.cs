using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FontSortingTest : MonoBehaviour
{
    public MeshRenderer rend;

    private void Awake()
    {
        rend.sortingOrder = 100;
    }
}

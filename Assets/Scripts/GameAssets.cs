using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets i;
    private void Awake()
    {
        if (i != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        i = this;
    }

    public GameObject floating_text;
}

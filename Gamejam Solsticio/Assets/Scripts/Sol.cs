using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sol : MonoBehaviour
{
    [SerializeField] float velocidade;
    void Update()
    {
        transform.Translate(Vector2.left * velocidade * Time.deltaTime);
    }
}

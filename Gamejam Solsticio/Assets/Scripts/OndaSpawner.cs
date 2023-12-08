using System;
using Unity.Mathematics;
using UnityEngine;

public class OndaSpawner : MonoBehaviour
{
    public static float velocidade;

    public GameObject[] pontos;
    public GameObject onda;
    public GameObject offda; // n faz nada, é so foda
    public float fullTimer;
    float timer;

    private void Start()
    {
        velocidade = 7;
        SpawnPlataforma();
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            SpawnPlataforma();
            timer = fullTimer;
        }
    }

    private void SpawnPlataforma()
    {
        int pontoAleatorio = UnityEngine.Random.Range(0, pontos.Length);
        GameObject novaOnda = Instantiate(onda);

        novaOnda.transform.position = new Vector2(this.transform.position.x, pontos[pontoAleatorio].transform.position.y);
    }
}

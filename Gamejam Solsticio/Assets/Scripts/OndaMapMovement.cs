using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OndaMapMovement : MonoBehaviour
{
    public float velocidade = 5f;
    void Update()
    {
        transform.Translate(Vector2.left * velocidade * Time.deltaTime);

        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator death()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }
}

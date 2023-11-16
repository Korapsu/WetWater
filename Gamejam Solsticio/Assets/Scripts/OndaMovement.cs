using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OndaMovement : MonoBehaviour
{
    public int ondaAtual = 1;
    public Transform[] pontos;
    public float timer;

    [SerializeField] LayerMask ondaMask;
    bool isDead = false;

    void Start()
    {
        Vector2 startPosition = new Vector2(this.gameObject.transform.position.x, pontos[0].position.y);
        this.gameObject.transform.position = startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;

        if((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && ondaAtual < (pontos.Length -1))
        {
            ondaAtual += 1;
            Vector2 newPosition = new Vector2(this.gameObject.transform.position.x, pontos[ondaAtual].position.y);
            this.gameObject.transform.position = newPosition;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector3.up, 10, ondaMask);
            if (hit.collider != null)
            {
                newPostion(hit.transform, pontos[ondaAtual]);
            }
        }
        else if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && ondaAtual > 0)
        {
            ondaAtual -= 1; 
            Vector2 newPosition = new Vector2(this.gameObject.transform.position.x, pontos[ondaAtual].position.y);
            this.gameObject.transform.position = newPosition;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector3.up, 10, ondaMask);
            if (hit.collider != null){
                newPostion(hit.transform, pontos[ondaAtual]);
            }
        }

        timer += Time.deltaTime;
    }

    void newPostion(Transform onda, Transform pos)
    {
        onda.transform.position = new Vector3(onda.transform.position.x, pos.transform.position.y);
    }

    public void death()
    {
        GetComponent<Animator>().SetTrigger("dead");
        GetComponent<BoxCollider2D>().enabled = false;
        isDead = true;
    }
}

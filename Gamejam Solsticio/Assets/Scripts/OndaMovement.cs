using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OndaMovement : MonoBehaviour
{
    public int ondaAtual = 1;
    public Transform[] pontos;
    public float clocktimer, timer; [SerializeField] TextMeshProUGUI text;

    [SerializeField] Material material;

    [SerializeField] LayerMask ondaMask;
    bool isDead = false;


    void Start()
    {
        Vector2 startPosition = new Vector2(this.gameObject.transform.position.x, pontos[0].position.y);
        this.gameObject.transform.position = startPosition;
    }

    void Update()
    {
        if (isDead) return;

        movement();
        death();
        
        timer += Time.deltaTime;
        clocktimer += Time.deltaTime;

        if (clocktimer >= 1f)
        {
            AudioManager.Instance.sfxsrc.PlayOneShot(AudioManager.Instance.clock);
            clocktimer = 0;
        }
    }
    void movement()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && ondaAtual < (pontos.Length - 1))
        {
            AudioManager.Instance.sfxsrc.PlayOneShot(AudioManager.Instance.jump);
            ondaAtual += 1;
            Vector2 newPosition = new Vector2(this.gameObject.transform.position.x, pontos[ondaAtual].position.y);
            this.gameObject.transform.position = newPosition;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector3.up, 10, ondaMask);
            if (hit.collider != null) newPostion(hit.transform, pontos[ondaAtual]);
        }
        else if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && ondaAtual > 0)
        {
            AudioManager.Instance.sfxsrc.PlayOneShot(AudioManager.Instance.descend);
            ondaAtual -= 1;
            Vector2 newPosition = new Vector2(this.gameObject.transform.position.x, pontos[ondaAtual].position.y);
            this.gameObject.transform.position = newPosition;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector3.up, 10, ondaMask);
            if (hit.collider != null) newPostion(hit.transform, pontos[ondaAtual]);
        }

        void newPostion(Transform onda, Transform pos)
        {
            onda.transform.position = new Vector3(onda.transform.position.x, pos.transform.position.y);
        }
    }

    public void death()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector3.up, 10, ondaMask);
        if (timer < 5 || hit.collider == null || Mathf.Abs(hit.transform.position.y - transform.position.y)  < 2 ) return;

        GetComponentInChildren<Animator>().SetTrigger("dead");
        GetComponent<BoxCollider2D>().enabled = false;
        isDead = true;
        StartCoroutine(die());
        AudioManager.Instance.sfxsrc.PlayOneShot(AudioManager.Instance.death);

        IEnumerator die()
        {
            yield return new WaitForSeconds(1f); // mudando o delay pro som da morte ser tocado corretamente
            SceneManager.LoadScene("Death");
        }

    }

    private void LateUpdate()
    {
        text.text = $"{timer: 0}";

        if (material.mainTextureOffset.x > 1000) material.mainTextureOffset -= new Vector2(material.mainTextureOffset.x, 0);
        else material.mainTextureOffset += 0.2f * Time.deltaTime * Vector2.right;
    }
}

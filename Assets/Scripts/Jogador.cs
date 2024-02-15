using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Jogador : MonoBehaviour
{
    public Rigidbody2D rb;
    public float forcaPulo;
    public LayerMask layerChao;
    public float minAlturaChao = 1;
    private bool estaNoChao;
    private float pontos;
    public float multiplicadorPontos = 1;
    public Text pontosText;
    public Animator animatorComponent;

    // Update is called once per frame
    void Update()
    {
        pontos += Time.deltaTime * multiplicadorPontos;

        pontosText.text = $"Pontos: {Mathf.FloorToInt(pontos)}";

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Pular();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Agaixar();
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            Levantar();
        }
    }

    void Pular()
    {
        if (estaNoChao)
        {
            rb.AddForce(Vector2.up * forcaPulo);
        }
    }

    private void Agaixar()
    {
        animatorComponent.SetBool("Agaixado", true);
    }

    private void Levantar()
    {
        animatorComponent.SetBool("Agaixado", false);
    }

    private void FixedUpdate()
    {
        estaNoChao = Physics2D.Raycast(transform.position, Vector2.down, minAlturaChao, layerChao);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Inimigo"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}

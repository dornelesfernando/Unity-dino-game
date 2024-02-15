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
    public float pontos;
    private float highscore;
    public float multiplicadorPontos = 1;
    public Text pontosText;
    public Text highscoreText;
    public Animator animatorComponent;
    public AudioSource pularAudioSource;
    public AudioSource oneHundredPointsAudioSource;
    public AudioSource deathAudioSource;
    private void Start()
    {
        highscore = PlayerPrefs.GetFloat("HIGHSCORE");
        highscoreText.text = $"Hi {Mathf.FloorToInt(highscore)}";
    }
    // Update is called once per frame
    void Update()
    {
        pontos += Time.deltaTime * multiplicadorPontos;

        var pontosInt = Mathf.FloorToInt(pontos);
        pontosText.text = pontosInt.ToString();


        if (pontosInt > 0
            && pontosInt % 100 == 0
            && !oneHundredPointsAudioSource.isPlaying)
        {
            oneHundredPointsAudioSource.Play();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) Pular();

        if (Input.GetKeyDown(KeyCode.DownArrow)) Agaixar();
        else if (Input.GetKeyUp(KeyCode.DownArrow)) Levantar();
    }

    void Pular()
    {
        if (estaNoChao)
        {
            rb.AddForce(Vector2.up * forcaPulo);
            pularAudioSource.Play();
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
            if (highscore < pontos)
            {
                highscore = pontos;

                PlayerPrefs.SetFloat("HIGHSCORE", highscore);
            }
            deathAudioSource.Play();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;

public class GeradorInimigos : MonoBehaviour
{
    public GameObject[] cactoPrefabs;
    public GameObject dinossauroVoadorPrefab;
    public float dinossauroVoadorYMinimo = -1;
    public float dinossauroVoadorYMaximo = 1;
    public float pontuacaoDinossaroVoadorMinima = 300;
    public Jogador jogadorScript;
    public float delayInicial;
    public float delayEntreCactos;
    public float distanciaMinima = 10;
    public float distancaiMaxima = 20;
    // Start is called before the first frame update
    private void Start()
    {
        // InvokeRepeating("GerarInimigos", delayInicial, delayEntreCactos);
        StartCoroutine(GerarInimigos());
    }

    // Update is called once per frame
    private IEnumerator GerarInimigos()
    {
        yield return new WaitForSeconds(delayInicial);

        GameObject ultimoInimigoGerado = null;

        var distanciaNecessaria = 0f;

        while (true)
        {
            var geracaoObjetoLiberada = ultimoInimigoGerado == null
             || UnityEngine.Vector3.Distance(transform.position, ultimoInimigoGerado.transform.position) >= distanciaNecessaria;

            if (geracaoObjetoLiberada)
            {
                var random = UnityEngine.Random.Range(1, 7);

                if (jogadorScript.pontos >= pontuacaoDinossaroVoadorMinima && random <= 2)
                {
                    var posicaoYAleatoria = UnityEngine.Random.Range(dinossauroVoadorYMinimo, dinossauroVoadorYMaximo);

                    var posicao = new UnityEngine.Vector3(
                        transform.position.x,
                        transform.position.y + posicaoYAleatoria,
                        transform.position.z
                    );

                    ultimoInimigoGerado = Instantiate(dinossauroVoadorPrefab, posicao, quaternion.identity);
                }
                else
                {
                    var quantidadeCactos = cactoPrefabs.Length;
                    var indiceAleatorio = UnityEngine.Random.Range(0, quantidadeCactos);
                    var cactoPrefab = cactoPrefabs[indiceAleatorio];
                    ultimoInimigoGerado = Instantiate(cactoPrefab, transform.position, quaternion.identity);
                }

                distanciaNecessaria = UnityEngine.Random.Range(distanciaMinima, distancaiMaxima);
            }

            yield return null;

            // yield return new WaitForSeconds(delayEntreCactos);
        }
    }
}

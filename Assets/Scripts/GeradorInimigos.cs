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
    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating("GerarInimigos", delayInicial, delayEntreCactos);
    }

    // Update is called once per frame
    private void GerarInimigos()
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

            Instantiate(dinossauroVoadorPrefab, posicao, quaternion.identity);
        }
        else
        {
            var quantidadeCactos = cactoPrefabs.Length;
            var indiceAleatorio = UnityEngine.Random.Range(0, quantidadeCactos);
            var cactoPrefab = cactoPrefabs[indiceAleatorio];
            Instantiate(cactoPrefab, transform.position, quaternion.identity);
        }

    }
}

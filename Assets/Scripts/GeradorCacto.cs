using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;

public class GeradorCacto : MonoBehaviour
{
    public GameObject cactoPrefab;
    public float delayInicial;
    public float delayEntreCactos;
    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating("GerarCacto", delayInicial, delayEntreCactos);
    }

    // Update is called once per frame
    private void GerarCacto()
    {
        Instantiate(cactoPrefab, transform.position, quaternion.identity);
    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InstancieteBalaCanhao : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject balaPrefab;
    public GameObject vfxExplosionPrefab;

    [Header("Canhões Laterais Principais")]
    public Transform leftCannonPoint;
    public Transform rightCannonPoint;

    [Header("Canhões Extras")]
    public GameObject[] canhoesExtras;

    [Header("Disparo")]
    public float shootForce = 500f;
    public float cooldownTime = 1.5f;
    public float intervaloEntreCanhoes = 0.2f; // Tempo entre disparos individuais
    private float leftCooldown = 0f;
    private float rightCooldown = 0f;
    public float spawnOffset = 1f;

    [Header("VFX")]
    public float vfxLifetime = 2f;

    private List<Transform> canhoesEsquerdos = new List<Transform>();
    private List<Transform> canhoesDireitos = new List<Transform>();

    void Start()
    {
        if (leftCannonPoint != null) canhoesEsquerdos.Add(leftCannonPoint);
        if (rightCannonPoint != null) canhoesDireitos.Add(rightCannonPoint);
    }

    void Update()
    {
        if (leftCooldown > 0f) leftCooldown -= Time.deltaTime;
        if (rightCooldown > 0f) rightCooldown -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && leftCooldown <= 0f)
        {
            StartCoroutine(DispararCanhoesComDelay(canhoesEsquerdos, -transform.right));
            leftCooldown = cooldownTime;
        }

        if (Input.GetMouseButtonDown(1) && rightCooldown <= 0f)
        {
            StartCoroutine(DispararCanhoesComDelay(canhoesDireitos, transform.right));
            rightCooldown = cooldownTime;
        }
    }

    IEnumerator DispararCanhoesComDelay(List<Transform> canhoes, Vector3 direcao)
    {
        List<Transform> copiaCanhoes = new List<Transform>(canhoes); // <- CÓPIA SEGURA

        foreach (Transform canhao in copiaCanhoes)
        {
            if (canhao == null) continue;

            Vector3 spawnPos = canhao.position + (direcao.normalized * spawnOffset);
            GameObject bala = Instantiate(balaPrefab, spawnPos, Quaternion.identity);

            Rigidbody rb = bala.GetComponent<Rigidbody>();
            rb.AddForce(direcao.normalized * shootForce);

            var script = bala.GetComponent<BolaDeCanhao>();
            if (script != null)
            {
                script.shooterTag = "Player";
                script.shooterObject = gameObject;
            }

            if (vfxExplosionPrefab != null)
            {
                GameObject vfx = Instantiate(vfxExplosionPrefab, canhao.position, Quaternion.identity);
                Destroy(vfx, vfxLifetime);
            }

            AudioManager.Instance.TocarSomDirecional(0, transform.position);

            yield return new WaitForSeconds(intervaloEntreCanhoes);
        }
    }

    public void AtivarCanhoesExtras()
    {
        foreach (GameObject canhaoGO in canhoesExtras)
        {
            canhaoGO.SetActive(true);
            Transform pontoDeDisparo = canhaoGO.transform;

            Vector3 localPos = transform.InverseTransformPoint(pontoDeDisparo.position);
            if (localPos.x < 0)
                canhoesEsquerdos.Add(pontoDeDisparo);
            else
                canhoesDireitos.Add(pontoDeDisparo);
        }

        Debug.Log("Canhões extras ativados e adicionados aos lados corretos.");
    }
}

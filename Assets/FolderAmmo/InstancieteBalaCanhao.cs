using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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
    [Header("UI de Cooldown")]
    public Slider sliderCooldownEsquerda;
    public Slider sliderCooldownDireita;
    private float cooldownMultiplier = 1f;
    private Coroutine cooldownResetCoroutine;


    [Header("VFX")]
    public float vfxLifetime = 2f;

    private List<Transform> canhoesEsquerdos = new List<Transform>();
    private List<Transform> canhoesDireitos = new List<Transform>();

    void Start()
    {
        if (sliderCooldownEsquerda != null) sliderCooldownEsquerda.value = 1;
        if (sliderCooldownDireita != null) sliderCooldownDireita.value = 1;

        if (leftCannonPoint != null) canhoesEsquerdos.Add(leftCannonPoint);
        if (rightCannonPoint != null) canhoesDireitos.Add(rightCannonPoint);
    }

    void Update()
    {
        // Atualiza e mostra slider da esquerda
        if (leftCooldown > 0f)
        {
            leftCooldown -= Time.deltaTime;

            if (sliderCooldownEsquerda != null)
            {
                sliderCooldownEsquerda.gameObject.SetActive(true);
                sliderCooldownEsquerda.value = 1f - Mathf.Clamp01(leftCooldown / (cooldownTime * cooldownMultiplier));
            }
        }
        else
        {
            if (sliderCooldownEsquerda != null)
                sliderCooldownEsquerda.gameObject.SetActive(false);
        }

        // Atualiza e mostra slider da direita
        if (rightCooldown > 0f)
        {
            rightCooldown -= Time.deltaTime;

            if (sliderCooldownDireita != null)
            {
                sliderCooldownDireita.gameObject.SetActive(true);
                sliderCooldownDireita.value = 1f - Mathf.Clamp01(rightCooldown / (cooldownTime * cooldownMultiplier));
            }
        }
        else
        {
            if (sliderCooldownDireita != null)
                sliderCooldownDireita.gameObject.SetActive(false);
        }

        // Disparo Esquerdo
        if (Input.GetMouseButtonDown(0) && leftCooldown <= 0f && Time.timeScale > 0f)
        {
            StartCoroutine(DispararCanhoesComDelay(canhoesEsquerdos, -transform.right));
            leftCooldown = cooldownTime * cooldownMultiplier;
        }

        // Disparo Direito
        if (Input.GetMouseButtonDown(1) && rightCooldown <= 0f && Time.timeScale > 0f)
        {
            StartCoroutine(DispararCanhoesComDelay(canhoesDireitos, transform.right));
            rightCooldown = cooldownTime * cooldownMultiplier;
        }
    }

    public void AplicarModificadorCooldown(float fator, float duracao)
    {
        if (cooldownResetCoroutine != null)
            StopCoroutine(cooldownResetCoroutine);

        cooldownMultiplier = fator;
        cooldownResetCoroutine = StartCoroutine(ResetarModificadorCooldown(duracao));
    }

    private IEnumerator ResetarModificadorCooldown(float duracao)
    {
        yield return new WaitForSeconds(duracao);
        cooldownMultiplier = 1f;
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

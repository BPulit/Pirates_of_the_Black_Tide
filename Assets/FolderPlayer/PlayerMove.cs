using UnityEngine;
using System.Collections.Generic;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody rb;

    [Header("Movimento normal")]
    public float turnSpeed = 50f;
    public float anchorBrake = 2f;
    public float tiltAmount = 15f;
    public float tiltSmooth = 5f;
    public float repulsionForce = 400f;

    private bool engineOn = false;
    private float currentTilt = 0f;

    [Header("Modo orbital (boss)")]
    public bool modoOrbital = false;
    public List<Transform> centrosDaArena = new();
    private Transform centroAtual;

    public float raioArena = 30f;

    public float velocidadeAngular = 30f;
    public float velocidadeAtual = 30f;
    public float velocidadeMinima = 10f;
    public float velocidadeMaxima = 30f;
    public float aceleracao = 10f;
    private bool querMover = false;
    private bool querParar = false;


    [Header("Flutuação")]
    public float suavizacaoAltura = 2f;

    [Header("Multiplicadores de Tripulantes")]
    public float multiplicadorVelocidade = 1f;
    public float multiplicadorRotacao = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
{
    // Comando para mover
    if (Input.GetKeyDown(KeyCode.W))
    {
        querMover = true;
    }

    // Comando para parar
    if (Input.GetKeyDown(KeyCode.S))
    {
        // Só toca som se o barco estiver se movendo
        if (rb.linearVelocity.magnitude > 0.5f)
        {
            AudioManager.Instance.TocarSomEfeito(6);
        }

        querParar = true;
    }
}


        void FixedUpdate()
    {
        if (modoOrbital && centrosDaArena.Count == 0)
        {
            modoOrbital = false;
            if (rb.isKinematic) rb.isKinematic = false;
        }

        if (modoOrbital)
        {
            if (!rb.isKinematic) rb.isKinematic = true;
            MoverNoModoOrbital();
        }
        else
        {
            if (rb.isKinematic) rb.isKinematic = false;
            MoverNormal();
        }
    }


        void MoverNormal()
    {
        if (querMover)
        {
            engineOn = true;
            querMover = false;
        }

        if (querParar)
        {
            engineOn = false;
            querParar = false;
        }

        if (engineOn)
        {
            rb.AddForce(transform.forward * StatusPlayer.Instance.velocidade * multiplicadorVelocidade, ForceMode.Force);
        }
        else
        {
            rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, Vector3.zero, anchorBrake * Time.deltaTime);
        }

        float targetTilt = 0f;

        if (rb.linearVelocity.magnitude > 0.1f)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.up, -turnSpeed * multiplicadorRotacao * Time.deltaTime);
                targetTilt = tiltAmount;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(Vector3.up, turnSpeed * multiplicadorRotacao * Time.deltaTime);
                targetTilt = -tiltAmount;
            }
        }

        currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.deltaTime * tiltSmooth);
        transform.localRotation = Quaternion.Euler(0f, transform.localEulerAngles.y, currentTilt);

        Vector3 pos = transform.position;
        float alturaOnda = WaterMath.CalcularAlturaDaOnda(pos);
        float ySuavizado = Mathf.Lerp(pos.y, alturaOnda, Time.deltaTime * suavizacaoAltura);
        transform.position = new Vector3(pos.x, ySuavizado, pos.z);
    }

    void MoverNoModoOrbital()
    {
        float velocidadeFinal = velocidadeAtual * multiplicadorVelocidade;

        if (Input.GetKey(KeyCode.S))
        {
            velocidadeAtual = Mathf.Max(velocidadeAtual - aceleracao * Time.deltaTime, velocidadeMinima);
        }
        else
        {
            velocidadeAtual = Mathf.Min(velocidadeAtual + aceleracao * Time.deltaTime, velocidadeMaxima);
        }

        if (centroAtual == null)
        {
            modoOrbital = false;
            return;
        }

        transform.RotateAround(centroAtual.position, Vector3.up, velocidadeFinal * Time.deltaTime);

        Vector3 direcaoCentro = (transform.position - centroAtual.position).normalized;
        transform.position = centroAtual.position + direcaoCentro * raioArena;

        transform.rotation = Quaternion.LookRotation(Vector3.Cross(Vector3.up, direcaoCentro));
    }

    public void AtivarModoOrbital(Transform centro)
    {
        if (!centrosDaArena.Contains(centro))
            centrosDaArena.Add(centro);

        centroAtual = centro;
        modoOrbital = true;

        Vector3 direcaoInicial = (transform.position - centro.position).normalized;
        transform.position = centro.position + direcaoInicial * raioArena;

        if (!rb.isKinematic) rb.isKinematic = true;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cenario"))
        {
            Vector3 awayDirection = transform.position - collision.contacts[0].point;
            awayDirection.y = 0;

            StatusPlayer.Instance.TomarDano(1);
            rb.AddForce(awayDirection.normalized * repulsionForce, ForceMode.Impulse);
        }
    }
    public void DesativarModoOrbital()
    {
        modoOrbital = false;
        centroAtual = null;

        if (rb != null)
            rb.isKinematic = false;
    }
    public Transform GetCentroAtual()
    {
        return centroAtual;
    }
    public void DestruirCentroAtual()
    {
        if (centroAtual != null)
        {
            Destroy(centroAtual.gameObject);
            centroAtual = null;
        }
    }



}

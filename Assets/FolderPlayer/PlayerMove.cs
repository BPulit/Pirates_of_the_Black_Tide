using UnityEngine;

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
    public Transform centroDaArena;
    public float raioArena = 30f;

    public float velocidadeAngular = 30f;
    public float velocidadeAtual = 30f;
    public float velocidadeMinima = 10f;
    public float velocidadeMaxima = 30f;
    public float aceleracao = 10f;

    [Header("Flutuação")]
    public float suavizacaoAltura = 2f;

    [Header("Multiplicadores de Tripulantes")]
    public float multiplicadorVelocidade = 1f;
    public float multiplicadorRotacao = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (modoOrbital && centroDaArena == null)
        {
            modoOrbital = false;
            if (rb.isKinematic) rb.isKinematic = false;
        }

        if (modoOrbital && centroDaArena != null)
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
        if (Input.GetKeyDown(KeyCode.W)) engineOn = true;

        if (Input.GetKeyDown(KeyCode.S))
        {
            engineOn = false;
            AudioManager.Instance.TocarSomEfeito(6);
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

        transform.RotateAround(centroDaArena.position, Vector3.up, velocidadeFinal * Time.deltaTime);

        Vector3 direcaoCentro = (transform.position - centroDaArena.position).normalized;
        transform.position = centroDaArena.position + direcaoCentro * raioArena;

        transform.rotation = Quaternion.LookRotation(Vector3.Cross(Vector3.up, direcaoCentro));
    }

    public void AtivarModoOrbital(Transform centro)
    {
        centroDaArena = centro;
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
}

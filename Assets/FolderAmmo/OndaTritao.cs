using UnityEngine;

public class OndaTritao : MonoBehaviour
{
    private Vector3 direcao;
    private float velocidade;
    private float duracao;
    private int dano;
    private float tempoVivo;

    public void Configurar(Vector3 direcao, float velocidade, float duracao, int dano)
    {
        this.direcao = direcao.normalized;
        this.velocidade = velocidade;
        this.duracao = duracao;
        this.dano = dano;
    }

    void Update()
    {
        transform.position += direcao * velocidade * Time.deltaTime;
        tempoVivo += Time.deltaTime;

        if (tempoVivo >= duracao)
            Destroy(gameObject);
    }

   private void OnTriggerEnter(Collider other)
{
    var enemy = other.GetComponentInParent<StatusEnemie>();
    if (enemy != null)
    {
        enemy.TakeDamage(dano);
    }

    var kraken = other.GetComponentInParent<KrakenStatus>();
    if (kraken != null)
    {
        kraken.TakeDamage(dano);
    }

    var seaMonster = other.GetComponentInParent<SeaMonsterStatus>();
    if (seaMonster != null)
    {
        Debug.Log("Acertou SeaMonster!");
        seaMonster.TomarDano(dano);
    }
}
}

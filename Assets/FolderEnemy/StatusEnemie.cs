using UnityEngine;

public class StatusEnemie : MonoBehaviour
{
    public int vida = 5; // Valor base setado manualmente no Inspector

    public int valueXp = 5;
    private int saudeAtual;
    public System.Action OnDeath;

    void Start()
    {
        int nivelJogador = PlayerXpManage.instance.nivel;
        int vidaEscalada = vida * nivelJogador;

        saudeAtual = vidaEscalada;
        Debug.Log("Inimigo gerado com Vida: " + saudeAtual);
    }

    public void TakeDamage(int amount)
    {
        saudeAtual -= amount;
        if (saudeAtual <= 0)
        {
            Die();
        }
    }

    public int GetVidaAtual()
    {
        return saudeAtual;
    }

    void Die()
    {
        AudioManager.Instance.TocarSomDirecional(2, transform.position);
        PlayerXpManage.instance.GanharXP(valueXp);

        EnemyDropManager dropManager = GetComponent<EnemyDropManager>();
        if (dropManager != null)
        {
            dropManager.DroparItensEMoedas();
        }

        if (OnDeath != null) OnDeath.Invoke();
        CurrencyManager.instance.IncrementarNaviosDestruidos();

        Destroy(gameObject);
    }
}

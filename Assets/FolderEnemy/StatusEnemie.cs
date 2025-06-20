using UnityEngine;

public class StatusEnemie : MonoBehaviour
{
    public int vida;

    public int valueXp = 5;
    private int saudeAtual;
    public System.Action OnDeath;
   

    void Start()
    {
        int nivelJogador = PlayerXpManage.instance.nivel;
        int vidaMin = nivelJogador;
        int vidaMax = nivelJogador * 2;
        vida = Random.Range(vidaMin, vidaMax + 1);
        Debug.Log("Inimigo gerado com Vida: " + vida);
        saudeAtual = vida;
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

        // Dropa itens e moedas se tiver um DropManager
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

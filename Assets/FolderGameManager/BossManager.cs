using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager instance;

    private bool bossAtivo = false;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public bool PodeAtivarBoss()
    {
        return !bossAtivo;
    }

    public void AtivarBoss()
    {
        bossAtivo = true;
    }

    public void FinalizarBoss()
    {
        bossAtivo = false;
    }
}

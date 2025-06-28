using UnityEngine;

public class SeaMonsterAnimatorControl : MonoBehaviour
{
    public Animator animator; // Referência pública agora
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void AtacarRabo()
    {
        if (animator == null) return;
        string trigger = Random.value < 0.5f ? "SeaMonsterTailAtack" : "SeaMonsterSideTailAtack";
        animator.SetTrigger(trigger);
    }

    public void AtacarCabeca()
    {
        if (animator == null) return;
        animator.SetTrigger("SeaMonsterHeadAtack");
    }

    public void Morrer()
    {
        if (animator == null) return;
        animator.SetTrigger("SeaMonsterDie");
    }
}

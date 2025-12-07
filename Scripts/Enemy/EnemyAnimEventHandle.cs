using UnityEngine;

public class EnemyAnimEventHandle : MonoBehaviour
{
    [SerializeField] private EnemyManager enemyManager;

    public void TryDealDamage()
    {
        enemyManager.attackHandler.Charge();
    }
}

using UnityEngine;

[RequireComponent(typeof(BossMovement), typeof(BossShooting))]
public class BossController : MonoBehaviour
{
    private BossMovement bossMovement;
    private BossShooting bossShooting;

    void Awake()
    {
        bossMovement = GetComponent<BossMovement>();
        bossShooting = GetComponent<BossShooting>();
    }

    void Update()
    {
        // Gọi di chuyển & tấn công
        bossMovement.enabled = true;
        bossShooting.enabled = true;
    }
}

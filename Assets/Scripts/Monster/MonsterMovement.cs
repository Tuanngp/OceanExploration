using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    private MonsterAnimation monsterAnimation;
    private WarningSystem warningSystem;
    private bool hasSpottedPlayer = false;

    void Start()
    {
        monsterAnimation = GetComponent<MonsterAnimation>();
        warningSystem = WarningSystem.Instance;
    }

    void Update()
    {
        if (target != null)
        {
            if (!hasSpottedPlayer && IsTargetVisible())
            {
                hasSpottedPlayer = true;
                warningSystem?.ShowWarning();
                Debug.Log("Monster đã phát hiện tàu, bắt đầu đuổi theo!");
            }

            if (hasSpottedPlayer)
            {
                MoveToTarget();
            }
        }
    }

    private void MoveToTarget()
    {
        if (target == null) return;

        Vector3 direction = (target.position - transform.position).normalized;
        bool isMoving = direction.magnitude > 0.01f;

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            monsterAnimation?.SetMoving(true);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (angle > 90f || angle < -90f)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                angle += 180f;
            }
            else
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }


            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            monsterAnimation?.SetMoving(false);
        }
    }

    private bool IsTargetVisible()
    {
        if (target == null) return false;
        return Vector3.Distance(transform.position, target.position) < 60f;
    }
}

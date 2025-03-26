using UnityEngine;

public class PetAI : MonoBehaviour
{
    private UpgradeManager upgradeManager;
    public float moveSpeed = 15f;  // Tăng tốc độ di chuyển một chút
    public float detectionRadius = 50f;
    public float followDistance = 5f;  // Giảm xuống để kiểm tra
    public float smoothTime = 0.2f; // Thời gian để di chuyển mượt

    private GameObject submarine;
    private GameObject targetCoin;
    private Vector3 velocity = Vector3.zero; // Dùng cho SmoothDamp

    void Start()
    {

        submarine = GameObject.FindGameObjectWithTag("Player");
        upgradeManager = submarine.GetComponent<UpgradeManager>();
        if (submarine == null)
        {
            // Debug.LogError("Không tìm thấy Submarine trong Scene! Đảm bảo bạn đã gán tag 'Player'!");
            return;
        }

        transform.position = submarine.transform.position + new Vector3(-3, -5, 0);
        // Debug.Log("Pet đã được gán vị trí ban đầu tại tàu ngầm: " + transform.position);
    }

    void Update()
    {
        targetCoin = FindNearestCoin();

        if (targetCoin != null)
        {
            MoveTowards(targetCoin.transform.position);
        }
        else if (submarine != null)
        {
            float distanceToSubmarine = Vector3.Distance(transform.position, submarine.transform.position);

            if (distanceToSubmarine > followDistance)
            {
                MoveTowards(submarine.transform.position);
            }
        }
    }

    void MoveTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        // Debug.Log("Moving towards: " + targetPosition + " | Direction: " + direction);

        // Di chuyển mượt mà về phía mục tiêu
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, moveSpeed);

        // Xoay hướng theo chuyển động
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

    GameObject FindNearestCoin()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        GameObject[] rareCoins = GameObject.FindGameObjectsWithTag("RareCoin");

        GameObject nearestCoin = null;
        float shortestDistance = Mathf.Infinity;

        // Debug.Log($"Tìm thấy {coins.Length} Coin và {rareCoins.Length} RareCoin trong scene.");

        // Kiểm tra Coin bình thường
        foreach (GameObject coin in coins)
        {
            float distance = Vector3.Distance(transform.position, coin.transform.position);
            if (distance < shortestDistance && distance <= detectionRadius)
            {
                shortestDistance = distance;
                nearestCoin = coin;
            }
        }

        // Kiểm tra RareCoin (ưu tiên hơn nếu gần hơn)
        foreach (GameObject rareCoin in rareCoins)
        {
            float distance = Vector3.Distance(transform.position, rareCoin.transform.position);
            if (distance < shortestDistance && distance <= detectionRadius)
            {
                shortestDistance = distance;
                nearestCoin = rareCoin;
            }
        }

        if (nearestCoin != null)
        {
            // Debug.Log("Coin gần nhất ở vị trí: " + nearestCoin.transform.position);
        }
        else
        {
            // Debug.Log("Không tìm thấy coin nào trong phạm vi.");
        }

        return nearestCoin;
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            upgradeManager.AddResources(10);
        }
        else if (other.CompareTag("RareCoin"))
        {
            Destroy(other.gameObject);
            upgradeManager.AddResources(50);
        }
    }
}

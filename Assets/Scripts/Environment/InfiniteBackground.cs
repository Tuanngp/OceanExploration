using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public Transform player; 
    public Transform[] backgrounds; 
    private float backgroundWidth; 

    private void Start()
    {
        if (backgrounds.Length == 0) return;

       
        backgroundWidth = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
       
        Transform leftMost = backgrounds[0];
        Transform rightMost = backgrounds[0];

        foreach (Transform bg in backgrounds)
        {
            if (bg.position.x < leftMost.position.x)
                leftMost = bg;
            if (bg.position.x > rightMost.position.x)
                rightMost = bg;
        }

        
        if (player.position.x > rightMost.position.x - backgroundWidth / 2)
        {
            leftMost.position = new Vector3(rightMost.position.x + backgroundWidth, leftMost.position.y, leftMost.position.z);
        }
        
        else if (player.position.x < leftMost.position.x + backgroundWidth / 2)
        {
            rightMost.position = new Vector3(leftMost.position.x - backgroundWidth, rightMost.position.y, rightMost.position.z);
        }
    }
}

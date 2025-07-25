using UnityEngine;

public class Animation : MonoBehaviour
{
    Animator playerAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (playerAnimator != null)
            {
                playerAnimator.SetBool("Attack", true);
                StartCoroutine(ResetAttackBool());
            }
        }
    
    }
    // Attack 애니메이션 후 bool 파라미터를 false로 돌려주는 코루틴
    private System.Collections.IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(0.5f); // 애니메이션 길이에 맞게 조절
        if (playerAnimator != null)
        {
            playerAnimator.SetBool("Attack", false);
        }
    }
}

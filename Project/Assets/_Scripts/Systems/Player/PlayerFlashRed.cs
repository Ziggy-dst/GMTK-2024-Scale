using System.Collections;
using _Scripts.Managers;
using UnityEngine;


public class PlayerFlashRed : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Color hitColor = Color.red;
    public float flashDuration = 0.1f;

    private Color originalColor;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy Bullet"))
        {
            StartCoroutine(FlashRed());
            Destroy(other.gameObject); // 销毁弹幕
            Destroy(gameObject);

            GameManager.Instance.ChangeGameState(GameState.Lose);
        }
    }

    IEnumerator FlashRed()
    {
        spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }


}
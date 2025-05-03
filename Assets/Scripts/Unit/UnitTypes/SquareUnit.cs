using System.Collections;
using UnityEngine;

public class SquareUnit : Unit
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<FireTower>(out var tower))
        {
            tower.towerShooter.provokedTargets.Add(transform);
            SoundSystem.Instance.Sound("Provacation").Play();
            StartProvocationUI();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<FireTower>(out var tower))
        {
            tower.towerShooter.provokedTargets.Remove(transform);
        }
    }

    private void StartProvocationUI()
    {
        StartCoroutine(ProvocationUI(0.2f));
    }

    private IEnumerator ProvocationUI(float duration)
    {
        if (spriteRenderer == null)
            yield break;

        Color startColor = spriteRenderer.color;
        Color targetColor = Color.red;
        Vector3 startScale = transform.localScale;
        Vector3 newScale = new Vector3(1.5f, 1.5f, 1.5f);

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            Color newColor = Color.Lerp(startColor, targetColor, elapsedTime / duration);
            transform.localScale = Vector3.Lerp(startScale, newScale, elapsedTime / duration);
            spriteRenderer.color = newColor;

            yield return null;
        }

        spriteRenderer.color = targetColor;
        transform.localScale = newScale;

        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            Color newColor = Color.Lerp(targetColor, startColor, elapsedTime / duration);
            transform.localScale = Vector3.Lerp(newScale, startScale, elapsedTime / duration);
            spriteRenderer.color = newColor;

            yield return null;
        }

        transform.localScale = startScale;
        spriteRenderer.color = startColor;
    }
}

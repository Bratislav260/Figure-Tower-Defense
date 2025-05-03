using UnityEngine;

public class GameOverLogic : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Unit"))
        {
            SoundSystem.Instance.StopLastPlayedMusics();
            SoundSystem.Instance.Sound("GameOver").Play();
            EventManager.GameOver();
        }
    }
}

using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private AudioClip moveClip;
    [SerializeField] private AudioClip loseClip;
    [SerializeField] private GamePlayManager gamePlayManager;
    [SerializeField] private GameObject explosionPrefab;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SoundManager.Instance.PlaySound(moveClip);
            // Mouse tıklamasında dönüş yönünü tersine çevir
            rotateSpeed *= -1;
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Obstacle")) return;

        Instantiate(explosionPrefab, transform.GetChild(0).position, Quaternion.identity);
        gamePlayManager.GameEnded();
        SoundManager.Instance.PlaySound(loseClip);
        Destroy(gameObject);
    }
}
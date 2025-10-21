using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float minRotateSpeed;
    [SerializeField] private float maxRotateSpeed;
    [SerializeField] private float minRotateTime;
    [SerializeField] private float maxRotateTime;

    private float currentRotateSpeed;
    private float rotateTime;
    private float currentRotateTime;

    private void Awake()
    {
        SetRandomRotation();
        currentRotateTime = 0f;
    }

    private void Update()
    {
        currentRotateTime += Time.deltaTime;
        // Geçerli dönüş süresi, atanan dönüş süresini aştığında yeni rastgele değerler al
        if (currentRotateTime > rotateTime)
        {
            SetRandomRotation();
            currentRotateTime = 0f;
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, currentRotateSpeed * Time.fixedDeltaTime);
    }

    private void SetRandomRotation()
    {
        // Min ve max arasında rastgele bir dönüş hızı üret
        float speedLerp = Random.Range(0f, 1f);
        currentRotateSpeed = Mathf.Lerp(minRotateSpeed, maxRotateSpeed, speedLerp);
        // Dönüş yönünü rastgele belirle (saat yönünde veya saat yönünün tersine)
        currentRotateSpeed *= Random.Range(0, 2) == 0 ? -1f : 1f;

        // Min ve max arasında rastgele bir dönüş süresi üret
        float timeLerp = Random.Range(0f, 1f);
        rotateTime = Mathf.Lerp(minRotateTime, maxRotateTime, timeLerp);
    }
}
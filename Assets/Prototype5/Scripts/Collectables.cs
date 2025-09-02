using UnityEngine;

namespace Prototype5
{
    public class Collectables : MonoBehaviour
    {
        private bool isCollected = false;

        [SerializeField] private float flyDuration = 0.5f;
        [SerializeField] private AnimationCurve flyCurve;

        [SerializeField] private float floatAmplitude = 0.05f;
        [SerializeField] private float floatFrequency = 0.8f;
        [SerializeField] private float rotationSpeed = 20f;

        private Vector3 startPos;
        private Vector2 floatDirection;
        private float floatOffset;

        private void Start()
        {
            startPos = transform.position;
            floatDirection = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0.8f, 1.0f)).normalized;

            floatOffset = Random.Range(0f, Mathf.PI * 2f);
        }

        private void Update()
        {
            if (!isCollected)
            {
                float floatAmount = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
                transform.position = startPos + (Vector3)(floatDirection * floatAmount);

                transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (isCollected) return;

            if (collision.CompareTag("Player"))
            {
                isCollected = true;
                StartCoroutine(FlyToPlayer(collision.transform));
            }
        }

        private System.Collections.IEnumerator FlyToPlayer(Transform player)
        {
            // Disable collision and physics
            Collider2D col = GetComponent<Collider2D>();
            if (col != null) col.enabled = false;

            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Kinematic;
            }

            // Save start state
            Vector3 start = transform.position;
            Vector3 end = player.position;
            float elapsed = 0f;

            // Scale pop-in effect
            Vector3 originalScale = transform.localScale;
            transform.localScale = originalScale * 0.5f;

            while (elapsed < flyDuration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / flyDuration);
                if (flyCurve != null)
                    t = flyCurve.Evaluate(t);

                transform.position = Vector3.Lerp(start, end, t);
                transform.localScale = Vector3.Lerp(originalScale * 0.5f, originalScale, t);

                yield return null;
            }

            transform.position = end;
            transform.localScale = originalScale;

            // Begin orbiting
            FloatingCollectable fc = gameObject.AddComponent<FloatingCollectable>();
            fc.SetPlayer(player);

            // Register with manager
            CollectablesManager.Instance.Collect(fc);
        }
    }
}

    



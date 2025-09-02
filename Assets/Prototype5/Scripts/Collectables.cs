using UnityEngine;

namespace Prototype5
{
    public class Collectables : MonoBehaviour
    {
        private bool isCollected = false;

        [SerializeField] private float flyDuration = 0.5f;
        [SerializeField] private AnimationCurve flyCurve;

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

    



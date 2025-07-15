using UnityEngine;

namespace Prototype3
{
    public class Crosshair : MonoBehaviour
    {
        [SerializeField] Texture2D image;
        [SerializeField] int size;
        [SerializeField] float maxAngle;
        [SerializeField] float minAngle;

        float lookHeight;

        public void Lookheight(float value)
        {
            lookHeight += value;

            if(lookHeight > maxAngle || lookHeight < minAngle)
            {
                lookHeight -= value;
            }
        }

        void OnGUI()
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            screenPosition.y = Screen.height - screenPosition.y;
            GUI.DrawTexture(new Rect(screenPosition.x, screenPosition.y - lookHeight, size, size), image);
        }
    }
}

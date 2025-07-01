using UnityEngine;

namespace Prototype1
{
    public enum PowerUpType { None, Pushback, Smash }

    public class Powerup : MonoBehaviour
    {
        public PowerUpType powerUpType;
    }
}

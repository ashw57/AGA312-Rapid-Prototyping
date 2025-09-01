using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

namespace Prototype5
{
    public class CollectablesManager : MonoBehaviour
    {
        public static CollectablesManager Instance;

        public int collectablesNeeded = 5;
        public int currentCollectables = 0;

        public Door doorToUnlock;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void Collect()
        {
            currentCollectables++;

            if(currentCollectables >= collectablesNeeded)
            {
                doorToUnlock.Unlock();
            }
        }
    }
}


using UnityEngine;
using System.Collections.Generic;

namespace Prototype5
{
    public class CollectablesManager : MonoBehaviour
    {
        public static CollectablesManager Instance;

        public int collectablesNeeded = 5;
        public int currentCollectables = 0;

        public Door doorToUnlock;

        public List<FloatingCollectable> collectedItems = new List<FloatingCollectable>();
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

        public void Collect(FloatingCollectable collectable)
        {
            currentCollectables++;
            collectedItems.Add(collectable);
            UpdateOrbitSpacing();

            if(currentCollectables >= collectablesNeeded)
            {
                doorToUnlock.Unlock();

                UnlockMessageUI.Instance.ShowMessage();
            }
        }

        private void UpdateOrbitSpacing()
        {
            int count = collectedItems.Count;

            for (int i = 0; i < count; i++)
            {
                collectedItems[i].SetOrbitIndex(i, count);
            }
        }
    }
}


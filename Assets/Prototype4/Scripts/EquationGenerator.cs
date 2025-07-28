using UnityEngine;
using TMPro;
using System.Collections.Generic;

namespace Prototype4
{

    public class EquationGenerator : MonoBehaviour
    {
        public enum Difficulty { EASY, MEDIUM, HARD }
        public Difficulty difficulty;

        public BV.Range easyRange;
        public BV.Range mediumRange;
        public BV.Range hardRange;

        public int numberOne;
        public int numberTwo;
        public int correctAnswer;
        public List<int> dummyAnswers;

        [Header("Prefabs and Spawn Points")]
        public GameObject numberOnePrefab;
        public GameObject numberTwoPrefab;
        public GameObject answerPrefab;

        public Transform numberOneSpawnPoint;
        public Transform numberTwoSpawnPoint;
        public Transform answerSpawnPoint;

        private List<GameObject> spawnedObjects = new List<GameObject>();

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
                GenerateMultiplication();
        }

        public void GenerateMultiplication()
        {
            ClearPreviousObjects();
            GenerateRandomNumbers();
            correctAnswer = numberOne * numberTwo;
            GenerateDummyAnswers();

            Debug.Log(numberOne + " x " + numberTwo + " = " + correctAnswer);

            InstantiateNumber(numberOnePrefab, numberOne, numberOneSpawnPoint);
            InstantiateNumber(numberTwoPrefab, numberTwo, numberTwoSpawnPoint);
            InstantiateNumber(answerPrefab, correctAnswer, answerSpawnPoint);
        }

        public void GenerateAddition()
        {
            ClearPreviousObjects();
            GenerateRandomNumbers();
            correctAnswer = numberOne + numberTwo;
            GenerateDummyAnswers();

            Debug.Log(numberOne + " + " + numberTwo + " = " + correctAnswer);

            InstantiateNumber(numberOnePrefab, numberOne, numberOneSpawnPoint);
            InstantiateNumber(numberTwoPrefab, numberTwo, numberTwoSpawnPoint);
            InstantiateNumber(answerPrefab, correctAnswer, answerSpawnPoint);
        }

        public void GenerateSubtraction()
        {
            ClearPreviousObjects();
            GenerateRandomNumbers();
            correctAnswer = numberOne - numberTwo;
            GenerateDummyAnswers();

            Debug.Log(numberOne + " - " + numberTwo + " = " + correctAnswer);

            InstantiateNumber(numberOnePrefab, numberOne, numberOneSpawnPoint);
            InstantiateNumber(numberTwoPrefab, numberTwo, numberTwoSpawnPoint);
            InstantiateNumber(answerPrefab, correctAnswer, answerSpawnPoint);
        }

        public void GenerateDivision()
        {
            ClearPreviousObjects();
            GenerateRandomNumbers();
            while (numberTwo == 0)
                numberTwo = GetRandomNumber();

            float tempAnswer = numberOne / numberTwo;
            correctAnswer = Mathf.RoundToInt(tempAnswer);
            GenerateDummyAnswers();

            Debug.Log(numberOne + " / " + numberTwo + " = " + correctAnswer);

            InstantiateNumber(numberOnePrefab, numberOne, numberOneSpawnPoint);
            InstantiateNumber(numberTwoPrefab, numberTwo, numberTwoSpawnPoint);
            InstantiateNumber(answerPrefab, correctAnswer, answerSpawnPoint);
        }

        private void GenerateRandomNumbers()
        {
            numberOne = GetRandomNumber();
            numberTwo = GetRandomNumber();
        }

        private int GetRandomNumber()
        {
            switch (difficulty)
            {
                case Difficulty.EASY:
                    return (int)Random.Range(easyRange.min, easyRange.max);
                case Difficulty.MEDIUM:
                    return (int)Random.Range(mediumRange.min, mediumRange.max);
                case Difficulty.HARD:
                    return (int)Random.Range(hardRange.min, hardRange.max);
                default:
                    return (int)Random.Range(easyRange.min, easyRange.max);
            }
        }

        private void GenerateDummyAnswers()
        {
            for (int i = 0; i < dummyAnswers.Count; i++)
            {
                int dummy;
                do
                {
                    dummy = Random.Range(correctAnswer - 10, correctAnswer + 10);
                }
                while (dummy == correctAnswer || dummyAnswers.Contains(dummy));
                dummyAnswers[i] = dummy;
                Debug.Log("Dummy Answer: " + dummyAnswers[i]);
            }
        }

        private void InstantiateNumber(GameObject prefab, int number, Transform spawnPoint)
        {
            if (prefab == null || spawnPoint == null)
            {
                Debug.LogWarning("Prefab or spawn point not assigned. ");
                return;
            }

            GameObject obj = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            obj.name = "Number_" + number;
            spawnedObjects.Add(obj);

            var tmp = obj.GetComponentInChildren<TextMeshProUGUI>();
            if (tmp != null)
            {
                tmp.text = number.ToString();
                return;
            }
        }

        private void ClearPreviousObjects()
        {
            foreach (var obj in spawnedObjects)
            {
                if (obj != null)
                    Destroy(obj);
            }
            spawnedObjects.Clear();
        }
    }
}

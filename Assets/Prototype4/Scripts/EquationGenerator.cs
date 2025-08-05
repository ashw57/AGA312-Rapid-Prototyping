using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Prototype4
{
    public class EquationGenerator : MonoBehaviour
    {
        private int correctAnswersCount = 0;

        [Header("Symbol Prefabs")]
        public GameObject plusPrefab;
        public GameObject minusPrefab;
        public GameObject multiplyPrefab;
        public GameObject dividePrefab;

        [Header("Reward Prefabs")]
        public List<GameObject> rewardPrefabs; // Prefabs to spawn upon correct answer
        public List<Transform> rewardSpawnZone; // Where to spawn Prefabs

        public Transform symbolSpawnPoint;

        public enum Difficulty { EASY, MEDIUM, HARD }
        public Difficulty difficulty;

        public Range easyRange;
        public Range mediumRange;
        public Range hardRange;

        public int numberOne;
        public int numberTwo;
        public int correctAnswer;
        public List<int> dummyAnswers;

        [SerializeField] private TextMeshProUGUI feedbackTextCorrect;
        [SerializeField] private TextMeshProUGUI feedbackTextIncorrect;
        [SerializeField] private float answerSpacing = 2f;

        public void GenerateAddition() => GenerateEquation((a, b) => a + b, "+");
        public void GenerateSubtraction() => GenerateEquation((a, b) => a - b, "-");
        public void GenerateMultiplication() => GenerateEquation((a, b) => a * b, "x");

        [Header("Prefabs and Spawn Points")]
        public GameObject numberOnePrefab;
        public GameObject numberTwoPrefab;
        public List<GameObject> answerPrefabs;

        public Transform numberOneSpawnPoint;
        public Transform numberTwoSpawnPoint;
        public Transform answerSpawnPoint;

        private List<GameObject> spawnedObjects = new List<GameObject>();

        [Header("Answer Settings")]
        [Range(2, 10)]
        public int totalAnswerCount = 4;

        [Header("Answer Materials")]
        public List<Material> answerMaterials;

        private Material currentAnswerMaterial;

        // New list to track spawned reward instances
        private List<GameObject> spawnedRewards = new List<GameObject>();

        private void Start()
        {
            GenerateRandomEquation();
        }

        private void GenerateEquation(Func<int, int, int> operation, string symbol)
        {
            dummyAnswers.Clear();
            ClearPreviousObjects();
            currentAnswerMaterial = GetRandomAnswerMaterial();
            GenerateRandomNumbers();
            correctAnswer = operation(numberOne, numberTwo);
            Debug.Log($"{numberOne} {symbol} {numberTwo} = {correctAnswer}");

            InstantiateNumber(numberOnePrefab, numberOne, numberOneSpawnPoint);
            InstantiateSymbol(symbol, symbolSpawnPoint);
            InstantiateNumber(numberTwoPrefab, numberTwo, numberTwoSpawnPoint);
            GenerateDummyAnswers();
        }

        public void GenerateDivision()
        {
            dummyAnswers.Clear();
            ClearPreviousObjects();
            currentAnswerMaterial = GetRandomAnswerMaterial();

            numberTwo = GetRandomNumber();
            while (numberTwo == 0)
                numberTwo = GetRandomNumber();

            int multiplier = GetRandomNumber();
            numberOne = numberTwo * multiplier;
            correctAnswer = numberOne / numberTwo;

            Debug.Log(numberOne + " ÷ " + numberTwo + " = " + correctAnswer);

            InstantiateNumber(numberOnePrefab, numberOne, numberOneSpawnPoint);
            InstantiateSymbol("÷", symbolSpawnPoint);
            InstantiateNumber(numberTwoPrefab, numberTwo, numberTwoSpawnPoint);

            GenerateDummyAnswers();
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
                    return (int)UnityEngine.Random.Range(easyRange.min, easyRange.max);
                case Difficulty.MEDIUM:
                    return (int)UnityEngine.Random.Range(mediumRange.min, mediumRange.max);
                case Difficulty.HARD:
                    return (int)UnityEngine.Random.Range(hardRange.min, hardRange.max);
                default:
                    return (int)UnityEngine.Random.Range(easyRange.min, easyRange.max);
            }
        }

        private void GenerateDummyAnswers()
        {
            dummyAnswers.Clear();

            List<int> allAnswers = new List<int> { correctAnswer };

            while (allAnswers.Count < totalAnswerCount)
            {
                int dummy = UnityEngine.Random.Range(Mathf.Max(0, correctAnswer - 10), correctAnswer + 11);
                if (dummy != correctAnswer && !allAnswers.Contains(dummy))
                    allAnswers.Add(dummy);
            }

            allAnswers = allAnswers.OrderBy(_ => UnityEngine.Random.value).ToList();

            for (int i = 0; i < allAnswers.Count; i++)
            {
                Vector3 spawnPosition = answerSpawnPoint.position + new Vector3(i * answerSpacing, 0, 0);
                GameObject chosenPrefab = GetRandomAnswerPrefab();
                GameObject answerObj = Instantiate(chosenPrefab, spawnPosition, Quaternion.identity);

                answerObj.name = "Answer_" + allAnswers[i];
                spawnedObjects.Add(answerObj);

                Renderer rend = answerObj.GetComponentInChildren<Renderer>();
                if (rend != null && currentAnswerMaterial != null)
                {
                    rend.material = currentAnswerMaterial;
                }

                TextMeshPro tmp = answerObj.GetComponentInChildren<TextMeshPro>();
                if (tmp != null)
                {
                    tmp.text = allAnswers[i].ToString();
                }

                AnswerButton3D button = answerObj.GetComponent<AnswerButton3D>();
                if (button != null)
                {
                    button.Setup(allAnswers[i], correctAnswer, this, feedbackTextCorrect, feedbackTextIncorrect);
                }
            }
        }

        private Material GetRandomAnswerMaterial()
        {
            if (answerMaterials == null || answerMaterials.Count == 0)
            {
                Debug.LogWarning("No answer materials assigned.");
                return null;
            }

            return answerMaterials[UnityEngine.Random.Range(0, answerMaterials.Count)];
        }

        private void InstantiateNumber(GameObject prefab, int number, Transform spawnPoint)
        {
            if (prefab == null || spawnPoint == null)
            {
                Debug.LogWarning("Prefab or spawn point not assigned.");
                return;
            }

            GameObject obj = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            obj.name = "Number_" + number;
            spawnedObjects.Add(obj);

            var tmp = obj.GetComponentInChildren<TextMeshPro>();
            if (tmp != null)
            {
                tmp.text = number.ToString();
                return;
            }
        }

        public void GenerateRandomEquation()
        {
            int random = UnityEngine.Random.Range(0, 4);

            switch (random)
            {
                case 0:
                    GenerateAddition();
                    break;
                case 1:
                    GenerateSubtraction();
                    break;
                case 2:
                    GenerateMultiplication();
                    break;
                case 3:
                    GenerateDivision();
                    break;
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

            // Optionally also clear spawned rewards on new equation start?
            // If you want to keep rewards across equations, remove this:
            // foreach (var reward in spawnedRewards)
            // {
            //     if (reward != null)
            //         Destroy(reward);
            // }
            // spawnedRewards.Clear();
        }

        private GameObject GetSymbolPrefab(string symbol)
        {
            switch (symbol)
            {
                case "+":
                    return plusPrefab;
                case "-":
                    return minusPrefab;
                case "x":
                case "*":
                    return multiplyPrefab;
                case "/":
                case "÷":
                    return dividePrefab;
                default:
                    Debug.LogWarning("Symbol prefab not assigned for symbol: " + symbol);
                    return null;
            }
        }

        private void InstantiateSymbol(string symbol, Transform spawnPoint)
        {
            GameObject prefab = GetSymbolPrefab(symbol);
            if (prefab == null || spawnPoint == null)
            {
                Debug.LogWarning("Prefab or spawn point not assigned.");
                return;
            }

            GameObject obj = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            obj.name = "Symbol_" + symbol;
            spawnedObjects.Add(obj);
        }

        public void OnCorrectAnswer()
        {
            correctAnswersCount++;
            Debug.Log("Correct answer selected! Total: " + correctAnswersCount);
            SpawnRandomReward();
        }

        private void SpawnRandomReward()
        {
            if (rewardPrefabs == null || rewardPrefabs.Count == 0)
            {
                Debug.LogWarning("No reward prefabs assigned.");
                return;
            }

            if (rewardSpawnZone == null || rewardSpawnZone.Count == 0)
            {
                Debug.LogWarning("No reward spawn zones assigned.");
                return;
            }

            GameObject randomReward = rewardPrefabs[UnityEngine.Random.Range(0, rewardPrefabs.Count)];

            Transform selectedZone = rewardSpawnZone[UnityEngine.Random.Range(0, rewardSpawnZone.Count)];

            Vector3 spawnCenter = selectedZone.position;
            Vector3 spawnSize = selectedZone.localScale;

            Vector3 randomPosition = new Vector3(
                UnityEngine.Random.Range(-0.5f, 0.5f) * spawnSize.x,
                UnityEngine.Random.Range(-0.5f, 0.5f) * spawnSize.y,
                UnityEngine.Random.Range(-0.5f, 0.5f) * spawnSize.z
            );

            randomPosition += spawnCenter;

            GameObject rewardInstance = Instantiate(randomReward, randomPosition, Quaternion.identity);

            // Track spawned reward
            spawnedRewards.Add(rewardInstance);
        }

        // NEW: Remove one spawned reward on incorrect answer
        public void RemoveReward()
        {
            if (spawnedRewards.Count > 0)
            {
                GameObject rewardToRemove = spawnedRewards[spawnedRewards.Count - 1];
                spawnedRewards.RemoveAt(spawnedRewards.Count - 1);

                if (rewardToRemove != null)
                {
                    Destroy(rewardToRemove);
                    Debug.Log("Reward removed due to incorrect answer.");
                }
            }
            else
            {
                Debug.Log("No rewards to remove.");
            }
        }

        private GameObject GetRandomAnswerPrefab()
        {
            if (answerPrefabs == null || answerPrefabs.Count == 0)
            {
                Debug.LogWarning("No answer prefabs assigned.");
                return null;
            }

            return answerPrefabs[UnityEngine.Random.Range(0, answerPrefabs.Count)];
        }

        private void OnDrawGizmosSelected()
        {
            if (rewardSpawnZone != null)
            {
                Gizmos.color = Color.yellow;

                foreach (Transform zone in rewardSpawnZone)
                {
                    if (zone != null)
                    {
                        Gizmos.DrawWireCube(zone.position, zone.localScale);
                    }
                }
            }
        }

        public int GetCorrectAnswerCount()
        {
            return correctAnswersCount;
        }

        public int GetRewardCount()
        {
            return spawnedRewards.Count;
        }
    }
}
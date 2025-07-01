using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CardManager : MonoBehaviour
{
    public List<CardData> cardData;
    public GameObject cardPrefab;
    public List<GameObject> cardsInHand;
    public int handCount = 4;

    public CardData GetCard(CardID _cardID) => cardData.Find(x => x.cardID == _cardID);

    private void BuildDeck()
    {
        ListX.DestroyList(cardsInHand);
        ListX.ShuffleList(cardData);

        for(int i = 0; i < handCount; i++)
        {
            GameObject newCard = Instantiate(cardPrefab, new Vector3(i + 2, 0, 0), transform.rotation);
            newCard.GetComponent<Card>().Initialize(ListX.GetRandomItemFromList(cardData));
            cardsInHand.Add(newCard);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            BuildDeck();
    }
}

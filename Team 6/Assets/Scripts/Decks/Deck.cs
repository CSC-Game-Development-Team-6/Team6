using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Deck
{
    GameObject[] Cards;
    public Deck(int size)
    {
        this.Cards = new GameObject[size];
    }
    public void DrawNewHand(Deck hand)
    {
        foreach (GameObject card in Cards)
        {
            card.GetComponentInChildren<CardComponent>().setCard();
            card.GetComponentInChildren<CardComponent>().drawed = true;
        }

    }
}

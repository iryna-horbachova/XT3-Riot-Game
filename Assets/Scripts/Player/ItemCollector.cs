using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int cards = 0;

    [SerializeField] private Text cardsText;
    [SerializeField] private AudioSource collectionSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Card"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            cards++;
            cardsText.text = "Cards: " + cards;
        }
    }
}

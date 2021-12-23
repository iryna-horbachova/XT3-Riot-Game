using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int cards = 0;
    private int keys = 0;

    [SerializeField] private Text cardsText;
    [SerializeField] private Text keysText;
    [SerializeField] private AudioSource collectionSoundEffect;
    [SerializeField] private AudioSource keySoundEffect;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Card"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            cards++;
            cardsText.text = "Cards: " + cards;
        }
        else if (collision.gameObject.CompareTag("Key"))
        {
            keySoundEffect.Play();
            Destroy(collision.gameObject);
            keys++;
            keysText.text = "Keys: " + keys;
        }
    }
}

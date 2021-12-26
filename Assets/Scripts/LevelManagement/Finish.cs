using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishSoundEffect;
    private GameObject player;
    [SerializeField] private ItemCollector playerItemCollector;

    private bool levelCompleted = false;

    void Start()
    {
        finishSoundEffect = GetComponent<AudioSource>();
        player = GameObject.Find("OrangeRobot");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "OrangeRobot" && !levelCompleted)
        {
            finishSoundEffect.Play();
            levelCompleted = true;
            Invoke("CompleteLevel", 2f);
        }
    }

    private void CompleteLevel()
    {
        // Store cards
        int cards = PlayerPrefs.GetInt("Cards", 0);
        int collectedCards = player.GetComponent<ItemCollector>().cards;
        PlayerPrefs.SetInt("Cards", cards + collectedCards);
        Debug.Log(cards + collectedCards);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

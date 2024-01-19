using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;

public delegate void GameOver();

public class TileGenerator : MonoBehaviour
{
    [Header ("Deck")]
    [SerializeField] private int maxDeckSize;
    private int currentSize;
    private WorldTile[] tiles;
    private WorldTile nextCard;
    public GameObject nextCardSprite;
    public static GameOver OnGameOver;

    [Header("Card Pool")]
    public GameObject cardBack;
    public List<GameObject> deck;
    public float offset = 10f;
    public Vector3 initialCardPos = new Vector3(-870f,-340f,0f);
    public GameObject canvas;
    public TMP_Text deckSizeUI;

    void Start()
    {
        deck = new List<GameObject>();
        currentSize = maxDeckSize - 1;
        tiles = Resources.LoadAll<WorldTile>("Squares");
        GenerateNextCard();
        InitializeDeck();
        GridManager.OnGetTile += GetTile;
        GridManager.OnAddCard += AddCard;
        deckSizeUI.text = currentSize.ToString();
    }


    // We instantiate a cardBack prefab for each card of the deck
    private void InitializeDeck() {
        for (int tile = 0; tile < maxDeckSize; tile++) {
            GameObject instantiated_card = Instantiate(cardBack, new Vector3(initialCardPos.x, initialCardPos.y, initialCardPos.z), Quaternion.identity) as GameObject;
            deck.Add(instantiated_card);
            instantiated_card.transform.SetParent(canvas.transform);
            initialCardPos.y += offset;
        }
    }

    void OnDestroy() {
        GridManager.OnGetTile -= GetTile;
        GridManager.OnAddCard -= AddCard;
    }

    // Generate a random card and add it to the next card in the UI
    private void GenerateNextCard() {
        nextCard = GenerateTile();
        nextCardSprite.GetComponent<Image>().sprite = nextCard.m_AnimatedSprites[0];
    }

    // Update the deck size in the UI
    private void UpdateDeckSizeUI() {
        deckSizeUI.text = currentSize.ToString();
    }

    // Return the current card and generate the next one, update the UI, we are using object pooling
    // so we deactivate the last card in the UI 
    private WorldTile GetTile(Vector3Int mouseOnGrid) {
        if (currentSize >= 0) {
            WorldTile lastCard = nextCard;
            GenerateNextCard();
            deck[currentSize].SetActive(false);
            currentSize--;
            Debug.Log("At " + deck[currentSize + 1]);
            UpdateDeckSizeUI();
            return lastCard;
        } else {
            return null;
        }
    }

    // We add "amount" of cards to the deck clamping the value with maxDeckSize
    // here we reactivate the backCard
    public void AddCard(int amount) {
        float cardAmount = Mathf.Clamp(amount + currentSize + 1, 0f, maxDeckSize);
        int test = 0;
        for (int i = currentSize + 1; i < cardAmount; i++) {
            deck[i].SetActive(true);
            currentSize++;
        }
        UpdateDeckSizeUI();
    }

    // Return a random card
    private WorldTile GenerateTile() {
       return tiles[Random.Range(0, tiles.Length)];
    }

    private bool DeckIsEmpty() {
        return currentSize < 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (DeckIsEmpty() && OnGameOver != null) {
            OnGameOver();    
        }
    }
}

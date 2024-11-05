using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private Image _characterImage;

    public static DialogueManager Instance;
    private Queue<string> _sentenceQueue;

    public static event Action OnDialogueStart;
    public static event Action OnDialogueEnd;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        _sentenceQueue = new Queue<string>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _dialoguePanel.activeSelf)
        {
            NextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        OnDialogueStart?.Invoke();
        FeedbackManager.Instance.PlayFeedback("DialogueFocusFB");

        _dialoguePanel.SetActive(true);
        _sentenceQueue.Clear();

        _nameText.text = dialogue.CharacterName;
        _characterImage.sprite = dialogue.CharacterSprite;


        foreach (string sentence in dialogue.Sentences)
        {
            _sentenceQueue.Enqueue(sentence);
        }

        Debug.Log("Starting dialogue with " + dialogue.CharacterName);
        NextSentence();
    }

    public void NextSentence()
    {
        if (_sentenceQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = _sentenceQueue.Dequeue();
        _dialogueText.text = sentence;
        
    }

    private void EndDialogue()
    {
        OnDialogueEnd?.Invoke();    

        _nameText.text = "";
        _dialogueText.text = "";
        _characterImage.sprite = null;

        _dialoguePanel.SetActive(false);

        FeedbackManager.Instance.PlayFeedback("DialogueUnFocusFB");
    }

}
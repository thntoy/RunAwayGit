using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using System;

public class FeedbackManager : MonoBehaviour
{
    [Serializable]
    public struct FeedbackItem
    {
        public string Key;
        public MMFeedbacks Feedback;
    }

    [SerializeField] private List<FeedbackItem> _feedbackItemLists = new List<FeedbackItem>();

    public static FeedbackManager Instance;
    private Dictionary<string, MMFeedbacks> _keyToFeedback = new Dictionary<string, MMFeedbacks>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        SetupFeedbacks();
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    public void PlayFeedback(string key)
    {
        if (_keyToFeedback.TryGetValue(key, out MMFeedbacks feedback))
        {
            feedback.PlayFeedbacks();
        }
    }

    private void AddFeedback(string key, MMFeedbacks feedback)
    {
        _keyToFeedback[key] = feedback;
    }

    public void SetupFeedbacks()
    {
        _keyToFeedback.Clear();
        foreach (FeedbackItem feedbackItem in _feedbackItemLists)
        {
            AddFeedback(feedbackItem.Key, feedbackItem.Feedback);
        }
    }
}



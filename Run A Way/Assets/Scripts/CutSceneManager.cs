using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneManager : MonoBehaviour
{
    public static CutSceneManager Instance;

    [Serializable]
    public struct CutScene
    {
        public string SceneKey;
        public List<Sprite> Sprites;
    }

    [SerializeField] private GameObject _cutScenePanel;
    [SerializeField] private Image _cutSceneImage;
    [SerializeField] private List<CutScene> _cutScenes;

    private Dictionary<string, CutScene> _keyToCutScene;
    private Coroutine _currentCutSceneCoroutine;


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
    }

    private void Start()
    {
        SetupCutScenes();
    }

    public void PlayCutScene(string key)
    {
        if (_keyToCutScene.TryGetValue(key, out CutScene cutScene))
        {
            StartCoroutine(PlayCutSceneCoroutine(cutScene));
        }
    }

    public void StopCutScene()
    {
        if (_currentCutSceneCoroutine != null)
        {
            StopCoroutine(_currentCutSceneCoroutine);
            _currentCutSceneCoroutine = null;
            _cutScenePanel.SetActive(false);
        }
    }

    private IEnumerator PlayCutSceneCoroutine(CutScene cutScene)
    {
        foreach (var sprite in cutScene.Sprites)
        {
            FeedbackManager.Instance.PlayFeedback("ScreenFadeFB");
            yield return new WaitForSeconds(0.65f);

            if(!_cutScenePanel.activeSelf)
                _cutScenePanel.SetActive(true);

            _cutSceneImage.sprite = sprite;
            
            yield return new WaitForSeconds(3f); // Delay between each sprite
        }

        FeedbackManager.Instance.PlayFeedback("ScreenFadeFB");
        yield return new WaitForSeconds(0.65f);
       
        _cutSceneImage.sprite = null;
        _cutScenePanel.SetActive(false);
        _currentCutSceneCoroutine = null;
    }

    private void AddCutScene(string key, CutScene cutScene)
    {
        _keyToCutScene[key] = cutScene;
    }

    public void SetupCutScenes()
    {
        _keyToCutScene = new Dictionary<string, CutScene>();
        foreach (CutScene cutScene in _cutScenes)
        {
            AddCutScene(cutScene.SceneKey, cutScene);
        }
    }

}

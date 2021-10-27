using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class StartButtonController : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    // Start is called before the first frame update
    void Start()
    {
        actions.Add("Apple", turnGameOn);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray(), ConfidenceLevel.High);
        keywordRecognizer.OnPhraseRecognized += SpeechThatWasRecognized;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void checkVoiceRecognition()
    {
        keywordRecognizer.Start();
    }

    private void SpeechThatWasRecognized(PhraseRecognizedEventArgs speech)
    {
        actions[speech.text].Invoke();
    }

    private void turnGameOn()
    {
        Debug.Log("Hello world");
        keywordRecognizer.Dispose();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

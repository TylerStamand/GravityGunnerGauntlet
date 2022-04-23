using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class Dialogue
{
    public string name;
    
    public UnityEvent OnComplete;
    public void EnvokeOnComplete() => OnComplete?.Invoke();

    [TextArea(3, 10)]
    public string[] sentences;
}

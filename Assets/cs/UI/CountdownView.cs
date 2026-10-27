using System.Collections;
using TMPro;
using UnityEngine;

public class CountdownView : MonoBehaviour, ICountdown {
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string[] _words = { "READY", "GO!" };
    [SerializeField] private float _interval = 1f;

    public IEnumerator Run() {
        foreach (string word in _words) {
            _text.text = word;
            yield return new WaitForSeconds(_interval);
        }
        _text.text = "";
    }
}

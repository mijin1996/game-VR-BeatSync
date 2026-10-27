using System.Collections;
using TMPro;
using UnityEngine;

public class CountdownView : MonoBehaviour, ICountdown {
    [SerializeField] private TMP_Text _text;
    [SerializeField] private string[] _words = { "READY", "GO!" };
    [SerializeField] private float _interval = 1f;
    [SerializeField] private InterfaceReference<ISound> _sound;
    [SerializeField] private InterfaceReference<IEffect> _effect;

    public IEnumerator Run() {
        for (int i = 0; i < _words.Length; i++) {
            _text.text = _words[i];
            // 게임 시작 순간 화면이 멈칫해도, 글자가 먼저 화면에 그려진 다음부터 시간을 잼
            yield return null;
            // 마지막 글자(GO!)가 나올 때 소리 + 이펙트
            if (i == _words.Length - 1) {
                if (_sound.Value != null) {
                    _sound.Value.PlayGo();
                }
                if (_effect.Value != null) {
                    _effect.Value.PlayGo(_text.transform);
                }
            }
            yield return new WaitForSeconds(_interval);
        }
        _text.text = "";
    }
}

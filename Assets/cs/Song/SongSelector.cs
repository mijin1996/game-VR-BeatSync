using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SongSelector : MonoBehaviour, ISongSelector {
    [SerializeField] private SongData[] _songs;
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private Button _prevButton;
    [SerializeField] private Button _nextButton;

    private int _index;

    public SongData Selected {
        get {
            if (_songs == null || _songs.Length == 0) {
                return null;
            }
            return _songs[_index];
        }
    }

    public event Action<SongData> SongSelected;

    void Awake() {
        if (_prevButton != null) {
            _prevButton.onClick.AddListener(Prev);
        }
        if (_nextButton != null) {
            _nextButton.onClick.AddListener(Next);
        }
        // 처음엔 첫 곡을 표시만 하고 미리듣기는 하지 않음 (대기음 유지)
        RefreshTitle();
    }

    public void Prev() {
        Move(-1);
    }

    public void Next() {
        Move(1);
    }

    private void Move(int step) {
        // 누른 < > 버튼이 '선택된 상태'로 남으면 스페이스/엔터(Submit)가 그 버튼을 또 눌러서 노래가 바뀜 → 선택 해제
        if (EventSystem.current != null) {
            EventSystem.current.SetSelectedGameObject(null);
        }
        if (_songs == null || _songs.Length == 0) {
            return;
        }
        _index = (_index + step + _songs.Length) % _songs.Length;   // 끝에서 넘기면 처음으로
        RefreshTitle();
        if (Selected != null) {
            SongSelected?.Invoke(Selected);
        }
    }

    private void RefreshTitle() {
        if (_titleText == null) {
            return;
        }
        _titleText.text = Selected != null ? Selected.Title : "-";
    }
}

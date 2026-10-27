using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public abstract class ListSelector<T> : MonoBehaviour, IListSelect where T : UnityEngine.Object {
    [SerializeField, FormerlySerializedAs("_songs")] private T[] _items;
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private Button _prevButton;
    [SerializeField] private Button _nextButton;

    private int _index;

    public T Selected {
        get {
            if (_items == null || _items.Length == 0) {
                return null;
            }
            return _items[_index];
        }
    }

    public event Action Changed;

    protected abstract string TitleOf(T item);
    protected abstract void OnSelected(T item);

    void Awake() {
        if (_prevButton != null) {
            _prevButton.onClick.AddListener(Prev);
        }
        if (_nextButton != null) {
            _nextButton.onClick.AddListener(Next);
        }
        // 처음엔 첫 항목을 표시만 하고 선택 알림은 하지 않음
        RefreshTitle();
    }

    public void Prev() {
        Move(-1);
    }

    public void Next() {
        Move(1);
    }

    private void Move(int step) {
        // 누른 < > 버튼이 '선택된 상태'로 남으면 스페이스/엔터(Submit)가 그 버튼을 또 눌러서 항목이 바뀜 → 선택 해제
        if (EventSystem.current != null) {
            EventSystem.current.SetSelectedGameObject(null);
        }
        if (_items == null || _items.Length == 0) {
            return;
        }
        _index = (_index + step + _items.Length) % _items.Length;   // 끝에서 넘기면 처음으로
        RefreshTitle();
        if (Selected != null) {
            OnSelected(Selected);
            Changed?.Invoke();
        }
    }

    private void RefreshTitle() {
        if (_titleText == null) {
            return;
        }
        _titleText.text = Selected != null ? TitleOf(Selected) : "-";
    }
}

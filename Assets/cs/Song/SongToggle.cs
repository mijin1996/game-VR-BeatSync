using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class SongToggle : MonoBehaviour {
    [SerializeField] private SongData _song;
    [SerializeField] private TMP_Text _label;

    private Toggle _toggle;

    public SongData Song => _song;
    public Toggle Toggle => _toggle != null ? _toggle : (_toggle = GetComponent<Toggle>());

    void Awake() {
        if (_label != null && _song != null) {
            _label.text = _song.Title;
        }
    }
}

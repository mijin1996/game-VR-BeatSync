using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameButtons : MonoBehaviour {
    [SerializeField] private InterfaceReference<IGameFlow> _game;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _homeButton;

    void Awake() {
        if (_startButton != null) {
            _startButton.onClick.AddListener(() => _game.Value.StartGame());
        }
        if (_restartButton != null) {
            _restartButton.onClick.AddListener(() => _game.Value.Restart());
        }
        if (_homeButton != null) {
            _homeButton.onClick.AddListener(() => _game.Value.GoHome());
        }
    }

    void Update() {
        // 테스트용: 키보드 스페이스바 = START 버튼 (대기 상태가 아니면 GM 이 무시함)
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame) {
            _game.Value.StartGame();
        }
    }
}

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [Header("Buttons")]
    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;

    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button moveLeftButton;

    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAlternateButton;
    [SerializeField] private Button pauseButton;

    [SerializeField] private Button gamepadInteractButton;
    [SerializeField] private Button gamepadInteractAlternateButton;
    [SerializeField] private Button gamepadPauseButton;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;

    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI moveLeftText;

    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAlternateText;
    [SerializeField] private TextMeshProUGUI pauseText;

    [SerializeField] private TextMeshProUGUI gamepadInteractText;
    [SerializeField] private TextMeshProUGUI gamepadInteractAlternateText;
    [SerializeField] private TextMeshProUGUI gamepadPauseText;

    [Header("Other")]
    [SerializeField] private Transform pressToRebindKeyTransform;

    private void Awake()
    {
        Instance = this;

        AddButtonListener(soundEffectsButton, () => {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        AddButtonListener(musicButton, () => {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        AddButtonListener(closeButton, () => {
            Hide();
        });

        AddButtonListener(moveUpButton, () => {
            RebindBinding(GameInput.Binding.Move_Up);
        });

        AddButtonListener(moveDownButton, () => {
            RebindBinding(GameInput.Binding.Move_Down);
        });

        AddButtonListener(moveLeftButton, () => {
            RebindBinding(GameInput.Binding.Move_Left);
        });

        AddButtonListener(moveRightButton, () => {
            RebindBinding(GameInput.Binding.Move_Right);
        });

        AddButtonListener(interactButton, () => {
            RebindBinding(GameInput.Binding.Interact);
        });

        AddButtonListener(interactAlternateButton, () => {
            RebindBinding(GameInput.Binding.InteractAlternate);
        });

        AddButtonListener(pauseButton, () => {
            RebindBinding(GameInput.Binding.Pause);
        });

        AddButtonListener(gamepadInteractButton, () => {
            RebindBinding(GameInput.Binding.Gamepad_Interact);
        });

        AddButtonListener(gamepadInteractAlternateButton, () => {
            RebindBinding(GameInput.Binding.Gamepad_InteractAlternate);
        });

        AddButtonListener(gamepadPauseButton, () => {
            RebindBinding(GameInput.Binding.Gamepad_Pause);
        });
    }

    private void Start()
    {
        if (KitchenGameManager.Instance != null)
        {
            KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;
        }

        UpdateVisual();
        HidePressToRebindKey();
        Hide();
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        if (soundEffectsText != null)
            soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);

        if (musicText != null)
            musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        if (moveUpText != null)
            moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);

        if (moveDownText != null)
            moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);

        if (moveLeftText != null)
            moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);

        if (moveRightText != null)
            moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);

        if (interactText != null)
            interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);

        if (interactAlternateText != null)
            interactAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);

        if (pauseText != null)
            pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);

        if (gamepadInteractText != null)
            gamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);

        if (gamepadInteractAlternateText != null)
            gamepadInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);

        if (gamepadPauseText != null)
            gamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);
    }

    public void Show()
    {
        gameObject.SetActive(true);

        soundEffectsButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindKey()
    {
        if (pressToRebindKeyTransform != null)
        {
            pressToRebindKeyTransform.gameObject.SetActive(true);
        }
    }

    private void HidePressToRebindKey()
    {
        if (pressToRebindKeyTransform != null)
        {
            pressToRebindKeyTransform.gameObject.SetActive(false);
        }
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKey();

        GameInput.Instance.RebindBinding(binding, () => {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }

    private void AddButtonListener(Button button, Action action)
    {
        if (button != null)
        {
            button.onClick.AddListener(() => action());
        }
        else
        {
            Debug.LogWarning("Ada Button yang belum di-assign di Inspector!", this);
        }
    }
}
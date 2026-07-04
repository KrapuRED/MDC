using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class CustomButtonUI : MonoBehaviour, IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerClickHandler
{
    [Header("Visual State Colors")]
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color hoverColor = new Color(0.9f, 0.9f, 0.9f);
    [SerializeField] private Color pressedColor = new Color(0.75f, 0.75f, 0.75f);
    [SerializeField] private Color disabledColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);

    [Header("Behaviour")]
    [SerializeField] private bool isInteractable = true;

    private Image _targetImage;

    public bool IsHovering { get; private set; }
    public bool IsPressed { get; private set; }
    public bool IsInteractable
    {
        get => isInteractable;
        set
        {
            isInteractable = value;
            RefreshVisualState();
        }
    }

    // Subscribe to these from other scripts instead of hardcoding logic here
    public UnityEvent OnHoverEnter;
    public UnityEvent OnHoverExit;
    public UnityEvent OnPressed;
    public UnityEvent OnClicked;

    private void Awake()
    {
        _targetImage = GetComponent<Image>();
        RefreshVisualState();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isInteractable) return;

        IsHovering = true;
        RefreshVisualState();
        OnHoverEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isInteractable) return;

        IsHovering = false;
        IsPressed = false; // dragging off the button while held cancels the press visual
        RefreshVisualState();
        OnHoverExit?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isInteractable) return;

        IsPressed = true;
        RefreshVisualState();
        OnPressed?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isInteractable) return;

        OnClicked?.Invoke();
    }

    private void RefreshVisualState()
    {
        if (_targetImage == null) return;

        if (!isInteractable)
        {
            _targetImage.color = disabledColor;
        }
        else if (IsPressed)
        {
            _targetImage.color = pressedColor;
        }
        else if (IsHovering)
        {
            _targetImage.color = hoverColor;
        }
        else
        {
            _targetImage.color = normalColor;
        }
    }
}

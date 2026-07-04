using UnityEngine;
using UnityEngine.InputSystem;

public class CursorController : MonoBehaviour
{
    
    [SerializeField] private bool isDragging = false;
    [SerializeField] private DataFile hoveredObject2D;
    [SerializeField] private DataFile draggedObject2D;
    private Vector2 dragOffset2D;

    [Header("Detection Config")]
    [SerializeField] private float grabRadius = 0.6f;

    private IInteractable _hoveredInteractable;
    private GamePlayInput _gamePlayInput;
    private Camera mainCam;

    private void Awake()
    {
        _gamePlayInput = new GamePlayInput();
        Cursor.lockState = CursorLockMode.None;

        mainCam = Camera.main;
    }

    private void OnEnable()
    {
        _gamePlayInput.Enable();
    }

    private void OnDisable()
    {
        _gamePlayInput.Disable();
    }

    private void Start()
    {
        // Daftarkan event Click (Gunakan started atau performed tergantung konfigurasi action-mu)
        _gamePlayInput.Mouse.Click.performed += _ => OnClick();

        _gamePlayInput.Mouse.Drag.started += _ => OnStartDrag();
        _gamePlayInput.Mouse.Drag.canceled += _ => OnEndDrag();
    }

    private void LateUpdate()
    {
        if (isDragging && draggedObject2D == null)
        {
            isDragging = false;
        }

        if (!isDragging)
            OnDetechObject();

        if (isDragging && draggedObject2D != null)
        {
            Vector2 mouseWorldPos = GetMouseWorldPosition2D();
            draggedObject2D.transform.position = mouseWorldPos + dragOffset2D;
        }
    }

    private Vector2 GetMouseWorldPosition2D()
    {
        Vector2 screenPos = _gamePlayInput.Mouse.Position.ReadValue<Vector2>();
        return mainCam.ScreenToWorldPoint(screenPos);
    }

    private void OnDetechObject()
    {
        Vector2 mouseWorldPos = GetMouseWorldPosition2D();
        Collider2D[] hits = Physics2D.OverlapCircleAll(mouseWorldPos, grabRadius);

        DataFile closestData = null;
        float closestDataDistance = grabRadius;

        IInteractable closestInteractable = null;
        float closestInteractableDistance = grabRadius;

        foreach (Collider2D hit in hits)
        {
            float distance = Vector2.Distance(mouseWorldPos, hit.transform.position);

            // Deteksi DataFile terdekat
            if (hit.TryGetComponent(out DataFile dataFile) && distance < closestDataDistance)
            {
                closestDataDistance = distance;
                closestData = dataFile;
            }

            // Deteksi IInteractable terdekat
            if (hit.TryGetComponent(out IInteractable interactable) && distance < closestInteractableDistance)
            {
                closestInteractableDistance = distance;
                closestInteractable = interactable;
            }
        }

        hoveredObject2D = closestData;
        _hoveredInteractable = closestInteractable;
    }

    private void StartClick()
    {

    }

    private void OnClick()
    {
        // Jika sedang nge-drag objek, batalkan interaksi klik tombol/objek lain
        if (isDragging) return;

        if (_hoveredInteractable != null)
        {
            Debug.Log($"Mengklik & Berinteraksi dengan objek!");
            _hoveredInteractable.OnInteract(); // Panggil fungsi interaksinya
        }
    }

    private void OnStartDrag()
    {
        if (hoveredObject2D == null) return;

        if (hoveredObject2D.TryGetComponent(out DataFile dataFile))
        {
            draggedObject2D = dataFile;
            draggedObject2D.OnDrag();
            isDragging = true;
        }
    }

    private void OnEndDrag()
    {
        if (draggedObject2D != null)
        {
            Vector2 finalDropPos = GetMouseWorldPosition2D();
            draggedObject2D.OnDrop(finalDropPos);
        }

        isDragging = false;
        draggedObject2D = null;
    }
}

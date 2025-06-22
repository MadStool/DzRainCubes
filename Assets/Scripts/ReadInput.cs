using UnityEngine;

public class ReadingInput : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private CubeInteraction _interaction;

    private void Awake()
    {
        if (_mainCamera == null)
            _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            HandleClick();
    }

    private void HandleClick()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.TryGetComponent(out ExplodableCube cube))
        {
            _interaction.HandleCubeClick(cube);
        }
    }
}
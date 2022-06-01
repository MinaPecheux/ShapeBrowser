using UnityEngine;
using UnityEngine.EventSystems;

public class GeometriesManager : MonoBehaviour
{
    [SerializeField] private Shader _shader;
    [SerializeField] private Texture[] _textures;

    private Vector2 _startPosition;
    private Vector2 _move;

    private static Color[] _COLORS = new Color[]
    {
        Color.white,
        new Color(1f, 0.4f, 0.4f),
        new Color(0.4f, 0.4f, 1f),
    };

    private Material _material;

    private void Awake()
    {
        _material = new Material(_shader);

        foreach (Transform child in transform)
            child.gameObject.GetComponent<MeshRenderer>().material = _material;

        SetGeometry("Cube");
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            _startPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            _move = ((Vector2) Input.mousePosition - _startPosition).normalized;
            transform.Rotate(new Vector3(0f, -_move.x, 0f));
        }
#else
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                _startPosition = t.position;
            }
            else if (t.phase == TouchPhase.Moved)
            {
                _move = (t.position - _startPosition).normalized;
                transform.Rotate(new Vector3(0f, -_move.x * 3f, 0f));
            }
        }
#endif
    }

    public void SetColor(int index)
    {
        _material.color = _COLORS[index];
    }

    public void SetTexture(int index)
    {
        _material.mainTexture = _textures[index];
    }

    public void SetGeometry(string geometry)
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(child.name == geometry);
    }
}

using UnityEngine;

public class CameraController2D : MonoBehaviour
{
    [Header("Hintergrund-Referenz")]
    public SpriteRenderer backgroundRenderer; // Ziehe hier dein Hintergrund-Sprite per Drag-and-Drop rein

    private Camera cam;
    private Vector3 dragOrigin;

    private float minX, maxX, minY, maxY;

    void Start()
    {
        cam = GetComponent<Camera>();
        UpdateBounds();
    }

    private int count = 0;
    void Update()
    {
        // Falls sich der Hintergrund während des Spiels bewegt oder skaliert,
        // werden die Grenzen hier kontinuierlich angepasst.
        if (backgroundRenderer != null)
        {
            UpdateBounds();
        }
        if (count%60== 0)
        {
            print(this.transform.position);
        }
        count++;
        //HandlePan();
        ClampCamera();
    }

    void UpdateBounds()
    {
        if (backgroundRenderer == null) return;

        // Holt sich die exakten Welt-Koordinaten der Ränder des Hintergrund-Sprites
        Bounds bgBounds = backgroundRenderer.bounds;
        minX = bgBounds.min.x;
        maxX = bgBounds.max.x;
        minY = bgBounds.min.y;
        maxY = bgBounds.max.y;
    }

    void HandlePan()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            transform.position += difference;
        }
    }

    void ClampCamera()
    {
        float verticalExtent = cam.orthographicSize;
        float horizontalExtent = verticalExtent * Screen.width / (float)Screen.height;

        // Begrenzt die Kamera basierend auf den dynamischen Hintergrund-Grenzen
        float clampedX = Mathf.Clamp(transform.position.x, minX + horizontalExtent, maxX - horizontalExtent);
        float clampedY = Mathf.Clamp(transform.position.y, minY + verticalExtent, maxY - verticalExtent);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}

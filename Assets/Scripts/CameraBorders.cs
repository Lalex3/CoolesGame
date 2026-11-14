using UnityEngine;


public class CameraBorder : MonoBehaviour
{
    public Camera cam;
    public SpriteRenderer map;
    public float maxScale = 3f;   // wie weit man maximal reinzoomen darf


    //Befor the Script is Running the mainCamera is feched if no other Camera is put in the Inspector
    void Awake()
    {
        if (cam == null) cam = Camera.main;
    }

    void LateUpdate()
    {
        float camH = cam.orthographicSize * 2f;
        float camW = camH * cam.aspect;

        // 1) Zoom begrenzen: das Bild darf nie kleiner als der sichtbare Bereich werden
        Vector2 spriteSize = map.sprite.bounds.size;   // Größe bei Scale 1
        float minScale = Mathf.Max(camW / spriteSize.x, camH / spriteSize.y);
        float s = Mathf.Clamp(transform.localScale.x, minScale, Mathf.Max(minScale, maxScale));
        transform.localScale = new Vector3(s, s, 1f);

        // 2) Position begrenzen: die Bildränder dürfen nicht in die Kamera rutschen
        float limitX = spriteSize.x * s * 0.5f - camW * 0.5f;
        float limitY = spriteSize.y * s * 0.5f - camH * 0.5f;
        Vector3 c = cam.transform.position;
        Vector3 p = transform.position;
        p.x = Mathf.Clamp(p.x, c.x - limitX, c.x + limitX);
        p.y = Mathf.Clamp(p.y, c.y - limitY, c.y + limitY);
        transform.position = p;
    }
}

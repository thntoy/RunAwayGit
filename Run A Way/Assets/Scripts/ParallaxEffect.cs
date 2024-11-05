using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private Vector3 _startPosition;
    private Camera _cam;

    // The factor by which the object's movement is multiplied
    public Vector3 ParallaxMultiplier = new Vector3(0.5f, 0f, 0f);

    void Start()
    {
        _startPosition = transform.position;
        _cam = Camera.main;
    }

    void Update()
    {
        Vector3 deltaMovement = Vector3.Scale(_cam.transform.position, ParallaxMultiplier);
        transform.position = _startPosition + deltaMovement;
    }
}

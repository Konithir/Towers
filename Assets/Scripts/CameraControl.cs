using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private float _lastTime;
    private const float SPEED = 7f;

    private static CameraControl _singleton;
    public static CameraControl Get()
    {
        return _singleton;
    }

    private void Awake()
    {
        _singleton = this;
    }

    void Update()
    {

        if (Input.GetKey("d"))
        {
            transform.position = new Vector3(transform.position.x + SPEED * (Time.time - _lastTime), transform.position.y, transform.position.z);
        }
        if (Input.GetKey("a"))
        {
            transform.position = new Vector3(transform.position.x - SPEED * (Time.time - _lastTime), transform.position.y, transform.position.z);
        }
        if (Input.GetKey("w"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + SPEED * (Time.time - _lastTime), transform.position.z);
        }
        if (Input.GetKey("s"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - SPEED * (Time.time - _lastTime), transform.position.z);
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y , transform.position.z + 15f * SPEED * (Time.time - _lastTime));
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y , transform.position.z - 15f * SPEED * (Time.time - _lastTime));
        }

        _lastTime = Time.time;
    }
}

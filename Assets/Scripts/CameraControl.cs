using UnityEngine;

public class CameraControl : MonoBehaviour
{

    float LastTime;
    const float Speed = 7f;

    private static CameraControl singleton;
    public static CameraControl Get()
    {
        return singleton;
    }

    private void Awake()
    {
        singleton = this;
    }

    void Update()
    {

        if (Input.GetKey("d"))
        {
            transform.position = new Vector3(transform.position.x + Speed * (Time.time - LastTime), transform.position.y, transform.position.z);
        }
        if (Input.GetKey("a"))
        {
            transform.position = new Vector3(transform.position.x - Speed * (Time.time - LastTime), transform.position.y, transform.position.z);
        }
        if (Input.GetKey("w"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Speed * (Time.time - LastTime), transform.position.z);
        }
        if (Input.GetKey("s"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - Speed * (Time.time - LastTime), transform.position.z);
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y , transform.position.z + 15f * Speed * (Time.time - LastTime));
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y , transform.position.z - 15f * Speed * (Time.time - LastTime));
        }

        LastTime = Time.time;
    }
}

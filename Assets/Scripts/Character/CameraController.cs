using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0F;

    [SerializeField]
    private Transform target;

    private void Awake()
    {
        if (!target) target = FindObjectOfType<Character>().transform;
    }

    private void Update()
    {
        //var HitBox = GameObject.Find("HitBox");
        //HitBox.transform.position = new Vector3(HitBox.transform.position.x, HitBox.transform.position.y, 200f);
        Vector3 position = target.position;         
        position.z = -10.0F;
        transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
    }
}

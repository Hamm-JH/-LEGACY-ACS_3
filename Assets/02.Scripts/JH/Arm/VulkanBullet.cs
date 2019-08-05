using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulkanBullet : MonoBehaviour
{
    private Rigidbody rigBody;

    private void Awake()
    {
        rigBody = GetComponent<Rigidbody>();
        Destroy(gameObject, 1.5f);
    }

    // Start is called before the first frame update
    void Start()
    {
        rigBody.AddRelativeForce(Vector3.forward * 240000f, ForceMode.Force);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

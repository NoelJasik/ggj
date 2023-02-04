using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.localPosition - PlayerMovement.PlayerTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.localPosition = PlayerMovement.PlayerTransform.localPosition + offset;
    }
}

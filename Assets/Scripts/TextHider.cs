using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHider : MonoBehaviour
{
    [SerializeField] MeshRenderer mashrender;
    
    void Start()
    { 
        GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space) == true)
        mashrender.enabled = false; 
        
        mashrender.enabled = true;
    }
}

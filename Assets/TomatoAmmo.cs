using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TomatoAmmo : MonoBehaviour
{
    public float lifeDuration = 0;
    public float maxLifeDuration = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        lifeDuration += Time.fixedDeltaTime;
        if(lifeDuration > maxLifeDuration)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            if(other.gameObject.CompareTag("Bot"))
            {
                
            }
            else
            {
                
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Bot"))
        {
            
        }

        else
        {
            
        }
    }
}

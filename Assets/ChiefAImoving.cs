using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChiefAImoving : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject playerGO;

    // Start is called before the first frame update

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = playerGO.transform.position;
    }
}

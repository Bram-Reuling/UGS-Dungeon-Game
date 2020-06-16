using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Movement speed of the Enemy")]
    [Range(0f, 10f)]
    public int moveSpeed;

    [Header("Player Detection")]
    [Tooltip("The minimum range a player needs to be in before it gets 'detected' by the 'AI'.")]
    public float maxLength;

    private GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 _diff = transform.position - player.transform.position;
        float length = _diff.magnitude;

        if (length <= maxLength)
        {
            transform.LookAt(player.transform.position);

            Vector3 _normalDiff = _diff.normalized;

            transform.position -= _normalDiff * Time.deltaTime * moveSpeed;
        }
    }
}

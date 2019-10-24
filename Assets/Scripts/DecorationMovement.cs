using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Vector3 _vectorDirection;

    public void SetVectorMovement(Vector2 direction)
    {
        _vectorDirection = new Vector3(direction.x, 0, direction.y);
        transform.LookAt(transform.position + _vectorDirection * -1);
    }

    void Update()
    {
        //transform.position = 
        transform.position = Vector3.MoveTowards(transform.position, transform.position + _vectorDirection, _speed * Time.deltaTime);
        //transform.Translate(_speed * _vectorDirection * Time.deltaTime);
    }
}

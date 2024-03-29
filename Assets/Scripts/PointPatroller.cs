﻿using System;
using UnityEngine;

public class PointPatroller : MonoBehaviour
{
    [SerializeField] private Transform[] _patrollingPoints;
    [SerializeField] private float _patrolDelay;
    [SerializeField] private float _step;
    [SerializeField] private LayerMask _obstacleMask;
    
    private float _timer = 0;
    [SerializeField] private Player player;
    private int _currentPatrolPointIndex = 0;

    private void Awake()
    {
        _timer = _patrolDelay;
    }

    private void Update()
    {
        PatrolTimerTick();
        MoveToPoint(_currentPatrolPointIndex);
    }

    private void PatrolTimerTick()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            int nextPointIndex = _currentPatrolPointIndex + 1;

            if (nextPointIndex >= _patrollingPoints.Length)
                nextPointIndex = 0;

            _currentPatrolPointIndex = nextPointIndex;
            _timer = _patrolDelay;
        }
    }

    private void MoveToPoint(int pointIndex)
    {
        transform.position = new Vector3(_patrollingPoints[pointIndex].position.x, transform.position.y, _patrollingPoints[pointIndex].position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            player.Kill();
            Debug.Log("Player was killed");
        }
            
            
            
    }
}

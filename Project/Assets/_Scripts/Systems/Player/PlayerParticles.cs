using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Managers;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    public static Action<Vector3, ResourceType> OnBulletHit;
    private ParticleSystem _particleSystem;

    private void OnEnable()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        OnBulletHit += FireParticles;
    }
    
    private void OnDisable()
    {
        OnBulletHit -= FireParticles;
    }

    void FireParticles(Vector3 position, ResourceType resourceType)
    {
        //set fire position
        var shape = _particleSystem.shape;
        var rotation = Quaternion.AngleAxis(transform.parent.rotation.eulerAngles.z, Vector3.back);
        shape.position = rotation * (position - transform.position);

        //set color
        var main = _particleSystem.main;
        switch (resourceType)
        {
            case ResourceType.Blue:
                main.startColor = new ParticleSystem.MinMaxGradient(Color.blue);
                break;
            case ResourceType.Red:
                main.startColor = new ParticleSystem.MinMaxGradient(Color.red);
                break;
            case ResourceType.Green:
                main.startColor = new ParticleSystem.MinMaxGradient(Color.green);
                break;
        }
        
        //fire
        _particleSystem.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFireBallModifier : BallModifier
{
    [Header("Fire Ball Stats")]
    [SerializeField] private Color ballColour;
    [SerializeField] private Color[] trailColour = new Color[2];
    private void Start()
    {
        GetComponentInParent<TrailRenderer>().startColor = trailColour[0];
        GetComponentInParent<TrailRenderer>().endColor = trailColour[1];
        myBall.GetComponent<SpriteRenderer>().color = ballColour;
        GetComponent<ParticleSystem>().Play();
    }
    private void Update()
    {
        this.gameObject.transform.position = new Vector3(0, 0, 0);
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        Vector3 targetPosition = myBall.transform.position;
        var shape = particleSystem.shape;
        shape.position = targetPosition;
    }
}

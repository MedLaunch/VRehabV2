using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourLiquid : MonoBehaviour
{
    ParticleSystem part;
    private int wineScore;
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        //part.transform.rotation = this.transform.rotation;
        part.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        //part.transform.rotation = this.transform.rotation;
        bool drip = false;
        bool oldDrip = false;
        //if(Mathf.Abs(this.transform.rotation.z) >= 90 || Mathf.Abs(part.transform.rotation.x) >= 90)
        if (Vector3.Dot(transform.up, Vector3.down) < 0)
        {
            drip = true;
        }
        else
        {
            drip = false;
        }
        if (oldDrip != drip)
        {
            SetDrip(drip);
            oldDrip = drip;
        }
    }
    void FixedUpdate()
    {
        ParticleSystem.Particle[] particleList = new ParticleSystem.Particle[part.particleCount];
        int length = part.GetParticles(particleList);

    }
    void SetDrip(bool drip)
    {
        if (drip)
        {
            part.Play();
        }
        else
        {
            part.Stop();
        }
    }
}

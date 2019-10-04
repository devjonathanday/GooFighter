using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplatParticle : MonoBehaviour
{
    [System.Serializable]
    public class SplashTracker
    {
        public GameObject CurrentObject;
        public float LifeTimer;

        public SplashTracker(GameObject _Current, float _LifeTimer)
        {
            CurrentObject = _Current;
            LifeTimer = _LifeTimer;
        }
        public void UpdateSplash()
        {
            LifeTimer -= Time.deltaTime;
        }
        public void DeAllocate()
        {
            Destroy(CurrentObject);
        }
    }

    //Object to be placed into scene to show where splash had landed
    public GameObject SplashObjectPrefab;
    //Life time for the splashes
    public float SplashLifeTime;

    //All the current splashes in the game
    public List<SplashTracker> Splashes = new List<SplashTracker>();

    //Current Particle System
    ParticleSystem Particle;
    //Current list of Collision events from Particle system
    List<ParticleCollisionEvent> CollisionEvents = new List<ParticleCollisionEvent>();

    void Start()
    {
        //Set the Current gameobjects particlesystem
        Particle = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        //Itterate through each splash
        for(int i = 0; i < Splashes.Count; i++)
        {
            //Update each splash
            Splashes[i].UpdateSplash();
            //If the splash is out of time
            if(Splashes[i].LifeTimer <= 0)
            {
                Splashes[i].DeAllocate();
                //Remove the splash
                Splashes.RemoveAt(i);
            }
        }
    }
    void OnParticleCollision(GameObject other)
    {
        //Sets the CollisionEvents to the Particles collision event, And gets the cound from it 
        int CollisionEventCount = Particle.GetCollisionEvents(other, CollisionEvents);

        //Gets the Rigidbody of the object collided with
        Rigidbody RB = other.GetComponent<Rigidbody>();
        //Checks to see if the rigidbody exists
        if(RB != null)
        {
            for(int i = 0; i < CollisionEventCount; i++)
            {
                //Gets the intersection point of where the collider collided
                Vector3 IntersectionPoint = CollisionEvents[i].intersection;
                Splashes.Add(new SplashTracker(Instantiate(SplashObjectPrefab, IntersectionPoint,SplashObjectPrefab.transform.rotation, other.transform), SplashLifeTime));
            }
        }
    }


    //TODO::Create a function that takes in the Velocity,Color, and Position (Hit point) and play the burst of animation
}

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

        public SplashTracker(GameObject _Current, float _LifeTimer, Color _Color)
        {
            CurrentObject = _Current;
            CurrentObject.GetComponent<Renderer>().material.color = _Color;
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

    //Particles to be Emmited
    public int AmountOfGoo = 20;

    //Current Particle System
    ParticleSystem Particle;
    //Current list of Collision events from Particle system
    //List<ParticleCollisionEvent> CollisionEvents = new List<ParticleCollisionEvent>();
    List<ParticleSystem.Particle> ParticlesHaveEntered = new List<ParticleSystem.Particle>(); 
    //Normal Particles
    ParticleSystem OtherParticles;

    void Start()
    {
        //Set the Current gameobjects particlesystem
        Particle = GetComponent<ParticleSystem>();
        //Set the OtherParticles to the Normal Particle System
        OtherParticles = transform.parent.GetChild(0).GetComponent<ParticleSystem>();
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

        if(Input.GetKeyDown(KeyCode.V))
        {
            DisplayHitParticle(new Vector3(-1, 2, -1), Color.red, new Vector3(1, 1, -1));
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            DisplayHitParticle(new Vector3(0, 2, 4), Color.red, new Vector3(1, 1, -1));
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            DisplayHitParticle(new Vector3(4, 2, 2), Color.red, new Vector3(1, 1, -1));
        }
    }
    private void OnParticleTrigger()
    {
        //Get the count of and sets the Particles that have entered the space
        int ParticlesEnterCount = Particle.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, ParticlesHaveEntered);

        //Goes through all particles that have been triggered
        for (int i = 0; i < ParticlesEnterCount; i++)
        {
            //Gets the position of where they should be
            Vector3 NewPosition = new Vector3(ParticlesHaveEntered[i].position.x, 0.1f, ParticlesHaveEntered[i].position.z);
            //Creates the splash
            Splashes.Add(new SplashTracker(Instantiate(SplashObjectPrefab, NewPosition, SplashObjectPrefab.transform.rotation), SplashLifeTime, ParticlesHaveEntered[i].startColor));
        }
    }


    public void DisplayHitParticle(Vector3 _Velocity, Color _Color, Vector3 _Position)
    {
        //Changes the position of where the particles should spawn
        transform.parent.position = _Position;

        //Set the start color of the Paricles
        OtherParticles.startColor = Particle.startColor = _Color;

        //Emit both particle effects (Normal Particles and Collision particle)
        //Using Emit, as I want Particles to spawn RIGHT NOW rather than when Play wants them to
        Particle.Emit(1);
        
        OtherParticles.Emit(AmountOfGoo);

        //Sets the velocity of the particles
        SetParticleVelocity(_Velocity, Particle, true);
        SetParticleVelocity(_Velocity, OtherParticles, false);
    }

    void SetParticleVelocity(Vector3 _Velocity, ParticleSystem _ParticleSystem, bool _CollisionParticle)
    {
        //Create new array of particles the size of the old array
        ParticleSystem.Particle[] Particles = new ParticleSystem.Particle[_ParticleSystem.particleCount];
        //Set the new array equal to the new array
        _ParticleSystem.GetParticles(Particles);

        if (_CollisionParticle)
        {
            //Set Last Added Particle to correct velocity
            Particles[Particles.Length - 1].velocity = _Velocity;
        }
        else
        {
            //Set Last AmountOfGoo particles to the correct Velocity
            //Move through the new array
            for (int i = Particles.Length - 1; i > (Particles.Length - AmountOfGoo) - 1; i--)
            {
                //Set each Particles velocity to the Desired Velocity
                Particles[i].velocity = _Velocity;
            }
        }

        //Set the old particle array to the new one
        _ParticleSystem.SetParticles(Particles);
    }
}

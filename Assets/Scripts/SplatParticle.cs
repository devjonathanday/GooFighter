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
        Renderer CurrentMaterial;

        public SplashTracker(GameObject _Current, float _LifeTimer, Color _Color)
        {
            CurrentObject = _Current;
            CurrentMaterial = CurrentObject.GetComponent<Renderer>();
            CurrentMaterial.material.color = _Color;
            LifeTimer = _LifeTimer;
        }
        public void SetColor(Color _Color)
        {
            CurrentMaterial.material.color = _Color;
        }
        public void UpdateSplash(bool _Fade)
        {
            LifeTimer -= Time.deltaTime;
            if (_Fade) CurrentMaterial.material.color = new Color(CurrentMaterial.material.color.r, CurrentMaterial.material.color.g, CurrentMaterial.material.color.b, Mathf.Clamp(LifeTimer, 0, 1));
        }
        public void DeAllocate(List<SplashTracker> _Recycle)
        {
            _Recycle.Add(this);
            CurrentObject.SetActive(false);
        }
    }

    //Y Offset
    public float YOffset = .1f;

    //Object to be placed into scene to show where splash had landed
    public GameObject SplashObjectPrefab;
    //The holder of all the splashes
    public GameObject GooHolder;
    //Life time for the splashes
    public float SplashLifeTime;

    //All the current splashes in the game
    List<SplashTracker> Splashes = new List<SplashTracker>();

    //All the Recycled Splashes
    List<SplashTracker> RecycledSplashes = new List<SplashTracker>();

    //Particles to be Emmited
    public int AmountOfGoo = 20;

    //Fades GooSplash out of existance
    public bool FadeSplash = false;

    //Current Particle System
    ParticleSystem Particle;
    //Current list of Particles that have entered the trigger with the ground
    List<ParticleSystem.Particle> ParticlesHaveEntered = new List<ParticleSystem.Particle>();


    void Start()
    {
        //Set the Current gameobjects particlesystem
        Particle = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        //Itterate through each splash
        for (int i = 0; i < Splashes.Count; i++)
        {
            //Update each splash
            Splashes[i].UpdateSplash(FadeSplash);
            //If the splash is out of time
            if (Splashes[i].LifeTimer <= 0)
            {
                Splashes[i].DeAllocate(RecycledSplashes);
                //Remove the splash
                Splashes.RemoveAt(i);
            }
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
            Vector3 NewPosition = new Vector3(ParticlesHaveEntered[i].position.x, YOffset, ParticlesHaveEntered[i].position.z);

            if (RecycledSplashes.Count > 0)
            {
                //Get the Particle
                SplashTracker RecycledSplash = RecycledSplashes[0];
                //Remove Particle from the Recycled List
                RecycledSplashes.RemoveAt(0);

                //Set the position of the Recycled Particles
                RecycledSplash.CurrentObject.transform.position = NewPosition;

                //Sets the correct color
                RecycledSplash.SetColor(ParticlesHaveEntered[i].startColor);

                //Sets the timer back to max
                RecycledSplash.LifeTimer = SplashLifeTime;

                //Dumby you forgot to enable to object
                RecycledSplash.CurrentObject.SetActive(true);

                //Adds the recycled Splashes to the Splash List
                Splashes.Add(RecycledSplash);
            }
            else
            {
                //Creates the splash
                Splashes.Add(new SplashTracker(Instantiate(SplashObjectPrefab, NewPosition, SplashObjectPrefab.transform.rotation, GooHolder.transform), SplashLifeTime, ParticlesHaveEntered[i].startColor));
            }
        }
    }


    public void DisplayHitParticle(Vector3 _Velocity, Color _Color, Vector3 _Position)
    {
        //Changes the position of where the particles should spawn
        transform.parent.position = _Position;

        //I know this is obsolete, Don't worry about it
        //Set the start color of the Paricles
#pragma warning disable 0618
        Particle.startColor = _Color;
#pragma warning restore 0618

        //Emit both particle effects (Normal Particles and Collision particle)
        //Using Emit, as I want Particles to spawn RIGHT NOW rather than when Play wants them to
        Particle.Emit(AmountOfGoo);

        //Sets the velocity of the particles
        SetParticleVelocity(_Velocity, Particle);
    }

    void SetParticleVelocity(Vector3 _Velocity, ParticleSystem _ParticleSystem)
    {
        //Create new array of particles the size of the old array
        ParticleSystem.Particle[] Particles = new ParticleSystem.Particle[_ParticleSystem.particleCount];
        //Set the new array equal to the new array
        _ParticleSystem.GetParticles(Particles);

        //Set Last Added Particle to correct velocity
        Particles[Particles.Length - 1].velocity = _Velocity;

        //Set the old particle array to the new one
        _ParticleSystem.SetParticles(Particles);
    }
}

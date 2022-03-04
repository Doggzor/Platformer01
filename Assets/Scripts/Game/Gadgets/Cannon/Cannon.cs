using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dungeon
{
    public class Cannon : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField, Min(0f), Tooltip("Time in seconds before the first shot")]
        private float delay;
        [SerializeField, Min(1.0f), Tooltip("Time in seconds between shots")]
        private float fireRate = 1.0f;
        [Header("Components")]
        [SerializeField]
        private Expirable projectile;
        [SerializeField]
        private SpriteRenderer bodyLightsRed;
        [SerializeField]
        protected Transform turretController;
        [SerializeField]
        private Transform turretLightsPivot;
        private Animation anim;
        private ParticleSystem fireParticles;

        private float timer;
        private float bodyLightsChargeUpStartTime;
        private float bodyLightsMultiplier;
        private float turretLightChargeUpStartTime;
        private float turretLightMultiplier;
        private Vector3 turretLightZeroScale = new Vector3(1.0f, 0.0f, 1.0f);
        private void Awake()
        {
            anim = GetComponent<Animation>();
            fireParticles = turretController.GetComponent<ParticleSystem>();

            timer = delay;
            bodyLightsChargeUpStartTime = fireRate * 0.6f;
            bodyLightsMultiplier = 1.0f / bodyLightsChargeUpStartTime;
            turretLightChargeUpStartTime = fireRate * 0.2f;
            turretLightMultiplier = 1.0f / turretLightChargeUpStartTime;
        }
        private void Start()
        {
            
        }
        private void Update()
        {
            timer -= Time.deltaTime;
            ChargeUp();
            if (timer <= 0.1f)
            {
                anim.Play();
                if (timer <= 0) Fire();
            }
        }

        private void Fire()
        {
            timer = fireRate;
            fireParticles.Play();
            Instantiate(projectile, transform.position, Quaternion.Euler(0f, 0f, turretController.transform.rotation.eulerAngles.z));
        }
        private void ChargeUp()
        {
            if (timer <= bodyLightsChargeUpStartTime)
            {                  
                bodyLightsRed.SetColorAlpha((bodyLightsChargeUpStartTime - timer) * bodyLightsMultiplier);
                if (timer <= turretLightChargeUpStartTime)
                    turretLightsPivot.localScale = (turretLightChargeUpStartTime - timer) * turretLightMultiplier * Vector3.up + turretLightZeroScale;
            }
            else ResetLights();
        }

        private void ResetLights()
        {
            turretLightsPivot.localScale = turretLightZeroScale;
            bodyLightsRed.SetColorAlpha(0f);
        }

        public void RotateTurret(float angle)
        {
            turretController.transform.Rotate(Vector3.forward * angle);
        }
    }
}

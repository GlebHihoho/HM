using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class Grenade : MonoBehaviour
    {
        [SerializeField] private float delay = 5f;
        [SerializeField] private float radius = 5f;
        [SerializeField] private float force = 300f;
        [SerializeField] private float damage = 110f;
        [SerializeField] private GameObject explosionEffect;

        private float countdown;
        private bool isExploded = false;

        void Start()
        {
            countdown = delay;
        }

        void Update()
        {
            countdown -= Time.deltaTime;

            if (countdown <= 0f && !isExploded)
            {
                
                Explode();
                isExploded = true;
            }
        }

        void Explode()
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);

            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider collider in colliders)
            {
                DestructibleObject destructible = collider.GetComponent<DestructibleObject>();

                print(destructible);
                if (destructible != null) 
                {
                    destructible.ReceiveDamage(damage);
                }

                Rigidbody rd = collider.GetComponent<Rigidbody>();

                if (rd != null)
                {
                    rd.AddExplosionForce(force, transform.position, radius);
                }
            }

            Destroy(gameObject);
        }
    }
}

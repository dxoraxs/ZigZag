using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalDestroy : MonoBehaviour
{
    [SerializeField] private Vector3 _rotationSpeed;
    [SerializeField] private GameObject explosionParticlesPrefab;

    private void FixedUpdate()
    {
        transform.Rotate(_rotationSpeed * Time.deltaTime);
    }

    public void OnDestroyCrystal()
    {
        GameObject explosion = Instantiate(explosionParticlesPrefab, transform.position, explosionParticlesPrefab.transform.rotation);
        Destroy(explosion, explosion.GetComponent<ParticleSystem>().main.startLifetimeMultiplier);
        Destroy(gameObject);
    }
}
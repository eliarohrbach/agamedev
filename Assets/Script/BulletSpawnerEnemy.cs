using Unity.VisualScripting;
using UnityEngine;

public class BulletSpawnerEnemy : BulletSpawner
{
    private GameObject player;

    void Start() 
    {
        player = GameObject.FindWithTag("Player");

        // enemies should not start to shoot straight away & all at once
        passedCooldownTime = Time.time + Random.Range(GunCooldown, GunCooldown * 2);
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();

        Vector3 rayOrigin = bulletSpawnPoint.position;
        Vector3 rayDirection = player.transform.position - rayOrigin;
        RaycastHit hit;

        Physics.Raycast(rayOrigin, rayDirection, out hit, Mathf.Infinity);

        if (hit.collider.tag == "Player")
        {
            float rayHitAngle = Vector3.Angle(rayDirection, hit.normal);
            rayHitAngle = hit.transform.position.y < transform.position.y ? 180 - rayHitAngle : 180 + rayHitAngle;
            Quaternion parentRotation = GetComponentInParent<Transform>().rotation;

            // hit.normal is 90� to surface, if enemy position too high/too low bullet angle is falsified because of capsule geometry
            Quaternion bulletSpawnRotation = Quaternion.Euler(rayHitAngle, parentRotation.eulerAngles.y, parentRotation.eulerAngles.z);
            spawnBullet(bulletSpawnPoint.position, bulletSpawnRotation);

            // visual ray for debugging
            Debug.DrawRay(rayOrigin, rayDirection * hit.distance, Color.red);
        } 
    }
}

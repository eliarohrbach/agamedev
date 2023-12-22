using UnityEngine;
using UnityEngine.UI;

public class BulletSpawnerPlayer : BulletSpawner
{
    public Image crosshair;
    private float targetCrosshairRotation = -90f; // turn crosshair clockwise
    private float angleVelocity;

    // Start is called before the first frame update
    void Start()
    {
        angleVelocity = targetCrosshairRotation / GunCooldown;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        
        if (Input.GetMouseButtonDown(0) && Time.time > passedCooldownTime)
        {
            Vector3 bulletSpawnPosition = bulletSpawnPoint.position;
            bulletSpawnPosition += bulletSpawnPoint.forward; // offset bullet from camera, so playermodel is not hit
            spawnBullet(bulletSpawnPosition, bulletSpawnPoint.rotation);
            //gunSound.Play();
        }

        if (Time.time < passedCooldownTime)
        {
            float newCrosshairRotation = crosshair.transform.rotation.eulerAngles.z + (angleVelocity * Time.deltaTime);
            crosshair.transform.rotation = Quaternion.Euler(0, 0, newCrosshairRotation);
        } 
    }
}

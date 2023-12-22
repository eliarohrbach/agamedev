using UnityEngine;

public abstract class BulletSpawner : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public ParticleSystem muzzleFlash;
    public AudioSource gunSound;

    [SerializeField]
    private float gunCooldown = 3f;

    public float GunCooldown { get { return gunCooldown; } protected set { gunCooldown = value; } }
    protected float passedCooldownTime;

    protected void spawnBullet(Vector3 spawnPosition, Quaternion spawnRotation) {
        if (Time.time > passedCooldownTime) 
        {
            muzzleFlash.Play();
            gunSound.Play();
            Instantiate(bulletPrefab, spawnPosition, spawnRotation);
            passedCooldownTime = Time.time + gunCooldown;
        }
    }

    protected void Update() 
    {
        gunSound.pitch = Time.timeScale;
    }
}

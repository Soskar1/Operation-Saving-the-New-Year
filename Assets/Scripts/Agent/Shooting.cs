using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shooting : MonoBehaviour
{
    [HideInInspector] public Game game;

    [Header("Weapon's ID")]
    public int weaponID;

    [Header("Bullet's options")]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float spread;
    [SerializeField] private float launchForce;

    [Header("Delay")]
    public float startTimeBtwShots;
    [HideInInspector] public float timeBtwShots;

    [Header("Ammo")]
    [SerializeField] private bool infiniteAmmo;
    public int maxAmmo;
    public int currentAmmo;
    [SerializeField] private GameObject ammoHUD;
    [SerializeField] private TextMeshProUGUI ammoAmountText;

    [Header("Sounds")]
    [SerializeField] private AudioSource source;
    [SerializeField] private List<AudioClip> clips;

    private void Awake()
    {
        game = FindObjectOfType<Game>();

        if (ammoHUD != null)
        {
            ammoHUD.SetActive(true);
            SetAmmoText();
        }
    }

    void Update()
    {
        if (!game.inPause)
        {
            if (AmmoCheck()) //проверка на количество патрон
            {
                if (timeBtwShots <= 0)
                {
                    if (Input.GetMouseButton(0))
                    {
                        Fire();
                    }
                }
                else
                {
                    timeBtwShots -= Time.deltaTime;
                }
            }
        }
    }

    private void Fire()
    {
        GameObject snowballInstance = Instantiate(bullet, shootPoint.position, Quaternion.identity);
        snowballInstance.transform.Rotate(0, 0, Random.Range(-spread, spread));
        snowballInstance.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;

        int whichSoundToPlay = Random.Range(0, clips.Count);
        source.clip = clips[whichSoundToPlay];
        source.Play();

        currentAmmo--;
        SetAmmoText();

        timeBtwShots = startTimeBtwShots;
    }

    private bool AmmoCheck()
    {
        if (infiniteAmmo || currentAmmo > 0)
        {
            return true;
        }

        return false;
    }

    public void GetAmmo(int ammoAmount)
    {
        currentAmmo += ammoAmount;

        if (currentAmmo > maxAmmo)
        {
            currentAmmo = maxAmmo;
        }

        SetAmmoText();
    }

    public void SetAmmoText()
    {
        ammoAmountText.SetText(currentAmmo + "/" + maxAmmo);
    }
}

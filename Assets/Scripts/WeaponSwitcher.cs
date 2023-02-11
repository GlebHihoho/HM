using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject handgun;
    [SerializeField] private GameObject autoRifle;
    private GameObject activeWeapon;

    // Start is called before the first frame update
    void Start()
    {
        activeWeapon = handgun;
        autoRifle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (activeWeapon == handgun){
                activeWeapon = autoRifle;

                autoRifle.SetActive(true);
                handgun.SetActive(false);
            }
            else {
                activeWeapon = handgun;

                handgun.SetActive(true);
                autoRifle.SetActive(false);
            }
        }
    }
}

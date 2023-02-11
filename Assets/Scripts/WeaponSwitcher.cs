using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class WeaponSwitcher : MonoBehaviour
    {
        [SerializeField] private GameObject handgun;
        [SerializeField] private GameObject autoRifle;
        [SerializeField] private GameObject grenade;
        public static GameObject activeWeapon;

        void Start()
        {
            activeWeapon = handgun;
            autoRifle.SetActive(false);
            grenade.SetActive(false);
        }

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

            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                activeWeapon = handgun;

                handgun.SetActive(true);
                autoRifle.SetActive(false);
                if (grenade != null) grenade.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2)) {
                activeWeapon = autoRifle;

                handgun.SetActive(false);
                autoRifle.SetActive(true);
                if (grenade != null) grenade.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.G)) {
                activeWeapon = grenade;

                handgun.SetActive(false);
                autoRifle.SetActive(false);

                if (grenade != null) grenade.SetActive(true);
            }
        }
    }
}

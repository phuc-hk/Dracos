using Assets.HeroEditor.Common.CharacterScripts;
using Assets.HeroEditor.Common.ExampleScripts;
using HeroEditor.Common;
using HeroEditor.Common.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] SpriteGroupEntry item;
    [SerializeField] EquipmentPart part;
    [SerializeField] int weaponDamage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Pickup(other.GetComponent<Character>());
            other.GetComponentInChildren<MeleeWeapon>().SetWeaponDamage(weaponDamage);
            Destroy(gameObject);
            //StartCoroutine(HideForSeconds(5));
        }

    }

    private void Pickup(Character character)
    {
        character.Equip(item, part);
    }

    //IEnumerator HideForSeconds(float hideTime)
    //{
    //    ShowPickup(false);
    //    yield return new WaitForSeconds(hideTime);
    //    ShowPickup(true);
    //}

    //private void ShowPickup(bool isShow)
    //{
    //    GetComponent<Collider>().enabled = isShow;
    //    foreach (Transform child in transform)
    //    {
    //        child.gameObject.SetActive(isShow);
    //    }
    //}

    //public bool HandleRaycast(PlayerController controller)
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Pickup(controller.GetComponent<Fighter>());
    //    }
    //    return true;
    //}

    //public CursorType GetCursorType()
    //{
    //    return CursorType.Pickup;
    //}
}

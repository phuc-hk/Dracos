using Assets.HeroEditor.Common.CharacterScripts;
using Assets.HeroEditor.Common.CharacterScripts.Firearms;
using Assets.HeroEditor.Common.CharacterScripts.Firearms.Enums;
using Assets.HeroEditor.Common.ExampleScripts;
using HeroEditor.Common.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attacking : MonoBehaviour
{
    public Character Character;
    public BowExample BowExample;
    public Firearm Firearm;
    public Transform ArmL;
    public Transform ArmR;
    //public KeyCode FireButton;
    //public KeyCode ReloadButton;
    [Header("Check to disable arm auto rotation.")]
    public bool FixedArm;
    public AudioSource audioSource;
    public AudioClip attackSound;
    //private void Awake()
    //{
    //    playerInput = GetComponent<PlayerInput>();
    //}

    //private void Start()
    //{
    //    playerInput.actions["Fire"].performed += OnAttack;
    //}

    void Start()
    {
        //Character.UnEquip(EquipmentPart.Shield);
        //Character.UnEquip(EquipmentPart.MeleeWeapon1H);
        //Character.Equip(Character.SpriteCollection.Shield[1], EquipmentPart.Shield);
    }

    public void OnAttack(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            Attack();
        }

        if (value.canceled && (Character.WeaponType == WeaponType.Bow))
        {
            BowExample.ChargeButtonUp = true;
        }
    }

    public void OnCrouch(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            Crouch();
        }
    }

    private void Crouch()
    {
        Character.Crouch();
    }

    public void Attack()
    {
        if ((Character.WeaponType == WeaponType.Firearms1H || Character.WeaponType == WeaponType.Firearms2H) && Firearm.Params.Type == FirearmType.Unknown)
        {
            throw new Exception("Firearm params not set.");
        }

        if (Character.Animator.GetInteger("State") >= (int)CharacterState.DeathB) return;

        switch (Character.WeaponType)
        {
            case WeaponType.Melee1H:
            case WeaponType.Melee2H:
            case WeaponType.MeleePaired:
                Character.Slash();
                break;
            case WeaponType.Bow:
                BowExample.ChargeButtonDown = true;
                break;
            case WeaponType.Firearms1H:
            case WeaponType.Firearms2H:
                Firearm.Fire.FireButtonDown = true;
                Firearm.Fire.FireButtonPressed = true;
                break;
            case WeaponType.Supplies:
                Character.Animator.Play(Time.frameCount % 2 == 0 ? "UseSupply" : "ThrowSupply", 0); // Play animation randomly.
                break;
        }

        Character.GetReady();

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(attackSound);
        }
    }

    /// <summary>
    /// Called each frame update, weapon to mouse rotation example.
    /// </summary>
    public void LateUpdate()
    {
        switch (Character.GetState())
        {
            case CharacterState.DeathB:
            case CharacterState.DeathF:
                return;
        }

        Transform arm;
        Transform weapon;

        switch (Character.WeaponType)
        {
            case WeaponType.Bow:
                arm = ArmL;
                weapon = Character.BowRenderers[3].transform;
                break;
            case WeaponType.Firearms1H:
            case WeaponType.Firearms2H:
                arm = ArmR;
                weapon = Firearm.FireTransform;
                break;
            default:
                return;
        }

        if (Character.IsReady())
        {
            //RotateArm(arm, weapon, FixedArm ? arm.position + 1000 * Vector3.right : Camera.main.ScreenToWorldPoint(Input.mousePosition), 0, 40);

            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);
                //if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
                //{
                    RotateArm(arm, weapon, FixedArm ? arm.position + 1000 * Vector3.right : touch.position, 0, 40);
                //}
            }
        }
    }

    public float AngleToTarget;
    public float AngleToArm;

    /// <summary>
    /// Selected arm to position (world space) rotation, with limits.
    /// </summary>
    

    public void RotateArm(Transform arm, Transform weapon, Vector2 touchPosition, float angleMin, float angleMax)
    {
        var target = arm.transform.InverseTransformPoint(touchPosition);

        var angleToTarget = Vector2.SignedAngle(Vector2.right, target);
        var angleToArm = Vector2.SignedAngle(weapon.right, arm.transform.right) * Math.Sign(weapon.lossyScale.x);
        var fix = weapon.InverseTransformPoint(arm.transform.position).y / target.magnitude;

        if (fix < -1) fix = -1;
        else if (fix > 1) fix = 1;

        var angleFix = Mathf.Asin(fix) * Mathf.Rad2Deg;
        var angle = angleToTarget + angleFix + arm.transform.localEulerAngles.z;

        angle = NormalizeAngle(angle);
        
        if (angle > 90)
        {
            angle = 180 - angle;
        }

        if (angle > angleMax)
        {
            angle = angleMax;
        }
        else if (angle < angleMin)
        {
            angle = angleMin;
        }

        if (float.IsNaN(angle))
        {
            Debug.LogWarning(angle);
        }

        arm.transform.localEulerAngles = new Vector3(0, 0, angle + angleToArm);
    }

    private static float NormalizeAngle(float angle)
    {
        while (angle > 180) angle -= 360;
        while (angle < -180) angle += 360;

        return angle;
    }
}

//RotateArm(arm, weapon, FixedArm ? arm.position + 1000 * Vector3.right : Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), 20, 45);
//if (Input.touchCount > 0)
//{
//    RotateArm(arm, weapon, FixedArm ? arm.position + 1000 * Vector3.right : Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position), -90, 90);
//}
//if (Input.touchCount > 0)
//{
//    var touch = Input.GetTouch(0);
//    if (touch.phase == UnityEngine.TouchPhase.Began || touch.phase == UnityEngine.TouchPhase.Moved)
//    {
//        RotateArm(arm, weapon, FixedArm ? arm.position + 1000 * Vector3.right : Camera.main.ScreenToWorldPoint(touch.position), 0, 30);
//    }
//}


//public void RotateArm(Transform arm, Transform weapon, Vector2 target, float angleMin, float angleMax) // TODO: Very hard to understand logic.
//{
//    target = arm.transform.InverseTransformPoint(target);

//    var angleToTarget = Vector2.SignedAngle(Vector2.right, target);
//    var angleToArm = Vector2.SignedAngle(weapon.right, arm.transform.right) * Math.Sign(weapon.lossyScale.x);
//    var fix = weapon.InverseTransformPoint(arm.transform.position).y / target.magnitude;

//    AngleToTarget = angleToTarget;
//    AngleToArm = angleToArm;

//    if (fix < -1) fix = -1;
//    else if (fix > 1) fix = 1;

//    var angleFix = Mathf.Asin(fix) * Mathf.Rad2Deg;
//    var angle = angleToTarget + angleFix + arm.transform.localEulerAngles.z;

//    angle = NormalizeAngle(angle);

//    if (angle > angleMax)
//    {
//        angle = angleMax;
//    }
//    else if (angle < angleMin)
//    {
//        angle = angleMin;
//    }

//    if (float.IsNaN(angle))
//    {
//        Debug.LogWarning(angle);
//    }

//    arm.transform.localEulerAngles = new Vector3(0, 0, angle + angleToArm);
//}
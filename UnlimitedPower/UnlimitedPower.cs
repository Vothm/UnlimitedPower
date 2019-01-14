using Modding;
using System;
using UnityEngine;
using GlobalEnums;


namespace UnlimitedPower
{
    //Mod to have UNLIMITED POWER
    public class UnlimitedPower : Mod<ModHooksGlobalSettings>
    {
    
        internal static UnlimitedPower Instance;

        public override void Initialize()
        {
            Instance = this;
            Log("init");
            ModHooks.Instance.HeroUpdateHook += OnHeroUpdate;
            ModHooks.Instance.AttackHook += OnAttack;
            ModHooks.Instance.DashVectorHook += Instance_DashVectorHook;
        }


        private Vector2 Instance_DashVectorHook(Vector2 change)
        {
            return change * (1 + (4 * Time.deltaTime));
        }

        //public float FocusCostCalc()
        //{

        //}

        public void OnAttack(AttackDirection dir)
        {
            HeroController.instance.AddMPCharge(100);
        }
        

        public void OnHeroUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                HeroController.instance.DOUBLE_JUMP_STEPS += 1;
                HeroController.instance.ResetAirMoves();
                HeroController.instance.DOUBLE_JUMP_STEPS -= 1;
            }
        }

        public void OnAfterAttack(AttackDirection dir)
        {
            LogDebug("He's dead");
        }
    }
}

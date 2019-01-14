using Modding;
using System;
using UnityEngine;
using GlobalEnums;
using On;
using IL;
using System.IO;

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
        private static Texture2D loadImageFromAssembly(string imageName)
        {
            //Create texture from bytes
            Texture2D tex = new Texture2D(1, 1);
            tex.LoadImage(getBytes(imageName));
            return tex;
        }

        private static byte[] getBytes(string filename)
        {
            Stream dataStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(filename);
            if (dataStream == null) return null;

            byte[] buffer = new byte[dataStream.Length];
            dataStream.Read(buffer, 0, buffer.Length);
            dataStream.Dispose();
            return buffer;
        }



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

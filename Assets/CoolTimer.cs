using Unity.VisualScripting;
using UnityEngine;

public class CoolTimer
{
    public float CooldownTimer;
    public bool Cooldown = false;
    float Cooltime;
    public CoolTimer(float Cooltime)
    {
        this.Cooltime = Cooltime;
        CooldownTimer = Cooltime;
    }
        
    public void Cooltimer()
    {
        if(Cooldown)
        {
            CooldownTimer -= Time.deltaTime;
            if(CooldownTimer<0)
            {
                CooldownTimer = Cooltime;
                Cooldown = false;
            }
        }
    }
}

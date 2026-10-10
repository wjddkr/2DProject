using UnityEngine;

public abstract class Stat : MonoBehaviour
{
    public abstract int Hp{get; set;}
    public abstract int MaxChegan{get; set;}
    public int Chegan;

    public void Damage(int Damage, int CheganDamage)
    {
            Hp -= Damage;
            Chegan += CheganDamage;
            
    }
}

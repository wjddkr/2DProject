using UnityEngine;

public class Stat
{
    public int Hp;
    
    public Stat(int Hp)
    {
        this.Hp = Hp;
    }

    public void Damage(int Damage)
    {
            Hp -= Damage;
        
    }
}

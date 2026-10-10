using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class PlayerStat : Stat
{
    [Header("Stat")]
    public int hp;
    public int maxchegan;

    public static PlayerStat Instance;

    void Awake()
    {
        Instance = this;
    }
    public override int Hp{ get => hp; set => hp = value; }
    public override int MaxChegan{ get => maxchegan; set => maxchegan = value; }
}

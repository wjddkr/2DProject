using UnityEngine.UI;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    public Slider slider;
    // Update is called once per frame
    void Update()
    {
        slider.value = (float)PlayerStat.Instance.Hp/100f;
    }
}

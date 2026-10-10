using UnityEngine.UI;
using UnityEngine;

public class CheganBar : MonoBehaviour
{
    public Slider leftslider;
    public Slider rightslider;
    void Update()
    {
        leftslider.value = PlayerStat.Instance.Chegan/100f;
        leftslider.gameObject.SetActive(PlayerStat.Instance.Chegan!=0);

        rightslider.value = PlayerStat.Instance.Chegan/100f;
        rightslider.gameObject.SetActive(PlayerStat.Instance.Chegan!=0);
    }
}

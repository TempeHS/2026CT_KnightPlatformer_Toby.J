using UnityEngine;
using UnityEngine.UI;

public class HealthHeartScript : MonoBehaviour
{
    public Sprite fullHeart, halfHeart, quarterHeart, emptyHeart;
    public Image heartImage;

    private void Awake()
    {
        heartImage = GetComponent<Image>();
    }


    public enum heartStatus
    {
        Empty = 0,
        Quarter = 1,
        Half = 2,
        Full = 3
    
    }

    public void SetHeartImage(heartStatus status)
    {
        switch (status)
        {
            case heartStatus.Empty:
                heartImage.sprite = emptyHeart;
                break;

            case heartStatus.Half:
                heartImage.sprite = halfHeart;
                break;

            case heartStatus.Quarter:
                heartImage.sprite = quarterHeart;
                break;

            case heartStatus.Full:
                heartImage.sprite = fullHeart;
                break;
        }
    }

}
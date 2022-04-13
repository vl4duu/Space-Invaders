using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GifAnimator : MonoBehaviour
{
    public float duration;
 
    [FormerlySerializedAs("sprites")] [SerializeField] private Sprite[] animatedImages;
     
    private Image image;
    private int index = 0;
    private float timer = 0;
 
    void Start()
    {
        image = GetComponent<Image>();
    }
    private void Update()
    {
        if((timer+=Time.deltaTime) >= (duration / animatedImages.Length))
        {
            timer = 0;
            image.sprite = animatedImages[index];
            index = (index + 1) % animatedImages.Length;
        }
    }
}
using UnityEngine;
using UnityEngine.UI;

public class SteppedHorizontalScrollView : MonoBehaviour
{
    public Scrollbar scrollBar;

    //TODO: —юда можно спавнить картинки
    [SerializeField] private Transform contentPlaceholder;

    float scrollPosition = 0;
    float[] positions;
    float distance;
    void Start()
    {
        positions = new float[transform.childCount];
        distance = 1f / (positions.Length - 1);
    }

    void Update()
    {
        for(int i = 0; i < positions.Length; i++)
        {
            positions[i] = distance * i;
        }
        if(Input.GetMouseButton(0))
        {
            scrollPosition = scrollBar.value;
        } else
        {
            for (int i = 0; i < positions.Length; i++)
            {
                if(scrollPosition < positions[i] + (distance / 2) && scrollPosition > positions[i] - (distance / 2))
                {
                    scrollBar.value = Mathf.Lerp(scrollBar.value, positions[i], 0.05f);
                }
            }
        }
    }
}

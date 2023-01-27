using BreakInfinity;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class FloatingText : MonoBehaviour
{
    private static int sorting_order;
    private const float DISAPPEAR_TIMER_MAX = 1f;
    
    private TMP_Text text;
    private float disappear_timer;
    private Color text_color;
    private Vector3 move_vector;

    public static FloatingText Create(Vector3 position, BigDouble click_power, bool is_critical)
    {
        GameObject floating_text_go = Instantiate(GameAssets.i.floating_text, position, Quaternion.identity);
        FloatingText floating_text = floating_text_go.transform.GetComponent<FloatingText>();
        
        floating_text.Setup(click_power, is_critical);

        return floating_text;
    }

    private void Awake()
    {
        text = transform.GetComponent<TMP_Text>();
    }

    private void Update()
    {
        var move_speed = 5f;
        transform.position += move_vector * Time.deltaTime;
        move_vector -= move_vector * (5f * Time.deltaTime);

        disappear_timer -= Time.deltaTime;

        if (disappear_timer > DISAPPEAR_TIMER_MAX * .8f)
        {
            float increaseScaleAmount = 4;
            transform.localScale += Vector3.one * (increaseScaleAmount * Time.deltaTime);
        }
        else
        {
            float decreaseScaleAmount = 2;
            if(transform.localScale.x > 0)
                transform.localScale -= Vector3.one * (decreaseScaleAmount * Time.deltaTime);
        }

        if (disappear_timer < 0)
        {
            float disappear_speed = 3f;
            text_color.a -= disappear_speed * Time.deltaTime;
            text.color = text_color;
            
            if(text_color.a < 0)
                Destroy(gameObject);
        }
    }

    public void Setup(BigDouble amount, bool is_critical)
    {
        text.SetText(GameManager.UI_Manager.ScoreShow(amount));

        if (is_critical)
        {
            text.fontSize = 14;
            text.color = Color.red;
        }
            
        text_color = text.color;
        disappear_timer = DISAPPEAR_TIMER_MAX;
        
        move_vector = new Vector3(Random.Range(-.8f, .8f), 1) * 20f;
        
    }
}

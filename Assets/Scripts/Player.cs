using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    Animator animator;
    Rigidbody2D rb;
    Vector2 inputDir;
    bool canBreathe;
    float airStorage = 0f;
    bool isDying;
    bool isDead;
    SpriteRenderer sprite;
    GameObject particles;

    public GameObject FinishCanvasThing;
    public RectTransform airImage;
    public Vector2 minMaxVerSpeed = new Vector2(1f, 5f);
    public Vector2 minMaxHorSpeed = new Vector2(2f, 10f);
    public float burstAirSpeed = 5f;
    public float maxAir = 250f;
    public float airFillSpeed = 25f;
    public float airLoss = 1f;
    public float dieTime;
    public float dieFallSpeed = 50f;
    public Vector2 airImageSize = new Vector2(0f, 200f);

    void Start () {
        ScreenFader.FadeIn();

        particles = transform.GetChild(1).gameObject;
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        inputDir = new Vector2();

        airStorage = maxAir;
        airImage.sizeDelta = new Vector2(maxAir, maxAir);
    }
	
	void Update () {
        if (!isDead)
        {
            bool breathing = Input.GetKey(KeyCode.Space);
            inputDir = new Vector2();
            if (canBreathe && breathing)
            {
                //Inhale
                if (airStorage < maxAir)
                {
                    float fillAmount = airFillSpeed * Time.deltaTime;
                    airStorage += fillAmount;
                    //float mappedAmount = map(fillAmount, 0f, maxAir, airImageSize.x, airImageSize.y);
                    airImage.sizeDelta += new Vector2(fillAmount, fillAmount);
                }
            }
            else if (!breathing)
            {
                //Can move
                float hor = Input.GetAxis("Horizontal");
                float ver = Input.GetAxis("Vertical");

                if (canBreathe && ver > 0f)
                    ver = 0f;

                inputDir = new Vector2(hor * map(airStorage, 0f, 250f, minMaxVerSpeed.y, minMaxVerSpeed.x), ver * map(airStorage, 0f, 250f, minMaxHorSpeed.y, minMaxHorSpeed.x));
            }
            /*else if(!canBreathe && breathing)
            {
                if (airStorage > airLoss * 5f)
                {
                    float fillAmount = -airLoss * 5f * Time.deltaTime;
                    airStorage += fillAmount;
                    airImage.sizeDelta += new Vector2(fillAmount, fillAmount);

                    inputDir = new Vector2(0f, burstAirSpeed);
                }
            }*/

            if (!canBreathe)
            {
                //Drain air
                if (airStorage > 0f)
                {
                    float fillAmount = airLoss * Time.deltaTime;
                    airStorage -= fillAmount;
                    //float mappedAmount = map(fillAmount, 0f, maxAir, airImageSize.x, airImageSize.y);
                    airImage.sizeDelta -= new Vector2(fillAmount, fillAmount);
                }
            }

            if (airStorage <= 100f)
            {
                animator.SetBool("isDrowning", true);
            }
            else if (airStorage > 100f)
            {
                animator.SetBool("isDrowning", false);
            }
            if(airStorage <= 0f && !isDead)
            {
                Dying();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isDead && inputDir != Vector2.zero)
            rb.AddForce(inputDir, ForceMode2D.Force);

        if (inputDir != Vector2.zero)
        {
            Vector2 dir = inputDir;
            float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;

            if (-angle >= 0f && -angle < 90f)
            {
                sprite.flipX = false;
                sprite.flipY = false;
            }
            else if (-angle >= 90f && -angle < 180f)
            {
                sprite.flipX = false;
                sprite.flipY = true;
            }
            if (-angle >= 180f && -angle < 270f)
            {
                sprite.flipX = false;
                sprite.flipY = true;
            }
            if (-angle >= 270f && -angle < 360f)
            {
                sprite.flipX = false;
                sprite.flipY = false;
            }

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, angle), Time.fixedDeltaTime * 20f);
        }
    }
    
    void Dying()
    {
        StartCoroutine(ScreenFader.FadeOut());
        animator.SetBool("isDrowning", false);
        animator.SetTrigger("Die");
        Camera.main.GetComponent<CameraMovement>().target = null;
        GetComponent<CircleCollider2D>().enabled = false;
        isDead = true;
        rb.velocity = new Vector2(0f, dieFallSpeed);
        Camera.main.GetComponent<CameraMovement>().PlayBubblesSound();
        FinishCanvasThing.SetActive(true);
        //GameObject.FindGameObjectWithTag("ScoreScreen").SetActive(true);
        StartCoroutine(CountScore.Activate(CollectableSystem.instance.score));
    }

    float map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Breathable"))
        {
            animator.SetBool("isSwimming", false);
            particles.SetActive(false);
            rb.AddForce(new Vector2(0, -rb.velocity.y * 10f));
            canBreathe = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Breathable"))
        {
            animator.SetBool("isSwimming", true);
            particles.SetActive(true);
            canBreathe = false;
        }
    }
}

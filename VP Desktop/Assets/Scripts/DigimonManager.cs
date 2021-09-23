using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class DigimonManager : MonoBehaviour
{
    [SerializeField]
    GameObject digimon;
    [SerializeField]
    Button digimonButton;
    [SerializeField]
    public RectTransform sprite;

    public Animator animator;

    public static DigimonManager instance;

    #region MenuGlobals
    [SerializeField]
    CanvasGroup menuCanvas;

    #endregion

    #region StateGlobals
    private StateMachine stateController;

    private StatusTimer[] timers;

    public StatusContainer CanPoop = new StatusContainer(false);
    public StatusContainer IsHungry = new StatusContainer(false);

    float timerSync;

    [SerializeField] //For Testing
    float poopTimer = 180;
    float hungerTimer = 180;

    int hungerMax = 5;
    int hungerLevel;
    int careMistakesMax = 5;
    int careMistakes;
    #endregion

    private void Awake() 
    {
        if(instance == null) 
        {
            instance = this;
        } else if(instance != this) 
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        stateController = gameObject.AddComponent<StateMachine>();
        stateController.Initalise();

        timers = new StatusTimer[] {
            new StatusTimer(CanPoop, poopTimer),
            new StatusTimer(IsHungry,hungerTimer)
        };
        digimonButton.onClick.AddListener(ShowMenu);
        hungerLevel = hungerMax;
    }

    private void ShowMenu()
    {
        menuCanvas.alpha = 0 ? 1 : 0;
    }
    private void Update()
    {
        CheckTimers();
    }
    #region States
    public void AddCareMistake()
    {
        careMistakes++;
        if(careMistakes == careMistakesMax)
        {
            Destroy(sprite.gameObject);
        }
    }
    public void ShowHungerEmote()
    {
        Debug.Log(hungerLevel);
        //animator.SetBool("isHungry", IsHungry.Status);
        StartCoroutine("StopEmote");
    }
    private IEnumerator StopEmote()
    {
        yield return new WaitForSeconds(5);
        //animator.SetBool("isHungry", IsHungry.Status);
    }
    public int CheckhungerLevel()
    {
        if(hungerLevel > 0)
        {
            hungerLevel--;
        }
        else
        {
            hungerLevel = 0;
        }
        return hungerLevel;
    }

    public void Move(Vector2 position, Action onComplete = null)
    {
        bool moving = true;
        animator.SetBool("isWalking", moving);
        float x = position.x;
        float y = position.y;
        sprite.localScale = new Vector3(x < transform.position.x ? -1 : 1, 1, 1);
        Vector3 newPos = new Vector3(x, y, 0);
        transform.DOMove(newPos, 1).OnComplete(() =>
        {
            onComplete?.Invoke();
            moving = false;
            animator.SetBool("isWalking", moving);
            animator.speed = 1;
        }).SetEase(Ease.Linear);
    }

    public void MoveRandom( Action onComplete = null)
    {
       int x = UnityEngine.Random.Range(100, Screen.width - 100);
       int y = UnityEngine.Random.Range(100, Screen.height - 100);
       Move(new Vector2(x, y), onComplete);
    }
   

    private void CheckTimers()
    {
        float currentTime = Time.time - timerSync;
        if (currentTime > 1)
        {
            foreach (StatusTimer timer in timers)
            {
                timer.RunTimer();
            }
            timerSync = Time.time;
        }
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static PlayerControllers pc;

    [Header("Player")]
    public GameObject player;

    [Header("GameSetting")]
    public int difficultyLevel;
    public int numberOfTomatoes;
    public GameObject prefabKnife;

    [Header("Chief's image")]
    public List<GameObject> chiefsImages;
    public float speedChiefsImages;
    public int numberOfChiefsImages;

    [Header("Quest system")]
    public string questTextNote1 = "ЗАДАНИЕ: Отклеить 10 этикеток от бананов.";
    public string questTextNote2 = "ЗАДАНИЕ: найти 5 яиц.";
    public string questTextNote3 = "ЗАДАНИЕ: Найти нож шефа и положить его на место.";

    public string TextStartGameNote = "ЗАДАНИЕ: прочитать первую записку на кухне";
    public string TextNoteBeforeQuest1 = "ОГУЗОК! Ты опоздал на работу! Пока не отклеишь этикетки от всех бананов, не показывайся мне на глаза!";
    public string TextNoteAfterQuest1 = "ЗАДАНИЕ: прочитать новую записку на кухне.";
    public string TextNoteBeforeQuest2 = "Инвалиды! Где мои яйца?!";
    public string TextNoteAfterQuest2 = "ЗАДАНИЕ: прочитать новую записку на кухне.";
    public string TextNoteBeforeQuest3 = "Кто из вас, инвалидов, посмел взять мой нож? Я знаю, что это ты, огузок! Тебе конец.";

    public bool isStartGame;
    public bool isInvokeStartGame;
    public bool isTakeFirstNote;
    public bool isTakeSecondNote;
    public bool isTakeThirdNote;
    public bool isFirstQuestEnded;
    public bool isSecondQuestEnded;
    public bool isThirdQuestEnded;

    public int takeBananaInt;
    public int takeEggsInt;

    public bool isRespawnedFirstNote;
    public bool isRespawnedSecondNote;
    public bool isRespawnedThirdNote;

    public bool isTakeKnife;
    public bool isSetKnifeOnPlace;
    public bool isInvokedFinish;

    public List<GameObject> Notes;
    public List<GameObject> Bananas;
    public List<GameObject> Eggs;
    public GameObject Knife;

    [Header("UI system")]
    public GameObject NotePanel;
    public TextMeshProUGUI NoteText;
    public float timeLifeNote = 5f;

    public GameObject QuestPanel;
    public TextMeshProUGUI QuestText;

    public GameObject TakeItemPanel;
    public TextMeshProUGUI TakedItemIntText;

    public GameObject DifficultyLevelPanel;
    public GameObject PlayerUIStatPanel;
    public TextMeshProUGUI AmmoIntText;
    public TextMeshProUGUI DurationSpeedIntText;


    void Awake()
    {
        Instance = this;
        PlayerTrigger.gm = Instance;
        PlayerControllers.gm = Instance;

        isStartGame = false;
        isInvokeStartGame = false;
        isTakeFirstNote = false;
        isTakeSecondNote = false;
        isTakeThirdNote = false;
        isFirstQuestEnded = false;
        isSecondQuestEnded = false;
        isThirdQuestEnded = false;

        takeBananaInt = 0;
        takeEggsInt = 0;

        isRespawnedFirstNote = false;
        isRespawnedSecondNote = false;
        isRespawnedThirdNote = false;

        isTakeKnife = false;
        isSetKnifeOnPlace = false;
        isInvokedFinish = false;

        NotePanel.SetActive(false);
        TakeItemPanel.SetActive(false);
        QuestPanel.SetActive(false);
        PlayerUIStatPanel.SetActive(false);

        //StartCoroutine();
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (var note in Notes)
        {
            note.gameObject.SetActive(false);
        }

        foreach(var banana in Bananas)
        {
            banana.gameObject.SetActive(false);
        }

        foreach (var egg in Eggs)
        {
            egg.gameObject.SetActive(false);
        }

        foreach (var image in chiefsImages)
        {
            image.gameObject.SetActive(false);
            image.GetComponent<NavMeshAgent>().speed = 0;
        }

        Knife.gameObject.SetActive(false);

        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        if (isStartGame == true && isTakeFirstNote == false) 
        {
            isTakeFirstNote = true;
            StartCoroutine(Quest1());
        }

        if(takeBananaInt >= 10 && isRespawnedSecondNote == false)
        {
            isRespawnedSecondNote = true;
            QuestText.text = TextNoteAfterQuest1;
            StartCoroutine(Quest2());
        }

        if(takeEggsInt >= 5 && isRespawnedThirdNote == false)
        {
            isRespawnedThirdNote = true;
            QuestText.text = TextNoteAfterQuest2;
            StartCoroutine(Quest3());
        }

        if(isSetKnifeOnPlace == true && isInvokedFinish == false)
        {
            isInvokedFinish = true;
            StartCoroutine(EndGame());
        }

        // UI Updating
        if(isTakeThirdNote == true)
        {
            if(isTakeKnife == false)
            {
                TakedItemIntText.text = "НОЖ ШЕФА: X 0";
            }
            else
            {
                TakedItemIntText.text = "НОЖ ШЕФА: X 1";
            }
        }
        else if(isTakeSecondNote == true)
        {
            TakedItemIntText.text = "ЯЙЦА: X " + takeEggsInt;
        }
        else if (isTakeFirstNote == true)
        {
            TakedItemIntText.text = "БАНАНЫ: X " + takeBananaInt;
        }

        AmmoIntText.text = "X "+pc.ammoOfPotatoes;
        DurationSpeedIntText.text = "" + ((int)pc.durationBonusSpeed + (-1 * (int)pc.cooldownBonusSpeed)) + "s / " + (int)pc.durationBonusSpeed + "s";
    }

    void TakeDifficultyLevel(int difficultyInt)
    {
        if (difficultyInt == 0)
        {
            numberOfChiefsImages = 1;
            numberOfTomatoes *= 4;
            speedChiefsImages *= 0.5f;

            Destroy(chiefsImages[1].gameObject);
            Destroy(chiefsImages[2].gameObject);
        }
        else if (difficultyInt == 1)
        {
            numberOfChiefsImages = 2;
            numberOfTomatoes *= 4;
            speedChiefsImages *= 1f;

            Destroy(chiefsImages[1].gameObject);
        }
        else if (difficultyInt == 2)
        {
            numberOfChiefsImages = 2;
            numberOfTomatoes *= 2;
            speedChiefsImages *= 2f;

            Destroy(chiefsImages[1].gameObject);
        }
        else if (difficultyInt == 3)
        {
            numberOfChiefsImages = 3;
            numberOfTomatoes *= 1;
            speedChiefsImages *= 2f;
        }
        else return;

        QuestPanel.gameObject.SetActive(true);
        QuestText.text = TextStartGameNote;
        DifficultyLevelPanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        isStartGame = true;
    }

    public void TakeEasyLevel()
    {
        TakeDifficultyLevel(0);
    }

    public void TakeNormalLevel()
    {
        TakeDifficultyLevel(1);
    }

    public void TakeHardLevel()
    {
        TakeDifficultyLevel(2);
    }

    public void TakeVeryHardLevel()
    {
        TakeDifficultyLevel(3);
    }

    public void TakeNote1()
    {
        isTakeFirstNote = true;

        TakeItemPanel.SetActive(true);
        NoteText.text = TextNoteBeforeQuest1;
        NotePanel.SetActive(true);
        QuestText.text = questTextNote1;
        QuestPanel.SetActive(true);
        PlayerUIStatPanel.SetActive(true);

        StartCoroutine(DisableNotePanel());
        
        foreach(var banana in Bananas)
        {
            banana.gameObject.SetActive(true);
        }

        foreach(var image in chiefsImages)
        {
            if(image != null)
            {
                image.gameObject.SetActive(true);
                image.GetComponent<NavMeshAgent>().speed = speedChiefsImages;
            }
        }
    }

    public void TakeNote2()
    {
        isTakeSecondNote = true;

        TakeItemPanel.SetActive(true);
        NoteText.text = TextNoteBeforeQuest2;
        NotePanel.SetActive(true);
        QuestText.text = questTextNote2;
        QuestPanel.SetActive(true);

        StartCoroutine(DisableNotePanel());

        foreach (var egg in Eggs)
        {
            egg.gameObject.SetActive(true);
        }
    }
    public void TakeNote3()
    {
        isTakeThirdNote = true;
        TakeItemPanel.SetActive(true);
        NoteText.text = TextNoteBeforeQuest3;
        NotePanel.SetActive(true);
        QuestText.text = questTextNote3;
        QuestPanel.SetActive(true);

        StartCoroutine(DisableNotePanel());
        
        Knife.gameObject.SetActive(true);
    }

    IEnumerator StartedGame()
    {
        foreach(var enemy in chiefsImages)
        {
            //МЕТОД ПРИСВОЕНИЯ ПЕРЕМЕННОЙ СКОРОСТИ У КАРТИНОК ШЕФА
        }
        yield return null;
    }

    IEnumerator Quest1()
    {
        isRespawnedFirstNote = true;
        Notes[0].gameObject.SetActive(true);
        //МЕТОД СОЗДАНИЯ ВТОРОЙ ЗАПИСКИ
        //МЕТОД ИЗМЕНЕНИЯ ТЕКСТА В ПАНЕЛИ ЗАДАЧИ
        yield return null;
    }

    IEnumerator Quest2()
    {
        isRespawnedSecondNote = true;
        Notes[1].gameObject.SetActive(true);
        //МЕТОД СОЗДАНИЯ ВТОРОЙ ЗАПИСКИ
        //МЕТОД ИЗМЕНЕНИЯ ТЕКСТА В ПАНЕЛИ ЗАДАЧИ
        yield return null;
    }

    IEnumerator Quest3()
    {
        isRespawnedThirdNote = true;
        Notes[2].gameObject.SetActive(true);
        //МЕТОД СОЗДАНИЯ ТРЕТЬЕЙ ЗАПИСКИ
        //МЕТОД ИЗМЕНЕНИЯ ТЕКСТА В ПАНЕЛИ ЗАДАЧИ
        yield return null;
    }

    IEnumerator DisableNotePanel()
    {
        yield return new WaitForSeconds(timeLifeNote);
        NotePanel.SetActive(false);
        yield return null;
    }
    IEnumerator EndGame()
    {
        foreach(var image in chiefsImages)
        {
            if(image != null)
            {
                Destroy(image.gameObject);
            }
        }

        NotePanel.SetActive(false);
        TakeItemPanel.SetActive(false);
        QuestPanel.SetActive(false);
        PlayerUIStatPanel.SetActive(false);

        Debug.Log("WIN THE GAME");
        yield return null;
    }
}

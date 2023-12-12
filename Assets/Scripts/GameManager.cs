using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;

    [Header("GameSetting")]
    public int difficultyLevel;
    public int numberOfTomatoes;

    [Header("Chief's image")]
    public List<GameObject> chiefsImages;
    public float speedChiefsImages;
    public int numberOfChiefsImages;

    [Header("Quest system")]
    public string questTextNote1 = "ÇÀÄÀÍÈÅ: Îòêëåèòü 10 ıòèêåòîê îò áàíàíîâ.";
    public string questTextNote2 = "ÇÀÄÀÍÈÅ: íàéòè 5 ÿèö.";
    public string questTextNote3 = "ÇÀÄÀÍÈÅ: Íàéòè íîæ øåôà è ïîëîæèòü åãî íà ìåñòî.";

    public string TextNoteBeforeQuest1 = "ÎÃÓÇÎÊ! Òû îïîçäàë íà ğàáîòó! Ïîêà íå îòêëåèøü ıòèêåòêè îò âñåõ áàíàíîâ, íå ïîêàçûâàéñÿ ìíå íà ãëàçà!";
    public string TextNoteAfterQuest1 = "ÇÀÄÀÍÈÅ: ïğî÷èòàòü íîâóş çàïèñêó íà êóõíå.";
    public string TextNoteBeforeQuest2 = "Èíâàëèäû! Ãäå ìîè ÿéöà?!";
    public string TextNoteAfterQuest2 = "ÇÀÄÀÍÈÅ: ïğî÷èòàòü íîâóş çàïèñêó íà êóõíå.";
    public string TextNoteBeforeQuest3 = "Êòî èç âàñ, èíâàëèäîâ, ïîñìåë âçÿòü ìîé íîæ? ß çíàş, ÷òî ıòî òû, îãóçîê! Òåáå êîíåö.";

    public bool isStartGame;
    public bool isStartedGame;
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


    void Awake()
    {
        isStartGame = false;
        isStartedGame = false;
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
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isStartGame == true && isStartedGame == false) 
        {
            isStartedGame = true;
            StartCoroutine(StartedGame());
        }

        if(takeBananaInt == 10 && isRespawnedSecondNote == false)
        {
            isRespawnedSecondNote = true;
            StartCoroutine(Quest2());
        }

        if(takeEggsInt == 5 && isRespawnedThirdNote == false)
        {
            isRespawnedThirdNote = true;
            StartCoroutine(Quest3());
        }

        if(isSetKnifeOnPlace == true && isInvokedFinish == false)
        {
            isInvokedFinish = true;
            StartCoroutine(EndGame());
        }
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

        isStartGame = true;
    }

    void TakeEasyLevel()
    {
        TakeDifficultyLevel(0);
    }

    void TakeNormalLevel()
    {
        TakeDifficultyLevel(1);
    }

    void TakeHardLevel()
    {
        TakeDifficultyLevel(2);
    }

    void TakeVeryHardLevel()
    {
        TakeDifficultyLevel(3);
    }

    void TakeNote1()
    {
        //ÌÅÒÎÄ ÈÇÌÅÍÅÍÈß ÒÅÊÑÒÀ Â ÏÀÍÅËÈ ÇÀÄÀ×È
        //ÌÅÒÎÄ ÏÎßÂËÅÍÈß ÁÀÍÀÍÎÂ Â ÎÏĞÅÄÅËÅÍÍÛÕ ÌÅÑÒÀÕ
    }

    void TakeNote2()
    {
        //ÌÅÒÎÄ ÈÇÌÅÍÅÍÈß ÒÅÊÑÒÀ Â ÏÀÍÅËÈ ÇÀÄÀ×È
        //ÌÅÒÎÄ ÏÎßÂËÅÍÈß ÏÎÌÈÄÎĞÎÂ Â ÎÏĞÅÄÅËÅÍÍÛÕ ÌÅÑÒÀÕ
    }
    void TakeNote3()
    {
        //ÌÅÒÎÄ ÈÇÌÅÍÅÍÈß ÒÅÊÑÒÀ Â ÏÀÍÅËÈ ÇÀÄÀ×È
        //ÌÅÒÎÄ ÏÎßÂËÅÍÈß ÍÎÆÀ ØÅÔÀ Â ÎÏĞÅÄÅËÅÍÍÛÕ ÌÅÑÒÀÕ
    }

    IEnumerator StartedGame()
    {
        foreach(var enemy in chiefsImages)
        {
            //ÌÅÒÎÄ ÏĞÈÑÂÎÅÍÈß ÏÅĞÅÌÅÍÍÎÉ ÑÊÎĞÎÑÒÈ Ó ÊÀĞÒÈÍÎÊ ØÅÔÀ
        }
        yield return null;
    }

    IEnumerator Quest2()
    {
        isRespawnedSecondNote = true;
        //ÌÅÒÎÄ ÑÎÇÄÀÍÈß ÂÒÎĞÎÉ ÇÀÏÈÑÊÈ
        //ÌÅÒÎÄ ÈÇÌÅÍÅÍÈß ÒÅÊÑÒÀ Â ÏÀÍÅËÈ ÇÀÄÀ×È
        yield return null;
    }

    IEnumerator Quest3()
    {
        isRespawnedThirdNote = true;
        //ÌÅÒÎÄ ÑÎÇÄÀÍÈß ÒĞÅÒÜÅÉ ÇÀÏÈÑÊÈ
        //ÌÅÒÎÄ ÈÇÌÅÍÅÍÈß ÒÅÊÑÒÀ Â ÏÀÍÅËÈ ÇÀÄÀ×È
        yield return null;
    }

    IEnumerator EndGame()
    {
        //ÌÅÒÎÄ ÓÍÈ×ÒÎÆÅÍÈß ÊÀĞÒÈÍÎÊ ØÅÔÀ
        //ÌÅÒÎÄ ÂÛÇÎÂÀ ÏÎÁÅÄÍÛÕ İËÅÌÅÍÒÎÂ UI 
        yield return null;
    }
}

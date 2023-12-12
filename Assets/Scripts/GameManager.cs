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
    public string questTextNote1 = "�������: �������� 10 �������� �� �������.";
    public string questTextNote2 = "�������: ����� 5 ���.";
    public string questTextNote3 = "�������: ����� ��� ���� � �������� ��� �� �����.";

    public string TextNoteBeforeQuest1 = "������! �� ������� �� ������! ���� �� �������� �������� �� ���� �������, �� ����������� ��� �� �����!";
    public string TextNoteAfterQuest1 = "�������: ��������� ����� ������� �� �����.";
    public string TextNoteBeforeQuest2 = "��������! ��� ��� ����?!";
    public string TextNoteAfterQuest2 = "�������: ��������� ����� ������� �� �����.";
    public string TextNoteBeforeQuest3 = "��� �� ���, ���������, ������ ����� ��� ���? � ����, ��� ��� ��, ������! ���� �����.";

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
        //����� ��������� ������ � ������ ������
        //����� ��������� ������� � ������������ ������
    }

    void TakeNote2()
    {
        //����� ��������� ������ � ������ ������
        //����� ��������� ��������� � ������������ ������
    }
    void TakeNote3()
    {
        //����� ��������� ������ � ������ ������
        //����� ��������� ���� ���� � ������������ ������
    }

    IEnumerator StartedGame()
    {
        foreach(var enemy in chiefsImages)
        {
            //����� ���������� ���������� �������� � �������� ����
        }
        yield return null;
    }

    IEnumerator Quest2()
    {
        isRespawnedSecondNote = true;
        //����� �������� ������ �������
        //����� ��������� ������ � ������ ������
        yield return null;
    }

    IEnumerator Quest3()
    {
        isRespawnedThirdNote = true;
        //����� �������� ������� �������
        //����� ��������� ������ � ������ ������
        yield return null;
    }

    IEnumerator EndGame()
    {
        //����� ����������� �������� ����
        //����� ������ �������� ��������� UI 
        yield return null;
    }
}

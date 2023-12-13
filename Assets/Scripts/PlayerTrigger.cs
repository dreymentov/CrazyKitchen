using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTrigger : MonoBehaviour
{
    public static PlayerControllers pc;
    public static GameManager gm;

    public GameObject banana;
    public GameObject egg;
    public GameObject knife;
    public GameObject tomato;
    public GameObject steak;
    public GameObject setKnife;

    public bool inBananaZone;
    public bool inEggZone;
    public bool inKnifeZone;
    public bool inTomatoZone;
    public bool inSteakZone;
    public bool inSetKnifeZone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Вот он тот редкий случай Update. OnTriggerStay не каждый кадр работает - отсюда и задержки такие
    //на сцене смотри, чтобы бананы не пересекались, так как пересекающийся с нами банан заходит в переменную banana и удаляется при нажатии на е
    //так же можешь делать и яйца, и нож
    void Update()
    {
        if (inBananaZone && Input.GetKeyDown(KeyCode.E))
        {
            inBananaZone = false;
            Destroy(banana);
            gm.takeBananaInt++;
        }

        if (inEggZone && Input.GetKeyDown(KeyCode.E))
        {
            inEggZone = false;
            Destroy(egg);
            gm.takeEggsInt++;
        }

        if (inKnifeZone && Input.GetKeyDown(KeyCode.E))
        {
            inKnifeZone = false;
            Destroy(knife);
            gm.isTakeKnife = true;
        }

        if (inTomatoZone)
        {
            inTomatoZone = false;
            Destroy(tomato);
            pc.ammoOfPotatoes++;

        }

        if (inSteakZone)
        {
            inKnifeZone = false;
            inSteakZone = false;
            Destroy(steak);
            pc.cooldownBonusSpeed = 0;
        }

        if(inSetKnifeZone && Input.GetKeyDown(KeyCode.E))
        {
            inSetKnifeZone = false;
            Instantiate(gm.prefabKnife, setKnife.transform.position, Quaternion.identity);
            gm.isSetKnifeOnPlace = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Note"))
        {
            Destroy(other.gameObject);

            if (gm.takeEggsInt >= 5)
            {
                gm.TakeNote3();
            }
            else if (gm.takeBananaInt >= 10)
            {
                gm.TakeNote2();
            }
            else
            {
                gm.TakeNote1();
            }
        }

        if (other.gameObject.CompareTag("Banana"))
        {
            banana = other.gameObject;
            inBananaZone = true;
        }

        if (other.gameObject.CompareTag("Egg"))
        {
            egg = other.gameObject;
            inEggZone = true;
        }

        if (other.gameObject.CompareTag("Knife"))
        {
            knife = other.gameObject;
            inKnifeZone = true;
        }

        if(other.gameObject.CompareTag("SetForKnife"))
        {
            setKnife = other.gameObject;
            inSetKnifeZone = true;
        }

        if (other.gameObject.CompareTag("Tomato"))
        {
            tomato = other.gameObject;
            inTomatoZone = true;
        }

        if (other.gameObject.CompareTag("Steak"))
        {
            steak = other.gameObject;
            inSteakZone = true;
        }
    }


    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Note"))
        {
            Destroy(other.gameObject);

            if (gm.takeEggsInt >= 5)
            {
                gm.TakeNote3();
            }
            else if (gm.takeBananaInt >= 10)
            {
                gm.TakeNote2();
            }
            else
            {
                gm.TakeNote1();
            }
        }

        if (other.gameObject.CompareTag("Banana"))
        {
            banana = null;
            inBananaZone = false;
        }

        if (other.gameObject.CompareTag("Egg"))
        {
            egg = null;
            inEggZone = false;
        }

        if (other.gameObject.CompareTag("Knife"))
        {
            knife = null;
            inKnifeZone = false;
        }

        if (other.gameObject.CompareTag("SetForKnife"))
        {
            setKnife = null;
            inSetKnifeZone = false;
        }

        if (other.gameObject.CompareTag("Tomato"))
        {
            tomato = null;
            inTomatoZone = false;
        }

        if (other.gameObject.CompareTag("Steak"))
        {
            inSteakZone = false;
            steak = null;
        }
    }
}

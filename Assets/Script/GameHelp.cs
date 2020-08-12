using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHelp : MonoBehaviour
{
    public GameObject fistGameObject;
    public GameObject scondeGameObject;
    public GameObject thirdGameObject;
    public GameObject[] handAnim;
    public bool ok;
    public Text textShow;

    // Start is called before the first frame update
    void Start()
    {
        if (GameSys.Instans.HelpActiveter == true)
        {
            Tutarial();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Tutarial()
    {
        textShow.text = Fa.faConvertLine(" این رو بکش اینجا ");
        handAnim[0].SetActive(true);
        if (fistGameObject.GetComponent<TouchRotate>().touched)
        {
            if (fistGameObject.GetComponent<TouchRotate>().rotate)
            {
                handAnim[0].SetActive(false);
                textShow.text = Fa.faConvertLine("اینجا اسم چیزی رو که باید درست کنی رو ببین ");
                if (ok == true)
                {
                    ok = false;
                    textShow.text = Fa.faConvertLine("این یکی رو هم بیارش ");
                    handAnim[1].SetActive(true);
                    if (scondeGameObject.GetComponent<TouchRotate>().touched)
                        if (scondeGameObject.GetComponent<TouchRotate>().rotate)
                        {
                            handAnim[1].SetActive(false);
                            handAnim[2].SetActive(true);
                            textShow.text = Fa.faConvertLine("این یکی رو هم بیارش ");
                            if (thirdGameObject.GetComponent<TouchRotate>().touched)
                                if (thirdGameObject.GetComponent<TouchRotate>().rotate)
                                {
                                    handAnim[2].SetActive(false);
                                    textShow.text = Fa.faConvertLine(" اینا چراغ  های شهر سوخته هستند. وقتی حرکت درستی انجام بدی روشن میشن ");
                                    GameSys.Instans.HelpActiveter = false;
                                }

                        }

                }
            }
        }

    }

    public void Ok()
    {
        ok = true;
    }
}

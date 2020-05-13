using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class canvasSc : MonoBehaviour
{
    Transform levels;
    Transform mainMenu;
    GameObject gLLHandler;

    [SerializeField]
    GameObject gLL;
    Transform menu;

    public float startTime = 0;
    public float endTime = 0;

    public float[] lvl1Scores;
    public float[] lvl2Scores;
    public float[] lvl3Scores;

    audioManagerScript audioHandler;
    generalLevelLayoutScript gLLHandlerSc;
    GraphicRaycaster m_Raycaster;
    GraphicRaycaster m_Raycaster2;
    PointerEventData m_PointerEventData;
    PointerEventData m_PointerEventData2;
    EventSystem m_EventSystem;
    EventSystem m_EventSystem2;
    [HideInInspector]
    public bool canGuess = false;
    [HideInInspector]
    public bool isCorrect = false;
    [HideInInspector]
    public string guess;
    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        levels = gameObject.transform.GetChild(4);
        mainMenu = gameObject.transform.GetChild(2);
        //gLLHandler = gameObject.transform.GetChild(5);
        audioHandler = GameObject.FindGameObjectWithTag("audioManager").GetComponent<audioManagerScript>();
        //gLLHandler.gameObject.SetActive(true);
        //GameObject.FindGameObjectWithTag("generalLevelLayout");

        lvl1Scores = new float[6];
        lvl2Scores = new float[6];
        lvl3Scores = new float[6];
        resetArrays();
        mainMenu.gameObject.SetActive(true);
        //mainMenu.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "AAAAAAAAAAAAAAAAAa";
        //levels.gameObject.SetActive(false);
        //gLLHandler.gameObject.SetActive(false);
        m_Raycaster = gameObject.GetComponent<GraphicRaycaster>();
        m_EventSystem = gameObject.GetComponent<EventSystem>();
        menu = gameObject.transform.FindChild("scoreMenu");
        Debug.LogError("MENU: " + menu);
        //levels.SetActive(false);
        //gLLHandler.SetActive(false);
    }

    public void startLevel(int num)
    {
        mainMenu.gameObject.SetActive(false);
        levels.gameObject.SetActive(false);
        gLLHandler = Instantiate(gLL, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        gLLHandler.name = "gLL";
        //gLLHandler.gameObject.SetActive(true);
        gLLHandlerSc = gLLHandler.GetComponent<generalLevelLayoutScript>();
        gLLHandlerSc.levelIndex = num;
        //gLLHandlerSc.shuffle();
    }

    public void loadScoreMenu()
    {
        mainMenu.gameObject.SetActive(false);
        menu.gameObject.SetActive(true);
    }

    public void resetArrays()
    {
        for(int i=0; i < 6; i++)
        {
            lvl1Scores[i] = 0;
            lvl2Scores[i] = 0;
            lvl3Scores[i] = 0;
        }
    }

    public void addScore(int lvlIndex, int puzzIndex,float score)
    {
        string str1 = lvlIndex.ToString();
        string str2 = puzzIndex.ToString();
        switch (lvlIndex)
        {
            case 0:
                Debug.LogError("SCORE: " + score + " LVLINDEX: " + lvlIndex + " PUZZINDEX: " + puzzIndex);
                    if (lvl1Scores[puzzIndex] < score)
                    {
                        Debug.LogWarning("LVL1SCORE: " + lvl1Scores[puzzIndex]);
                        lvl1Scores[puzzIndex] = score;
                        menu.transform.FindChild(str1).FindChild(str2).GetComponent<TextMeshProUGUI>().text = "Bölüm " + (puzzIndex +1).ToString() + ": " + score.ToString();
                    } 
                break;
            case 1:
                Debug.LogError("SCORE: " + score + " LVLINDEX: " + lvlIndex + " PUZZINDEX: " + puzzIndex);
               
                    if (lvl2Scores[puzzIndex] < score)
                    {
                        lvl2Scores[puzzIndex] = score;
                        Debug.LogWarning("LVL2SCORE: " + lvl2Scores[puzzIndex]);
                        menu.transform.FindChild(str1).FindChild(str2).GetComponent<TextMeshProUGUI>().text = "Bölüm " + (puzzIndex+1).ToString() + ": " + score.ToString();
                    }    
                break;
            case 2:
                Debug.LogError("SCORE: " + score + " LVLINDEX: " + lvlIndex + " PUZZINDEX: " + puzzIndex); 
                    if (lvl3Scores[puzzIndex] < score)
                    {
                        lvl3Scores[puzzIndex] = score;
                        Debug.LogWarning("LVL3SCORE: " + lvl3Scores[puzzIndex]);
                        menu.transform.FindChild(str1).FindChild(str2).GetComponent<TextMeshProUGUI>().text = "Bölüm " + (puzzIndex + 1).ToString() + ": " + score.ToString();
                    }
                break;
            default:          
                break;
        }
   
    }

    public void setGLLStartTime(float time)
    {
        startTime = time;
        Debug.Log("START TIME:" + startTime);
    }

    public void setGLLEndTime(float end)
    {
        endTime = end;
        Debug.Log("END TIME: " + endTime);
        float final = endTime - startTime;
        Debug.LogError("DIFF: " +  final);
    }

    public void callLevel1()
    {
        startLevel(0);
    }
    public void callLevel2()
    {
        startLevel(1);
    }
    public void callLevel3()
    {
        startLevel(2);
    }
    public void loadLevelMenu()
    {
        audioHandler.playClick();
        mainMenu.gameObject.SetActive(false);
        if (GameObject.FindGameObjectWithTag("generalLevelLayout"))
        {
            gLLHandler.gameObject.SetActive(false);
            Destroy(gLLHandler);
        }
        //gLLHandler.gameObject.SetActive(false);
        levels.gameObject.SetActive(true);
        //levels.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = "AAAAAAAAAAAAAAAAAAaa";
    }
    public void loadMainMenu()
    {
        levels.gameObject.SetActive(false);
        //gLLHandler.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
        menu.gameObject.SetActive(false);
    }
    public void quitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }



    public void bleag()
    {
        Debug.Log("BLEAGH");
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            //Debug.Log(1);
           

                m_PointerEventData = new PointerEventData(m_EventSystem);
                m_PointerEventData.position = Input.mousePosition;
                List<RaycastResult> results = new List<RaycastResult>();

                m_Raycaster.Raycast(m_PointerEventData, results);
                foreach (RaycastResult result in results)
                {
                    Button btn = result.gameObject.GetComponent<Button>();
                    if (btn != null)
                    {
                        if (btn.interactable && btn.tag == "letterButton")
                        {
                            //Debug.Log(1);
                            //Debug.Log("Hit " + result.gameObject.name);
                            GameObject.FindGameObjectWithTag("guessText").transform.GetChild(0).GetComponent<TextMeshProUGUI>().text += btn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
                            guess += btn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
                            btn.interactable = false;
                            canGuess = true;
                        }
                        //Debug.Log("Hit " + result.gameObject.name);
                    }
                }
                //Debug.Log(guess);
        }
        if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log(3);
            if (canGuess && gLLHandler.gameObject.activeSelf)
            {
                //Debug.Log("ADJĞFHGAPDFKHALŞDFGADFKHAGDFADF: " + guess);
                gLLHandlerSc.checkGuess(guess);
                //gLLHandlerSc.reloadGuess();
                canGuess = false;
                guess = "";
            }
        }
        if (Input.touchCount > 0)
        {
            Touch finger = Input.GetTouch(0);
            if (gLLHandler.gameObject.activeSelf)
            {
                if (finger.phase == TouchPhase.Began || finger.phase == TouchPhase.Stationary || finger.phase == TouchPhase.Stationary)
                {
                    var touchPosition = Camera.main.ScreenToWorldPoint(finger.position);
                    touchPosition.z = 0f;
                    m_PointerEventData2 = new PointerEventData(m_EventSystem2);
                    m_PointerEventData2.position = touchPosition;
                    List<RaycastResult> results2 = new List<RaycastResult>();
                    m_Raycaster2.Raycast(m_PointerEventData2, results2);
                    foreach (RaycastResult result in results2)
                    {
                        Button btn = result.gameObject.GetComponent<Button>();
                        if (btn != null)
                        {
                            if (btn.interactable && btn.tag == "letterButton")
                            {
                                Debug.Log("Hit " + result.gameObject.name);
                                GameObject.FindGameObjectWithTag("guessText").transform.GetChild(0).GetComponent<TextMeshProUGUI>().text += btn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
                                guess += btn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
                                btn.interactable = false;
                                canGuess = true;
                            }
                            //Debug.Log("Hit " + result.gameObject.name);
                        }
                    }
                }
                if (finger.phase == TouchPhase.Ended)
                {
                    if (canGuess)
                    {
                        Debug.Log(guess);
                        //gLLHandlerSc.reloadGuess();
                        gLLHandlerSc.checkGuess(guess);
                        canGuess = false;
                        guess = "";
                    }
                }
            }
        }
    }

    public void playGame()
    {
        //Debug.Log("SCENE: " + SceneManager.GetActiveScene().buildIndex);
        //int ind = SceneManager.GetActiveScene().buildIndex + 1;
        //SceneManager.LoadScene(ind);
    }


}

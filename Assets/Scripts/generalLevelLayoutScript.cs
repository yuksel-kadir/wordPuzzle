using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
public class generalLevelLayoutScript : MonoBehaviour
{  
  
    public Dictionary<string, List<Vector2>> currentLevelCoordinates;

    public Dictionary<int, List<string>> levelAnswers;

    public string[] currentLevelLetters;

    public List<string> currentLevelAnswers;

    public List<string> currentFoundGuess = new List<string>();

    public int levelIndex;
    public int puzzleIndex;
    public int correctGuessNumber = 0;

    public float chapterStartTime;
    public float chapterEndTime;

    [HideInInspector]
    public bool canGuess = false;
    
   // [HideInInspector]
   // public bool letDisplayLetter = false;
    public bool completed = true;
    public bool chapterStart = false;

    public float plus = 0;
    public float minus = 0;

    [HideInInspector]
    public string guess;

    [HideInInspector]
    public GameObject letterHand;
    public GameObject wordPrefab;
    
    audioManagerScript audioHandler;
    //puzzleHandler puzzHandler;
    canvasSc canvasHandler;
    

    public bool sameAnswer = false;

    [SerializeField]
    Transform[,] letters;//PUZZLE LETTERS
    Transform[] letterButtons;
    List<int> puzzleIndexList = new List<int>() {0,1,2,3,4,5};
    

    void Start()
    {
        audioHandler = GameObject.FindGameObjectWithTag("audioManager").GetComponent<audioManagerScript>();
        canvasHandler = GameObject.FindGameObjectWithTag("mainCanvas").GetComponent<canvasSc>();
        //puzzHandler = gameObject.transform.GetChild(0).GetComponent<puzzleHandler>();
        
        //canvasHandler.onGameObjectActivated();
        letters = new Transform[7,7];
        int size = gameObject.transform.GetChild(3).childCount;
        //Debug.Log("Size: " + size);
        letterButtons = new Transform[size];
        canvasHandler.setGLLStartTime(Time.time);
        chapterStart = false;
        //INPUT BUTTONS TO ARRAY
        getInputButtons();
      
  
        setupPuzzle();
        setupInput();
    }

    public void addLevelWordsToDictionary()
    {
        levelAnswers = new Dictionary<int, List<string>>();
        switch (levelIndex)
        {
            case 0:
                levelAnswers.Add(0, new List<string> { "ÇAM", "MAÇ" });
                levelAnswers.Add(1, new List<string> { "AŞK", "KAŞ" });
                levelAnswers.Add(2, new List<string> { "FAL", "LAF" });
                levelAnswers.Add(3, new List<string> { "ŞUT", "TUŞ" });
                levelAnswers.Add(4, new List<string> { "ŞIK", "KIŞ" });
                levelAnswers.Add(5, new List<string> { "ARZ", "ZAR" });
                break;
            case 1:
                levelAnswers.Add(0, new List<string> { "URFA", "FUAR" });
                levelAnswers.Add(1, new List<string> { "PERİ", "PİRE" });
                levelAnswers.Add(2, new List<string> { "PARA", "ARAP", "ARPA" });
                levelAnswers.Add(3, new List<string> { "BAL", "BALO", "ALO" });
                levelAnswers.Add(4, new List<string> { "KATI", "ATKI", "KITA", "ATIK" });
                levelAnswers.Add(5, new List<string> { "SULU", "ULUS", "USLU", "USUL" });
                break;
            case 2:
                levelAnswers.Add(0, new List<string> { "DEHA", "HAD", "HANDE", "DANE", "HANE" });
                levelAnswers.Add(1, new List<string> { "YÜREK", "KÜRE", "ÜRE", "ÜYE", "KÜR" });
                levelAnswers.Add(2, new List<string> { "BAZ", "BEZ", "BEYAZ", "BAY", "BEY", "YAZ" });
                levelAnswers.Add(3, new List<string> { "ASIM", "MAYIS", "SAYIM", "SAYI", "YAS" });
                levelAnswers.Add(4, new List<string> { "YEME", "ELEME", "EYLEME", "EMEL", "EYLEM", "YEL" });
                levelAnswers.Add(5, new List<string> { "YAK", "KAY", "LAKE", "LEYLA", "LALE", "ELA", "KALE", "KAL", "LEYLAK" });
                break;
        }
    }

    public void setup()
    {
        int num;
        int len = 0;
        string inputLetters = "";
        num = levelAnswers[puzzleIndex].Count;
        for (int i = 0; i < num; i++)
        {
            string kel = levelAnswers[puzzleIndex][i];
            currentLevelAnswers.Add(kel);
            if (kel.Length > inputLetters.Length)
            {
                inputLetters = kel;
            }
        }

        currentLevelLetters = new string[inputLetters.Length];

        for (int j = 0; j < inputLetters.Length; j++)
        {
            currentLevelLetters[j] = inputLetters[j].ToString();
        }

        int num2 = levelAnswers[puzzleIndex].Count;
        //Debug.LogError("NUM2: " + num2);
        //Debug.LogError("LEVELINDEX: " + levelIndex + " PUZZLEINDEX: " + puzzleIndex);
        for (int k=0; k<num2;k++)
        {
            //Debug.LogError("k: " + k + " " + "puzzleIndex: " + puzzleIndex);
            string wor = levelAnswers[puzzleIndex][k];
            //Debug.LogError("WOR:" + wor);
            int num3 = currentLevelCoordinates[wor].Count;
            for (int l=0; l<num3; l++)
            {
                int x = (int)currentLevelCoordinates[wor][l].x;
                int y = (int)currentLevelCoordinates[wor][l].y;
                letters[x, y].gameObject.SetActive(true);
            }
        }
    }


    public void setupPuzzle()
    {
        //PUZZLE BUTTONS TO ARRAY
        buttonsToArray();
        
        pickRandomPuzzle();
        cleanAllPuzzleLetters();
        disableAllPuzzleLetters();
      
        loadCurrentLevelCoordinates();
        addLevelWordsToDictionary();
        setup();   
    }

    public void loadCurrentLevelCoordinates()
    {
        switch (levelIndex)
        {
            case 0:
                currentLevelCoordinates = new Dictionary<string, List<Vector2>>();
                
                currentLevelCoordinates.Add("MAÇ", new List<Vector2> { new Vector2(2,1), new Vector2(3,1), new Vector2(4,1)});
                currentLevelCoordinates.Add("ÇAM", new List<Vector2> { new Vector2(4,1), new Vector2(4,2), new Vector2(4,3)});

                currentLevelCoordinates.Add("KAŞ", new List<Vector2> { new Vector2(2,1), new Vector2(2,2), new Vector2(2,3)});
                currentLevelCoordinates.Add("AŞK", new List<Vector2> { new Vector2(4,1), new Vector2(3,1), new Vector2(2,1)});

                currentLevelCoordinates.Add("FAL", new List<Vector2> { new Vector2(2,1), new Vector2(3,1), new Vector2(4,1)});
                currentLevelCoordinates.Add("LAF", new List<Vector2> { new Vector2(4,1), new Vector2(4,2), new Vector2(4,3)});

                currentLevelCoordinates.Add("TUŞ", new List<Vector2> { new Vector2(4,2), new Vector2(4,3), new Vector2(4,4)});
                currentLevelCoordinates.Add("ŞUT", new List<Vector2> { new Vector2(4,4), new Vector2(3,4), new Vector2(2,4)});

                currentLevelCoordinates.Add("ZAR", new List<Vector2> { new Vector2(2,2), new Vector2(2,3), new Vector2(2,4)});
                currentLevelCoordinates.Add("ARZ", new List<Vector2> { new Vector2(2,3), new Vector2(3,3), new Vector2(4,3)});

                currentLevelCoordinates.Add("ŞIK", new List<Vector2> { new Vector2(2,2), new Vector2(2,3), new Vector2(2,4)});
                currentLevelCoordinates.Add("KIŞ", new List<Vector2> { new Vector2(2,4), new Vector2(3,4), new Vector2(4,4)});
                break;
            case 1:
                currentLevelCoordinates = new Dictionary<string, List<Vector2>>();
                currentLevelCoordinates.Add("URFA", new List<Vector2> { new Vector2(2, 1), new Vector2(2, 2), new Vector2(2, 3), new Vector2(2, 4) });
                currentLevelCoordinates.Add("FUAR", new List<Vector2> { new Vector2(2, 3), new Vector2(3, 3), new Vector2(4, 3), new Vector2(5, 3) });

                currentLevelCoordinates.Add("PERİ", new List<Vector2> { new Vector2(2, 1), new Vector2(2, 2), new Vector2(2, 3), new Vector2(2, 4) });
                currentLevelCoordinates.Add("PİRE", new List<Vector2> { new Vector2(2, 1), new Vector2(3, 1), new Vector2(4, 1), new Vector2(5, 1) });

                currentLevelCoordinates.Add("PARA", new List<Vector2> { new Vector2(2, 1), new Vector2(2, 2), new Vector2(2, 3), new Vector2(2, 4) });
                currentLevelCoordinates.Add("ARAP", new List<Vector2> { new Vector2(2, 2), new Vector2(3, 2), new Vector2(4, 2), new Vector2(5, 2) });
                currentLevelCoordinates.Add("ARPA", new List<Vector2> { new Vector2(2, 4), new Vector2(3, 4), new Vector2(4, 4), new Vector2(5, 4) });

                currentLevelCoordinates.Add("BAL", new List<Vector2> { new Vector2(2, 1), new Vector2(3, 1), new Vector2(4, 1) });
                currentLevelCoordinates.Add("BALO", new List<Vector2> { new Vector2(2, 1), new Vector2(2, 2), new Vector2(2, 3), new Vector2(2, 4) });
                currentLevelCoordinates.Add("ALO", new List<Vector2> { new Vector2(1, 3), new Vector2(2, 3), new Vector2(3, 3) });

                currentLevelCoordinates.Add("KATI", new List<Vector2> { new Vector2(1, 1), new Vector2(2, 1), new Vector2(3, 1), new Vector2(4, 1) });
                currentLevelCoordinates.Add("ATKI", new List<Vector2> { new Vector2(2, 1), new Vector2(2, 2), new Vector2(2, 3), new Vector2(2, 4) });
                currentLevelCoordinates.Add("KITA", new List<Vector2> { new Vector2(2, 3), new Vector2(3, 3), new Vector2(4, 3), new Vector2(5, 3) });
                currentLevelCoordinates.Add("ATIK", new List<Vector2> { new Vector2(5, 3), new Vector2(5, 4), new Vector2(5, 5), new Vector2(5, 6) });

                currentLevelCoordinates.Add("SULU", new List<Vector2> { new Vector2(0, 1), new Vector2(1, 1), new Vector2(2, 1), new Vector2(3, 1) });
                currentLevelCoordinates.Add("ULUS", new List<Vector2> { new Vector2(1, 1), new Vector2(1, 2), new Vector2(1, 3), new Vector2(1, 4) });
                currentLevelCoordinates.Add("USLU", new List<Vector2> { new Vector2(3, 1), new Vector2(3, 2), new Vector2(3, 3), new Vector2(3, 4) });
                currentLevelCoordinates.Add("USUL", new List<Vector2> { new Vector2(3, 4), new Vector2(4, 4), new Vector2(5, 4), new Vector2(6, 4) });
                break;
            case 2:
                currentLevelCoordinates = new Dictionary<string, List<Vector2>>();
                currentLevelCoordinates.Add("DEHA", new List<Vector2> { new Vector2(2, 1), new Vector2(2, 2), new Vector2(2, 3), new Vector2(2, 4) });
                currentLevelCoordinates.Add("HAD", new List<Vector2> { new Vector2(1, 3), new Vector2(1, 4), new Vector2(1, 5) });
                currentLevelCoordinates.Add("HANDE", new List<Vector2> { new Vector2(1, 3), new Vector2(2, 3), new Vector2(3, 3), new Vector2(4, 3), new Vector2(5, 3) });
                currentLevelCoordinates.Add("DANE", new List<Vector2> { new Vector2(4, 3), new Vector2(4, 4), new Vector2(4, 5), new Vector2(4, 6), });
                currentLevelCoordinates.Add("HANE", new List<Vector2> { new Vector2(5, 1), new Vector2(5, 2), new Vector2(5, 3), new Vector2(5, 4) });

                currentLevelCoordinates.Add("YÜREK", new List<Vector2> { new Vector2(2, 1), new Vector2(2, 2), new Vector2(2, 3), new Vector2(2, 4), new Vector2(2, 5) });
                currentLevelCoordinates.Add("KÜRE", new List<Vector2> { new Vector2(1, 2), new Vector2(2, 2), new Vector2(3, 2), new Vector2(4, 2) });
                currentLevelCoordinates.Add("ÜRE", new List<Vector2> { new Vector2(0, 4), new Vector2(1, 4), new Vector2(2, 4) });
                currentLevelCoordinates.Add("ÜYE", new List<Vector2> { new Vector2(0, 4), new Vector2(0, 5), new Vector2(0, 6) });
                currentLevelCoordinates.Add("KÜR", new List<Vector2> { new Vector2(2, 5), new Vector2(3, 5), new Vector2(4, 5) });

                currentLevelCoordinates.Add("BAZ", new List<Vector2> { new Vector2(5, 0), new Vector2(5, 1), new Vector2(5, 2) });
                currentLevelCoordinates.Add("BEZ", new List<Vector2> { new Vector2(3, 2), new Vector2(4, 2), new Vector2(5, 2) });
                currentLevelCoordinates.Add("BEYAZ", new List<Vector2> { new Vector2(3, 2), new Vector2(3, 3), new Vector2(3, 4), new Vector2(3, 5), new Vector2(3, 6) });
                currentLevelCoordinates.Add("BAY", new List<Vector2> { new Vector2(1, 4), new Vector2(2, 4), new Vector2(3, 4) });
                currentLevelCoordinates.Add("BEY", new List<Vector2> { new Vector2(1, 4), new Vector2(1, 5), new Vector2(1, 6) });
                currentLevelCoordinates.Add("YAZ", new List<Vector2> { new Vector2(1, 6), new Vector2(2, 6), new Vector2(3, 6) });

                currentLevelCoordinates.Add("ASIM", new List<Vector2> { new Vector2(5, 0), new Vector2(5, 1), new Vector2(5, 2), new Vector2(5, 3) });
                currentLevelCoordinates.Add("MAYIS", new List<Vector2> { new Vector2(3, 1), new Vector2(3, 2), new Vector2(3, 3), new Vector2(3, 4), new Vector2(3, 5) });
                currentLevelCoordinates.Add("SAYIM", new List<Vector2> { new Vector2(1, 3), new Vector2(2, 3), new Vector2(3, 3), new Vector2(4, 3), new Vector2(5, 3) });
                currentLevelCoordinates.Add("SAYI", new List<Vector2> { new Vector2(1, 3), new Vector2(1, 4), new Vector2(1, 5), new Vector2(1, 6) });
                currentLevelCoordinates.Add("YAS", new List<Vector2> { new Vector2(1, 5), new Vector2(2, 5), new Vector2(3, 5) });

                currentLevelCoordinates.Add("YEME", new List<Vector2> { new Vector2(0,0), new Vector2(1,0), new Vector2(2,0), new Vector2(3,0)});
                currentLevelCoordinates.Add("ELEME", new List<Vector2> { new Vector2(1,0), new Vector2(1,1), new Vector2(1,2), new Vector2(1,3), new Vector2(1,4)});
                currentLevelCoordinates.Add("EYLEME", new List<Vector2> { new Vector2(1,2), new Vector2(2,2), new Vector2(3,2), new Vector2(4,2), new Vector2(5,2), new Vector2(6,2)});
                currentLevelCoordinates.Add("EMEL", new List<Vector2> { new Vector2(1,4), new Vector2(2,4), new Vector2(3,4), new Vector2(4,4)});
                currentLevelCoordinates.Add("EYLEM", new List<Vector2> { new Vector2(4,2), new Vector2(4,3), new Vector2(4,4), new Vector2(4,5), new Vector2(4,6)});
                currentLevelCoordinates.Add("YEL", new List<Vector2> { new Vector2(6,1), new Vector2(6,2), new Vector2(6,3)});

                currentLevelCoordinates.Add("YAK", new List<Vector2> { new Vector2(1,0), new Vector2(2,0), new Vector2(3,0)});
                currentLevelCoordinates.Add("KAY", new List<Vector2> { new Vector2(3,0), new Vector2(3,1), new Vector2(3,2) });
                currentLevelCoordinates.Add("LAKE", new List<Vector2> { new Vector2(4,2), new Vector2(4,3), new Vector2(4,4), new Vector2(4,5)});
                currentLevelCoordinates.Add("LEYLA", new List<Vector2> { new Vector2(1,2), new Vector2(1,3), new Vector2(1,4), new Vector2(1,5), new Vector2(1,6), });
                currentLevelCoordinates.Add("LALE", new List<Vector2> { new Vector2(1,5), new Vector2(2,5), new Vector2(3,5), new Vector2(4,5)});
                currentLevelCoordinates.Add("ELA", new List<Vector2> { new Vector2(5,0), new Vector2(5,1), new Vector2(5,2)});
                currentLevelCoordinates.Add("KALE", new List<Vector2> { new Vector2(6,2), new Vector2(6,3), new Vector2(6,4), new Vector2(6,5) });
                currentLevelCoordinates.Add("KAL", new List<Vector2> { new Vector2(4,4), new Vector2(5,4), new Vector2(6,4)});
                currentLevelCoordinates.Add("LEYLAK", new List<Vector2> { new Vector2(1, 2), new Vector2(2, 2), new Vector2(3, 2), new Vector2(4, 2), new Vector2(5, 2), new Vector2(6,2)});
                break;
            default:
                break;
        }
        /*
        foreach (KeyValuePair<string, List<Vector2>> kvp in currentLevelCoordinates)
        {
            List<Vector2> smt = kvp.Value;
            string text = string.Format("BOTTOMFEEDER: "+kvp.Key);
            Debug.Log(text);
            Debug.Log("SMT.COUNT: " + smt.Count);
            for (int i = 0; i<smt.Count;i++)
            {
             string bisi = smt[i].x.ToString() + " // " + smt[i].y.ToString();
              Debug.LogWarning("ANTISOCIAL: " + bisi);
            }
        }*/
    }   
 
    public void setupInput()
    {
            randomlyPlaceLetters();
    }

    public void cleanAllPuzzleLetters()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                letters[i, j].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                //letters[i, j].gameObject.SetActive(false);
            }
        }
    }

    public void disableAllPuzzleLetters()
    {
        for (int i=0; i<7; i++)
        {
            for (int j=0; j<7;j++)
            {
                //letters[i, j].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                letters[i, j].gameObject.SetActive(false);
            }
        }
    }

    public void pickRandomPuzzle()
    {
        int randomNum;
        int range;
            range = puzzleIndexList.Count;
            randomNum = Random.Range(0, range);
            if (puzzleIndexList.Count == 0)
            {
                Debug.Log("SEVİYE BİTTİ!");
                float finishTime = chapterEndTime - chapterStartTime;
                float score = plus - minus - (finishTime / 5f);
                canvasHandler.addScore(levelIndex, puzzleIndex, score);
                canvasHandler.setGLLEndTime(Time.time);
                canvasHandler.loadLevelMenu();
            }
            else
            {
            //FINISH
            if (chapterStart)
            {
                chapterEndTime = Time.time;
                float finishTime = chapterEndTime - chapterStartTime;
                //Debug.LogError("End time!!!: " + chapterEndTime);
                //Debug.LogError("Finish time: " + finishTime);
                //Debug.LogError("PLUS: " + plus);
                //Debug.LogError("MINUS: " + minus);
                float finish = (int)(finishTime / 2f)*(-1);
                //Debug.LogError("FINISH/2" + finish);
                float bisi;
                bisi = (-1 * minus) - finish;
                //Debug.LogError("EX: " + bisi.ToString());
                float score = plus - bisi;
                //Debug.Log("SCOREEEEEEEE: " + score);
                canvasHandler.addScore(levelIndex, puzzleIndex, score);
                plus = 0;
                minus = 0;
            }
                chapterStart = true;
                puzzleIndex = puzzleIndexList[randomNum];
                puzzleIndexList.Remove(puzzleIndex);
                gameObject.transform.FindChild("Header").GetChild(0).GetComponent<TextMeshProUGUI>().text = "Bölüm " + (puzzleIndex + 1);
                //START
                chapterStartTime = Time.time;
                Debug.LogError("START TIME!!: " + chapterStartTime);
            }
        
        Debug.LogError("RAAAAAAAAAAAAAAAAANGE: " + range);
        Debug.Log("START LEVEL INDEX: " + levelIndex);
        Debug.Log("START PUZZLE INDEX: " + puzzleIndex);
    }

    //PUT INPUT BUTTONS TO  AN ARRAY
    public void getInputButtons()
    {
        for (int i=0; i<letterButtons.Length;i++)
        {
            letterButtons[i] = gameObject.transform.GetChild(3).GetChild(i);
            //Debug.Log("ADFADFADFAD" + letterButtons[i]);
        }
    }

    public void disableAllButtons()
    {
        for (int i=0;i<letterButtons.Length;i++)
        {
            letterButtons[i].gameObject.SetActive(false);
        }
    }
    
    public void randomlyPlaceLetters()
    {
        int num;
        switch (levelIndex)
        {
            case 0:
                //int range = currentLevelLetters.Length;
                //Debug.Log("gözlerimi: " + range);
                for (int k = 2; k >= 0; k--)
                {
                    int rnd = Random.Range(0, (k + 1));
                  //  Debug.Log("rnd: " + rnd);
                    string temp = currentLevelLetters[k];
                    currentLevelLetters[k] = currentLevelLetters[rnd];
                    currentLevelLetters[rnd] = temp;
                    //range--;
                }
                for (int i=0; i<letterButtons.Length; i++)
                {
                    if (i==0 || i==2 || i==6)
                    {
                        letterButtons[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        letterButtons[i].GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                        letterButtons[i].gameObject.SetActive(false);
                    }
                }
                letterButtons[0].GetChild(0).GetComponent<TextMeshProUGUI>().text = currentLevelLetters[0];
                letterButtons[2].GetChild(0).GetComponent<TextMeshProUGUI>().text = currentLevelLetters[1];
                letterButtons[6].GetChild(0).GetComponent<TextMeshProUGUI>().text = currentLevelLetters[2];
                break;
            case 1:
                num = currentLevelLetters.Length - 1;
                for (int k = num; k >= 0; k--)
                {
                    int rnd = Random.Range(0, (k + 1));
                  //  Debug.Log("rnd: " + rnd);
                    string temp = currentLevelLetters[k];
                    currentLevelLetters[k] = currentLevelLetters[rnd];
                    currentLevelLetters[rnd] = temp;
                }
                for (int i = 0; i < letterButtons.Length; i++)
                {
                    if (i == 0 || i == 2 || i == 6 || i== 4)
                    {
                        letterButtons[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        letterButtons[i].GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                        letterButtons[i].gameObject.SetActive(false);
                    }
                }
                letterButtons[0].GetChild(0).GetComponent<TextMeshProUGUI>().text = currentLevelLetters[0];
                letterButtons[2].GetChild(0).GetComponent<TextMeshProUGUI>().text = currentLevelLetters[1];
                letterButtons[4].GetChild(0).GetComponent<TextMeshProUGUI>().text = currentLevelLetters[2];
                letterButtons[6].GetChild(0).GetComponent<TextMeshProUGUI>().text = currentLevelLetters[3];
                break;
            case 2:
                 num = currentLevelLetters.Length - 1;
                for (int k = num; k >= 0; k--)
                {
                    int rnd = Random.Range(0, (k + 1));
                 //   Debug.Log("rnd: " + rnd);
                    string temp = currentLevelLetters[k];
                    currentLevelLetters[k] = currentLevelLetters[rnd];
                    currentLevelLetters[rnd] = temp;
                }
                for (int i = 0; i < letterButtons.Length; i++)
                {
                    if (i<currentLevelLetters.Length)
                    {
                        letterButtons[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        letterButtons[i].GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                        letterButtons[i].gameObject.SetActive(false);
                    }
                }
                for (int i=0;i<currentLevelLetters.Length;i++)
                {
                    letterButtons[i].GetChild(0).GetComponent<TextMeshProUGUI>().text = currentLevelLetters[i];
                }
                break;
            default:
                break;
        }
    }    

    //PUT PUZZLE BUTTONS TO  AN ARRAY
    public void buttonsToArray()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                //Debug.Log(gameObject.transform.GetChild(0).GetChild(i + j));
                string num = i.ToString() + j.ToString();
                //Debug.Log(num);
                letters[i,j] = gameObject.transform.GetChild(0).FindChild(num);
            }
        }
    }

    public void clearButtons()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
              //  Debug.Log(letters[i, j]);
               letters[i, j].GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
            }
        }
    }
 
    public IEnumerator wakemUpALittle()
    {
        correctAnswer();
        yield return new WaitForSeconds(0.5f);
        currentFoundGuess.Clear();
        currentLevelAnswers.Clear();
        //correctAnswer();
        setupPuzzle();
        setupInput();
        correctGuessNumber = 0;
        reloadGuess();
    }

    public void displayPuzzleLettersV2(string guess) {

        List<Vector2> vec = currentLevelCoordinates[guess];
        for (int i = 0; i < vec.Count; i++)
        {
            int x = (int)vec[i].x;
            int y = (int)vec[i].y;
            TextMeshProUGUI text = letters[x, y].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            text.text = guess[i].ToString();
           //Debug.LogError(text.text);
        }
       
    }

    public void checkGuess(string guess)
    {    
                if (currentLevelCoordinates.ContainsKey(guess))
                {
                    if (currentFoundGuess.Contains(guess))
                    {
                        //play
                        //Debug.Log("BRUH");
                        audioHandler.playSameCorrectAnswer();
                        reloadGuess();
                    }
                    else
                    {
                       // Debug.Log("GUESS: " + guess);
                        correctGuessNumber++;
                //printGuess();
                //Debug.LogError("CURRENT LEVEL ANSWERS NUMBERS: " + currentLevelAnswers.Count);
                        if (correctGuessNumber == currentLevelAnswers.Count)
                        {
                    //letDisplayLetter = true;
                            plus += guess.Length * 5;
                            correctAnswer();
                            displayPuzzleLettersV2(guess);
                            StartCoroutine(wakemUpALittle());
                        }
                        else
                        {
                            plus += guess.Length * 5;
                            correctAnswer();
                            currentFoundGuess.Add(guess);
                            //letDisplayLetter = true;
                            displayPuzzleLettersV2(guess);
                            reloadGuess();
                        }
                    }
                }
                else
                {
                
                    minus -= guess.Length * 1;
                    wrongAnswer();
                    reloadGuess();
                }           
    }

    

    public void correctAnswer()
    {
        audioHandler.playCorrect();
        //Do something in puzzle
     
    }

    public void wrongAnswer()
    {
        audioHandler.playWrong();
       
        //Do something in puzzle
    }

    public void shuffleButtonClicked()
    {
        //audioHandler.playBack();
        randomlyPlaceLetters();
    }

    public void reloadGuess()
    {
        GameObject.FindGameObjectWithTag("guessText").transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "TAHMİN: ";
        GameObject panel = GameObject.FindGameObjectWithTag("inputPanel");
        //Debug.Log(2);
        int children = panel.transform.childCount;
        for (int i = 0; i < children; i++)
        {
            //Debug.Log("Refreshing guess...");
            panel.transform.GetChild(i).GetComponent<Button>().interactable = true;
        }
    }

    void Update()
    {
       
    }
}

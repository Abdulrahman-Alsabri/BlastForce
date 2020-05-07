using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    static Text coinsText;
    static Text scoreText;
    static Text statusText;
    static Text timerText;
    static Image rankImage;
    static Image rankBGImage;
    static Sprite rankS;
    static Sprite rankA;
    static Sprite rankB;
    static Sprite rankC;
    static Sprite rankD;
    static int score = 0;
    private bool isScore = true;

    static float timeForBonusKills = 3f;

    static int totalBasic = 0;
    static int totalFrontSpike = 0;
    static int totalFlying = 0;
    static int totalChasing = 0;

    static int availableCoins = 0;
    static int totalCoins = 0;
    static int coinsCount = 0;

    static int pointsBasic = 10;
    static int pointsFrontSpike = 15;
    static int pointsFlying = 20;
    static int pointsChasing = 25;
    static int pointsBoss = 30;

    static int multiplierGoons = 0;
    static bool startTimerBasic = false;
    static float timer = 0;

    static float timerStartTime = 60f;
    static float timerCurrentTime = 60f;
    public bool triggerTimer = false;

    // Start is called before the first frame update
    void Start()
    {
        // searches for scoreText
        GameObject temp = GameObject.Find("Score");
        if (temp != null)
        {
            // gets scoreText
            scoreText = temp.GetComponent<Text>();
        }
        else
        {
            Debug.Log("ScoreText not found");
        }


        // searches for coinsText
        GameObject temp1 = GameObject.Find("Coins");
        if (temp1 != null)
        {
            // gets statusText
            coinsText = temp1.GetComponent<Text>();
        }
        else
        {
            Debug.Log("StatusText not found");
        }


        // searches for statusText
        GameObject temp2 = GameObject.Find("Status");
        if (temp2 != null)
        {
            // gets statusText
            statusText = temp2.GetComponent<Text>();
        }
        else
        {
            Debug.Log("StatusText not found");
        }


        // searches for timerText
        GameObject temp3 = GameObject.Find("Timer");
        if (temp3 != null)
        {
            // gets statusText
            timerText = temp3.GetComponent<Text>();
        }
        else
        {
            Debug.Log("StatusText not found");
        }


        // searches for rankImage
        GameObject temp4 = GameObject.Find("Rank");
        if (temp4 != null)
        {
            // gets statusText
            rankImage = temp4.GetComponent<Image>();
        }
        else
        {
            Debug.Log("RankImage not found");
        }


        // searches for rankBGImage
        GameObject temp5 = GameObject.Find("RankBG");
        if (temp4 != null)
        {
            // gets statusText
            rankBGImage = temp5.GetComponent<Image>();
        }
        else
        {
            Debug.Log("RankBGImage not found");
        }

        // Search for rank images
        rankS = Resources.Load<Sprite>("rankS");
        rankA = Resources.Load<Sprite>("rankA");
        rankB = Resources.Load<Sprite>("rankB");
        rankC = Resources.Load<Sprite>("rankC");
        rankD = Resources.Load<Sprite>("rankD");


        timerCurrentTime = timerStartTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isScore)
        {
            scoreText.text = "Score: " + score;
            if (availableCoins != 0)
            {
                coinsText.text = "Coins: " + coinsCount + "/" + availableCoins;
            }
            else
            {
                coinsText.text = "";
            }
        }

        if (startTimerBasic)
        {
            timer += Time.deltaTime;
        }

        if (triggerTimer)
        {
            triggerTimer = false;
            InvokeRepeating("TimerEffects", 0f, 1f);
        }
    }
    
    public void StopTimerEffects()
    {
        CancelInvoke("TimerEffects");
    }

    void TimerEffects()
    {
        timerCurrentTime -= 1;
        if (timerCurrentTime >= 0)
        { 
            string updatedStatus = "Timer: 00:" + timerCurrentTime.ToString("0") + "   ";
            timerText.text = updatedStatus;

            score -= 1;
            statusText.text = "-" + 1 + " every second Due to Timer's Effects";
        }
        else
        {
            string updatedStatus = "Timer: -00:" + Mathf.Abs(timerCurrentTime).ToString("0") + "   ";
            timerText.text = updatedStatus;
            timerText.color = Color.red;

            score -= 5;
            statusText.text = "-" + 5 + " every second For Being Too Late!";
        }
    }

    public void Winning()
    {
        string updatedStatus = "Congratulations! You've made it! Your Score is: " + score;

        isScore = false;
        scoreText.text = "";

        statusText.fontSize = 44;
        statusText.text = updatedStatus;

        if (score >= 1000)
        {
            if (coinsCount == availableCoins)
            {
                rankImage.sprite = rankS;
            }
            else
            {
                rankImage.sprite = rankA;
            }
        }
        else if (score < 1000 && score >= 800)
        {
            if (coinsCount - 1 >= availableCoins)
            {
                rankImage.sprite = rankA;
            }
            else
            {
                rankImage.sprite = rankB;
            }
        }
        else if (score < 800 && score >= 600)
        {
            if (coinsCount - 2 >= availableCoins)
            {
                rankImage.sprite = rankB;
            }
            else
            {
                rankImage.sprite = rankC;
            }
        }
        else if (score < 600 && score >= 400)
        {
            if (coinsCount - 3 >= availableCoins)
            {
                rankImage.sprite = rankC;
            }
            else
            {
                rankImage.sprite = rankD;
            }
        }
        else if (score < 400 && score >= 200)
        {
            if (coinsCount - 4 >= availableCoins)
            {
                rankImage.sprite = rankD;
            }
            // else
            // {

            // }
        }

        rankImage.sprite = rankS;
        rankBGImage.enabled = true;
        rankImage.enabled = true;
    }

    public void EditStatus(string newStatus)
    {
        StartCoroutine(StatusHelper(newStatus));
    }

    IEnumerator StatusHelper(string givenStatus)
    {
        statusText.text = givenStatus;
        yield return new WaitForSeconds(4);
        statusText.text = "";
    }

    public void playerDamaged(string goonName)
    {
        int deductedPoints = 0;

        if (goonName == "BasicGoon")
        {
            // Basic Goon Doesn't Hurt
            // score -= pointsBasic;
            // deductedPoints = pointsFrontSpike;
        }
        else if (goonName == "FrontSpikeGoon")
        {
            deductedPoints = pointsFrontSpike;
            score -= deductedPoints;
        }
        else if (goonName == "FlyingGoon")
        {
            deductedPoints = pointsFlying;
            score -= deductedPoints;
        }
        else if (goonName == "ChasingGoon")
        {
            deductedPoints = pointsChasing;
            score -= deductedPoints;
        }
        else if (goonName == "BigBoss")
        {
            deductedPoints = pointsBoss;
            score -= deductedPoints;
        }

        string updatedStatus = "-" + deductedPoints.ToString() + " Damage From " + GetGoonName(goonName);
        EditStatus(updatedStatus);
    }

    public void addToKills(string goonName)
    {
        if (goonName == "BasicGoon")
        {
            totalBasic += 1;
            startTimerBasic = true;

            HandleScoreMultiplier(pointsBasic, goonName);
        }
        else if (goonName == "FrontSpikeGoon")
        {
            totalFrontSpike += 1;
            startTimerBasic = true;

            HandleScoreMultiplier(pointsFrontSpike, goonName);
        }
        else if (goonName == "FlyingGoon")
        {
            totalFlying += 1;
            startTimerBasic = true;

            HandleScoreMultiplier(pointsFlying, goonName);
        }
        else if (goonName == "ChasingGoon")
        {
            totalChasing += 1;
            startTimerBasic = true;

            HandleScoreMultiplier(pointsChasing, goonName);
        }
    }

    public void setCoins(string sceneName)
    {
        if (sceneName == "Tutorial_2_NormalJump")
        {
            availableCoins = 1;

            string updatedStatus = "Look for a hidden golden coin.";
            EditInstruction(updatedStatus);
        }
        else if (sceneName == "Level1")
        {
            availableCoins = 6;

            string updatedStatus = "Look for those hidden golden coins.";
            EditInstruction(updatedStatus);
        }
        else
        {
            coinsCount = 0;
            availableCoins = 0;
        }
    }

    public void addToCoins()
    {
        totalCoins += 1;
        coinsCount += 1;

        string updatedStatus = "Congratulations! You have found a golden coin.";
        EditInstruction(updatedStatus);
    }

    public void BossDamaged(int phaseNumber)
    {
        if (phaseNumber == 4)
        {
            int addedPoints = pointsBoss;
            score += addedPoints;

            string updatedStatus = "+" + addedPoints.ToString() + " - Successfully Destroyed Level Boss!";
            EditStatus(updatedStatus);
        }
        else
        {
            string updatedStatus = "Phase " + phaseNumber + " - Successfully Hit Weak Point - 1 Flying Goon Is Spawned";
            EditStatus(updatedStatus);
        }
    }

    public void BossAreaTrigger()
    {
        string updatedStatus = "Warning: You've Entered Boss's Area - Hint: Aim for his back's weak point";
        EditInstruction(updatedStatus);
    }

    public void MainVillianTrigger()
    {
        string updatedStatus = "This World is Endangered. The Villian Is Activating Self Destruction. Escape to the Start.";
        EditInstruction(updatedStatus);
    }

    public void EndTimerMessage()
    {
        string updatedStatus = "The timer starts now. Escape faster to keep your score high!";
        EditInstruction(updatedStatus);
    }

    public void TriggerTimer()
    {
        string updatedStatus = "Timer: 01:00" + timerCurrentTime.ToString("0") + "   ";
        timerText.text = updatedStatus;

        triggerTimer = true;
    }

    public void EditInstruction(string newStatus)
    {
        StartCoroutine(InstructionalStatus(newStatus));
    }

    IEnumerator InstructionalStatus(string givenStatus)
    {
        statusText.text = givenStatus;
        yield return new WaitForSeconds(15);
        statusText.text = "";
    }

    void HandleScoreMultiplier(int goonPoints, string goonName)
    {
        if (timer <= timeForBonusKills)
        {
            multiplierGoons += 1;

            int addedPoints = multiplierGoons * goonPoints;
            score += addedPoints;

            string updatedStatus;

            if (multiplierGoons == 1)
            {
                updatedStatus = "+" + addedPoints.ToString() + " For Destroying " + multiplierGoons.ToString() + " " + GetGoonName(goonName);
                EditStatus(updatedStatus);
            }
            else
            {
                updatedStatus = "+" + goonPoints.ToString() + " ×" + multiplierGoons.ToString() + " For Destroying " + multiplierGoons.ToString() + " Goons in a Row";
                EditStatus(updatedStatus);
            }

        }
        else
        {
            startTimerBasic = false;
            timer = 0;
            multiplierGoons = 0;

            int addedPoints = 1 * goonPoints;
            score += addedPoints;

            string updatedStatus = "+" + addedPoints.ToString() + " For Destroying " + 1 + " " + GetGoonName(goonName);
            EditStatus(updatedStatus);
        }
    }

    string GetGoonName(string goonName)
    {
        string result = "";

        if (goonName == "BasicGoon")
        {
            result = "Basic Goon";
        }
        else if (goonName == "FrontSpikeGoon")
        {
            result = "Front-Spike Goon";
        }
        else if (goonName == "FlyingGoon")
        {
            result = "Flying Goon";
        }
        else if (goonName == "ChasingGoon")
        {
            result = "Chasing Goon";
        }
        else if (goonName == "BigBoss")
        {
            result = "Big Boss";
        }
        

        return result;
    }

    public void FinishedTutorial(string tutorialName)
    {
        string sceneName = "";

        if (tutorialName == "Tutorial_1_ShootingBlocks")
        {
            sceneName = "Tutorial 1: Shooting Blocks";
        }
        else if (tutorialName == "Tutorial_2_NormalJump")
        {
            sceneName = "Tutorial 2: Normal Jump";
        }
        else if (tutorialName == "Tutorial_3_RocketJump")
        {
            sceneName = "Tutorial 3: Rocket Jump";
        }
        else if (tutorialName == "Tutorial_4_Crouching")
        {
            sceneName = "Tutorial 4: Crouch";
        }
        else if (tutorialName == "Tutorial_5_ShootingEnemies")
        {
            sceneName = "Tutorial 5: Shooting Enemies";
        }
        else if (tutorialName == "Tutorial_6_ChargingBlocks")
        {
            sceneName = "Tutorial 6: Dashing Blocks";
        }
        else if (tutorialName == "Tutorial_7_ChargingEnemies")
        {
            sceneName = "Tutorial 7: Dashing Enemies";
        }

        int addedPoints = 25;
        score += addedPoints;

        string updatedStatus = "+" + addedPoints.ToString() + " for finishing " + sceneName;
        EditStatus(updatedStatus);
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /***game win, lose, restart***/
    bool gameWon = false;
    bool gameLost = false;

    //bool gameHasEnded = false;
    public float restartDelay = 1f;

    /***fruit count stuff ***/
    public int numFruits1 = 0;
    public int numFruits2 = 0;

    /***Texts ***/
    public Text fruitCounter1;
    public Text fruitCounter2;
    public Text shotTimer1;
    public Text shotTimer2;

    /***end fruit count stuff***/

    /*** gui***/
    //private GUIStyle style = new GUIStyle();

    public Transform player1;
    public Transform player2;
    public Transform targetPlayer;
    public Transform witch;
    /**public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            Invoke("Restart", restartDelay);
        }
    }**/

    private void Start()
    {
        fruitCounter1.text = "Fruit Count: 0";
        fruitCounter2.text = "Fruit Count: 0";
        shotTimer1.text = "Charge Full";
        shotTimer2.text = "Charge Full";

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }

        if (Input.GetKey(KeyCode.Q))
        {
            Application.Quit();
        }

        if (numFruits1 == 25 || numFruits2 == 25)
        {
            gameWon = true;
            //EndGame();

            Debug.Log("GAME WON");
            Invoke("Restart", restartDelay);
        }

        if (player1.GetComponent<SpawnLightOrbP1>().getSpawnTimer() != player1.GetComponent<SpawnLightOrbP1>().getReloadTime())
        {
            shotTimer1.text = "Charging ... " + player1.GetComponent<SpawnLightOrbP1>().getSpawnTimer().ToString("f0");
        }
        else
        {
            shotTimer1.text = "Charge Full";
        }

        if (player2.GetComponent<SpawnLightOrbP2>().getSpawnTimer() != player2.GetComponent<SpawnLightOrbP2>().getReloadTime())
        {
            shotTimer2.text = "Charging ... " + player2.GetComponent<SpawnLightOrbP2>().getSpawnTimer().ToString("f0");
        }
        else
        {
            shotTimer2.text = "Charge Full";
        }

        fruitCounter1.text = "Fruit Count: " + player1.GetComponent<PlayerController>().getFruitCounter();
        fruitCounter2.text = "Fruit Count: " + player2.GetComponent<PlayerController>().getFruitCounter();
    }

    public void GameLost()
    {
        if (gameLost == false)
        {
            gameLost = true;
            Debug.Log("GAME OVER");
            Invoke("Restart", restartDelay);
        }
    }

    void Restart()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        SceneManager.LoadScene("PlayerControls");
        numFruits1 = 0;
        numFruits2 = 0;
        gameWon = false;
        gameLost = false;
        //gameHasEnded = false;
    }

    public void setTargetPlayer(Transform target)
    {
        this.targetPlayer = target;
    }

    public Transform getTargetPlayer()
    {
        return this.targetPlayer;
    }

    /***fruit increment***/
    public void UpdateFruitCount(int playerNum)
    {
        if (playerNum == 1)
        {
            numFruits1 += 1;
        }

        if (playerNum == 2)
        {
            numFruits2 += 1;
        }
    }

    /***end of fruit increment***/

    /*** GUI display ***/
    private void OnGUI()
    {
        //style.fontSize = 100;
        if (gameWon)
        {
            //GUI.Label(new Rect(100, 100, 100, 200), "You Win!", style);
        }
        else if (gameLost)
        {
            //GUI.Label(new Rect(100, 100, 100, 200), "You Lose!", style);
        }

        //style.fontSize = 20;
        //GUI.Label(new Rect(0, 0, 50, 50), "Fruit Count: " + numFruits1, style);
        //GUI.Label(new Rect(70, 0, 50, 50), "Fruit Count: " + numFruits2, style);
    }

    /***end of GUI display***/

    
    public Transform getWitch()
    {
        return this.witch;
    }

    
}

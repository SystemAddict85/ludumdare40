using System.Collections;
using UnityEngine;

public static class Global
{
    public static bool Paused = false;
    public static bool isGameStarted = false;
    public static int remainingLives = 3;

    public static void CallDialog(string text)
    {
        GameManager.Instance.UI.dialogBox.CallDialogBox(text);
    }    
    
    public static void ShowMessageBox(string message)
    {
        GameManager.Instance.UI.MessageBoxText.text = message;
        GameManager.Instance.UI.MessageBox.gameObject.SetActive(true);
    }

    public static void HideMessageBox()
    {
        GameManager.Instance.UI.MessageBox.gameObject.SetActive(false);
    }

    public static IEnumerator ChangeScore(int scoreChange)
    {
        GameManager.Instance.UI.score.ChangeScore(scoreChange);
        GameManager.Instance.UI.addScore.text.text = scoreChange > 0 ? '+' + scoreChange.ToString() : scoreChange.ToString();
        GameManager.Instance.UI.addScore.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.25f);
        GameManager.Instance.UI.addScore.gameObject.SetActive(false);
        
    }

    public static void GameOver()
    {
        Debug.Log("game over");
        remainingLives = 3;
        isGameStarted = false;
        Paused = false;
        MonoBehaviour.Destroy(GameManager.Instance);
        UnityEngine.SceneManagement.SceneManager.LoadScene("gameover");
    }

    public static void LowerLife()
    {
        --remainingLives;
        GameManager.Instance.UI.lifeBar.LowerLife();
        if(remainingLives <= 0)
        {
            GameOver();
        }
    }
    
}


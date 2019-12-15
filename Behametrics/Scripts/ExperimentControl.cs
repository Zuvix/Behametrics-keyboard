using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ExperimentControl : MonoBehaviour
{
    public string username;
    public string dominantHand;
    public string gender;
    public int count = 0;
    public TMP_InputField userInput;
    public TMP_Dropdown handInput;
    public TMP_Dropdown genderInput;

    public GameObject experimentPanel;
    public GameObject CorrectPanel;
    public TMP_Text funfactTxt;
    public GameObject WrongPanel;
    public GameObject EndPanel;

    public TMP_Text pwdTxt;
    public TMP_InputField pwdInput;
    public TMP_Text counterTxt;
    private PwdGen PwdGen=new PwdGen();
    private KeyLogger logger;
    string pwd;
    public List<KeyVector> data=new List<KeyVector>();
    private void Awake()
    {
        logger = this.gameObject.GetComponent<KeyLogger>();
    }
    public void SetUserInfo()
    {
        username = userInput.text;
        gender = genderInput.options[genderInput.value].text;
        dominantHand = handInput.options[handInput.value].text;
    }
    public void StartExperiment()
    {
        NewPassword();
    }
    public void EndExperiment()
    {
        //TODO save Data
    }
    public void ResolvePassword()
    {
        logger.TurnOff();
        if (pwd.Equals(pwdInput.text))
        {
            count++;
            counterTxt.text = count.ToString() + "/10";
            if (count >= 10 )
            {
                experimentPanel.SetActive(false);
                EndPanel.SetActive(true);
            }
            else
            {
                CorrectPanel.SetActive(true);
                funfactTxt.text = PwdGen.RandomFact();
            }
        }
        else
        {
            WrongPanel.SetActive(true);
        }
        if (count <= 10)
        {
            pwd = "";
            pwdInput.text = "";
            pwdTxt.text = "";
        }
    }
    public void NewPassword()
    {
        pwd = PwdGen.GeneratePassword(3, 2, 2, 6);
        pwdTxt.text = pwd;
        logger.TurnOn();
    }

    public void SaveAndRestart()
    {
        SaveData s = new SaveData();
        s.SaveAllData(data, username, dominantHand, gender);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}

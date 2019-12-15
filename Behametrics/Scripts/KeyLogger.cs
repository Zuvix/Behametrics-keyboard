using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Globalization;

public class KeyLogger : MonoBehaviour
{
    readonly char[] smallLetters = "abcdefghjkmnopqrstuvwxyz".ToCharArray();
    readonly char[] capitalLetters = "ABCDEFGHJKMNOPQRSTUVWXYZ".ToCharArray();
    readonly char[] numbers = "1234567890".ToCharArray();
    ExperimentControl control;
    char selectedKey = '*';
    KeyCode anyKey;
    KeyCode selectedKeyCode = KeyCode.CapsLock;
    List<KeyVector> KeyLogs;
    KeyVector currentKey;

    char lastKey;
    float FT = 0;
    bool fingerFlying = false;
    float timeKeyPressed = 0;
    float lastKeyPressed = 0;
    int keyId = 0;
    int updateCount = 0;
    string type = "";

   bool LoggerOn=false;

    private void Awake()
    {
        control = GameObject.Find("ExperimentControl").GetComponent<ExperimentControl>();
        KeyLogs = control.data;
    }
    /*void OnGUI()
    {
        if (LoggerOn)
        {
            Event e = Event.current;
            if (e.character >= 'a' && e.character <= 'z' || e.character >= 'A' && e.character <= 'Z' || e.character >= '0' && e.character <= 9)
            {
                selectedKey = e.character;
                //Debug.Log("Detected key code: " + e.keyCode + "   "+e.character);
                if (smallLetters.Contains(e.character))
                {
                    type = "small";
                    keyId++;
                }
                if (capitalLetters.Contains(e.character))
                {
                    type = "capital";
                    keyId++;
                }
                if (numbers.Contains(e.character))
                {

                    type = "num";
                    keyId++;
                }
                /*if ((e.keyCode >= KeyCode.A && e.keyCode <= KeyCode.Z) || (e.keyCode >= KeyCode.Alpha0 && e.keyCode <= KeyCode.Alpha9) || (e.keyCode >= KeyCode.Keypad0 && e.keyCode <= KeyCode.Keypad9))
                {
                    selectedKeyCode = e.keyCode;
                }
                anyKey = e.keyCode;
            }
            else
                selectedKey = '*';
           
        }
    }*/
    void Update()
    {
        if (LoggerOn && TouchScreenKeyboard.visible)
        {
            //Debug.Log(selectedKey);
            if (fingerFlying == true)
            {
                FT += Time.deltaTime;
            }
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    Debug.Log("begun");
                    fingerFlying = false;
                    currentKey = new KeyVector();
                    timeKeyPressed += Time.deltaTime;
                    currentKey.xCoordinate = Input.touches[0].position.x;
                    currentKey.yCoordinate = Input.touches[0].position.y;
                    currentKey.pressureDown = Input.touches[0].pressure;
                    currentKey.sizeDown = Input.touches[0].radius;
                    MeasureSensors();
                    updateCount++;
                    currentKey.timeFT1 = FT;
                    currentKey.pwdID = control.count;
                    FT = 0;

                }
                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    MeasureSensors();
                    updateCount++;
                    timeKeyPressed += Time.deltaTime;
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    Debug.Log("Ended");
                    fingerFlying = true;
                    currentKey.userName = control.username;
                    currentKey.timestamp = DateTime.Now;

                    currentKey.timeDT = timeKeyPressed;
                    currentKey.keyId = keyId;
                    if (lastKeyPressed != 0)
                    {
                        currentKey.timeFT2 = currentKey.timeFT1 + currentKey.timeDT;
                        currentKey.timeFT3 = lastKeyPressed + currentKey.timeFT1;
                        currentKey.timeFT4 = lastKeyPressed + currentKey.timeFT1 + currentKey.timeDT;
                        currentKey.lastKey = lastKey;
                    }
                    currentKey.keyPressed = selectedKey;
                    if (Input.touchCount > 0)
                    {
                        currentKey.pressureUp = Input.touches[0].pressure;
                        currentKey.sizeUp = Input.touches[0].radius;
                    }
                    currentKey.gyroscopeX /= updateCount;
                    currentKey.gyroscopeY /= updateCount;
                    currentKey.gyroscopeZ /= updateCount;
                    currentKey.accelX /= updateCount;
                    currentKey.accelY /= updateCount;
                    currentKey.accelZ /= updateCount;
                    currentKey.rotX /= updateCount;
                    currentKey.rotY /= updateCount;
                    currentKey.rotZ /= updateCount;

                    lastKeyPressed = timeKeyPressed;
                    timeKeyPressed = 0;
                    lastKey = selectedKey;
                    updateCount = 0;
                    Debug.Log(currentKey.keyPressed + "  " + currentKey.timeDT + "  " + currentKey.timeFT1);
                    currentKey.keyPressed = selectedKey;
                    lastKey = selectedKey;
                    KeyLogs.Add(currentKey);


                }
            }

        }
    }
    public void MeasureSensors()
    {
        if (SystemInfo.supportsGyroscope) {
            currentKey.gyroscopeX += Input.gyro.attitude.x;
            currentKey.gyroscopeY += Input.gyro.attitude.y;
            currentKey.gyroscopeZ += Input.gyro.attitude.z;
            currentKey.rotX += Input.gyro.rotationRate.x;
            currentKey.rotY += Input.gyro.rotationRate.y;
            currentKey.rotZ += Input.gyro.rotationRate.z;
        }
        if (SystemInfo.supportsAccelerometer) {
            currentKey.accelX += Input.acceleration.x;
            currentKey.accelY += Input.acceleration.y;
            currentKey.accelZ += Input.acceleration.z;
        }
    }
    void Restart()
    {
        FT = 0;
        fingerFlying = false;
        timeKeyPressed = 0;
        lastKeyPressed = 0;
        keyId = 0;
        updateCount = 0;
        type = "";
        selectedKey = '*';
        selectedKeyCode = KeyCode.CapsLock;
    }
    public void TurnOff()
    {
        Restart();
        LoggerOn = false;

    }
    public void TurnOn()
    {
        Restart();
        LoggerOn = true;
    }
    public void SetLastKey()
    {
       /* string key=control.pwdInput.text;
        selectedKey = key.Substring(key.Length - 1)[0];
        Debug.Log(selectedKey);
        if (selectedKey >= 'a')
            Debug.Log("viac jak a");
        if(selectedKey>='a'&& selectedKey<='z')
        {
            Debug.Log("medzi a z");
            currentKey.keyPressed = selectedKey;
            lastKey = selectedKey;
            KeyLogs.Add(currentKey);
            Debug.Log(currentKey.xCoordinate+" +x");
            Debug.Log(currentKey.timeDT + " time");
        }
        else
        {
            fingerFlying = true;
            timeKeyPressed = 0;
            currentKey = new KeyVector();
            updateCount = 0;
        }*/
    }
}

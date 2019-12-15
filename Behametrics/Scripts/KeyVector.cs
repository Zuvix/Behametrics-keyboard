using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class KeyVector
{
    //Header
    public int pwdID=0;
    public int keyId=0;
    public string userName="";
    public DateTime timestamp=DateTime.Now;

    public float xCoordinate=0;
    public float yCoordinate=0;
    public float timeDT=0;
    //time oneUP to secondDown
    public float timeFT1=0;
    //time oneUP to secondUp
    public float timeFT2=0;
    //time oneDown to oneDown
    public float timeFT3=0;
    //time oneDown secondUP
    public float timeFT4=0;

    public char keyPressed='*';
    public char lastKey='*';
    public float pressureUp=0;
    public float pressureDown = 0;
    public float sizeDown = 0;
    public float sizeUp = 0;
    public float gyroscopeX = 0;
    public float gyroscopeY = 0;
    public float gyroscopeZ = 0;
    public float accelX = 0;
    public float accelY = 0;
    public float accelZ = 0;
    public float rotX = 0;
    public float rotY = 0;
    public float rotZ = 0;

}

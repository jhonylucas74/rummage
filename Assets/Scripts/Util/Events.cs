using UnityEngine;

public static class Events {
    public static IntEvent OnSetPlayerTurn;
    public static SimpleEvent OnCheckHand;
    public static SimpleEvent OnEmptyHand;
    public static SimpleEvent OnFindHand;
}

public delegate void SimpleEvent();
public delegate void IntEvent(int i);
public delegate void FloatEvent(float f);
public delegate void BoolEvent(bool b);
public delegate void StringEvent(string s);
public delegate void Vector3Event(Vector3 v3);

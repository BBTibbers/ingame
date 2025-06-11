using System;
using UnityEngine;

public enum ECurrencyType
{
    Gold,
    Diamond,

    Count
}

public class Currency 
{
    //'도메인' (컨텐츠, 지식, 문제, 기획서 바탕으로 작성)
    // 도메인 클래스로 구현하면 표현력이 좋아지고, 종류와 값 모두 표현할 수 있다.
    private ECurrencyType _type;
    public ECurrencyType Type => _type;

    private int _value = 0;
    public int Value => _value;

    public Currency(ECurrencyType type, int value)
    {
        if (value < 0)
        {
            throw new Exception("value는 0보다 작을 수 없습니다.");
        }

        _type = type;
        _value = value;
    }

    public void Add(int addedValue)
    {
        if (addedValue < 0)
        {
            throw new Exception("추가 값은 음수가 될 수 없습니다.");
        }

        _value += addedValue;
    }

    public bool TryBuy(int value)
    {
        if (value < 0)
        {
            throw new Exception("차감 값은 음수가 될 수 없습니다.");
        }

        if (_value < value)
        {
            return false;
        }

        _value -= value; // 샀다

        return true;     // 샀다 성공
    }
}

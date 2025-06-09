using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    private Dictionary<ECurrencyType, Currency> _currencies;

    public Action OnDataChanged;
    private CurrencyRepository _repository;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Instance = this;
        Init();
    }

    private void Init()
    {
        {
            _currencies = new Dictionary<ECurrencyType, Currency>();
            _repository = new CurrencyRepository();
            List<CurrencyDTO> loadedCurrencies = _repository.Load();

            if (loadedCurrencies == null)
            {
                for (int i = 0; i < (int)ECurrencyType.Count; ++i)
                {
                    ECurrencyType type = (ECurrencyType)i;

                    // 골드, 다이아몬드 등을 0 값으로 생성후 딕셔너리에 삽입
                    Currency currency = new Currency(type, 0);
                    _currencies.Add(type, currency);
                }
            }
            else
            {
                foreach (var data in loadedCurrencies)
                {
                    Currency currency = new Currency(data.Type, data.Value);
                    _currencies.Add(currency.Type, currency);
                }
            }
        }
    }
    private List<CurrencyDTO> ToDtoList()
    {
        return _currencies.ToList().ConvertAll(currency => new CurrencyDTO(currency.Value));
    }

    public CurrencyDTO Get(ECurrencyType type)
    {
        return new CurrencyDTO(_currencies[type]);
    }

    public void Add(ECurrencyType type, int value)
    {
        _currencies[type].Add(value);

        // 다양한 이유로 여기에 규칙이 들어가기도한다.

        _repository.Save(ToDtoList());

        OnDataChanged?.Invoke();
    }


    public bool TryBuy(ECurrencyType type, int value)
    {
        if (!_currencies[type].TryBuy(value))
        {
            return false;
        }

        _repository.Save(ToDtoList());

        OnDataChanged?.Invoke();

        return true;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class StoreItemModel
{
    public event Action<Item> OnOpenItem;
    public event Action<Item> OnCloseItem;
    public event Action<Item> OnSelectItem;
    public event Action<Item> OnDeselectItem;

    private readonly ItemGroup _itemGroup;
    private readonly string _fileName;
    public string FilePath => Path.Combine(Application.persistentDataPath, $"{_fileName}.json");

    private Item _currentItem;
    private ItemData _currentItemData;
    private List<ItemData> _itemDatas = new List<ItemData>();

    public StoreItemModel(string fileName, ItemGroup itemGroup)
    {
        _fileName = fileName;
        _itemGroup = itemGroup;
    }

    public void Initialize()
    {
        if (File.Exists(FilePath))
        {
            string loadedJson = File.ReadAllText(FilePath);
            ItemDatas itemDatas = JsonUtility.FromJson<ItemDatas>(loadedJson);

            //Debug.Log("Success");

            _itemDatas = itemDatas.Datas.ToList();
        }
        else
        {
            //Debug.Log("HDBNJJJJJJJJJJJJJJJJJJJJJJ");

            _itemDatas = new List<ItemData>();

            for (int i = 0; i < _itemGroup.items.Count; i++)
            {
                _itemDatas.Add(new ItemData(false, false));
            }
        }

        //for (int i = 0; i < _itemGroup.items.Count; i++)
        //{
        //    _itemGroup.items[i].SetData(_itemDatas[i]);

        //    if (_itemGroup.items[i].DesignData.IsOpen)
        //        OnOpenCoverCardDesign?.Invoke(_itemGroup.CoverCardDesigns[i]);
        //    else
        //        OnCloseCoverCardDesign?.Invoke(_itemGroup.CoverCardDesigns[i]);
        //}

        //SelectCoverCardDesign(GetSelectCoverCardDesignIndex());
    }

    public void Dispose()
    {
        string json = JsonUtility.ToJson(new ItemDatas(_itemDatas.ToArray()));
        File.WriteAllText(FilePath, json);
    }

    public void BuyCoverCardDesign(int number)
    {
        //var faceCardDesign = _itemGroup.GetItemById(number);

        //if (faceCardDesign.ItemData.IsOpen) return;

        //faceCardDesign..IsOpen = true;
        //OnOpenCoverCardDesign?.Invoke(faceCardDesign);
    }

    public void SelectItem(int number)
    {
        //if (_currentItem != null)
        //{
        //    _currentItem.DesignData.IsSelect = false;
        //    OnDeselectCoverCardDesign?.Invoke(_currentItem);
        //}

        //_currentItem = _itemGroup.GetCoverCardDesignById(number);

        //if (_currentItem != null)
        //{
        //    _currentItem.DesignData.IsSelect = true;
        //    OnSelectCoverCardDesign?.Invoke(_currentItem);
        //}
    }
}

[Serializable]
public class ItemDatas
{
    public ItemData[] Datas;

    public ItemDatas(ItemData[] datas)
    {
        Datas = datas;
    }
}

[Serializable]
public class ItemData
{
    public bool IsOpen;
    public bool IsSelect;

    public ItemData(bool isOpen, bool isSelect)
    {
        this.IsOpen = isOpen;
        this.IsSelect = isSelect;
    }

    public void Select()
    {
        IsSelect = true;
    }

    public void Open()
    {
        IsOpen = true;
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StoreItemModel : IStoreOpenItems
{
    public event Action<ItemGroup> OnOpenItems;
    public event Action<ItemGroup> OnCloseItems;

    public event Action<IStoreOpenItems, ItemGroup> OnSelectOpenItems;

    private readonly ItemGroups _itemGroups;
    private readonly string _fileName;
    public string FilePath => Path.Combine(Application.persistentDataPath, $"{_fileName}.json");

    private Item _currentItem;
    private ItemGroupDatas _itemGroupDatas;

    public StoreItemModel(string fileName, ItemGroups itemGroups)
    {
        _fileName = fileName;
        _itemGroups = itemGroups;
    }

    public void Initialize()
    {
        if (File.Exists(FilePath))
        {
            string loadedJson = File.ReadAllText(FilePath);
            _itemGroupDatas = JsonUtility.FromJson<ItemGroupDatas>(loadedJson);
        }
        else
        {
            List<ItemDatas> itemDatasList = new();


            for (int i = 0; i < _itemGroups.itemGroups.Count; i++)
            {
                List<ItemData> itemDataList = new();

                for (int j = 0; j < _itemGroups.itemGroups[i].items.Count; j++)
                {
                    itemDataList.Add(new ItemData(false));
                }

                itemDatasList.Add(new ItemDatas(itemDataList.ToArray(), false));
            }

            _itemGroupDatas = new ItemGroupDatas(itemDatasList.ToArray());
        }

        for (int i = 0; i < _itemGroups.itemGroups.Count; i++)
        {
            for (int j = 0; j < _itemGroups.itemGroups[i].items.Count; j++)
            {
                _itemGroups.itemGroups[i].items[j].SetData(_itemGroupDatas.ItemDatas[i].Datas[j]);
            }
        }



        for (int i = 0; i < _itemGroups.itemGroups.Count; i++)
        {
            var itemGroup = _itemGroupDatas.ItemDatas[i];

            if (itemGroup.IsOpen)
            {
                OnOpenItems?.Invoke(_itemGroups.itemGroups[i]);
            }
            else
            {
                OnCloseItems?.Invoke(_itemGroups.itemGroups[i]);
            }
        }

        //if (_itemGroupDatas.IsOpen)
        //{
        //    OnOpenItems?.Invoke();
        //}
        //else
        //{
        //    OnCloseItems?.Invoke();
        //}


        //for (int i = 0; i < _itemGroups.items.Count; i++)
        //{
        //    _itemGroups.items[i].SetData(_itemGroupDatas.Datas[i]);
        //}
    }

    public void Dispose()
    {
        string json = JsonUtility.ToJson(_itemGroupDatas);
        File.WriteAllText(FilePath, json);
    }

    public void SelectItems()
    {
        //OnSelectOpenItems?.Invoke(this, _itemGroups);
    }

    public void OpenItems()
    {
        //if(_itemGroupDatas.IsOpen) return;

        //_itemGroupDatas.IsOpen = true;
        //OnOpenItems?.Invoke();

        //for (int i = 0; i < _itemGroupDatas.Datas.Length; i++) 
        //{
        //    OnOpenItem?.Invoke(_itemGroups.items[i]);

        //    if(i == 0)
        //    {
        //        _currentItem = _itemGroups.items[i];
        //        OnSelectItem?.Invoke(_currentItem);
        //    }
        //    else
        //    {
        //        OnDeselectItem?.Invoke(_itemGroups.items[i]);
        //    }
        //}
    }
}

[Serializable]
public class ItemGroupDatas
{
    public ItemDatas[] ItemDatas;

    public ItemGroupDatas(ItemDatas[] datas)
    {
        ItemDatas = datas;
    }
}

[Serializable]
public class ItemDatas
{
    public ItemData[] Datas;
    public bool IsOpen;

    public ItemDatas(ItemData[] datas, bool isOpen)
    {
        Datas = datas;
        IsOpen = isOpen;
    }
}

[Serializable]
public class ItemData
{
    public bool IsSelect;

    public ItemData(bool isSelect)
    {
        this.IsSelect = isSelect;
    }

    public void Select()
    {
        IsSelect = true;
    }
}

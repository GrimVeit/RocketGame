using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StoreItemModel
{
    public event Action<ItemGroup> OnOpenItems;
    public event Action<ItemGroup> OnCloseItems;

    public event Action<ItemGroup> OnSelectItemGroupForBuyItemGroup;
    public event Action<ItemGroup> OnSelectItemGroupForSelectItem;

    public event Action<ItemGroup, Item> OnSelectItem;
    public event Action<ItemGroup, Item> OnDeselectItem;

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

            _itemGroups.itemGroups[i].SetData(_itemGroupDatas.ItemDatas[i]);
        }



        for (int i = 0; i < _itemGroups.itemGroups.Count; i++)
        {
            var itemDatas = _itemGroupDatas.ItemDatas[i];


            if (itemDatas.IsOpen)
            {
                OnOpenItems?.Invoke(_itemGroups.itemGroups[i]);
            }
            else
            {
                OnCloseItems?.Invoke(_itemGroups.itemGroups[i]);
            }


            for (int j = 0; j < _itemGroups.itemGroups[i].items.Count; j++)
            {
                var itemData = _itemGroupDatas.ItemDatas[i].Datas[j];

                if (itemData.IsSelect)
                {
                    OnSelectItem?.Invoke(_itemGroups.itemGroups[i], _itemGroups.itemGroups[i].items[j]);
                }
                else
                {
                    OnDeselectItem?.Invoke(_itemGroups.itemGroups[i], _itemGroups.itemGroups[i].items[j]);
                }
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

    public void SelectItemGroupForBuyItemGroup(int index)
    {
        var itemGroup = _itemGroups.GetItemGroupById(index);

        if(itemGroup == null)
        {
            Debug.LogError("Not found item group by id - " + index);
            return;
        }

        OnSelectItemGroupForBuyItemGroup?.Invoke(itemGroup);
    }

    public void SelectItemGroupForSelectItem(int index)
    {
        var itemGroup = _itemGroups.GetItemGroupById(index);

        if (itemGroup == null)
        {
            Debug.LogError("Not found item group by id - " + index);
            return;
        }

        OnSelectItemGroupForSelectItem?.Invoke(itemGroup);
    }

    public void OpenItemGroup(int index)
    {
        var itemGroup = _itemGroups.GetItemGroupById(index);

        if (itemGroup == null)
        {
            Debug.LogError("Not found item group by id - " + index);
            return;
        }

        itemGroup.ItemDatas.IsOpen = true;
        OnOpenItems?.Invoke(itemGroup);

        var item = itemGroup.GetRandomItem();

        SelectItem(itemGroup, item.ID);

    }

    public void SelectItem(int indexItemGroup, int indexItem)
    {
        var itemGroup = _itemGroups.GetItemGroupById(indexItemGroup);

        SelectItem(itemGroup, indexItem);
    }

    private void SelectItem(ItemGroup itemGroup, int indexItem)
    {
        var item = itemGroup.GetItemById(indexItem);

        if(item == null)
        {
            Debug.LogError($"Not found item in group id - {itemGroup.ID} by id - {indexItem}");
            return;
        }

        itemGroup.items.ForEach(ig =>
        {
            if (ig.ItemData.IsSelect)
            {
                ig.ItemData.IsSelect = false;
                OnDeselectItem?.Invoke(itemGroup, ig);
            }
        });

        item.ItemData.IsSelect = true;
        OnSelectItem?.Invoke(itemGroup, item);
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

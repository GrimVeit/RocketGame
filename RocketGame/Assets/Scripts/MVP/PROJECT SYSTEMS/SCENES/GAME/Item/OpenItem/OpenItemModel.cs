using System;

public class OpenItemModel
{
    public event Action<int> OnActivateOpenItem;
    public event Action<int> OnDeactivateOpenItem;

    public event Action<int> OnChooseSelectItemGroupForSelectItem;

    public void ActivateOpenItem(ItemGroup itemGroup)
    {
        OnActivateOpenItem?.Invoke(itemGroup.ID);
    }

    public void DeactivateOpenItem(ItemGroup itemGroup)
    {
        OnDeactivateOpenItem?.Invoke(itemGroup.ID);
    }


    public void ChooseSelectItemGroupForSelectItem(int id)
    {
        OnChooseSelectItemGroupForSelectItem?.Invoke(id);
    }
}

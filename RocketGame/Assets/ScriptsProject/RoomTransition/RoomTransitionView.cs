using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RoomTransitionView : View
{
    [SerializeField] private List<RoomTransition> transitions = new();

    public void Activate(int id)
    {
        var transition = GetTransitionById(id);

        if(transition == null)
        {
            Debug.LogError("Not found RoomTransition with id - " + id);
            return;
        }

        transition.Activate();
    }

    private RoomTransition GetTransitionById(int id)
    {
        return transitions.FirstOrDefault(t => t.ID == id);
    }
}

[System.Serializable]
public class RoomTransition
{
    public int ID => id;

    [SerializeField] private int id;
    [SerializeField] private Button button;
    [SerializeField] private Image imageButton;
    [SerializeField] private Sprite spriteActive;
    [SerializeField] private Sprite spriteDeactive;

    public void Activate()
    {
        button.enabled = true;
        imageButton.sprite = spriteActive;
    }

    public void Deactivate()
    {
        button.enabled = false;
        imageButton.sprite = spriteDeactive;
    }
}

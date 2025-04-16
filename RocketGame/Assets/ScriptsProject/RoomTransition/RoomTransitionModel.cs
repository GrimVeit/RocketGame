using System;

public class RoomTransitionModel
{
    public event Action<int> OnUnlockRoom;

    public void UnlockRoom(int room)
    {
        OnUnlockRoom?.Invoke(room);
    }
}

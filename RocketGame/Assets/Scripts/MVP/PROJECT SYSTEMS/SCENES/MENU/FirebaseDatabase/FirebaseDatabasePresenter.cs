using System;

public class FirebaseDatabasePresenter
{
    private FirebaseDatabaseModel firebaseDatabaseRealtimeModel;
    private FirebaseDatabaseView firebaseDatabaseRealtimeView;

    public FirebaseDatabasePresenter(FirebaseDatabaseModel firebaseDatabaseRealtimeModel, FirebaseDatabaseView firebaseDatabaseRealtimeView)
    {
        this.firebaseDatabaseRealtimeModel = firebaseDatabaseRealtimeModel;
        this.firebaseDatabaseRealtimeView = firebaseDatabaseRealtimeView;
    }

    public void Initialize()
    {
        ActivateEvents();

        firebaseDatabaseRealtimeModel.Initialize();
        firebaseDatabaseRealtimeView.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        firebaseDatabaseRealtimeModel.Dispose();
        firebaseDatabaseRealtimeView.Dispose();
    }

    private void ActivateEvents()
    {
        firebaseDatabaseRealtimeModel.OnGetUsersRecords += firebaseDatabaseRealtimeView.DisplayUsersRecords;
        firebaseDatabaseRealtimeModel.OnGetNickname += firebaseDatabaseRealtimeView.TestDebugNickname;
    }

    private void DeactivateEvents()
    {
        firebaseDatabaseRealtimeModel.OnGetUsersRecords -= firebaseDatabaseRealtimeView.DisplayUsersRecords;
        firebaseDatabaseRealtimeModel.OnGetNickname -= firebaseDatabaseRealtimeView.TestDebugNickname;
    }

    #region Input

    public event Action<UserData> OnGetUserFromPlace
    {
        add { firebaseDatabaseRealtimeModel.OnGetUserFromPlace += value; }
        remove { firebaseDatabaseRealtimeModel.OnGetUserFromPlace -= value; }
    }

    public void CreateEmptyDataToServer()
    {
        firebaseDatabaseRealtimeModel.CreateNewAccountInServer();
    }

    public void SaveChangeToServer()
    {
        firebaseDatabaseRealtimeModel.SaveChangesToServer();
    }

    public void DisplayUsersRecords()
    {
        firebaseDatabaseRealtimeModel.DisplayUsersRecords();
    }

    public void SetNickname(string nickname)
    {
        firebaseDatabaseRealtimeModel.SetNickname(nickname);
    }

    public void GetUserFromPlace(int place)
    {
        firebaseDatabaseRealtimeModel.GetUserFromPlace(place);
    }

    #endregion
}

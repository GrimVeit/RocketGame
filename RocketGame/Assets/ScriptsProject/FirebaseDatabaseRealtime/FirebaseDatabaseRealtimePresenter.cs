using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseDatabaseRealtimePresenter
{
    private FirebaseDatabaseRealtimeModel firebaseDatabaseRealtimeModel;
    private FirebaseDatabaseRealtimeView firebaseDatabaseRealtimeView;

    public FirebaseDatabaseRealtimePresenter(FirebaseDatabaseRealtimeModel firebaseDatabaseRealtimeModel, FirebaseDatabaseRealtimeView firebaseDatabaseRealtimeView)
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

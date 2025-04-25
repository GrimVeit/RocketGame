using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseDatabaseModel
{
    public event Action<UserData> OnGetUserFromPlace;
    public event Action<string> OnGetNickname;

    public event Action<List<UserData>> OnGetUsersRecords;

    public string Nickname { get; private set; }
    public float Record { get; private set; }

    private List<UserData> userRecordsDictionary = new List<UserData>();


    private FirebaseAuth auth;
    private DatabaseReference databaseReference;

    private ISoundProvider soundProvider;

    public FirebaseDatabaseModel(FirebaseAuth auth, DatabaseReference database, ISoundProvider soundProvider)
    {
        this.auth = auth;
        this.databaseReference = database;
        this.soundProvider = soundProvider;
    }

    public void Initialize()
    {
        Record = PlayerPrefs.GetFloat(PrefsKeys.WIN_RECORD, 0);

        Debug.Log(Record);
    }

    public void Dispose()
    {
        PlayerPrefs.SetFloat(PrefsKeys.WIN_RECORD, Record);
    }

    public void CreateNewAccountInServer()
    {
        Nickname = auth.CurrentUser.Email.Split('@')[0];
        Record = 0;
        PlayerPrefs.SetInt(PrefsKeys.WIN_RECORD, 0);
        PlayerPrefs.SetString(PrefsKeys.NICKNAME, Nickname);
        UserData user = new(Nickname, 0);
        string json = JsonUtility.ToJson(user);

        OnGetNickname?.Invoke(Nickname);

        Debug.Log(Nickname);

        databaseReference.Child("Users").Child(auth.CurrentUser.UserId).SetRawJsonValueAsync(json);
    }

    public void SetNickname(string nickname)
    {
        Nickname = nickname;
        OnGetNickname?.Invoke(Nickname);
    }

    public void SaveChangesToServer()
    {
        Nickname = auth.CurrentUser.Email.Split('@')[0];
        UserData user = new(Nickname, Record);
        string json = JsonUtility.ToJson(user);
        databaseReference.Child("Users").Child(auth.CurrentUser.UserId).SetRawJsonValueAsync(json);
    }

    public void GetUserFromPlace(int number)
    {
        Coroutines.Start(GetUser(number));
    }

    #region Records

    public void DisplayUsersRecords()
    {
        Coroutines.Start(GetUsersRecords());
    }

    private IEnumerator GetUsersRecords()
    {
        //var task = databaseReference.Child("Users").OrderByChild("Record").LimitToFirst(15).GetValueAsync();
        var task = databaseReference.Child("Users").OrderByChild("Record").LimitToLast(10).GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            Debug.Log("Error display record");
            yield break;
        }

        userRecordsDictionary.Clear();

        DataSnapshot data = task.Result;

        Debug.Log("Success " + data.ChildrenCount);

        foreach (var user in data.Children)
        {
            string name = user.Child("Nickname").Value.ToString();
            float record = float.Parse(user.Child("Record").Value.ToString());
            userRecordsDictionary.Add(new UserData(name, record));
        }

        OnGetUsersRecords?.Invoke(userRecordsDictionary);
    }

    private IEnumerator GetUser(int number)
    {
        //var task = databaseReference.Child("Users").OrderByChild("Record").LimitToFirst(number).GetValueAsync();
        var task = databaseReference.Child("Users").OrderByChild("Record").LimitToLast(number).GetValueAsync();

        yield return new WaitUntil(() => task.IsCompleted);

        if (task.IsFaulted)
        {
            Debug.Log("Error display record");
            yield break;
        }

        DataSnapshot data = task.Result;

        Debug.Log("Success " + data.ChildrenCount);

        foreach (var user in data.Children)
        {
            string name = user.Child("Nickname").Value.ToString();
            float record = float.Parse(user.Child("Record").Value.ToString());
            OnGetUserFromPlace?.Invoke(new UserData(name, record));
        }

        //OnGetUserFromPlace?.Invoke(new UserData(
        //    data.Child("Nickname").Value.ToString(),
        //    data.Child("Record").Value.ToString(),
        //    data.Child("Avatar").Value.ToString()));
    }

    #endregion
}

public class UserData
{
    public string Nickname;
    public float Record;

    public UserData(string nickname, float record)
    {
        Nickname = nickname;
        Record = record;
    }
}

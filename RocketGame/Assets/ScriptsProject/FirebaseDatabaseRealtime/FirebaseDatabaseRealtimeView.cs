using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FirebaseDatabaseRealtimeView : View
{
    public event Action OnChangeAvatar;

    [SerializeField] private Transform contentUsers;
    [SerializeField] private UserGrid userGridPrefab;

    [SerializeField] private Button buttonChangeAvatar;

    [Space]
    [Space]
    [Header("ACCOUNT DATA")]
    public int Avatar;
    public string Nickname;

    private List<UserGrid> spawnUsers = new List<UserGrid>();


    public void Initialize()
    {

    }

    public void Dispose()
    {

    }

    public void TestDebugNickname(string nickname)
    {
        Nickname = nickname;
    }

    public void DisplayUsersRecords(List<UserData> users)
    {
        for (int i = 0; i < contentUsers.childCount; i++)
        {
            spawnUsers.Clear();
            Destroy(contentUsers.GetChild(i).gameObject);
        }

        //users = users.OrderBy(x => x.Record).ToList();

        users = users.OrderByDescending(entry => entry.Record).ToList();

        foreach (var item in users)
        {
            //UserGrid grid = Instantiate(userGridPrefab, contentUsers);
            //grid.SetData(item.Nickname, item.Record.ToString(), spritesAvatar[item.Avatar]);
            //spawnUsers.Add(grid);
        }
    }
}

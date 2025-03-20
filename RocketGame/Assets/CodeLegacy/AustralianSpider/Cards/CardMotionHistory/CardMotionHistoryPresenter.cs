using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMotionHistoryPresenter : MonoBehaviour
{
    private readonly CardMotionHistoryModel model;
    private readonly CardMotionHistoryView view;

    public CardMotionHistoryPresenter(CardMotionHistoryModel model, CardMotionHistoryView view)
    {
        this.model = model;
        this.view = view;

        ActivateEvents();
    }

    public void Initialize()
    {
        view.Initialize();
    }

    public void Dispose()
    {
        DeactivateEvents();

        view.Dispose();
    }

    private void ActivateEvents()
    {
        view.OnClickToReturnButton += model.ReturmLastMotion;

        model.OnActivate += view.Activate;
        model.OnDeactivate += view.Deactivate;
    }

    private void DeactivateEvents()
    {
        view.OnClickToReturnButton -= model.ReturmLastMotion;

        model.OnActivate -= view.Activate;
        model.OnDeactivate -= view.Deactivate;
    }

    #region Input

    public event Action<CardInteractive, List<CardInteractive>, Column, bool> OnRemoveLastMotion
    {
        add { model.OnRemoveLastMotion += value; }
        remove { model.OnRemoveLastMotion -= value; }
    }

    public void AddMotion(CardInteractive cardInteractive, Column column, bool isActiveHigherCard)
    {
        model.AddMotion(cardInteractive, column, isActiveHigherCard);
    }

    public void CleanHistory()
    {
        model.ClearHistory();
    }

    #endregion
}

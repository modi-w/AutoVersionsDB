using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.UI.DBVersions
{
    public interface IDBVersionsViewSateManager
    {
        DBVersionsViewStateType LastViewState { get; }

        void ChangeViewState(DBVersionsViewStateType viewType);
        void ChangeViewState_AfterProcessComplete(ProcessTrace processResults);
    }
}
using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.UI.DBVersions
{
    public interface IDBVersionsViewSateManager
    {
        DBVersionsViewStateType LastViewState { get; }

        void ChangeViewState(DBVersionsViewStateType viewType);
        void ChangeViewStateAfterProcessComplete(ProcessTrace processResults);
    }
}
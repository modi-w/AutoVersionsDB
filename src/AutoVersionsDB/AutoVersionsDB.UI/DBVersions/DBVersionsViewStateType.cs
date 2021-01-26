

namespace AutoVersionsDB.UI.DBVersions
{
    public enum DBVersionsViewStateType
    {
        ReadyToRunSync,
        ReadyToSyncToSpecificState,
        MissingSystemTables,
        NewProject,
        HistoryExecutedFilesChanged,
        SetVirtual,
        InProcess,
        RestoreDatabaseError
    }
}

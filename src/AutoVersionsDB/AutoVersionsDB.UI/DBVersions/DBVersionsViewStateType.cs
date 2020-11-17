

namespace AutoVersionsDB.UI.DBVersions
{
    public enum DBVersionsViewStateType
    {
        ReadyToRunSync,
        ReadyToSyncToSpecificState,
        MissingSystemTables,
        HistoryExecutedFilesChanged,
        SetDBStateManually,
        InProcess,
        RestoreDatabaseError
    }
}

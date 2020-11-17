using AutoVersionsDB.NotificationableEngine;

namespace AutoVersionsDB.UI.EditProject
{
    public interface IEditProjectViewSateManager
    {
        EditProjectViewStateType LastViewState { get; }

        void ChangeViewState(EditProjectViewStateType viewType);
        void ClearUIElementsErrors();
        void HandleProcessErrors(bool isNewProjectConfig, ProcessResults processResults);
        void SetErrorsToUiElements(ProcessTrace processResults);
        void ShowHideEnvFields(bool isDevEnv);
    }
}
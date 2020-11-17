using AutoVersionsDB.NotificationableEngine;
using AutoVersionsDB.UI.EditProject;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.ProjectConfigsTests.TestDefinitions.UIAsserts
{
    public class EditProjectViewSateManagerForTests : IEditProjectViewSateManager
    {
        private readonly EditProjectViewSateManager _editProjectViewSateManager;

        public EditProjectViewStateType LastViewState => _editProjectViewSateManager.LastViewState;

        public EditProjectViewSateManagerForTests(EditProjectViewSateManager editProjectViewSateManager)
        {
            _editProjectViewSateManager = editProjectViewSateManager;
        }


        public void ChangeViewState(EditProjectViewStateType viewType)
        {
            _editProjectViewSateManager.ChangeViewState(viewType);

            ChangeViewStateForMockSniffer(LastViewState);
        }


        public virtual void ChangeViewStateForMockSniffer(EditProjectViewStateType viewType)
        {

        }

        public void ClearUIElementsErrors()
        {
            _editProjectViewSateManager.ClearUIElementsErrors();
        }

        public void HandleProcessErrors(bool isNewProjectConfig, ProcessResults processResults)
        {
            _editProjectViewSateManager.HandleProcessErrors(isNewProjectConfig, processResults);
        }

        public void SetErrorsToUiElements(ProcessTrace processResults)
        {
            _editProjectViewSateManager.SetErrorsToUiElements(processResults);
        }

        public void ShowHideEnvFields(bool isDevEnv)
        {
            _editProjectViewSateManager.ShowHideEnvFields(isDevEnv);
        }
    }
}

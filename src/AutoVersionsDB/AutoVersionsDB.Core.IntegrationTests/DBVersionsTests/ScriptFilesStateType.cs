﻿using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests.Helpers;
using AutoVersionsDB.DbCommands.Contract;
using AutoVersionsDB.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests
{
    public enum ScriptFilesStateType
    {
        None = 0,
        ValidScripts = 1,
        MissingFile = 2,
        ScriptError = 3,
        ChangedHistoryFiles_Incremental = 4,
        ChangedHistoryFiles_Repeatable = 5,
        WithDevDummyDataFiles = 6,
    }
}

﻿using AutoVersionsDB;
using AutoVersionsDB.Core;
using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.Core.IntegrationTests;
using AutoVersionsDB.Core.IntegrationTests.DBVersionsTests;

using AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles;
using AutoVersionsDB.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoVersionsDB.Core.IntegrationTests.TestsUtils.ScriptFiles
{
    public enum ScriptFilesStateType
    {
        None = 0,
        ValidScripts = 1,
        MissingFile = 2,
        ScriptError = 3,
        IncrementalChanged = 4,
        RepeatableChanged = 5,
        WithDevDummyDataFiles = 6,
        NoScriptFiles = 7
    }
}

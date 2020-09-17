using AutoVersionsDB.Core;
using AutoVersionsDB.NotificationableEngine;
using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Threading;

namespace AutoVersionsDB.ConsoleApp
{
    class Program
    {
        static int Main(string[] args)
        {
            NinjectManager.CreateKernel();

            Console.Title = "AutoVersionsDB";

            RootCommand rootCommand = new RootCommand();


            Command syncCommand = new Command("sync")
            {
                    new Option<string>(new string[]{"-c","--code" }, "The project code whitch you want to sync"),
            };

            syncCommand.Handler = CommandHandler.Create<string>((code) =>
            {
                ProcessTrace processReults;

                using (ConsoleSpinner spinner = new ConsoleSpinner())
                {
                    processReults = AutoVersionsDbAPI.SyncDB(code, onNotificationStateChanged);
                }


                clearConsoleLine(0);

                lock (ConsoleSpinner.ConsolWriteSync)
                {

                    if (processReults.HasError)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("The process complete with errors:");
                        Console.WriteLine("--------------------------------");
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(processReults.GetOnlyErrorsHistoryAsString());

                        if (!string.IsNullOrWhiteSpace(processReults.InstructionsMessage))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(processReults.InstructionsMessage);
                        }

                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("The process complete successfully");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            });



            rootCommand.Add(syncCommand);



            return rootCommand.InvokeAsync("sync --code TestP1").Result;
            //return rootCommand.InvokeAsync(args).Result;




            //Command projectCommand = new Command("project");
            //rootCommand.Add(projectCommand);


            //Command projectNewCommand = new Command("new")
            //{
            //    new Option(new string[]{"-c","--projectcode" },"Unique indentifier for project"),
            //    new Option(new string[]{ "-desc", "--description" },"Description for the project"),
            //    new Option(new string[]{"-dbt","--db-type" },"Database Type (Sql server)"),
            //    new Option(new string[]{"-connstr","--connection-string" },"connection String for the Database"),
            //    new Option(new string[]{"-connstrm","--connection-string-master" },"connection String for the master Database (with dbowner privileges)"),
            //    new Option(new string[]{"-buf","--backup-folder" },"Backup up folder path for saving the database before run"),
            //    new Option(new string[]{"-de","--dev-environment" },"Is the project run on dev environment (allow to use dummy data scripts files)"),
            //    new Option(new string[]{"-sf","--scripts-base-folder" },"For dev environment only - the scripts base folder path. Where all the project scripts files located"),
            //    new Option(new string[]{"-sf","--scripts-base-folder" },"For dev environment only - the scripts base folder path. Where all the project scripts files located"),
            //};
            //projectCommand.Add(projectNewCommand);




            //Command child = new Command("child1")
            //    {
            //        new Option<int>(
            //            "--int-option",
            //            getDefaultValue: () => 42,
            //            description: "An option whose argument is parsed as an int"),
            //        new Option<bool>(
            //            "--bool-option",
            //            "An option whose argument is parsed as a bool"),
            //        new Option<FileInfo>(
            //            "--file-option",
            //            "An option whose argument is parsed as a FileInfo")
            //    };

            //rootCommand.Add(child);

            //// rootCommand.Description = "My sample app";


            //// Note that the parameters of the handler method are matched according to the names of the options
            //child.Handler = CommandHandler.Create<int, bool, FileInfo>((intOption, boolOption, fileOption) =>
            //{
            //    Console.WriteLine($"The value for --int-option is: {intOption}");
            //    Console.WriteLine($"The value for --bool-option is: {boolOption}");
            //    Console.WriteLine($"The value for --file-option is: {fileOption?.FullName ?? "null"}");
            //});

            //// Parse the incoming args and invoke the handler
            //return rootCommand.InvokeAsync("child1 --int-option 333").Result;
        }

        private static int numberOfLineForLastMessage = 1;
        private static void clearConsoleLine(int cursorLeft)
        {
            Console.SetCursorPosition(cursorLeft, Console.CursorTop - (numberOfLineForLastMessage - 1));

            for (int i = 0; i < numberOfLineForLastMessage; i++)
            {
                Console.Write(new String(' ', Console.BufferWidth));
                if (i == 0 && cursorLeft>0)
                {
                    Console.SetCursorPosition(0, Console.CursorTop);
                }
                else
                {
                    Console.SetCursorPosition(cursorLeft, Console.CursorTop + 1);
                }
            }

            Console.SetCursorPosition(cursorLeft, Console.CursorTop - (numberOfLineForLastMessage));
        }

        private static void onNotificationStateChanged(ProcessTrace processTrace, StepNotificationState notificationStateItem)
        {
            lock (ConsoleSpinner.ConsolWriteSync)
            {
                clearConsoleLine(3);

                int cursorTopStart = Console.CursorTop;

                Console.SetCursorPosition(3, Console.CursorTop);
                Console.Write(notificationStateItem.ToString());

                int cursorTopEnd = Console.CursorTop;

                numberOfLineForLastMessage = cursorTopEnd - cursorTopStart + 1;
            }
        }
    }
}

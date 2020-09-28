//using AutoVersionsDB.Core.ConfigProjects;
//using Moq;
//using Ninject;
//using System;
//using System.Collections.Generic;
//using System.Reflection;
//using System.Text;

//namespace AutoVersionsDB.Core.IntegrationTests.DBVersionsTests
//{
//    public static class MockObjectsProvider
//    {
//        private static StandardKernel _ninjectKernelContainer;

//        public static Mock<ProjectConfigsStorage> MockProjectConfigsStorage;

//        public static void Init()
//        {
//            _ninjectKernelContainer = new StandardKernel();
//            _ninjectKernelContainer.Load(Assembly.GetExecutingAssembly());

//            MockProjectConfigsStorage = new Mock<ProjectConfigsStorage>();
//            MockProjectConfigsStorage.Setup(m => m.IsIdExsit(It.IsAny<string>())).Returns(true);
//            _ninjectKernelContainer.Bind<ProjectConfigsStorage>().ToConstant(MockProjectConfigsStorage.Object);

//            NinjectUtils.SetKernelInstance(_ninjectKernelContainer);

//        }
//    }
//}

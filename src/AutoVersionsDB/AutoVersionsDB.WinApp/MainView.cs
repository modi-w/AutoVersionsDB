﻿using AutoVersionsDB.UI;
using AutoVersionsDB.UI.Main;
using AutoVersionsDB.WinApp.Utils;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoVersionsDB.WinApp
{

    public partial class MainView : Form
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
        public new void SuspendLayout()
        {
            //Comment: In .net core, this method cause to layout to the window to be too big respect to its children controls.
            //  And because that we cant set ignore to ths method to the auto generate code, we implement nothing.
        }

        private readonly MainViewModel _viewModel;


        public MainView(MainViewModel viewModel)
        {
            InitializeComponent();


            _viewModel = viewModel;

            //if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            //{
            //    _viewModel.MainViewModelData.PropertyChanged += ViewModel_PropertyChanged;
            //    SetDataBindings();
            //}

            Load += Main_Load;



            ////chooseProject1.OnSetNewProject += ChooseProject1_OnSetNewProject1;
            ////chooseProject1.OnNavToProcess += ChooseProject1_OnNavToProcess;
            ////chooseProject1.OnEditProject += ChooseProject1_OnEditProject;

            ////editProjectConfigDetails1.OnNavToProcess += EditProjectConfigDetails1_OnNavToProcess;
            ////dbVersionsMangement1.OnEditProject += DBVersionsMangement1_OnEditProject;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!this.DesignMode)
            {
                _viewModel.MainViewModelData.PropertyChanged += ViewModel_PropertyChanged;
                SetDataBindings();
            }



        }


        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(_viewModel.MainViewModelData.CurrentView):

                    SetView(_viewModel.MainViewModelData.CurrentView);
                    break;

                default:
                    break;
            }
        }

        private void SetDataBindings()
        {
            lnkBtnChooseProject.DataBindings.Clear();
            lnkBtnChooseProject.DataBindings.Add(
                AsyncBindingHelper.GetBinding(
                    lnkBtnChooseProject,
                    nameof(lnkBtnChooseProject.Visible),
                    _viewModel.MainControls,
                    nameof(_viewModel.MainControls.BtnChooseProjectVisible)
                )
            );
        }


        private void Main_Load(object sender, EventArgs e)
        {
            //tabMainLayout.Appearance = TabAppearance.FlatButtons;
            tabMainLayout.ItemSize = new Size(0, 1);
            tabMainLayout.SizeMode = TabSizeMode.Fixed;

            tabMainLayout.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabMainLayout.DrawItem += TabMainLayout_DrawItem;

            //tabMainLayout.Selected += TabMainLayout_Selected;

            //       tabMainLayout.Width = this.Width - 50;
        }


        public void SetView(ViewType viewType)
        {
            tabMainLayout.BeginInvoke((MethodInvoker)(() =>
            {

                switch (viewType)
                {
                    case ViewType.ChooseProject:

                        tabMainLayout.SelectTab(tbChooseProject);
                        break;

                    case ViewType.EditProjectConfig:

                        tabMainLayout.SelectTab(tbEditProjectConfig);
                        break;

                    case ViewType.DBVersions:

                        tabMainLayout.SelectTab(tbDBVersionsMangement);

                        break;

                    default:
                        break;
                }
            }));
        }



        private void TabMainLayout_DrawItem(object sender, DrawItemEventArgs e)
        {
            Rectangle r = tabMainLayout.GetTabRect(tabMainLayout.TabPages.Count - 1);
            RectangleF rf = new RectangleF(r.X + r.Width, r.Y - 5, tabMainLayout.Width - (r.X + r.Width), r.Height + 5);
            using (Brush b = new SolidBrush(Color.White))
            {
                e.Graphics.FillRectangle(b, rf);
            }
        }



        private void LnkBtnChooseProject_Click(object sender, EventArgs e)
        {
            _viewModel.NavToChooseProjectCommand.Execute(null);
        }


    }
}

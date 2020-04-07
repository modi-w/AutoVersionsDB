using AutoVersionsDB.Core.ConfigProjects;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AutoVersionsDB.WinApp
{
    public delegate void OnNavToProcessHandler(ProjectConfigItem projectConfigItem);
    public delegate void OnRefreshProjectListHandler();


    public partial class Main : Form
    {


        public Main()
        {
            InitializeComponent();

            this.Load += Main_Load;



            chooseProject1.OnSetNewProject += ChooseProject1_OnSetNewProject1;
            chooseProject1.OnNavToProcess += ChooseProject1_OnNavToProcess;

            editProjectConfigDetails1.OnNavToProcess += EditProjectConfigDetails1_OnNavToProcess;
            dbVersionsMangement1.OnEditProject += DbVersionsMangement1_OnEditProject;

            lnkBtnChooseProject.Visible = false;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //tabMainLayout.Appearance = TabAppearance.FlatButtons;
            tabMainLayout.ItemSize = new Size(0, 1);
            tabMainLayout.SizeMode = TabSizeMode.Fixed;

            tabMainLayout.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabMainLayout.DrawItem += TabMainLayout_DrawItem;

            tabMainLayout.Selected += TabMainLayout_Selected;

     //       tabMainLayout.Width = this.Width - 50;
        }

        private void EditProjectConfigDetails1_OnNavToProcess(ProjectConfigItem projectConfigItem)
        {
            dbVersionsMangement1.SetProjectConfigItem(projectConfigItem);

            tabMainLayout.SelectTab(tbDBVersionsMangement);
        }

        private void DbVersionsMangement1_OnEditProject(ProjectConfigItem projectConfigItem)
        {
            editProjectConfigDetails1.SetProjectConfigItem(projectConfigItem);

            tabMainLayout.SelectedTab = tbEditProjectConfig;
        }



        private void TabMainLayout_Selected(object sender, TabControlEventArgs e)
        {
            lnkBtnChooseProject.Visible = tabMainLayout.SelectedTab != tbChooseProject;
        }

        private void TabMainLayout_DrawItem(object sender, DrawItemEventArgs e)
        {
            Rectangle r = tabMainLayout.GetTabRect(tabMainLayout.TabPages.Count - 1);
            RectangleF rf = new RectangleF(r.X + r.Width, r.Y - 5, tabMainLayout.Width - (r.X + r.Width), r.Height + 5);
            Brush b = new SolidBrush(Color.White);
            e.Graphics.FillRectangle(b, rf);
        }

        private void ChooseProject1_OnSetNewProject1(object sender, EventArgs e)
        {
            ProjectConfigItem newProjectConfig = new ProjectConfigItem();
            editProjectConfigDetails1.SetProjectConfigItem(newProjectConfig);

            tabMainLayout.SelectedTab = tbEditProjectConfig;
        }


        private void ChooseProject1_OnNavToProcess(ProjectConfigItem projectConfigItem)
        {
            dbVersionsMangement1.SetProjectConfigItem(projectConfigItem);

            tabMainLayout.SelectTab(tbDBVersionsMangement);
        }

        private void lnkBtnChooseProject_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {


        }

        private void lnkBtnChooseProject_Click(object sender, EventArgs e)
        {
            tabMainLayout.SelectedTab = tbChooseProject;
        }
    }
}

using AutoVersionsDB.Core.ConfigProjects;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoVersionsDB.WinApp
{
    public delegate void OnNavToProcessHandler(ProjectConfigItem projectConfigItem);
    public delegate void OnRefreshProjectListHandler();
    public delegate void OnEditProjectHandler(ProjectConfigItem projectConfigItem);


    public partial class Main : Form
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
        public new void SuspendLayout()
        {
            //Comment: In .net core, this method cause to layout to the window to be too big respect to its children controls.
            //  And because that we cant set ignore to ths method to the auto generate code, we implement nothing.
        }



        public Main()
        {
            InitializeComponent();

            this.Load += Main_Load;



            chooseProject1.OnSetNewProject += ChooseProject1_OnSetNewProject1;
            chooseProject1.OnNavToProcess += ChooseProject1_OnNavToProcess;
            chooseProject1.OnEditProject += ChooseProject1_OnEditProject;

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
            tabMainLayout.SelectTab(tbDBVersionsMangement);

            Task.Run(() => {

                dbVersionsMangement1.SetProjectConfigItem(projectConfigItem);
            });
        }

        private void ChooseProject1_OnEditProject(ProjectConfigItem projectConfigItem)
        {
            tabMainLayout.SelectedTab = tbEditProjectConfig;
        
            Task.Run(() => {

                editProjectConfigDetails1.SetProjectConfigItem(projectConfigItem);
            });
        }
    

        private void DbVersionsMangement1_OnEditProject(ProjectConfigItem projectConfigItem)
        {
            tabMainLayout.SelectedTab = tbEditProjectConfig;

            Task.Run(() => {

                editProjectConfigDetails1.SetProjectConfigItem(projectConfigItem);
            });
        }



        private void TabMainLayout_Selected(object sender, TabControlEventArgs e)
        {
            lnkBtnChooseProject.Visible = tabMainLayout.SelectedTab != tbChooseProject;
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

        private void ChooseProject1_OnSetNewProject1(object sender, EventArgs e)
        {
            editProjectConfigDetails1.CreateNewProjectConfig();

            tabMainLayout.SelectedTab = tbEditProjectConfig;
        }


        private void ChooseProject1_OnNavToProcess(ProjectConfigItem projectConfigItem)
        {
            tabMainLayout.SelectTab(tbDBVersionsMangement);

            Task.Run(() => {

                dbVersionsMangement1.SetProjectConfigItem(projectConfigItem);
            });

        }

        private void LnkBtnChooseProject_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {


        }

        private void LnkBtnChooseProject_Click(object sender, EventArgs e)
        {
            tabMainLayout.SelectedTab = tbChooseProject;
        }
    }
}

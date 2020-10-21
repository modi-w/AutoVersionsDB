using AutoVersionsDB.Core.ConfigProjects;
using AutoVersionsDB.UI;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoVersionsDB.WinApp
{
    public delegate void OnNavToProcessHandler(string id);
    public delegate void OnRefreshProjectListHandler();
    public delegate void OnEditProjectHandler(string id);


    public partial class Main : Form, IViewContainer
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
        public new void SuspendLayout()
        {
            //Comment: In .net core, this method cause to layout to the window to be too big respect to its children controls.
            //  And because that we cant set ignore to ths method to the auto generate code, we implement nothing.
        }

        public ViewType CurrentView { get; private set; }

        private readonly MainViewModel _viewModel;


        public Main(MainViewModel viewModel)
        {
            InitializeComponent();

            this.Load += Main_Load;

            _viewModel = viewModel;

            SetDataBindings();


            //chooseProject1.OnSetNewProject += ChooseProject1_OnSetNewProject1;
            //chooseProject1.OnNavToProcess += ChooseProject1_OnNavToProcess;
            //chooseProject1.OnEditProject += ChooseProject1_OnEditProject;

            //editProjectConfigDetails1.OnNavToProcess += EditProjectConfigDetails1_OnNavToProcess;
            //dbVersionsMangement1.OnEditProject += DbVersionsMangement1_OnEditProject;

            lnkBtnChooseProject.Visible = false;
        }

        private void SetDataBindings()
        {
            this.lnkBtnChooseProject.DataBindings.Clear();
            this.lnkBtnChooseProject.DataBindings.Add(
                nameof(lnkBtnChooseProject.Visible),
                _viewModel, 
                nameof(_viewModel.BtnChooseProjectVisible),
                false, 
                DataSourceUpdateMode.OnPropertyChanged);
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
            switch (viewType)
            {
                case ViewType.ChooseProject:

                    tabMainLayout.SelectTab(tbChooseProject);
                    CurrentView = viewType;
                    break;

                case ViewType.EditProjectConfig:

                    tabMainLayout.SelectTab(tbEditProjectConfig);
                    CurrentView = viewType;
                    break;
          
                case ViewType.DBVersions:

                    tabMainLayout.SelectTab(tbDBVersionsMangement);
                    CurrentView = viewType;
                 
                    break;

                default:
                    break;
            }
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
            _viewModel.NavToChooseProject();
        }

      
    }
}

//********************************************************************************************************
// Product Name: MapWindow.Components.CategoriesViewer.dll Alpha
// Description:  The basic module for MapWindow.Components.CategoriesViewer version 6.0
//********************************************************************************************************
// The contents of this file are subject to the Mozilla Public License Version 1.1 (the "License"); 
// you may not use this file except in compliance with the License. You may obtain a copy of the License at 
// http://www.mozilla.org/MPL/ 
//
// Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF 
// ANY KIND, either expressed or implied. See the License for the specificlanguage governing rights and 
// limitations under the License. 
//
// The Original Code is from MapWindow.Components.CategoriesViewer.dll version 6.0
//
// The Initial Developer of this Original Code is Jiri Kadlec. Created 5/14/2009 4:13:28 PM
// 
// Contributor(s): (Open source contributors should list themselves and their modifications here). 
//
//********************************************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MapWindow.Data;
using MapWindow.Drawing;
using MapWindow.Forms;
using MapWindow.Tools;


namespace MapWindow.Components
{
    /// <summary>
    /// Dialog for the 'unique values' feature symbol classification scheme
    /// </summary>
    [ToolboxBitmap(typeof (RasterCategoryControl), "UserControl.ico")]
    public class RasterCategoryControl : UserControl
    {
        #region Events

        /// <summary>
        /// Occurs when the apply changes option has been triggered.
        /// </summary>
        public event EventHandler ChangesApplied;

        #endregion

        #region Private Variables


        //the original scheme which is modified only after clicking 'Apply'
        private IRasterLayer _originalLayer;

        //the local copy of the scheme
        private IRasterLayer _newLayer;

        //the attribute data Table
        private IRaster _raster;
        private IRasterSymbolizer _symbolizer;
        private IColorScheme _newScheme;
        private DataGridView dgvCategories;
        private DataGridViewImageColumn dataGridViewImageColumn1;
        private Button cmdRefresh;
        private ToolTip ttHelp;
        private IContainer components;
        private Button btnRamp;
        private NumericUpDown nudCategoryCount;
        private Label lblBreaks;
        private TabControl tabScheme;
        private Label label2;
        private ComboBox cmbInterval;
        private DataGridView dgvStatistics;
        private DataGridViewTextBoxColumn Stat;
        private DataGridViewTextBoxColumn Value;
        private TabPage tabStatistics;
        private Button btnAdd;
        private Button btnDelete;
        private bool _ignoreRefresh;
        SQLExpressionDialog _expressionDialog = new SQLExpressionDialog();
        private NumericUpDown nudSigFig;
        private Label lblSigFig;
        private Label label1;
        private ComboBox cmbIntervalSnapping;
        private bool _ignoreValidation;
        private Timer _cleanupTimer;
        private int _activeCategoryIndex;
        private bool _ignoreEnter;
        private double _minimum;
        private TabColorControl tccColorRange;
        private TabPage tabGraph;
        private CheckBox chkLog;
        private CheckBox chkShowStd;
        private CheckBox chkShowMean;
        private BreakSliderGraph breakSliderGraph1;
        private Label lblColumns;
        private NumericUpDown nudColumns;
        private Button btnQuick;
        private double _maximum;
        private mwProgressBar mwProgressBar1;
        private CheckBox chkHillshade;
        private Button btnShadedRelief;
        private System.Windows.Forms.ContextMenu _quickSchemes;
        private GroupBox groupBox1;
        private AngleControl angLightDirection;
        private DoubleBox dbxElevationFactor;
        private PropertyDialog _shadedReliefDialog;
        private TabColorDialog _tabColorDialog;
        private DoubleBox dbxMax;
        private DoubleBox dbxMin;
        private DataGridViewImageColumn colSymbol;
        private DataGridViewTextBoxColumn colValues;
        private DataGridViewTextBoxColumn colLegendText;
        private DataGridViewTextBoxColumn colCount;
        private int _dblClickEditIndex;
        #endregion

        #region Constructors

        /// <summary>
        /// Creates an empty FeatureCategoryControl without specifying any particular layer to use
        /// </summary>
        public RasterCategoryControl()
        {
           
            InitializeComponent();
            Configure();
        }

        /// <summary>
        /// Creates a new instance of the unique values category Table
        /// </summary>
        /// <param name="layer">The feature set that is used</param>
        public RasterCategoryControl(IRasterLayer layer)
        {
            InitializeComponent();
            Configure();
            Initialize(layer);
        }


        /// <summary>
        /// Handles the mouse whell, allowing the breakSldierGraph to zoom in or out.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            Point screenLoc = PointToScreen(e.Location);
            Point bsPoint = breakSliderGraph1.PointToClient(screenLoc);
            if(breakSliderGraph1.ClientRectangle.Contains(bsPoint))
            {
                breakSliderGraph1.DoMouseWheel(e.Delta, bsPoint.X);
                return;
            }
            base.OnMouseWheel(e);
        }

        private void Configure()
        {
            dgvCategories.CellFormatting += dgvCategories_CellFormatting;
            dgvCategories.CellDoubleClick += dgvCategories_CellDoubleClick;
            dgvCategories.SelectionChanged += new EventHandler(dgvCategories_SelectionChanged);
            dgvCategories.CellValidated += new DataGridViewCellEventHandler(dgvCategories_CellValidated);
            dgvCategories.MouseDown += new MouseEventHandler(dgvCategories_MouseDown);
            cmbInterval.SelectedItem = "EqualInterval";
            breakSliderGraph1.SliderSelected += new EventHandler<BreakSliderEventArgs>(breakSliderGraph1_SliderSelected);
            _quickSchemes = new ContextMenu();
            string[] names = Enum.GetNames(typeof(ColorSchemes));
            foreach (string name in names)
            {
                MenuItem mi = new MenuItem(name, QuickSchemeClicked);
                _quickSchemes.MenuItems.Add(mi);
            }
            cmbIntervalSnapping.Items.Clear();
            var result = Enum.GetValues(typeof (IntervalSnapMethods));
            foreach (var item in result)
            {
                cmbIntervalSnapping.Items.Add(item);
            }
            cmbIntervalSnapping.SelectedItem = IntervalSnapMethods.Rounding;
            _cleanupTimer = new Timer();
            _cleanupTimer.Interval = 10;
            _cleanupTimer.Tick += new EventHandler(_cleanupTimer_Tick);

            // Allows shaded Relief to be edited
            _shadedReliefDialog = new PropertyDialog();
            _shadedReliefDialog.ChangesApplied += PropertyDialog_ChangesApplied;
            
        }

        

        void dgvCategories_MouseDown(object sender, MouseEventArgs e)
        {
            if (_ignoreEnter) return;
            _activeCategoryIndex = dgvCategories.HitTest(e.X, e.Y).RowIndex;
        }

      

        void _cleanupTimer_Tick(object sender, EventArgs e)
        {
            // When a row validation causes rows above the edit row to be removed,
            // we can't easilly update the Table during the validation event.
            // The timer allows the validation to finish before updating the Table.
            _cleanupTimer.Stop();
            _ignoreValidation = true;
            UpdateTable();
            if(_activeCategoryIndex >=0 && _activeCategoryIndex < dgvCategories.Rows.Count)
            {
                dgvCategories.Rows[_activeCategoryIndex].Selected = true;
            }
            
            _ignoreValidation = false;
            _ignoreEnter = false;
        }

        void breakSliderGraph1_SliderSelected(object sender, BreakSliderEventArgs e)
        {
            int index = breakSliderGraph1.Breaks.IndexOf(e.Slider);
            dgvCategories.Rows[index].Selected = true;
        }

        void dgvCategories_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (_ignoreValidation) return;
            if (e.ColumnIndex == 2)
            {
                IColorCategory fctxt = _newScheme.Categories[e.RowIndex];
                fctxt.LegendText = (string)dgvCategories[e.ColumnIndex, e.RowIndex].Value;
                return;
            }

            if (e.ColumnIndex != 1) return;
            
            IColorCategory cb = _newScheme.Categories[e.RowIndex];
            if((string)dgvCategories[e.ColumnIndex, e.RowIndex].Value == cb.LegendText) return;
            _ignoreEnter = true;
            string exp = (string) dgvCategories[e.ColumnIndex, e.RowIndex].Value;
            cb.LegendText = exp;
            
            cb.Range = new Range(exp);
            if(cb.Range.Maximum != null && cb.Range.Maximum > _raster.Maximum)
            {
                cb.Range.Maximum = _raster.Maximum;
            }
            if(cb.Range.Minimum != null && cb.Range.Minimum > _raster.Maximum)
            {
                cb.Range.Minimum = _raster.Maximum;
            }
            if(cb.Range.Maximum != null && cb.Range.Minimum < _raster.Minimum)
            {
                cb.Range.Minimum = _raster.Minimum;
            }
            if (cb.Range.Minimum != null && cb.Range.Minimum < _raster.Minimum)
            {
                cb.Range.Minimum = _raster.Minimum;
            }
            cb.ApplyMinMax(_newScheme.EditorSettings);
            ColorCategoryCollection breaks = _newScheme.Categories;
            breaks.SuspendEvents();
            if (cb.Range.Minimum == null && cb.Range.Maximum == null)
            {
                breaks.Clear();
                breaks.Add(cb);
            }
            else if (cb.Range.Maximum == null)
            {
                List<IColorCategory> removeList = new List<IColorCategory>();

                int iPrev = e.RowIndex - 1;
                for (int i = 0; i < e.RowIndex; i++ )
                {
                     // If the specified max is below the minima of a lower range, remove the lower range.
                    if(breaks[i].Minimum > cb.Minimum)
                    {
                        removeList.Add(breaks[i]);
                        iPrev--;
                    }
                    else if (breaks[i].Maximum > cb.Minimum || i == iPrev)
                    {
                        // otherwise, if the maximum of a lower range is higher than the value, adjust it.
                        breaks[i].Maximum = cb.Minimum;
                        breaks[i].ApplyMinMax(_symbolizer.EditorSettings);
                    }
                }
                for (int i = e.RowIndex + 1; i < breaks.Count(); i++)
                {
                    // Since we have just assigned an absolute maximum, any previous categories
                    // that fell above the edited category should be removed.
                    removeList.Add(breaks[i]);
                }
                foreach (IColorCategory brk in removeList)
                {
                    // Do the actual removal.
                    breaks.Remove(brk);
                }
            }                
            else if (cb.Range.Minimum == null)
            {
                List<IColorCategory> removeList = new List<IColorCategory>();

                int iNext = e.RowIndex + 1;
                for (int i = e.RowIndex + 1; i < breaks.Count; i++)
                {
                    // If the specified max is below the minima of a lower range, remove the lower range.
                    if (breaks[i].Maximum < cb.Maximum)
                    {
                        removeList.Add(breaks[i]);
                        iNext++;
                    }
                    else if (breaks[i].Minimum < cb.Maximum || i == iNext)
                    {
                        // otherwise, if the maximum of a lower range is higher than the value, adjust it.
                        breaks[i].Minimum = cb.Maximum;
                        breaks[i].ApplyMinMax(_symbolizer.EditorSettings);

                    }
                }
                for (int i = 0; i < e.RowIndex; i++)
                {
                    // Since we have just assigned an absolute minimum, any previous categories
                    // that fell above the edited category should be removed.
                    removeList.Add(breaks[i]);
                }
             
                foreach (IColorCategory brk in removeList)
                {
                    // Do the actual removal.
                    breaks.Remove(brk);
                }
            }
            else 
            {
               // We have two values.  Adjust any above or below that conflict.
                List<IColorCategory> removeList = new List<IColorCategory>();
                int iPrev = e.RowIndex - 1;
                for (int i = 0; i < e.RowIndex; i++)
                {
                    // If the specified max is below the minima of a lower range, remove the lower range.
                    if (breaks[i].Minimum > cb.Minimum)
                    {
                        removeList.Add(breaks[i]);
                        iPrev--;
                    }
                    else if (breaks[i].Maximum > cb.Minimum || i == iPrev)
                    {
                        // otherwise, if the maximum of a lower range is higher than the value, adjust it.
                        breaks[i].Maximum = cb.Minimum;
                        breaks[i].ApplyMinMax(_symbolizer.EditorSettings);
                    }
                }
                int iNext = e.RowIndex + 1;
                for (int i = e.RowIndex + 1; i < breaks.Count; i++)
                {
                    // If the specified max is below the minima of a lower range, remove the lower range.
                    if (breaks[i].Maximum < cb.Maximum)
                    {
                        removeList.Add(breaks[i]);
                        iNext++;
                    }
                    else if (breaks[i].Minimum < cb.Maximum || i == iNext)
                    {
                        // otherwise, if the maximum of a lower range is higher than the value, adjust it.
                        breaks[i].Minimum = cb.Maximum;
                        breaks[i].ApplyMinMax(_symbolizer.EditorSettings);
                    }
                }
                foreach (IColorCategory brk in removeList)
                {
                    // Do the actual removal.
                    breaks.Remove(brk);
                }
               
                
            }
            breaks.ResumeEvents();
            _ignoreRefresh = true;
            cmbInterval.SelectedItem = IntervalMethods.Manual.ToString();
            _symbolizer.EditorSettings.IntervalMethod = IntervalMethods.Manual;
            _ignoreRefresh = false;
            UpdateStatistics(false);
            _cleanupTimer.Start();
            
            
        }

       

        void dgvCategories_SelectionChanged(object sender, EventArgs e)
        {
            if (breakSliderGraph1 == null) return;
            if (breakSliderGraph1.Breaks == null) return;
            if(dgvCategories.SelectedRows.Count > 0)
            {
                int index = dgvCategories.Rows.IndexOf(dgvCategories.SelectedRows[0]);
                if (breakSliderGraph1.Breaks.Count == 0 || index >= breakSliderGraph1.Breaks.Count) return;
                breakSliderGraph1.SelectBreak(breakSliderGraph1.Breaks[index]);
            }
            else
            {
                breakSliderGraph1.SelectBreak(null);
            }
            breakSliderGraph1.Invalidate();
        }

        /// <summary>
        /// Sets up the Table to work with the specified layer
        /// </summary>
        /// <param name="layer"></param>
        public void Initialize(IRasterLayer layer)
        {
            _originalLayer = layer;
            _newLayer = layer.Copy();
            _symbolizer = layer.Symbolizer;
            _newScheme = _symbolizer.Scheme;
            _raster = layer.DataSet;
            GetSettings();

        }

        

        #endregion

        
        #region Methods

       


       

        

        /// <summary>
        /// Updates the Table using the unique values
        /// </summary>
        private void UpdateTable()
        {
            dgvCategories.SuspendLayout();
            dgvCategories.Rows.Clear();

            ColorCategoryCollection breaks = _newScheme.Categories;
            int i = 0;
            if (breaks.Count > 0)
            {
                dgvCategories.Rows.Add(breaks.Count);
                foreach (IColorCategory brk in breaks)
                {

                    dgvCategories[1, i].Value = brk.Range.ToString(_symbolizer.EditorSettings.IntervalSnapMethod,
                                                                   _symbolizer.EditorSettings.IntervalRoundingDigits);
                    dgvCategories[2, i].Value = brk.LegendText;
                    i++;
                }
            }
            dgvCategories.ResumeLayout();
            dgvCategories.Invalidate();
        }

       

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the Maximum value currently displayed in the graph.
        /// </summary>
        public double Maximum
        {
            get { return _maximum; }
            set { _maximum = value; }
        }

        /// <summary>
        /// Gets or sets the Minimum value currently displayed in the graph.
        /// </summary>
        public double Minimum
        {
            get { return _minimum; }
            set { _minimum = value; }
        }


        #endregion

        #region Event Handlers

        /// <summary>
        /// Fires the apply changes situation externally, forcing the Table to 
        /// write its values to the original layer.
        /// </summary>
        public void ApplyChanges()
        {
            OnApplyChanges();
        }

        /// <summary>
        /// Applies the changes that have been specified in this control
        /// </summary>
        protected virtual void OnApplyChanges()
        {

            // SetSettings(); When applying a scheme settings are set, so don't bother here.
            _originalLayer.Symbolizer = _newLayer.Symbolizer.Copy();
            //_originalLayer.Symbolizer.Scheme.Categories.UpdateItemParentPointers();
            if (_originalLayer.Symbolizer.ShadedRelief.IsUsed)
            {
                if (_originalLayer.Symbolizer.ShadedRelief.HasChanged || _originalLayer.Symbolizer.HillShade == null)
                    _originalLayer.Symbolizer.CreateHillShade(mwProgressBar1);
            }
            _originalLayer.WriteBitmap(mwProgressBar1);
            if (ChangesApplied != null) ChangesApplied(_originalLayer, new EventArgs());
        }

      

      

        private void RefreshValues()
        {
            if (_ignoreRefresh) return;
            SetSettings();
            _newScheme.CreateCategories(_raster);
            UpdateTable();
            UpdateStatistics(true); // if the parameter is true, even on manual, the breaks are reset.
            breakSliderGraph1.Invalidate();
        }

        /// <summary>
        /// When the user double clicks the cell then we should display the detailed
        /// symbology dialog
        /// </summary>
        private void dgvCategories_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int count = _newScheme.Categories.Count;
            if (e.ColumnIndex == 0 && e.RowIndex < count)
            {
                _dblClickEditIndex = e.RowIndex;
                _tabColorDialog = new TabColorDialog();
                _tabColorDialog.ChangesApplied += new EventHandler(_tabColorDialog_ChangesApplied);
                _tabColorDialog.StartColor = _newScheme.Categories[_dblClickEditIndex].LowColor;
                _tabColorDialog.EndColor = _newScheme.Categories[_dblClickEditIndex].HighColor;
                _tabColorDialog.Show(ParentForm);
            }
        }

        void _tabColorDialog_ChangesApplied(object sender, EventArgs e)
        {
            if (_newScheme == null) return;
            if (_newScheme.Categories == null) return;
            if(_dblClickEditIndex < 0 || _dblClickEditIndex > _newScheme.Categories.Count) return;
            _newScheme.Categories[_dblClickEditIndex].LowColor = _tabColorDialog.StartColor;
            _newScheme.Categories[_dblClickEditIndex].HighColor = _tabColorDialog.EndColor;
            UpdateTable();
        }


        /// <summary>
        /// When the cell is formatted
        /// </summary>
        private void dgvCategories_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (_newScheme == null) return;
            int count = _newScheme.Categories.Count;
            if (count == 0) return;

            // Replace string values in the column with images.
            if (e.ColumnIndex != 0) return;
            Image img = e.Value as Image;
            if (img == null) return;
            Graphics g = Graphics.FromImage(img);
            g.Clear(Color.White);
            
            if (count > e.RowIndex)
            {

                Rectangle rect = new Rectangle(0, 0, img.Width, img.Height);
                _newScheme.DrawCategory(e.RowIndex, g, rect);
               
            }
            g.Dispose();
        }

        #endregion

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RasterCategoryControl));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ttHelp = new System.Windows.Forms.ToolTip(this.components);
            this.btnRamp = new System.Windows.Forms.Button();
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.btnQuick = new System.Windows.Forms.Button();
            this.angLightDirection = new MapWindow.Components.AngleControl();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.tabScheme = new System.Windows.Forms.TabControl();
            this.tabStatistics = new System.Windows.Forms.TabPage();
            this.dbxMax = new MapWindow.Components.DoubleBox();
            this.dbxMin = new MapWindow.Components.DoubleBox();
            this.nudSigFig = new System.Windows.Forms.NumericUpDown();
            this.lblSigFig = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbIntervalSnapping = new System.Windows.Forms.ComboBox();
            this.dgvStatistics = new System.Windows.Forms.DataGridView();
            this.Stat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbInterval = new System.Windows.Forms.ComboBox();
            this.nudCategoryCount = new System.Windows.Forms.NumericUpDown();
            this.lblBreaks = new System.Windows.Forms.Label();
            this.tabGraph = new System.Windows.Forms.TabPage();
            this.lblColumns = new System.Windows.Forms.Label();
            this.nudColumns = new System.Windows.Forms.NumericUpDown();
            this.chkLog = new System.Windows.Forms.CheckBox();
            this.chkShowStd = new System.Windows.Forms.CheckBox();
            this.chkShowMean = new System.Windows.Forms.CheckBox();
            this.breakSliderGraph1 = new MapWindow.Components.BreakSliderGraph();
            this.dgvCategories = new System.Windows.Forms.DataGridView();
            this.colSymbol = new System.Windows.Forms.DataGridViewImageColumn();
            this.colValues = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLegendText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkHillshade = new System.Windows.Forms.CheckBox();
            this.btnShadedRelief = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dbxElevationFactor = new MapWindow.Components.DoubleBox();
            this.mwProgressBar1 = new MapWindow.Components.mwProgressBar();
            this.tccColorRange = new MapWindow.Components.TabColorControl();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.tabScheme.SuspendLayout();
            this.tabStatistics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSigFig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatistics)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCategoryCount)).BeginInit();
            this.tabGraph.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRamp
            // 
            this.btnRamp.AccessibleDescription = null;
            this.btnRamp.AccessibleName = null;
            resources.ApplyResources(this.btnRamp, "btnRamp");
            this.btnRamp.BackgroundImage = null;
            this.btnRamp.Font = null;
            this.btnRamp.Name = "btnRamp";
            this.ttHelp.SetToolTip(this.btnRamp, resources.GetString("btnRamp.ToolTip"));
            this.btnRamp.UseVisualStyleBackColor = true;
            this.btnRamp.Click += new System.EventHandler(this.btnRamp_Click);
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.AccessibleDescription = null;
            this.cmdRefresh.AccessibleName = null;
            resources.ApplyResources(this.cmdRefresh, "cmdRefresh");
            this.cmdRefresh.BackgroundImage = null;
            this.cmdRefresh.Font = null;
            this.cmdRefresh.Name = "cmdRefresh";
            this.ttHelp.SetToolTip(this.cmdRefresh, resources.GetString("cmdRefresh.ToolTip"));
            this.cmdRefresh.UseVisualStyleBackColor = true;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // btnQuick
            // 
            this.btnQuick.AccessibleDescription = null;
            this.btnQuick.AccessibleName = null;
            resources.ApplyResources(this.btnQuick, "btnQuick");
            this.btnQuick.BackgroundImage = null;
            this.btnQuick.Font = null;
            this.btnQuick.Name = "btnQuick";
            this.ttHelp.SetToolTip(this.btnQuick, resources.GetString("btnQuick.ToolTip"));
            this.btnQuick.UseVisualStyleBackColor = true;
            this.btnQuick.Click += new System.EventHandler(this.btnQuick_Click);
            // 
            // angLightDirection
            // 
            this.angLightDirection.AccessibleDescription = null;
            this.angLightDirection.AccessibleName = null;
            resources.ApplyResources(this.angLightDirection, "angLightDirection");
            this.angLightDirection.Angle = 45;
            this.angLightDirection.BackColor = System.Drawing.SystemColors.Control;
            this.angLightDirection.BackgroundImage = null;
            this.angLightDirection.Caption = "Light Direction:";
            this.angLightDirection.Clockwise = true;
            this.angLightDirection.Font = null;
            this.angLightDirection.KnobColor = System.Drawing.Color.Green;
            this.angLightDirection.Name = "angLightDirection";
            this.angLightDirection.StartAngle = 90;
            this.ttHelp.SetToolTip(this.angLightDirection, resources.GetString("angLightDirection.ToolTip"));
            this.angLightDirection.AngleChanged += new System.EventHandler(this.angLightDirection_AngleChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleDescription = null;
            this.btnAdd.AccessibleName = null;
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.BackgroundImage = null;
            this.btnAdd.Font = null;
            this.btnAdd.Image = global::MapWindow.Images.add;
            this.btnAdd.Name = "btnAdd";
            this.ttHelp.SetToolTip(this.btnAdd, resources.GetString("btnAdd.ToolTip"));
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AccessibleDescription = null;
            this.btnDelete.AccessibleName = null;
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.BackgroundImage = null;
            this.btnDelete.Font = null;
            this.btnDelete.Image = global::MapWindow.Images.delete;
            this.btnDelete.Name = "btnDelete";
            this.ttHelp.SetToolTip(this.btnDelete, resources.GetString("btnDelete.ToolTip"));
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // tabScheme
            // 
            this.tabScheme.AccessibleDescription = null;
            this.tabScheme.AccessibleName = null;
            resources.ApplyResources(this.tabScheme, "tabScheme");
            this.tabScheme.BackgroundImage = null;
            this.tabScheme.Controls.Add(this.tabStatistics);
            this.tabScheme.Controls.Add(this.tabGraph);
            this.tabScheme.Font = null;
            this.tabScheme.Name = "tabScheme";
            this.tabScheme.SelectedIndex = 0;
            this.ttHelp.SetToolTip(this.tabScheme, resources.GetString("tabScheme.ToolTip"));
            // 
            // tabStatistics
            // 
            this.tabStatistics.AccessibleDescription = null;
            this.tabStatistics.AccessibleName = null;
            resources.ApplyResources(this.tabStatistics, "tabStatistics");
            this.tabStatistics.BackgroundImage = null;
            this.tabStatistics.Controls.Add(this.dbxMax);
            this.tabStatistics.Controls.Add(this.dbxMin);
            this.tabStatistics.Controls.Add(this.nudSigFig);
            this.tabStatistics.Controls.Add(this.lblSigFig);
            this.tabStatistics.Controls.Add(this.label1);
            this.tabStatistics.Controls.Add(this.cmbIntervalSnapping);
            this.tabStatistics.Controls.Add(this.dgvStatistics);
            this.tabStatistics.Controls.Add(this.label2);
            this.tabStatistics.Controls.Add(this.cmbInterval);
            this.tabStatistics.Controls.Add(this.nudCategoryCount);
            this.tabStatistics.Controls.Add(this.lblBreaks);
            this.tabStatistics.Font = null;
            this.tabStatistics.Name = "tabStatistics";
            this.ttHelp.SetToolTip(this.tabStatistics, resources.GetString("tabStatistics.ToolTip"));
            this.tabStatistics.UseVisualStyleBackColor = true;
            // 
            // dbxMax
            // 
            this.dbxMax.AccessibleDescription = null;
            this.dbxMax.AccessibleName = null;
            resources.ApplyResources(this.dbxMax, "dbxMax");
            this.dbxMax.BackColorInvalid = System.Drawing.Color.Salmon;
            this.dbxMax.BackColorRegular = System.Drawing.Color.Empty;
            this.dbxMax.BackgroundImage = null;
            this.dbxMax.Caption = "Exclude values >";
            this.dbxMax.Font = null;
            this.dbxMax.InvalidHelp = "The value entered could not be correctly parsed into a valid double precision flo" +
                "ating point value.";
            this.dbxMax.IsValid = true;
            this.dbxMax.Name = "dbxMax";
            this.dbxMax.NumberFormat = null;
            this.dbxMax.RegularHelp = "Enter a double precision floating point value.";
            this.ttHelp.SetToolTip(this.dbxMax, resources.GetString("dbxMax.ToolTip"));
            this.dbxMax.Value = 1000000;
            this.dbxMax.TextChanged += new System.EventHandler(this.dbxMax_TextChanged);
            // 
            // dbxMin
            // 
            this.dbxMin.AccessibleDescription = null;
            this.dbxMin.AccessibleName = null;
            resources.ApplyResources(this.dbxMin, "dbxMin");
            this.dbxMin.BackColorInvalid = System.Drawing.Color.Salmon;
            this.dbxMin.BackColorRegular = System.Drawing.Color.Empty;
            this.dbxMin.BackgroundImage = null;
            this.dbxMin.Caption = "Exclude Values < ";
            this.dbxMin.Font = null;
            this.dbxMin.InvalidHelp = "The value entered could not be correctly parsed into a valid double precision flo" +
                "ating point value.";
            this.dbxMin.IsValid = true;
            this.dbxMin.Name = "dbxMin";
            this.dbxMin.NumberFormat = null;
            this.dbxMin.RegularHelp = "Enter a double precision floating point value.";
            this.ttHelp.SetToolTip(this.dbxMin, resources.GetString("dbxMin.ToolTip"));
            this.dbxMin.Value = -100000;
            this.dbxMin.TextChanged += new System.EventHandler(this.dbxMin_TextChanged);
            // 
            // nudSigFig
            // 
            this.nudSigFig.AccessibleDescription = null;
            this.nudSigFig.AccessibleName = null;
            resources.ApplyResources(this.nudSigFig, "nudSigFig");
            this.nudSigFig.Font = null;
            this.nudSigFig.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nudSigFig.Name = "nudSigFig";
            this.ttHelp.SetToolTip(this.nudSigFig, resources.GetString("nudSigFig.ToolTip"));
            this.nudSigFig.ValueChanged += new System.EventHandler(this.nudSigFig_ValueChanged);
            // 
            // lblSigFig
            // 
            this.lblSigFig.AccessibleDescription = null;
            this.lblSigFig.AccessibleName = null;
            resources.ApplyResources(this.lblSigFig, "lblSigFig");
            this.lblSigFig.Font = null;
            this.lblSigFig.Name = "lblSigFig";
            this.ttHelp.SetToolTip(this.lblSigFig, resources.GetString("lblSigFig.ToolTip"));
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Font = null;
            this.label1.Name = "label1";
            this.ttHelp.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // cmbIntervalSnapping
            // 
            this.cmbIntervalSnapping.AccessibleDescription = null;
            this.cmbIntervalSnapping.AccessibleName = null;
            resources.ApplyResources(this.cmbIntervalSnapping, "cmbIntervalSnapping");
            this.cmbIntervalSnapping.BackgroundImage = null;
            this.cmbIntervalSnapping.Font = null;
            this.cmbIntervalSnapping.FormattingEnabled = true;
            this.cmbIntervalSnapping.Name = "cmbIntervalSnapping";
            this.ttHelp.SetToolTip(this.cmbIntervalSnapping, resources.GetString("cmbIntervalSnapping.ToolTip"));
            this.cmbIntervalSnapping.SelectedIndexChanged += new System.EventHandler(this.cmbIntervalSnapping_SelectedIndexChanged);
            // 
            // dgvStatistics
            // 
            this.dgvStatistics.AccessibleDescription = null;
            this.dgvStatistics.AccessibleName = null;
            this.dgvStatistics.AllowUserToAddRows = false;
            this.dgvStatistics.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AntiqueWhite;
            this.dgvStatistics.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(this.dgvStatistics, "dgvStatistics");
            this.dgvStatistics.BackgroundImage = null;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStatistics.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvStatistics.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStatistics.ColumnHeadersVisible = false;
            this.dgvStatistics.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Stat,
            this.Value});
            this.dgvStatistics.Font = null;
            this.dgvStatistics.Name = "dgvStatistics";
            this.dgvStatistics.RowHeadersVisible = false;
            this.dgvStatistics.ShowCellErrors = false;
            this.ttHelp.SetToolTip(this.dgvStatistics, resources.GetString("dgvStatistics.ToolTip"));
            // 
            // Stat
            // 
            resources.ApplyResources(this.Stat, "Stat");
            this.Stat.Name = "Stat";
            // 
            // Value
            // 
            this.Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            resources.ApplyResources(this.Value, "Value");
            this.Value.Name = "Value";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = null;
            this.label2.AccessibleName = null;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Font = null;
            this.label2.Name = "label2";
            this.ttHelp.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // cmbInterval
            // 
            this.cmbInterval.AccessibleDescription = null;
            this.cmbInterval.AccessibleName = null;
            resources.ApplyResources(this.cmbInterval, "cmbInterval");
            this.cmbInterval.BackgroundImage = null;
            this.cmbInterval.Font = null;
            this.cmbInterval.FormattingEnabled = true;
            this.cmbInterval.Items.AddRange(new object[] {
            resources.GetString("cmbInterval.Items"),
            resources.GetString("cmbInterval.Items1"),
            resources.GetString("cmbInterval.Items2")});
            this.cmbInterval.Name = "cmbInterval";
            this.ttHelp.SetToolTip(this.cmbInterval, resources.GetString("cmbInterval.ToolTip"));
            this.cmbInterval.SelectedIndexChanged += new System.EventHandler(this.cmbInterval_SelectedIndexChanged);
            // 
            // nudCategoryCount
            // 
            this.nudCategoryCount.AccessibleDescription = null;
            this.nudCategoryCount.AccessibleName = null;
            resources.ApplyResources(this.nudCategoryCount, "nudCategoryCount");
            this.nudCategoryCount.Font = null;
            this.nudCategoryCount.Name = "nudCategoryCount";
            this.ttHelp.SetToolTip(this.nudCategoryCount, resources.GetString("nudCategoryCount.ToolTip"));
            this.nudCategoryCount.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudCategoryCount.ValueChanged += new System.EventHandler(this.nudCategoryCount_ValueChanged);
            // 
            // lblBreaks
            // 
            this.lblBreaks.AccessibleDescription = null;
            this.lblBreaks.AccessibleName = null;
            resources.ApplyResources(this.lblBreaks, "lblBreaks");
            this.lblBreaks.Font = null;
            this.lblBreaks.Name = "lblBreaks";
            this.ttHelp.SetToolTip(this.lblBreaks, resources.GetString("lblBreaks.ToolTip"));
            // 
            // tabGraph
            // 
            this.tabGraph.AccessibleDescription = null;
            this.tabGraph.AccessibleName = null;
            resources.ApplyResources(this.tabGraph, "tabGraph");
            this.tabGraph.BackgroundImage = null;
            this.tabGraph.Controls.Add(this.lblColumns);
            this.tabGraph.Controls.Add(this.nudColumns);
            this.tabGraph.Controls.Add(this.chkLog);
            this.tabGraph.Controls.Add(this.chkShowStd);
            this.tabGraph.Controls.Add(this.chkShowMean);
            this.tabGraph.Controls.Add(this.breakSliderGraph1);
            this.tabGraph.Font = null;
            this.tabGraph.Name = "tabGraph";
            this.ttHelp.SetToolTip(this.tabGraph, resources.GetString("tabGraph.ToolTip"));
            this.tabGraph.UseVisualStyleBackColor = true;
            // 
            // lblColumns
            // 
            this.lblColumns.AccessibleDescription = null;
            this.lblColumns.AccessibleName = null;
            resources.ApplyResources(this.lblColumns, "lblColumns");
            this.lblColumns.Font = null;
            this.lblColumns.Name = "lblColumns";
            this.ttHelp.SetToolTip(this.lblColumns, resources.GetString("lblColumns.ToolTip"));
            // 
            // nudColumns
            // 
            this.nudColumns.AccessibleDescription = null;
            this.nudColumns.AccessibleName = null;
            resources.ApplyResources(this.nudColumns, "nudColumns");
            this.nudColumns.Font = null;
            this.nudColumns.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudColumns.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudColumns.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudColumns.Name = "nudColumns";
            this.ttHelp.SetToolTip(this.nudColumns, resources.GetString("nudColumns.ToolTip"));
            this.nudColumns.Value = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.nudColumns.ValueChanged += new System.EventHandler(this.nudColumns_ValueChanged);
            // 
            // chkLog
            // 
            this.chkLog.AccessibleDescription = null;
            this.chkLog.AccessibleName = null;
            resources.ApplyResources(this.chkLog, "chkLog");
            this.chkLog.BackgroundImage = null;
            this.chkLog.Font = null;
            this.chkLog.Name = "chkLog";
            this.ttHelp.SetToolTip(this.chkLog, resources.GetString("chkLog.ToolTip"));
            this.chkLog.UseVisualStyleBackColor = true;
            this.chkLog.CheckedChanged += new System.EventHandler(this.chkLog_CheckedChanged);
            // 
            // chkShowStd
            // 
            this.chkShowStd.AccessibleDescription = null;
            this.chkShowStd.AccessibleName = null;
            resources.ApplyResources(this.chkShowStd, "chkShowStd");
            this.chkShowStd.BackgroundImage = null;
            this.chkShowStd.Font = null;
            this.chkShowStd.Name = "chkShowStd";
            this.ttHelp.SetToolTip(this.chkShowStd, resources.GetString("chkShowStd.ToolTip"));
            this.chkShowStd.UseVisualStyleBackColor = true;
            this.chkShowStd.CheckedChanged += new System.EventHandler(this.chkShowStd_CheckedChanged);
            // 
            // chkShowMean
            // 
            this.chkShowMean.AccessibleDescription = null;
            this.chkShowMean.AccessibleName = null;
            resources.ApplyResources(this.chkShowMean, "chkShowMean");
            this.chkShowMean.BackgroundImage = null;
            this.chkShowMean.Font = null;
            this.chkShowMean.Name = "chkShowMean";
            this.ttHelp.SetToolTip(this.chkShowMean, resources.GetString("chkShowMean.ToolTip"));
            this.chkShowMean.UseVisualStyleBackColor = true;
            this.chkShowMean.CheckedChanged += new System.EventHandler(this.chkShowMean_CheckedChanged);
            // 
            // breakSliderGraph1
            // 
            this.breakSliderGraph1.AccessibleDescription = null;
            this.breakSliderGraph1.AccessibleName = null;
            resources.ApplyResources(this.breakSliderGraph1, "breakSliderGraph1");
            this.breakSliderGraph1.AttributeSource = null;
            this.breakSliderGraph1.BackColor = System.Drawing.Color.White;
            this.breakSliderGraph1.BackgroundImage = null;
            this.breakSliderGraph1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.breakSliderGraph1.BreakColor = System.Drawing.Color.Blue;
            this.breakSliderGraph1.BreakSelectedColor = System.Drawing.Color.Red;
            this.breakSliderGraph1.Font = null;
            this.breakSliderGraph1.FontColor = System.Drawing.Color.Black;
            this.breakSliderGraph1.IntervalMethod = MapWindow.Drawing.IntervalMethods.EqualInterval;
            this.breakSliderGraph1.LogY = false;
            this.breakSliderGraph1.MaximumSampleSize = 10000;
            this.breakSliderGraph1.MinHeight = 20;
            this.breakSliderGraph1.Name = "breakSliderGraph1";
            this.breakSliderGraph1.NumColumns = 40;
            this.breakSliderGraph1.RasterLayer = null;
            this.breakSliderGraph1.Scheme = null;
            this.breakSliderGraph1.ShowMean = false;
            this.breakSliderGraph1.ShowStandardDeviation = false;
            this.breakSliderGraph1.Title = "Statistical Breaks:";
            this.breakSliderGraph1.TitleColor = System.Drawing.Color.Black;
            this.breakSliderGraph1.TitleFont = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.ttHelp.SetToolTip(this.breakSliderGraph1, resources.GetString("breakSliderGraph1.ToolTip"));
            this.breakSliderGraph1.SliderMoved += new System.EventHandler<MapWindow.Components.BreakSliderEventArgs>(this.breakSliderGraph1_SliderMoved);
            // 
            // dgvCategories
            // 
            this.dgvCategories.AccessibleDescription = null;
            this.dgvCategories.AccessibleName = null;
            this.dgvCategories.AllowUserToAddRows = false;
            resources.ApplyResources(this.dgvCategories, "dgvCategories");
            this.dgvCategories.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCategories.BackgroundImage = null;
            this.dgvCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCategories.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSymbol,
            this.colValues,
            this.colLegendText,
            this.colCount});
            this.dgvCategories.Font = null;
            this.dgvCategories.MultiSelect = false;
            this.dgvCategories.Name = "dgvCategories";
            this.dgvCategories.RowHeadersVisible = false;
            this.dgvCategories.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ttHelp.SetToolTip(this.dgvCategories, resources.GetString("dgvCategories.ToolTip"));
            // 
            // colSymbol
            // 
            this.colSymbol.FillWeight = 49.97129F;
            resources.ApplyResources(this.colSymbol, "colSymbol");
            this.colSymbol.Image = global::MapWindow.Images.info;
            this.colSymbol.Name = "colSymbol";
            this.colSymbol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // colValues
            // 
            this.colValues.FillWeight = 142.132F;
            resources.ApplyResources(this.colValues, "colValues");
            this.colValues.Name = "colValues";
            this.colValues.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colLegendText
            // 
            this.colLegendText.FillWeight = 157.008F;
            resources.ApplyResources(this.colLegendText, "colLegendText");
            this.colLegendText.Name = "colLegendText";
            // 
            // colCount
            // 
            this.colCount.FillWeight = 50.88878F;
            resources.ApplyResources(this.colCount, "colCount");
            this.colCount.Name = "colCount";
            this.colCount.ReadOnly = true;
            this.colCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // chkHillshade
            // 
            this.chkHillshade.AccessibleDescription = null;
            this.chkHillshade.AccessibleName = null;
            resources.ApplyResources(this.chkHillshade, "chkHillshade");
            this.chkHillshade.BackgroundImage = null;
            this.chkHillshade.Font = null;
            this.chkHillshade.Name = "chkHillshade";
            this.ttHelp.SetToolTip(this.chkHillshade, resources.GetString("chkHillshade.ToolTip"));
            this.chkHillshade.UseVisualStyleBackColor = true;
            this.chkHillshade.CheckedChanged += new System.EventHandler(this.chkHillshade_CheckedChanged);
            // 
            // btnShadedRelief
            // 
            this.btnShadedRelief.AccessibleDescription = null;
            this.btnShadedRelief.AccessibleName = null;
            resources.ApplyResources(this.btnShadedRelief, "btnShadedRelief");
            this.btnShadedRelief.BackgroundImage = null;
            this.btnShadedRelief.Font = null;
            this.btnShadedRelief.Name = "btnShadedRelief";
            this.ttHelp.SetToolTip(this.btnShadedRelief, resources.GetString("btnShadedRelief.ToolTip"));
            this.btnShadedRelief.UseVisualStyleBackColor = true;
            this.btnShadedRelief.Click += new System.EventHandler(this.btnShadedRelief_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = null;
            this.groupBox1.AccessibleName = null;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.BackgroundImage = null;
            this.groupBox1.Controls.Add(this.dbxElevationFactor);
            this.groupBox1.Controls.Add(this.angLightDirection);
            this.groupBox1.Controls.Add(this.chkHillshade);
            this.groupBox1.Controls.Add(this.btnShadedRelief);
            this.groupBox1.Font = null;
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.ttHelp.SetToolTip(this.groupBox1, resources.GetString("groupBox1.ToolTip"));
            // 
            // dbxElevationFactor
            // 
            this.dbxElevationFactor.AccessibleDescription = null;
            this.dbxElevationFactor.AccessibleName = null;
            resources.ApplyResources(this.dbxElevationFactor, "dbxElevationFactor");
            this.dbxElevationFactor.BackColorInvalid = System.Drawing.Color.Salmon;
            this.dbxElevationFactor.BackColorRegular = System.Drawing.Color.Empty;
            this.dbxElevationFactor.BackgroundImage = null;
            this.dbxElevationFactor.Caption = "Elevation Factor";
            this.dbxElevationFactor.Font = null;
            this.dbxElevationFactor.InvalidHelp = "The value entered could not be correctly parsed into a valid double precision flo" +
                "ating point value.";
            this.dbxElevationFactor.IsValid = true;
            this.dbxElevationFactor.Name = "dbxElevationFactor";
            this.dbxElevationFactor.NumberFormat = "E7";
            this.dbxElevationFactor.RegularHelp = "The Elevation Factor is a constant multiplier that converts the elevation unit (e" +
                "g. feet) into the projection units (eg. decimal degrees).";
            this.ttHelp.SetToolTip(this.dbxElevationFactor, resources.GetString("dbxElevationFactor.ToolTip"));
            this.dbxElevationFactor.Value = 0;
            this.dbxElevationFactor.TextChanged += new System.EventHandler(this.dbxElevationFactor_TextChanged);
            // 
            // mwProgressBar1
            // 
            this.mwProgressBar1.AccessibleDescription = null;
            this.mwProgressBar1.AccessibleName = null;
            resources.ApplyResources(this.mwProgressBar1, "mwProgressBar1");
            this.mwProgressBar1.BackgroundImage = null;
            this.mwProgressBar1.Font = null;
            this.mwProgressBar1.Name = "mwProgressBar1";
            this.ttHelp.SetToolTip(this.mwProgressBar1, resources.GetString("mwProgressBar1.ToolTip"));
            // 
            // tccColorRange
            // 
            this.tccColorRange.AccessibleDescription = null;
            this.tccColorRange.AccessibleName = null;
            resources.ApplyResources(this.tccColorRange, "tccColorRange");
            this.tccColorRange.BackgroundImage = null;
            this.tccColorRange.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tccColorRange.Font = null;
            this.tccColorRange.HueShift = 0;
            this.tccColorRange.Name = "tccColorRange";
            this.tccColorRange.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ttHelp.SetToolTip(this.tccColorRange, resources.GetString("tccColorRange.ToolTip"));
            this.tccColorRange.UseRangeChecked = true;
            this.tccColorRange.ColorChanged += new System.EventHandler<MapWindow.Components.ColorRangeEventArgs>(this.tccColorRange_ColorChanged);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.FillWeight = 76.14214F;
            resources.ApplyResources(this.dataGridViewImageColumn1, "dataGridViewImageColumn1");
            this.dataGridViewImageColumn1.Image = global::MapWindow.Images.info;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // RasterCategoryControl
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.BackgroundImage = null;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mwProgressBar1);
            this.Controls.Add(this.btnQuick);
            this.Controls.Add(this.tccColorRange);
            this.Controls.Add(this.tabScheme);
            this.Controls.Add(this.dgvCategories);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnRamp);
            this.Controls.Add(this.cmdRefresh);
            this.Font = null;
            this.Name = "RasterCategoryControl";
            this.ttHelp.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.tabScheme.ResumeLayout(false);
            this.tabStatistics.ResumeLayout(false);
            this.tabStatistics.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSigFig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStatistics)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCategoryCount)).EndInit();
            this.tabGraph.ResumeLayout(false);
            this.tabGraph.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

       

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            _symbolizer.EditorSettings.RampColors = false;
            RefreshValues();
        }

        private void btnRamp_Click(object sender, EventArgs e)
        {
            _symbolizer.EditorSettings.RampColors = true;
            RefreshValues();
        }

        private void GetSettings()
        {
            _ignoreRefresh = true;
            EditorSettings settings = _symbolizer.EditorSettings;
            tccColorRange.Initialize(new ColorRangeEventArgs(settings.StartColor, settings.EndColor, settings.HueShift, settings.HueSatLight, settings.UseColorRange));           
            UpdateTable();
            cmbInterval.SelectedItem = settings.IntervalMethod.ToString();
            UpdateStatistics(false);
            nudCategoryCount.Value = _newScheme.EditorSettings.NumBreaks;
            cmbIntervalSnapping.SelectedItem = settings.IntervalSnapMethod;
            nudSigFig.Value = settings.IntervalRoundingDigits;
            angLightDirection.Angle = (int)_symbolizer.ShadedRelief.LightDirection;
            dbxElevationFactor.Value = _symbolizer.ShadedRelief.ElevationFactor;
            chkHillshade.Checked = _symbolizer.ShadedRelief.IsUsed;
            _ignoreRefresh = false;
        }
        
        private void SetSettings()
        {
            if (_ignoreRefresh) return;
            EditorSettings settings = _symbolizer.EditorSettings;
            settings.NumBreaks = (int)nudCategoryCount.Value;
            settings.IntervalSnapMethod = (IntervalSnapMethods)cmbIntervalSnapping.SelectedItem;
            settings.IntervalRoundingDigits = (int)nudSigFig.Value;
            
        }

        private void UpdateStatistics(bool clear)
        {

            // Graph
            SetSettings();
            breakSliderGraph1.RasterLayer = _newLayer;
            breakSliderGraph1.Title = _newLayer.LegendText;
            breakSliderGraph1.ResetExtents();
            if(_symbolizer.EditorSettings.IntervalMethod == IntervalMethods.Manual && !clear)
            {
                breakSliderGraph1.UpdateBreaks();
            }
            else
            {
                breakSliderGraph1.ResetBreaks(null);
            }
            Statistics stats = breakSliderGraph1.Statistics;

            // Stat list
            dgvStatistics.Rows.Clear();
            dgvStatistics.Rows.Add(7);
            dgvStatistics[0, 0].Value = "Count";
            dgvStatistics[1, 0].Value = stats.Count.ToString("#,###"); 
            dgvStatistics[0, 1].Value = "Min";
            dgvStatistics[1, 1].Value = stats.Minimum.ToString("#,###"); 
            dgvStatistics[0, 2].Value = "Max";
            dgvStatistics[1, 2].Value = stats.Maximum.ToString("#,###");
            dgvStatistics[0, 3].Value = "Sum";
            dgvStatistics[1, 3].Value = stats.Sum.ToString("#,###"); 
            dgvStatistics[0, 4].Value = "Mean";
            dgvStatistics[1, 4].Value = stats.Mean.ToString("#,###"); 
            dgvStatistics[0, 5].Value = "Median";
            dgvStatistics[1, 5].Value = stats.Median.ToString("#,###"); 
            dgvStatistics[0, 6].Value = "Std";
            dgvStatistics[1, 6].Value = stats.StandardDeviation.ToString("#,###"); 
        }

        private void nudCategoryCount_ValueChanged(object sender, EventArgs e)
        {
            if (_ignoreRefresh) return;
            _ignoreRefresh = true;
            cmbInterval.SelectedItem = IntervalMethods.EqualInterval.ToString();
            _ignoreRefresh = false;
            RefreshValues();
        }

        private void chkLog_CheckedChanged(object sender, EventArgs e)
        {
            breakSliderGraph1.LogY = chkLog.Checked;
        }

        private void chkShowMean_CheckedChanged(object sender, EventArgs e)
        {
            breakSliderGraph1.ShowMean = chkShowMean.Checked;
        }

        private void chkShowStd_CheckedChanged(object sender, EventArgs e)
        {
            breakSliderGraph1.ShowStandardDeviation = chkShowStd.Checked;
        }

        private void cmbInterval_SelectedIndexChanged(object sender, EventArgs e)
        {
            IntervalMethods method = (IntervalMethods)Enum.Parse(typeof (IntervalMethods), cmbInterval.SelectedItem.ToString());
            if (_symbolizer == null) return;
            _symbolizer.EditorSettings.IntervalMethod = method;
            RefreshValues();
        }

        private void breakSliderGraph1_SliderMoved(object sender, BreakSliderEventArgs e)
        {
            _ignoreRefresh = true;
            cmbInterval.SelectedItem = "Manual";
            _ignoreRefresh = false;
            _symbolizer.EditorSettings.IntervalMethod = IntervalMethods.Manual;
            int index = _newScheme.Categories.IndexOf(e.Slider.Category as IColorCategory);
            if (index == -1) return;
            UpdateTable();
            dgvCategories.Rows[index].Selected = true;
            
        }

        private void nudColumns_ValueChanged(object sender, EventArgs e)
        {
            breakSliderGraph1.NumColumns = (int)nudColumns.Value;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            nudCategoryCount.Value += 1;
          
            
        }

       

      
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if(dgvCategories.SelectedRows.Count == 0) return;
            List<IColorCategory> deleteList = new List<IColorCategory>();
            ColorCategoryCollection categories = _newScheme.Categories;
            int count = 0;
            foreach (DataGridViewRow row in dgvCategories.SelectedRows)
            {
                int index = dgvCategories.Rows.IndexOf(row);
                deleteList.Add(categories[index]);
                count++;
            }
            foreach (IColorCategory category in deleteList)
            {
                int index = categories.IndexOf(category);
                if (index > 0 && index < categories.Count - 1)
                {
                    categories[index - 1].Maximum = categories[index + 1].Minimum;
                    categories[index - 1].ApplyMinMax(_newScheme.EditorSettings);
                }
                _newScheme.RemoveCategory(category);
                breakSliderGraph1.UpdateBreaks();
                
               
            }
            UpdateTable();
            _newScheme.EditorSettings.IntervalMethod = IntervalMethods.Manual;
            _newScheme.EditorSettings.NumBreaks -= count;
            UpdateStatistics(false);
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (dgvCategories.IsCurrentCellInEditMode)
            {
                dgvCategories.EndEdit();
            }
            _ignoreValidation = true;
            int index = dgvCategories.SelectedRows[0].Index;
            IColorCategory cat = _newScheme.Categories[index];
            if (!_newScheme.DecreaseCategoryIndex(cat)) return;
            UpdateTable();
            index--;
            dgvCategories.Rows[index].Selected = true;
            _ignoreValidation = false;
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if(dgvCategories.IsCurrentCellInEditMode)
            {
                dgvCategories.EndEdit();
            }
            _ignoreValidation = true;
            int index = dgvCategories.SelectedRows[0].Index;
            IColorCategory cat = _newScheme.Categories[index];
            if(!_newScheme.IncreaseCategoryIndex(cat)) return;
            index++;
            UpdateTable();
            dgvCategories.Rows[index].Selected = true;
            _ignoreValidation = false;
        }

        private void nudSigFig_ValueChanged(object sender, EventArgs e)
        {
            if (_newScheme == null) return;
            _newScheme.EditorSettings.IntervalRoundingDigits = (int)nudSigFig.Value;
            
            
            RefreshValues();
        }

        private void cmbIntervalSnapping_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_newScheme == null) return;
            IntervalSnapMethods method =  (IntervalSnapMethods)cmbIntervalSnapping.SelectedItem;
            _newScheme.EditorSettings.IntervalSnapMethod = method;
            switch (method)
            {
                case IntervalSnapMethods.SignificantFigures:
                    lblSigFig.Visible = true;
                    nudSigFig.Visible = true;
                    nudSigFig.Minimum = 1;
                    lblSigFig.Text = "Significant Figures:";
                    break;
                case IntervalSnapMethods.Rounding:
                    nudSigFig.Visible = true;
                    lblSigFig.Visible = true;
                    nudSigFig.Minimum = 0;
                    lblSigFig.Text = "Rounding Digits:";
                    break;
                case IntervalSnapMethods.None:
                    lblSigFig.Visible = false;
                    nudSigFig.Visible = false;
                    break;
                case IntervalSnapMethods.DataValue:
                    lblSigFig.Visible = false;
                    nudSigFig.Visible = false;
                    break;
            }


            RefreshValues();
        }

        


        /// <summary>
        /// Handles disposing unmanaged memory
        /// </summary>
        /// <param name="disposing">The disposed item</param>
        protected override void Dispose(bool disposing)
        {
            breakSliderGraph1.Dispose();
            if(components != null && disposing)
            {
                foreach (Control control in components.Components)
                {
                    control.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void tccColorRange_ColorChanged(object sender, ColorRangeEventArgs e)
        {
            if (_ignoreRefresh) return;
            RasterEditorSettings settings = _newScheme.EditorSettings;
            settings.StartColor = e.StartColor;
            settings.EndColor = e.EndColor;
            settings.UseColorRange = e.UseColorRange;
            settings.HueShift = e.HueShift;
            settings.HueSatLight = e.HSL;
        }

        private void btnQuick_Click(object sender, EventArgs e)
        {
            _quickSchemes.Show(btnQuick, new Point(0,0));
        }

        private void QuickSchemeClicked(object sender, EventArgs e)
        {
            _ignoreRefresh = true;
            _newScheme.EditorSettings.NumBreaks = 2;
            nudCategoryCount.Value = 2;
            _ignoreRefresh = false;
            MenuItem mi = sender as MenuItem;
            if (mi == null) return;
            ColorSchemes cs = (ColorSchemes)Enum.Parse(typeof (ColorSchemes), mi.Text);
            _newScheme.ApplyScheme(cs, _raster);
            UpdateTable();
            UpdateStatistics(true); // if the parameter is true, even on manual, the breaks are reset.
            breakSliderGraph1.Invalidate();
        }

        private void chkHillshade_CheckedChanged(object sender, EventArgs e)
        {
            _symbolizer.ShadedRelief.IsUsed = chkHillshade.Checked;
        }

        private void btnShadedRelief_Click(object sender, EventArgs e)
        {
            _shadedReliefDialog.PropertyGrid.SelectedObject = _symbolizer.ShadedRelief.Copy();
            _shadedReliefDialog.ShowDialog();
        }

        void PropertyDialog_ChangesApplied(object sender, EventArgs e)
        {
            _symbolizer.ShadedRelief = (_shadedReliefDialog.PropertyGrid.SelectedObject as IShadedRelief).Copy();
            angLightDirection.Angle = (int)_symbolizer.ShadedRelief.LightDirection;
            dbxElevationFactor.Value = _symbolizer.ShadedRelief.ElevationFactor;
        }

        private void angLightDirection_AngleChanged(object sender, EventArgs e)
        {
            _symbolizer.ShadedRelief.LightDirection = angLightDirection.Angle;
        }

        private void dbxElevationFactor_TextChanged(object sender, EventArgs e)
        {
            _symbolizer.ShadedRelief.ElevationFactor = (float)dbxElevationFactor.Value;
        }

        private void dbxMin_TextChanged(object sender, EventArgs e)
        {
            _symbolizer.EditorSettings.Min = dbxMin.Value;
            _symbolizer.Scheme.CreateCategories(_raster);
            UpdateStatistics(true); // if the parameter is true, even on manual, the breaks are reset.
            UpdateTable();
        }

        private void dbxMax_TextChanged(object sender, EventArgs e)
        {
            _symbolizer.EditorSettings.Max = dbxMax.Value;
            _symbolizer.Scheme.CreateCategories(_raster);
            UpdateStatistics(true); // if the parameter is true, even on manual, the breaks are reset.
            UpdateTable();
        }

       

      

        

      
       
       
      

      

       

    }
}
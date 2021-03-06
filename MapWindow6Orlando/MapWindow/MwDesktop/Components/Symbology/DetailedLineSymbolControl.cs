//********************************************************************************************************
// Product Name: MapWindow.dll Alpha
// Description:  The core assembly for the MapWindow 6.0 distribution.
//********************************************************************************************************
// The contents of this file are subject to the Mozilla Public License Version 1.1 (the "License"); 
// you may not use this file except in compliance with the License. You may obtain a copy of the License at 
// http://www.mozilla.org/MPL/ 
//
// Software distributed under the License is distributed on an "AS IS" basis, WITHOUT WARRANTY OF 
// ANY KIND, either expressed or implied. See the License for the specific language governing rights and 
// limitations under the License. 
//
// The Original Code is MapWindow.dll
//
// The Initial Developer of this Original Code is Ted Dunsford. Created 4/28/2009 4:21:06 PM
// 
// Contributor(s): (Open source contributors should list themselves and their modifications here). 
//
//********************************************************************************************************
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MapWindow.Components;
using MapWindow.Drawing;
namespace MapWindow.Forms
{
    /// <summary>
    /// DetailedLineSymbolDialog
    /// </summary>
    public class DetailedLineSymbolControl : UserControl
    {

        #region Events

        /// <summary>
        /// Fires an event indicating that changes should be applied.
        /// </summary>
        public event EventHandler ChangesApplied;

        /// <summary>
        /// Occurs when the user clicks the AddToCustomSymbols button
        /// </summary>
        public event EventHandler<LineSymbolizerEventArgs> AddToCustomSymbols;

        #endregion

        #region Private Variables

        private ComboBox cmbStrokeType;
        private ILineSymbolizer _symbolizer;
        private CheckBox chkSmoothing;
        private GroupBox groupBox1;
        private Label label1;
        private Label lblScaleMode;
        private ComboBox cmbScaleMode;
        private TabControl tabStrokeProperties;
        private TabPage tabDash;
        private bool _ignoreChanges;
        private Label label3;
        private Label lblPreview;
        private DashControl dashControl1;
        private TabPage tabCartographic;
        private DoubleBox dblWidthCartographic;
        private LineJoinControl radLineJoin;
        private Label lblStartCap;
        private GroupBox grpCaps;
        private ComboBox cmbEndCap;
        private Label label4;
        private ComboBox cmbStartCap;
        private TabPage tabDecoration;
        private GroupBox grpFlip;
        private CheckBox chkFlipAll;
        private GroupBox grpRotation;
        private RadioButton radRotationFixed;
        private RadioButton radRotationWithLine;
        private CheckBox chkFlipFirst;
        private Label lblNumberOfPositions;
        private NumericUpDown nudDecorationCount;
        private Button btnEdit;
        private Label label6;
        private Label lblDecorationPreview;
        private StrokeCollectionControl ccStrokes;
        private DecorationCollectionControl ccDecorations;
        private DoubleBox dblOffset;
        private ToolTip ttHelp;
        private DoubleBox dblStrokeOffset;
        private TabPage tabSimple;
        private ComboBox cmbStrokeStyle;
        private Label label2;
        private DoubleBox dblWidth;
        private RampSlider sldOpacityCartographic;
        private Label lblOpacityCartographic;
        private RampSlider sldOpacitySimple;
        private ColorButton cbColorCartographic;
        private Label lblColorCartographic;
        private Label label9;
        private ColorButton cbColorSimple;
        private Label label5;
        private ILineSymbolizer _original;

       

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components;



        #endregion


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetailedLineSymbolControl));
            this.cmbStrokeType = new System.Windows.Forms.ComboBox();
            this.chkSmoothing = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPreview = new System.Windows.Forms.Label();
            this.lblScaleMode = new System.Windows.Forms.Label();
            this.cmbScaleMode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabStrokeProperties = new System.Windows.Forms.TabControl();
            this.tabSimple = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cbColorSimple = new MapWindow.Components.ColorButton();
            this.sldOpacitySimple = new MapWindow.Components.RampSlider();
            this.cmbStrokeStyle = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dblWidth = new MapWindow.Components.DoubleBox();
            this.tabCartographic = new System.Windows.Forms.TabPage();
            this.lblColorCartographic = new System.Windows.Forms.Label();
            this.lblOpacityCartographic = new System.Windows.Forms.Label();
            this.grpCaps = new System.Windows.Forms.GroupBox();
            this.lblStartCap = new System.Windows.Forms.Label();
            this.cmbEndCap = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbStartCap = new System.Windows.Forms.ComboBox();
            this.cbColorCartographic = new MapWindow.Components.ColorButton();
            this.sldOpacityCartographic = new MapWindow.Components.RampSlider();
            this.dblStrokeOffset = new MapWindow.Components.DoubleBox();
            this.radLineJoin = new MapWindow.Components.LineJoinControl();
            this.dblWidthCartographic = new MapWindow.Components.DoubleBox();
            this.tabDash = new System.Windows.Forms.TabPage();
            this.dashControl1 = new MapWindow.Components.DashControl();
            this.tabDecoration = new System.Windows.Forms.TabPage();
            this.dblOffset = new MapWindow.Components.DoubleBox();
            this.ccDecorations = new MapWindow.Components.DecorationCollectionControl();
            this.btnEdit = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.lblDecorationPreview = new System.Windows.Forms.Label();
            this.lblNumberOfPositions = new System.Windows.Forms.Label();
            this.nudDecorationCount = new System.Windows.Forms.NumericUpDown();
            this.grpRotation = new System.Windows.Forms.GroupBox();
            this.radRotationFixed = new System.Windows.Forms.RadioButton();
            this.radRotationWithLine = new System.Windows.Forms.RadioButton();
            this.grpFlip = new System.Windows.Forms.GroupBox();
            this.chkFlipFirst = new System.Windows.Forms.CheckBox();
            this.chkFlipAll = new System.Windows.Forms.CheckBox();
            this.ttHelp = new System.Windows.Forms.ToolTip(this.components);
            this.ccStrokes = new MapWindow.Components.StrokeCollectionControl();
            this.groupBox1.SuspendLayout();
            this.tabStrokeProperties.SuspendLayout();
            this.tabSimple.SuspendLayout();
            this.tabCartographic.SuspendLayout();
            this.grpCaps.SuspendLayout();
            this.tabDash.SuspendLayout();
            this.tabDecoration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDecorationCount)).BeginInit();
            this.grpRotation.SuspendLayout();
            this.grpFlip.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbStrokeType
            // 
            this.cmbStrokeType.AccessibleDescription = null;
            this.cmbStrokeType.AccessibleName = null;
            resources.ApplyResources(this.cmbStrokeType, "cmbStrokeType");
            this.cmbStrokeType.BackgroundImage = null;
            this.cmbStrokeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStrokeType.Font = null;
            this.cmbStrokeType.FormattingEnabled = true;
            this.cmbStrokeType.Items.AddRange(new object[] {
            resources.GetString("cmbStrokeType.Items"),
            resources.GetString("cmbStrokeType.Items1")});
            this.cmbStrokeType.Name = "cmbStrokeType";
            this.ttHelp.SetToolTip(this.cmbStrokeType, resources.GetString("cmbStrokeType.ToolTip"));
            this.cmbStrokeType.SelectedIndexChanged += new System.EventHandler(this.cmbStrokeType_SelectedIndexChanged);
            // 
            // chkSmoothing
            // 
            this.chkSmoothing.AccessibleDescription = null;
            this.chkSmoothing.AccessibleName = null;
            resources.ApplyResources(this.chkSmoothing, "chkSmoothing");
            this.chkSmoothing.BackgroundImage = null;
            this.chkSmoothing.Font = null;
            this.chkSmoothing.Name = "chkSmoothing";
            this.ttHelp.SetToolTip(this.chkSmoothing, resources.GetString("chkSmoothing.ToolTip"));
            this.chkSmoothing.UseVisualStyleBackColor = true;
            this.chkSmoothing.CheckedChanged += new System.EventHandler(this.chkSmoothing_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = null;
            this.groupBox1.AccessibleName = null;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.BackgroundImage = null;
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblPreview);
            this.groupBox1.Controls.Add(this.lblScaleMode);
            this.groupBox1.Controls.Add(this.cmbScaleMode);
            this.groupBox1.Controls.Add(this.chkSmoothing);
            this.groupBox1.Font = null;
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.ttHelp.SetToolTip(this.groupBox1, resources.GetString("groupBox1.ToolTip"));
            // 
            // label3
            // 
            this.label3.AccessibleDescription = null;
            this.label3.AccessibleName = null;
            resources.ApplyResources(this.label3, "label3");
            this.label3.Font = null;
            this.label3.Name = "label3";
            this.ttHelp.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // lblPreview
            // 
            this.lblPreview.AccessibleDescription = null;
            this.lblPreview.AccessibleName = null;
            resources.ApplyResources(this.lblPreview, "lblPreview");
            this.lblPreview.BackColor = System.Drawing.Color.White;
            this.lblPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPreview.Font = null;
            this.lblPreview.Name = "lblPreview";
            this.ttHelp.SetToolTip(this.lblPreview, resources.GetString("lblPreview.ToolTip"));
            // 
            // lblScaleMode
            // 
            this.lblScaleMode.AccessibleDescription = null;
            this.lblScaleMode.AccessibleName = null;
            resources.ApplyResources(this.lblScaleMode, "lblScaleMode");
            this.lblScaleMode.Font = null;
            this.lblScaleMode.Name = "lblScaleMode";
            this.ttHelp.SetToolTip(this.lblScaleMode, resources.GetString("lblScaleMode.ToolTip"));
            // 
            // cmbScaleMode
            // 
            this.cmbScaleMode.AccessibleDescription = null;
            this.cmbScaleMode.AccessibleName = null;
            resources.ApplyResources(this.cmbScaleMode, "cmbScaleMode");
            this.cmbScaleMode.BackgroundImage = null;
            this.cmbScaleMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScaleMode.Font = null;
            this.cmbScaleMode.FormattingEnabled = true;
            this.cmbScaleMode.Items.AddRange(new object[] {
            resources.GetString("cmbScaleMode.Items"),
            resources.GetString("cmbScaleMode.Items1"),
            resources.GetString("cmbScaleMode.Items2")});
            this.cmbScaleMode.Name = "cmbScaleMode";
            this.ttHelp.SetToolTip(this.cmbScaleMode, resources.GetString("cmbScaleMode.ToolTip"));
            this.cmbScaleMode.SelectedIndexChanged += new System.EventHandler(this.cmbScaleMode_SelectedIndexChanged);
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
            // tabStrokeProperties
            // 
            this.tabStrokeProperties.AccessibleDescription = null;
            this.tabStrokeProperties.AccessibleName = null;
            resources.ApplyResources(this.tabStrokeProperties, "tabStrokeProperties");
            this.tabStrokeProperties.BackgroundImage = null;
            this.tabStrokeProperties.Controls.Add(this.tabSimple);
            this.tabStrokeProperties.Controls.Add(this.tabCartographic);
            this.tabStrokeProperties.Controls.Add(this.tabDash);
            this.tabStrokeProperties.Controls.Add(this.tabDecoration);
            this.tabStrokeProperties.Font = null;
            this.tabStrokeProperties.Name = "tabStrokeProperties";
            this.tabStrokeProperties.SelectedIndex = 0;
            this.ttHelp.SetToolTip(this.tabStrokeProperties, resources.GetString("tabStrokeProperties.ToolTip"));
            // 
            // tabSimple
            // 
            this.tabSimple.AccessibleDescription = null;
            this.tabSimple.AccessibleName = null;
            this.tabSimple.AllowDrop = true;
            resources.ApplyResources(this.tabSimple, "tabSimple");
            this.tabSimple.BackgroundImage = null;
            this.tabSimple.Controls.Add(this.label5);
            this.tabSimple.Controls.Add(this.label9);
            this.tabSimple.Controls.Add(this.cbColorSimple);
            this.tabSimple.Controls.Add(this.sldOpacitySimple);
            this.tabSimple.Controls.Add(this.cmbStrokeStyle);
            this.tabSimple.Controls.Add(this.label2);
            this.tabSimple.Controls.Add(this.dblWidth);
            this.tabSimple.Font = null;
            this.tabSimple.Name = "tabSimple";
            this.ttHelp.SetToolTip(this.tabSimple, resources.GetString("tabSimple.ToolTip"));
            this.tabSimple.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AccessibleDescription = null;
            this.label5.AccessibleName = null;
            resources.ApplyResources(this.label5, "label5");
            this.label5.Font = null;
            this.label5.Name = "label5";
            this.ttHelp.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // label9
            // 
            this.label9.AccessibleDescription = null;
            this.label9.AccessibleName = null;
            resources.ApplyResources(this.label9, "label9");
            this.label9.Font = null;
            this.label9.Name = "label9";
            this.ttHelp.SetToolTip(this.label9, resources.GetString("label9.ToolTip"));
            // 
            // cbColorSimple
            // 
            this.cbColorSimple.AccessibleDescription = null;
            this.cbColorSimple.AccessibleName = null;
            resources.ApplyResources(this.cbColorSimple, "cbColorSimple");
            this.cbColorSimple.BackgroundImage = null;
            this.cbColorSimple.BevelRadius = 6;
            this.cbColorSimple.Color = System.Drawing.Color.Blue;
            this.cbColorSimple.Font = null;
            this.cbColorSimple.LaunchDialogOnClick = true;
            this.cbColorSimple.Name = "cbColorSimple";
            this.cbColorSimple.RoundingRadius = 15;
            this.ttHelp.SetToolTip(this.cbColorSimple, resources.GetString("cbColorSimple.ToolTip"));
            // 
            // sldOpacitySimple
            // 
            this.sldOpacitySimple.AccessibleDescription = null;
            this.sldOpacitySimple.AccessibleName = null;
            resources.ApplyResources(this.sldOpacitySimple, "sldOpacitySimple");
            this.sldOpacitySimple.BackgroundImage = null;
            this.sldOpacitySimple.ColorButton = null;
            this.sldOpacitySimple.FlipRamp = false;
            this.sldOpacitySimple.FlipText = false;
            this.sldOpacitySimple.Font = null;
            this.sldOpacitySimple.InvertRamp = false;
            this.sldOpacitySimple.Maximum = 1;
            this.sldOpacitySimple.MaximumColor = System.Drawing.Color.CornflowerBlue;
            this.sldOpacitySimple.Minimum = 0;
            this.sldOpacitySimple.MinimumColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.sldOpacitySimple.Name = "sldOpacitySimple";
            this.sldOpacitySimple.NumberFormat = null;
            this.sldOpacitySimple.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.sldOpacitySimple.RampRadius = 10F;
            this.sldOpacitySimple.RampText = "Opacity";
            this.sldOpacitySimple.RampTextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            this.sldOpacitySimple.RampTextBehindRamp = true;
            this.sldOpacitySimple.RampTextColor = System.Drawing.Color.Black;
            this.sldOpacitySimple.RampTextFont = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sldOpacitySimple.ShowMaximum = true;
            this.sldOpacitySimple.ShowMinimum = true;
            this.sldOpacitySimple.ShowTicks = true;
            this.sldOpacitySimple.ShowValue = false;
            this.sldOpacitySimple.SliderColor = System.Drawing.Color.Blue;
            this.sldOpacitySimple.SliderRadius = 4F;
            this.sldOpacitySimple.TickColor = System.Drawing.Color.DarkGray;
            this.sldOpacitySimple.TickSpacing = 5F;
            this.ttHelp.SetToolTip(this.sldOpacitySimple, resources.GetString("sldOpacitySimple.ToolTip"));
            this.sldOpacitySimple.Value = 0;
            // 
            // cmbStrokeStyle
            // 
            this.cmbStrokeStyle.AccessibleDescription = null;
            this.cmbStrokeStyle.AccessibleName = null;
            resources.ApplyResources(this.cmbStrokeStyle, "cmbStrokeStyle");
            this.cmbStrokeStyle.BackgroundImage = null;
            this.cmbStrokeStyle.Font = null;
            this.cmbStrokeStyle.FormattingEnabled = true;
            this.cmbStrokeStyle.Items.AddRange(new object[] {
            resources.GetString("cmbStrokeStyle.Items"),
            resources.GetString("cmbStrokeStyle.Items1"),
            resources.GetString("cmbStrokeStyle.Items2"),
            resources.GetString("cmbStrokeStyle.Items3"),
            resources.GetString("cmbStrokeStyle.Items4"),
            resources.GetString("cmbStrokeStyle.Items5")});
            this.cmbStrokeStyle.Name = "cmbStrokeStyle";
            this.ttHelp.SetToolTip(this.cmbStrokeStyle, resources.GetString("cmbStrokeStyle.ToolTip"));
            this.cmbStrokeStyle.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
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
            // dblWidth
            // 
            this.dblWidth.AccessibleDescription = null;
            this.dblWidth.AccessibleName = null;
            resources.ApplyResources(this.dblWidth, "dblWidth");
            this.dblWidth.BackColorInvalid = System.Drawing.Color.Salmon;
            this.dblWidth.BackColorRegular = System.Drawing.Color.Empty;
            this.dblWidth.BackgroundImage = null;
            this.dblWidth.Font = null;
            this.dblWidth.InvalidHelp = "The value entered could not be correctly parsed into a valid double precision flo" +
                "ating point value.";
            this.dblWidth.IsValid = true;
            this.dblWidth.Name = "dblWidth";
            this.dblWidth.NumberFormat = null;
            this.dblWidth.RegularHelp = "Enter a double precision floating point value.";
            this.ttHelp.SetToolTip(this.dblWidth, resources.GetString("dblWidth.ToolTip"));
            this.dblWidth.Value = 0;
            this.dblWidth.TextChanged += new System.EventHandler(this.dblWidth_TextChanged);
            // 
            // tabCartographic
            // 
            this.tabCartographic.AccessibleDescription = null;
            this.tabCartographic.AccessibleName = null;
            resources.ApplyResources(this.tabCartographic, "tabCartographic");
            this.tabCartographic.BackgroundImage = null;
            this.tabCartographic.Controls.Add(this.lblColorCartographic);
            this.tabCartographic.Controls.Add(this.lblOpacityCartographic);
            this.tabCartographic.Controls.Add(this.grpCaps);
            this.tabCartographic.Controls.Add(this.cbColorCartographic);
            this.tabCartographic.Controls.Add(this.sldOpacityCartographic);
            this.tabCartographic.Controls.Add(this.dblStrokeOffset);
            this.tabCartographic.Controls.Add(this.radLineJoin);
            this.tabCartographic.Controls.Add(this.dblWidthCartographic);
            this.tabCartographic.Font = null;
            this.tabCartographic.Name = "tabCartographic";
            this.ttHelp.SetToolTip(this.tabCartographic, resources.GetString("tabCartographic.ToolTip"));
            this.tabCartographic.UseVisualStyleBackColor = true;
            // 
            // lblColorCartographic
            // 
            this.lblColorCartographic.AccessibleDescription = null;
            this.lblColorCartographic.AccessibleName = null;
            resources.ApplyResources(this.lblColorCartographic, "lblColorCartographic");
            this.lblColorCartographic.Font = null;
            this.lblColorCartographic.Name = "lblColorCartographic";
            this.ttHelp.SetToolTip(this.lblColorCartographic, resources.GetString("lblColorCartographic.ToolTip"));
            // 
            // lblOpacityCartographic
            // 
            this.lblOpacityCartographic.AccessibleDescription = null;
            this.lblOpacityCartographic.AccessibleName = null;
            resources.ApplyResources(this.lblOpacityCartographic, "lblOpacityCartographic");
            this.lblOpacityCartographic.Font = null;
            this.lblOpacityCartographic.Name = "lblOpacityCartographic";
            this.ttHelp.SetToolTip(this.lblOpacityCartographic, resources.GetString("lblOpacityCartographic.ToolTip"));
            // 
            // grpCaps
            // 
            this.grpCaps.AccessibleDescription = null;
            this.grpCaps.AccessibleName = null;
            resources.ApplyResources(this.grpCaps, "grpCaps");
            this.grpCaps.BackgroundImage = null;
            this.grpCaps.Controls.Add(this.lblStartCap);
            this.grpCaps.Controls.Add(this.cmbEndCap);
            this.grpCaps.Controls.Add(this.label4);
            this.grpCaps.Controls.Add(this.cmbStartCap);
            this.grpCaps.Font = null;
            this.grpCaps.Name = "grpCaps";
            this.grpCaps.TabStop = false;
            this.ttHelp.SetToolTip(this.grpCaps, resources.GetString("grpCaps.ToolTip"));
            // 
            // lblStartCap
            // 
            this.lblStartCap.AccessibleDescription = null;
            this.lblStartCap.AccessibleName = null;
            resources.ApplyResources(this.lblStartCap, "lblStartCap");
            this.lblStartCap.Font = null;
            this.lblStartCap.Name = "lblStartCap";
            this.ttHelp.SetToolTip(this.lblStartCap, resources.GetString("lblStartCap.ToolTip"));
            // 
            // cmbEndCap
            // 
            this.cmbEndCap.AccessibleDescription = null;
            this.cmbEndCap.AccessibleName = null;
            resources.ApplyResources(this.cmbEndCap, "cmbEndCap");
            this.cmbEndCap.BackgroundImage = null;
            this.cmbEndCap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEndCap.Font = null;
            this.cmbEndCap.FormattingEnabled = true;
            this.cmbEndCap.Name = "cmbEndCap";
            this.ttHelp.SetToolTip(this.cmbEndCap, resources.GetString("cmbEndCap.ToolTip"));
            this.cmbEndCap.SelectedIndexChanged += new System.EventHandler(this.cmbEndCap_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AccessibleDescription = null;
            this.label4.AccessibleName = null;
            resources.ApplyResources(this.label4, "label4");
            this.label4.Font = null;
            this.label4.Name = "label4";
            this.ttHelp.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // cmbStartCap
            // 
            this.cmbStartCap.AccessibleDescription = null;
            this.cmbStartCap.AccessibleName = null;
            resources.ApplyResources(this.cmbStartCap, "cmbStartCap");
            this.cmbStartCap.BackgroundImage = null;
            this.cmbStartCap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStartCap.Font = null;
            this.cmbStartCap.FormattingEnabled = true;
            this.cmbStartCap.Name = "cmbStartCap";
            this.ttHelp.SetToolTip(this.cmbStartCap, resources.GetString("cmbStartCap.ToolTip"));
            this.cmbStartCap.SelectedIndexChanged += new System.EventHandler(this.cmbStartCap_SelectedIndexChanged);
            // 
            // cbColorCartographic
            // 
            this.cbColorCartographic.AccessibleDescription = null;
            this.cbColorCartographic.AccessibleName = null;
            resources.ApplyResources(this.cbColorCartographic, "cbColorCartographic");
            this.cbColorCartographic.BackgroundImage = null;
            this.cbColorCartographic.BevelRadius = 6;
            this.cbColorCartographic.Color = System.Drawing.Color.Blue;
            this.cbColorCartographic.Font = null;
            this.cbColorCartographic.LaunchDialogOnClick = true;
            this.cbColorCartographic.Name = "cbColorCartographic";
            this.cbColorCartographic.RoundingRadius = 15;
            this.ttHelp.SetToolTip(this.cbColorCartographic, resources.GetString("cbColorCartographic.ToolTip"));
            // 
            // sldOpacityCartographic
            // 
            this.sldOpacityCartographic.AccessibleDescription = null;
            this.sldOpacityCartographic.AccessibleName = null;
            resources.ApplyResources(this.sldOpacityCartographic, "sldOpacityCartographic");
            this.sldOpacityCartographic.BackgroundImage = null;
            this.sldOpacityCartographic.ColorButton = null;
            this.sldOpacityCartographic.FlipRamp = false;
            this.sldOpacityCartographic.FlipText = false;
            this.sldOpacityCartographic.Font = null;
            this.sldOpacityCartographic.InvertRamp = false;
            this.sldOpacityCartographic.Maximum = 1;
            this.sldOpacityCartographic.MaximumColor = System.Drawing.Color.CornflowerBlue;
            this.sldOpacityCartographic.Minimum = 0;
            this.sldOpacityCartographic.MinimumColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.sldOpacityCartographic.Name = "sldOpacityCartographic";
            this.sldOpacityCartographic.NumberFormat = null;
            this.sldOpacityCartographic.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.sldOpacityCartographic.RampRadius = 10F;
            this.sldOpacityCartographic.RampText = "Opacity";
            this.sldOpacityCartographic.RampTextAlignment = System.Drawing.ContentAlignment.BottomRight;
            this.sldOpacityCartographic.RampTextBehindRamp = true;
            this.sldOpacityCartographic.RampTextColor = System.Drawing.Color.Black;
            this.sldOpacityCartographic.RampTextFont = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sldOpacityCartographic.ShowMaximum = true;
            this.sldOpacityCartographic.ShowMinimum = true;
            this.sldOpacityCartographic.ShowTicks = true;
            this.sldOpacityCartographic.ShowValue = false;
            this.sldOpacityCartographic.SliderColor = System.Drawing.Color.Blue;
            this.sldOpacityCartographic.SliderRadius = 4F;
            this.sldOpacityCartographic.TickColor = System.Drawing.Color.DarkGray;
            this.sldOpacityCartographic.TickSpacing = 5F;
            this.ttHelp.SetToolTip(this.sldOpacityCartographic, resources.GetString("sldOpacityCartographic.ToolTip"));
            this.sldOpacityCartographic.Value = 0;
            // 
            // dblStrokeOffset
            // 
            this.dblStrokeOffset.AccessibleDescription = null;
            this.dblStrokeOffset.AccessibleName = null;
            resources.ApplyResources(this.dblStrokeOffset, "dblStrokeOffset");
            this.dblStrokeOffset.BackColorInvalid = System.Drawing.Color.Salmon;
            this.dblStrokeOffset.BackColorRegular = System.Drawing.Color.Empty;
            this.dblStrokeOffset.BackgroundImage = null;
            this.dblStrokeOffset.Font = null;
            this.dblStrokeOffset.InvalidHelp = "The value entered could not be correctly parsed into a valid double precision flo" +
                "ating point value.";
            this.dblStrokeOffset.IsValid = true;
            this.dblStrokeOffset.Name = "dblStrokeOffset";
            this.dblStrokeOffset.NumberFormat = null;
            this.dblStrokeOffset.RegularHelp = "Enter a double precision floating point value.";
            this.ttHelp.SetToolTip(this.dblStrokeOffset, resources.GetString("dblStrokeOffset.ToolTip"));
            this.dblStrokeOffset.Value = 0;
            // 
            // radLineJoin
            // 
            this.radLineJoin.AccessibleDescription = null;
            this.radLineJoin.AccessibleName = null;
            resources.ApplyResources(this.radLineJoin, "radLineJoin");
            this.radLineJoin.BackgroundImage = null;
            this.radLineJoin.Font = null;
            this.radLineJoin.Name = "radLineJoin";
            this.ttHelp.SetToolTip(this.radLineJoin, resources.GetString("radLineJoin.ToolTip"));
            this.radLineJoin.Value = MapWindow.Drawing.LineJoinTypes.Round;
            this.radLineJoin.ValueChanged += new System.EventHandler(this.radLineJoin_ValueChanged);
            // 
            // dblWidthCartographic
            // 
            this.dblWidthCartographic.AccessibleDescription = null;
            this.dblWidthCartographic.AccessibleName = null;
            resources.ApplyResources(this.dblWidthCartographic, "dblWidthCartographic");
            this.dblWidthCartographic.BackColorInvalid = System.Drawing.Color.Salmon;
            this.dblWidthCartographic.BackColorRegular = System.Drawing.Color.Empty;
            this.dblWidthCartographic.BackgroundImage = null;
            this.dblWidthCartographic.Font = null;
            this.dblWidthCartographic.InvalidHelp = "The value entered could not be correctly parsed into a valid double precision flo" +
                "ating point value.";
            this.dblWidthCartographic.IsValid = true;
            this.dblWidthCartographic.Name = "dblWidthCartographic";
            this.dblWidthCartographic.NumberFormat = null;
            this.dblWidthCartographic.RegularHelp = "Enter a double precision floating point value.";
            this.ttHelp.SetToolTip(this.dblWidthCartographic, resources.GetString("dblWidthCartographic.ToolTip"));
            this.dblWidthCartographic.Value = 0;
            this.dblWidthCartographic.TextChanged += new System.EventHandler(this.dblWidthCartographic_TextChanged);
            // 
            // tabDash
            // 
            this.tabDash.AccessibleDescription = null;
            this.tabDash.AccessibleName = null;
            resources.ApplyResources(this.tabDash, "tabDash");
            this.tabDash.BackgroundImage = null;
            this.tabDash.Controls.Add(this.dashControl1);
            this.tabDash.Font = null;
            this.tabDash.Name = "tabDash";
            this.ttHelp.SetToolTip(this.tabDash, resources.GetString("tabDash.ToolTip"));
            this.tabDash.UseVisualStyleBackColor = true;
            // 
            // dashControl1
            // 
            this.dashControl1.AccessibleDescription = null;
            this.dashControl1.AccessibleName = null;
            resources.ApplyResources(this.dashControl1, "dashControl1");
            this.dashControl1.BackgroundImage = null;
            this.dashControl1.BlockSize = new System.Drawing.SizeF(10F, 10F);
            this.dashControl1.ButtonDownDarkColor = System.Drawing.SystemColors.ControlDark;
            this.dashControl1.ButtonDownLitColor = System.Drawing.SystemColors.ActiveCaption;
            this.dashControl1.ButtonUpDarkColor = System.Drawing.SystemColors.Control;
            this.dashControl1.ButtonUpLitColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dashControl1.Font = null;
            this.dashControl1.HorizontalSlider.Color = System.Drawing.Color.Blue;
            this.dashControl1.HorizontalSlider.Image = null;
            this.dashControl1.HorizontalSlider.Position = ((System.Drawing.PointF)(resources.GetObject("resource.Position")));
            this.dashControl1.HorizontalSlider.Size = new System.Drawing.SizeF(10F, 15F);
            this.dashControl1.HorizontalSlider.Visible = true;
            this.dashControl1.LineColor = System.Drawing.Color.Red;
            this.dashControl1.LineWidth = 0;
            this.dashControl1.Name = "dashControl1";
            this.ttHelp.SetToolTip(this.dashControl1, resources.GetString("dashControl1.ToolTip"));
            this.dashControl1.VerticalSlider.Color = System.Drawing.Color.Lime;
            this.dashControl1.VerticalSlider.Image = null;
            this.dashControl1.VerticalSlider.Position = ((System.Drawing.PointF)(resources.GetObject("resource.Position1")));
            this.dashControl1.VerticalSlider.Size = new System.Drawing.SizeF(15F, 10F);
            this.dashControl1.VerticalSlider.Visible = true;
            // 
            // tabDecoration
            // 
            this.tabDecoration.AccessibleDescription = null;
            this.tabDecoration.AccessibleName = null;
            resources.ApplyResources(this.tabDecoration, "tabDecoration");
            this.tabDecoration.BackgroundImage = null;
            this.tabDecoration.Controls.Add(this.dblOffset);
            this.tabDecoration.Controls.Add(this.ccDecorations);
            this.tabDecoration.Controls.Add(this.btnEdit);
            this.tabDecoration.Controls.Add(this.label6);
            this.tabDecoration.Controls.Add(this.lblDecorationPreview);
            this.tabDecoration.Controls.Add(this.lblNumberOfPositions);
            this.tabDecoration.Controls.Add(this.nudDecorationCount);
            this.tabDecoration.Controls.Add(this.grpRotation);
            this.tabDecoration.Controls.Add(this.grpFlip);
            this.tabDecoration.Font = null;
            this.tabDecoration.Name = "tabDecoration";
            this.ttHelp.SetToolTip(this.tabDecoration, resources.GetString("tabDecoration.ToolTip"));
            this.tabDecoration.UseVisualStyleBackColor = true;
            // 
            // dblOffset
            // 
            this.dblOffset.AccessibleDescription = null;
            this.dblOffset.AccessibleName = null;
            resources.ApplyResources(this.dblOffset, "dblOffset");
            this.dblOffset.BackColorInvalid = System.Drawing.Color.Salmon;
            this.dblOffset.BackColorRegular = System.Drawing.Color.Empty;
            this.dblOffset.BackgroundImage = null;
            this.dblOffset.Font = null;
            this.dblOffset.InvalidHelp = "The value entered could not be correctly parsed into a valid double precision flo" +
                "ating point value.";
            this.dblOffset.IsValid = true;
            this.dblOffset.Name = "dblOffset";
            this.dblOffset.NumberFormat = null;
            this.dblOffset.RegularHelp = "Enter a double precision floating point value.";
            this.ttHelp.SetToolTip(this.dblOffset, resources.GetString("dblOffset.ToolTip"));
            this.dblOffset.Value = 0;
            // 
            // ccDecorations
            // 
            this.ccDecorations.AccessibleDescription = null;
            this.ccDecorations.AccessibleName = null;
            resources.ApplyResources(this.ccDecorations, "ccDecorations");
            this.ccDecorations.BackgroundImage = null;
            this.ccDecorations.Font = null;
            this.ccDecorations.Name = "ccDecorations";
            this.ttHelp.SetToolTip(this.ccDecorations, resources.GetString("ccDecorations.ToolTip"));
            // 
            // btnEdit
            // 
            this.btnEdit.AccessibleDescription = null;
            this.btnEdit.AccessibleName = null;
            resources.ApplyResources(this.btnEdit, "btnEdit");
            this.btnEdit.BackgroundImage = null;
            this.btnEdit.Font = null;
            this.btnEdit.Name = "btnEdit";
            this.ttHelp.SetToolTip(this.btnEdit, resources.GetString("btnEdit.ToolTip"));
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // label6
            // 
            this.label6.AccessibleDescription = null;
            this.label6.AccessibleName = null;
            resources.ApplyResources(this.label6, "label6");
            this.label6.Font = null;
            this.label6.Name = "label6";
            this.ttHelp.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // lblDecorationPreview
            // 
            this.lblDecorationPreview.AccessibleDescription = null;
            this.lblDecorationPreview.AccessibleName = null;
            resources.ApplyResources(this.lblDecorationPreview, "lblDecorationPreview");
            this.lblDecorationPreview.BackColor = System.Drawing.Color.White;
            this.lblDecorationPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDecorationPreview.Font = null;
            this.lblDecorationPreview.Name = "lblDecorationPreview";
            this.ttHelp.SetToolTip(this.lblDecorationPreview, resources.GetString("lblDecorationPreview.ToolTip"));
            // 
            // lblNumberOfPositions
            // 
            this.lblNumberOfPositions.AccessibleDescription = null;
            this.lblNumberOfPositions.AccessibleName = null;
            resources.ApplyResources(this.lblNumberOfPositions, "lblNumberOfPositions");
            this.lblNumberOfPositions.Font = null;
            this.lblNumberOfPositions.Name = "lblNumberOfPositions";
            this.ttHelp.SetToolTip(this.lblNumberOfPositions, resources.GetString("lblNumberOfPositions.ToolTip"));
            // 
            // nudDecorationCount
            // 
            this.nudDecorationCount.AccessibleDescription = null;
            this.nudDecorationCount.AccessibleName = null;
            resources.ApplyResources(this.nudDecorationCount, "nudDecorationCount");
            this.nudDecorationCount.Font = null;
            this.nudDecorationCount.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nudDecorationCount.Name = "nudDecorationCount";
            this.ttHelp.SetToolTip(this.nudDecorationCount, resources.GetString("nudDecorationCount.ToolTip"));
            this.nudDecorationCount.ValueChanged += new System.EventHandler(this.nudDecorationCount_ValueChanged);
            // 
            // grpRotation
            // 
            this.grpRotation.AccessibleDescription = null;
            this.grpRotation.AccessibleName = null;
            resources.ApplyResources(this.grpRotation, "grpRotation");
            this.grpRotation.BackgroundImage = null;
            this.grpRotation.Controls.Add(this.radRotationFixed);
            this.grpRotation.Controls.Add(this.radRotationWithLine);
            this.grpRotation.Font = null;
            this.grpRotation.Name = "grpRotation";
            this.grpRotation.TabStop = false;
            this.ttHelp.SetToolTip(this.grpRotation, resources.GetString("grpRotation.ToolTip"));
            // 
            // radRotationFixed
            // 
            this.radRotationFixed.AccessibleDescription = null;
            this.radRotationFixed.AccessibleName = null;
            resources.ApplyResources(this.radRotationFixed, "radRotationFixed");
            this.radRotationFixed.BackgroundImage = null;
            this.radRotationFixed.Font = null;
            this.radRotationFixed.Name = "radRotationFixed";
            this.radRotationFixed.TabStop = true;
            this.ttHelp.SetToolTip(this.radRotationFixed, resources.GetString("radRotationFixed.ToolTip"));
            this.radRotationFixed.UseVisualStyleBackColor = true;
            // 
            // radRotationWithLine
            // 
            this.radRotationWithLine.AccessibleDescription = null;
            this.radRotationWithLine.AccessibleName = null;
            resources.ApplyResources(this.radRotationWithLine, "radRotationWithLine");
            this.radRotationWithLine.BackgroundImage = null;
            this.radRotationWithLine.Font = null;
            this.radRotationWithLine.Name = "radRotationWithLine";
            this.radRotationWithLine.TabStop = true;
            this.ttHelp.SetToolTip(this.radRotationWithLine, resources.GetString("radRotationWithLine.ToolTip"));
            this.radRotationWithLine.UseVisualStyleBackColor = true;
            this.radRotationWithLine.CheckedChanged += new System.EventHandler(this.radRotationWithLine_CheckedChanged);
            // 
            // grpFlip
            // 
            this.grpFlip.AccessibleDescription = null;
            this.grpFlip.AccessibleName = null;
            resources.ApplyResources(this.grpFlip, "grpFlip");
            this.grpFlip.BackgroundImage = null;
            this.grpFlip.Controls.Add(this.chkFlipFirst);
            this.grpFlip.Controls.Add(this.chkFlipAll);
            this.grpFlip.Font = null;
            this.grpFlip.Name = "grpFlip";
            this.grpFlip.TabStop = false;
            this.ttHelp.SetToolTip(this.grpFlip, resources.GetString("grpFlip.ToolTip"));
            // 
            // chkFlipFirst
            // 
            this.chkFlipFirst.AccessibleDescription = null;
            this.chkFlipFirst.AccessibleName = null;
            resources.ApplyResources(this.chkFlipFirst, "chkFlipFirst");
            this.chkFlipFirst.BackgroundImage = null;
            this.chkFlipFirst.Font = null;
            this.chkFlipFirst.Name = "chkFlipFirst";
            this.ttHelp.SetToolTip(this.chkFlipFirst, resources.GetString("chkFlipFirst.ToolTip"));
            this.chkFlipFirst.UseVisualStyleBackColor = true;
            this.chkFlipFirst.CheckedChanged += new System.EventHandler(this.chkFlipFirst_CheckedChanged);
            // 
            // chkFlipAll
            // 
            this.chkFlipAll.AccessibleDescription = null;
            this.chkFlipAll.AccessibleName = null;
            resources.ApplyResources(this.chkFlipAll, "chkFlipAll");
            this.chkFlipAll.BackgroundImage = null;
            this.chkFlipAll.Font = null;
            this.chkFlipAll.Name = "chkFlipAll";
            this.ttHelp.SetToolTip(this.chkFlipAll, resources.GetString("chkFlipAll.ToolTip"));
            this.chkFlipAll.UseVisualStyleBackColor = true;
            this.chkFlipAll.CheckedChanged += new System.EventHandler(this.chkFlipAll_CheckedChanged);
            // 
            // ccStrokes
            // 
            this.ccStrokes.AccessibleDescription = null;
            this.ccStrokes.AccessibleName = null;
            resources.ApplyResources(this.ccStrokes, "ccStrokes");
            this.ccStrokes.BackgroundImage = null;
            this.ccStrokes.Font = null;
            this.ccStrokes.Name = "ccStrokes";
            this.ttHelp.SetToolTip(this.ccStrokes, resources.GetString("ccStrokes.ToolTip"));
            // 
            // DetailedLineSymbolControl
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.ccStrokes);
            this.Controls.Add(this.tabStrokeProperties);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmbStrokeType);
            this.Font = null;
            this.Name = "DetailedLineSymbolControl";
            this.ttHelp.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabStrokeProperties.ResumeLayout(false);
            this.tabSimple.ResumeLayout(false);
            this.tabSimple.PerformLayout();
            this.tabCartographic.ResumeLayout(false);
            this.tabCartographic.PerformLayout();
            this.grpCaps.ResumeLayout(false);
            this.grpCaps.PerformLayout();
            this.tabDash.ResumeLayout(false);
            this.tabDecoration.ResumeLayout(false);
            this.tabDecoration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDecorationCount)).EndInit();
            this.grpRotation.ResumeLayout(false);
            this.grpRotation.PerformLayout();
            this.grpFlip.ResumeLayout(false);
            this.grpFlip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        void radLineJoin_ValueChanged(object sender, EventArgs e)
        {
            if (_ignoreChanges) return;
            if (ccStrokes.SelectedStroke == null) return;
            IStroke stroke = ccStrokes.SelectedStroke;
            ICartographicStroke cs = stroke as ICartographicStroke;
            if (cs != null)
            {
                cs.JoinType = radLineJoin.Value;
            }
            UpdatePreview();
        }

        
      

       
        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of DetailedLineSymbolDialog
        /// </summary>
        public DetailedLineSymbolControl()
        {
            _original = new LineSymbolizer();
            _symbolizer = new LineSymbolizer();
            Configure();
        }

        /// <summary>
        /// Creates a new Detailed Line Symbol Dialog that displays a copy of the original,
        /// and when apply changes is pressed, will copy properties to the original.
        /// </summary>
        /// <param name="original">The current symbolizer being viewed on the map</param>
        public DetailedLineSymbolControl(ILineSymbolizer original)
        {
            _original = original;
            _symbolizer = original.Copy();
            Configure();
        }

        /// <summary>
        /// This constructor shows a different symbolizer in the view from what is currently loaded on the map.
        /// If apply changes is clicked, the properties of the current symbolizer will be copied to the original.
        /// </summary>
        /// <param name="original">The symbolizer on the map</param>
        /// <param name="displayed">The symbolizer that defines the form setup</param>
        public DetailedLineSymbolControl(ILineSymbolizer original, ILineSymbolizer displayed)
        {
            _symbolizer = displayed;
            _original = original;
            Configure();
        }



        private void Configure()
        {
            
            InitializeComponent();

            ccStrokes.Strokes = _symbolizer.Strokes;
            ccStrokes.AddClicked += ccStrokes_AddClicked;
            ccStrokes.SelectedItemChanged += ccStrokes_SelectedItemChanged;
            ccStrokes.ListChanged += ccStrokes_ListChanged;

            ccDecorations.AddClicked += ccDecorations_AddClicked;
            ccDecorations.SelectedItemChanged += ccDecorations_SelectedItemChanged;
            ccDecorations.ListChanged += ccDecorations_ListChanged;

            lblPreview.Paint += lblPreview_Paint;
            lblDecorationPreview.Paint += lblDecorationPreview_Paint;
            dashControl1.PatternChanged += dashControl1_PatternChanged;
            dblWidthCartographic.TextChanged += dblWidthCartographic_TextChanged;
            cbColorCartographic.ColorChanged += cbColorCartographic_ColorChanged;
            cbColorSimple.ColorChanged += cbColorSimple_ColorChanged;

            cmbScaleMode.SelectedItem = _symbolizer.ScaleMode.ToString();
            chkSmoothing.Checked = _symbolizer.Smoothing;
            dblOffset.TextChanged += dblOffset_TextChanged;
            dblStrokeOffset.TextChanged += dblStrokeOffset_TextChanged;
            sldOpacitySimple.ValueChanged += sldOpacitySimple_ValueChanged;
            sldOpacityCartographic.ValueChanged += sldOpacityCartographic_ValueChanged;
            if (_symbolizer != null && _symbolizer.Strokes != null && _symbolizer.Strokes.Count > 0)
            {
                ccStrokes.SelectedStroke = _symbolizer.Strokes[0];
            }
            
           
        }

        void cbColorSimple_ColorChanged(object sender, EventArgs e)
        {
            SetColor(cbColorSimple.Color);
        }

        void cbColorCartographic_ColorChanged(object sender, EventArgs e)
        {
            SetColor(cbColorCartographic.Color);
        }

        void sldOpacityCartographic_ValueChanged(object sender, EventArgs e)
        {
            UpdateOpacity((float)sldOpacityCartographic.Value);
        }

        void sldOpacitySimple_ValueChanged(object sender, EventArgs e)
        {
            UpdateOpacity((float)sldOpacitySimple.Value);
        }

        /// <summary>
        /// Updates the opacity of the simple/cartographic stroke
        /// </summary>
        /// <param name="value">THe floating point value to use for the opacity, where 0 is transparent and 1 is opaque</param>
        private void UpdateOpacity(float value)
        {
            if (_ignoreChanges) return;
            IStroke stroke = ccStrokes.SelectedStroke;
            if (stroke == null) return;
            ISimpleStroke ss = stroke as ISimpleStroke;
            if (ss != null)
            {
                if (ss.Opacity != value)
                {
                    ss.Opacity = value;
                    cbColorSimple.Color = ss.Color;
                    cbColorCartographic.Color = ss.Color;
                }
            }
        
        }

        void dblStrokeOffset_TextChanged(object sender, EventArgs e)
        {
            if (ccStrokes == null) return;
            ICartographicStroke cs = ccStrokes.SelectedStroke as ICartographicStroke;
            if (cs == null) return;
            cs.Offset = (float)dblStrokeOffset.Value;
            UpdatePreview();
        }

        void dblOffset_TextChanged(object sender, EventArgs e)
        {
            if (ccDecorations.SelectedDecoration == null) return;
            ccDecorations.SelectedDecoration.Offset = dblOffset.Value;
            UpdatePreview();
        }

        void lblDecorationPreview_Paint(object sender, PaintEventArgs e)
        {
            UpdateDecorationPreview(e.Graphics);
        }

        void ccDecorations_ListChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        void ccDecorations_SelectedItemChanged(object sender, EventArgs e)
        {
            if (ccDecorations.SelectedDecoration != null)
            {
                UpdateDecorationControls();
            }
            UpdatePreview();
        }

        void ccDecorations_AddClicked(object sender, EventArgs e)
        {
            ICartographicStroke currentStroke = ccStrokes.SelectedStroke as ICartographicStroke;
            if (currentStroke != null)
            {
                LineDecoration decoration = new LineDecoration();
                currentStroke.Decorations.Add(decoration);
                ccDecorations.RefreshList();
                ccDecorations.SelectedDecoration = decoration;
            }
        }

      
        void dblWidthCartographic_TextChanged(object sender, EventArgs e)
        {
            UpdateWidth(dblWidthCartographic.Value);
        }

        private void LoadLineCaps()
        {
            Array names = Enum.GetValues(typeof(LineCap));
            foreach (object name in names)
            {
                cmbEndCap.Items.Add(name);
                cmbStartCap.Items.Add(name);
            }
        }

        void dashControl1_PatternChanged(object sender, EventArgs e)
        {
            if (_ignoreChanges) return;
            IStroke stroke = ccStrokes.SelectedStroke;
            if (stroke == null) return;
            ICartographicStroke cs = stroke as ICartographicStroke;
            if (cs != null)
            {
                cs.DashStyle = DashStyle.Custom;
                cs.DashPattern = dashControl1.GetDashPattern();
                cs.DashButtons = dashControl1.DashButtons;
                cs.CompoundButtons = dashControl1.CompoundButtons;
                cs.CompoundArray = dashControl1.GetCompoundArray();
                cs.Width = cs.CompoundButtons.Length;
            }
            UpdatePreview();
        }

        void ccStrokes_ListChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        void lblPreview_Paint(object sender, PaintEventArgs e)
        {
            UpdatePreview(e.Graphics);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the control by updating the symbolizer
        /// </summary>
        /// <param name="original"></param>
        public void Initialize(ILineSymbolizer original)
        {
            _original = original;
            _symbolizer = original.Copy();
            ccStrokes.Strokes = _symbolizer.Strokes;
            chkSmoothing.Checked = _symbolizer.Smoothing;
            ccStrokes.RefreshList();
            if (_symbolizer.Strokes.Count > 0)
            {
                ccStrokes.SelectedStroke = _symbolizer.Strokes[0];
            }
            UpdatePreview();
            UpdateStrokeControls();
        }

        /// <summary>
        /// When the stroke is changed, this updates the controls to match it.
        /// </summary>
        private void UpdateStrokeControls()
        {
            _ignoreChanges = true;
            ICartographicStroke cs = ccStrokes.SelectedStroke as ICartographicStroke;
            if (cs != null)
            {
                cmbStrokeType.SelectedItem = "Cartographic";
                if (tabStrokeProperties.TabPages.Contains(tabSimple))
                {
                    tabStrokeProperties.TabPages.Remove(tabSimple);
                }
                if (tabStrokeProperties.TabPages.Contains(tabCartographic) == false)
                {
                    tabStrokeProperties.TabPages.Add(tabCartographic);
                }
                if (tabStrokeProperties.TabPages.Contains(tabDash) == false)
                {
                    tabStrokeProperties.TabPages.Add(tabDash);
                }
                if (tabStrokeProperties.TabPages.Contains(tabDecoration) == false)
                {
                    tabStrokeProperties.TabPages.Add(tabDecoration);
                }

                // Cartographic Tab Page
                if (cmbStartCap.Items.Count == 0)
                {
                    LoadLineCaps();
                }
                cmbStartCap.SelectedIndex = (int)cs.StartCap;
                cmbEndCap.SelectedIndex = (int)cs.EndCap;
                radLineJoin.Value = cs.JoinType;
                dblStrokeOffset.Value = cs.Offset;


                // Template Tab Page
                dashControl1.SetPattern(cs);
                dashControl1.Invalidate();

                // Decoration Tab Page
                ccDecorations.Decorations = cs.Decorations;
                if (cs.Decorations != null && cs.Decorations.Count > 0)
                {
                    ccDecorations.SelectedDecoration = cs.Decorations[0];
                }

            }
            else
            {
                cmbStrokeType.SelectedItem = "Simple";
                if (tabStrokeProperties.TabPages.Contains(tabDash))
                {
                    tabStrokeProperties.TabPages.Remove(tabDash);
                }
                if (tabStrokeProperties.TabPages.Contains(tabCartographic))
                {
                    tabStrokeProperties.TabPages.Remove(tabCartographic);
                }
                if (tabStrokeProperties.TabPages.Contains(tabDecoration))
                {
                    tabStrokeProperties.TabPages.Remove(tabDecoration);
                }
                if (tabStrokeProperties.TabPages.Contains(tabSimple) == false)
                {
                    tabStrokeProperties.TabPages.Add(tabSimple);
                }

            }
            ISimpleStroke ss = ccStrokes.SelectedStroke as ISimpleStroke;
            if (ss != null)
            {
                // Simple Tab Page
                cbColorSimple.Color = ss.Color;
                cbColorCartographic.Color = ss.Color;
                dblWidthCartographic.Value = ss.Width;
                dblWidth.Value = ss.Width;
                cmbStrokeStyle.SelectedIndex = (int)ss.DashStyle;
                sldOpacityCartographic.MaximumColor = Color.FromArgb(255, ss.Color.R, ss.Color.G, ss.Color.B);
                sldOpacitySimple.MaximumColor = Color.FromArgb(255, ss.Color.R, ss.Color.G, ss.Color.B);
                sldOpacitySimple.Value = ss.Opacity;
                sldOpacityCartographic.Value = ss.Opacity;
                sldOpacityCartographic.Invalidate();
                sldOpacitySimple.Invalidate();
            }

            _ignoreChanges = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current (copied) symbolizer or initializes this control to work with the
        /// specified symbolizer as the original.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ILineSymbolizer Symbolizer
        {
            get { return _symbolizer; }
            set
            {
                if (value != null) Initialize(value);
            }
        }


        #endregion

        #region Events

        #endregion

        #region  Protected Methods

        /// <summary>
        /// Forces the original to apply the changes to the new control
        /// </summary>
        public void ApplyChanges()
        {
            OnApplyChanges();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Fires the AddtoCustomSymbols event
        /// </summary>
        protected virtual void OnAddToCustomSymbols()
        {
            AddToCustomSymbols(this, new LineSymbolizerEventArgs(_symbolizer));
        }

        /// <summary>
        /// Fires the ChangesApplied event
        /// </summary>
        protected void OnApplyChanges()
        {
            _original.CopyProperties(_symbolizer);
            if (ChangesApplied != null) ChangesApplied(this, new EventArgs());
        }


        #endregion

        #region Event Handlers

        void dblWidth_TextChanged(object sender, EventArgs e)
        {
            UpdateWidth(dblWidth.Value);
        }

        private void UpdateWidth(double value)
        {
            if (_ignoreChanges) return;
            IStroke stroke = ccStrokes.SelectedStroke;
            if (stroke == null) return;
            ISimpleStroke ss = stroke as ISimpleStroke;
            if (ss != null)
            {
                ss.Width = dblWidth.Value;
            }
            // only call if changed, or else we will create an infinite loop here
            if (dblWidth.Value != value) dblWidth.Value = value;
            if (dblWidthCartographic.Value != value) dblWidthCartographic.Value = value;
            UpdatePreview();
        }

      

        private void SetColor(Color color)
        {
            if (_ignoreChanges) return;
            IStroke stroke = ccStrokes.SelectedStroke;
            if (stroke == null) return;
            ISimpleStroke ss = stroke as ISimpleStroke;
            if (ss != null)
            {
                ss.Color = color;
            }
            ICartographicStroke cs = stroke as ICartographicStroke;
            if (cs != null)
            {
                dashControl1.LineColor = color;
            }
            // only call if changed, or we will create an infinite loop here
            if (cbColorSimple.Color != color) cbColorSimple.Color = color;
            if (cbColorCartographic.Color != color) cbColorCartographic.Color = color;
            sldOpacityCartographic.MaximumColor = Color.FromArgb(255, color);
            sldOpacitySimple.MaximumColor = Color.FromArgb(255, color);
            sldOpacityCartographic.Invalidate();
            sldOpacitySimple.Invalidate();
            UpdatePreview();
        }


        private void ccStrokes_AddClicked(object sender, EventArgs e)
        {
            if (cmbStrokeType.SelectedItem.ToString() == "Simple")
            {
                _symbolizer.Strokes.Add(new SimpleStroke());
                ccStrokes.RefreshList();
            }
            if (cmbStrokeType.SelectedItem.ToString() == "Cartographic")
            {
                _symbolizer.Strokes.Add(new CartographicStroke());
                ccStrokes.RefreshList();
            }
            UpdatePreview();
        }

        private void ccStrokes_SelectedItemChanged(object sender, EventArgs e)
        {
            if (ccStrokes.SelectedStroke != null)
            {
                UpdateStrokeControls();
            }
            UpdatePreview();
        }

       

        private void UpdateDecorationControls()
        {
            _ignoreChanges = true;
            ILineDecoration decoration = ccDecorations.SelectedDecoration;
            if(decoration != null)
            {
                chkFlipAll.Checked = decoration.FlipAll;
                chkFlipFirst.Checked = decoration.FlipFirst;
                if (decoration.RotateWithLine)
                {
                    radRotationWithLine.Checked = true;
                }
                else
                {
                    radRotationFixed.Checked = true;
                }
                nudDecorationCount.Value = decoration.NumSymbols;
                dblOffset.Value = decoration.Offset;
            }
            _ignoreChanges = false;
            
        }

        private void cmbStrokeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_ignoreChanges) return;
            int index = ccStrokes.Strokes.IndexOf(ccStrokes.SelectedStroke);
            if (index == -1) return;
            string strokeType = cmbStrokeType.SelectedItem.ToString();
            if (strokeType == "Cartographic")
            {
                ICartographicStroke cs = new CartographicStroke();
                ccStrokes.Strokes[index] = cs;
                ccStrokes.RefreshList();
                ccStrokes.SelectedStroke = cs;
                //UpdateStrokeControls();
            }
            else if (strokeType == "Simple")
            {
                ISimpleStroke ss = new SimpleStroke();
                ccStrokes.Strokes[index] = ss;
                ccStrokes.RefreshList();
                ccStrokes.SelectedStroke = ss;
               // UpdateStrokeControls();
            }
            //UpdatePreview();
        }

        private void cmbScaleMode_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (cmbScaleMode.SelectedItem.ToString())
            {
                case "Simple": _symbolizer.ScaleMode = ScaleModes.Simple; break;
                case "Geographic": _symbolizer.ScaleMode = ScaleModes.Geographic; break;
                case "Symbolic": _symbolizer.ScaleMode = ScaleModes.Symbolic; break;
            }

           
        }

        private void chkSmoothing_CheckedChanged(object sender, EventArgs e)
        {
            _symbolizer.Smoothing = chkSmoothing.Checked;
            UpdatePreview();
        }

        

       

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_ignoreChanges) return;
            if (ccStrokes.SelectedStroke == null) return;
            ISimpleStroke ss = ccStrokes.SelectedStroke as ISimpleStroke;
            if (ss != null) ss.DashStyle = (DashStyle)cmbStrokeStyle.SelectedIndex;
            UpdatePreview();
        }

        private void cmbStartCap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_ignoreChanges) return;
            if (ccStrokes.SelectedStroke == null) return;
            IStroke stroke = ccStrokes.SelectedStroke;
            ICartographicStroke cs = stroke as ICartographicStroke;
            if (cs != null && cmbStartCap.SelectedIndex != -1)
            {
                cs.StartCap = (LineCap)cmbStartCap.SelectedItem;
            }
            UpdatePreview();
        }

        private void cmbEndCap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_ignoreChanges) return;
            if (ccStrokes.SelectedStroke == null) return;
            IStroke stroke = ccStrokes.SelectedStroke;
            ICartographicStroke cs = stroke as ICartographicStroke;
            if (cs != null && cmbEndCap.SelectedIndex != -1)
            {
                cs.EndCap = (LineCap)cmbEndCap.SelectedItem;
            }
            UpdatePreview();
        }

        private void nudDecorationCount_ValueChanged(object sender, EventArgs e)
        {
            if (_ignoreChanges) return;
            ILineDecoration dec = ccDecorations.SelectedDecoration;
            if (dec != null)
            {
                dec.NumSymbols = (int)nudDecorationCount.Value;
            }
            UpdatePreview();
        }

        private void chkFlipAll_CheckedChanged(object sender, EventArgs e)
        {
            if (_ignoreChanges) return;
            ILineDecoration dec = ccDecorations.SelectedDecoration;
            if (dec != null)
            {
                dec.FlipAll = chkFlipAll.Checked;
            }
            UpdatePreview();
        }

        private void chkFlipFirst_CheckedChanged(object sender, EventArgs e)
        {
            if (_ignoreChanges) return;
            ILineDecoration dec = ccDecorations.SelectedDecoration;
            if (dec != null)
            {
                dec.FlipFirst = chkFlipFirst.Checked;
            }
            UpdatePreview();
        }

        private void radRotationWithLine_CheckedChanged(object sender, EventArgs e)
        {
            if (_ignoreChanges) return;
            ILineDecoration dec = ccDecorations.SelectedDecoration;
            if (dec != null)
            {
                dec.RotateWithLine = radRotationWithLine.Checked;
            }
            UpdatePreview();
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ILineDecoration dec = ccDecorations.SelectedDecoration;
            if (dec == null) return;
            DetailedPointSymbolDialog dpd = new DetailedPointSymbolDialog(dec.Symbol);
            dpd.ChangesApplied += dpd_ChangesApplied;
            dpd.ShowDialog();
        }

        void dpd_ChangesApplied(object sender, EventArgs e)
        {
            UpdatePreview();
            ccDecorations.Refresh();
            lblDecorationPreview.Refresh();
        }

        private void btnAddToCustom_Click(object sender, EventArgs e)
        {
            OnAddToCustomSymbols();
        }



        #endregion

        #region Private Functions

        private void UpdatePreview()
        {
            Graphics g = lblPreview.CreateGraphics();
            UpdatePreview(g);
            g.Dispose();

            ICartographicStroke cs = ccStrokes.SelectedStroke as ICartographicStroke;
            if (cs != null)
            {
                g = lblDecorationPreview.CreateGraphics();
                UpdateDecorationPreview(g);
                g.Dispose();

            }
        }

        private void UpdatePreview(Graphics g)
        {
            g.FillRectangle(Brushes.White, new Rectangle(0, 0, lblPreview.Width, lblPreview.Height));
            GraphicsPath gp = new GraphicsPath();
            gp.AddLine(10, lblPreview.Height / 2, lblPreview.Width - 20, lblPreview.Height / 2);
            foreach (IStroke stroke in _symbolizer.Strokes)
            {
                stroke.DrawPath(g, gp, 1);
            }
            gp.Dispose();
            ccStrokes.Refresh();
        }

        private void UpdateDecorationPreview(Graphics g)
        {
            ILineDecoration dec = ccDecorations.SelectedDecoration;
            if (dec != null)
            {
                dec.Symbol.Draw(g, lblDecorationPreview.ClientRectangle);
            }
        }

        #endregion

     










    }
}
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
// The Initial Developer of this Original Code is Ted Dunsford. Created 11/20/2008 9:11:11 AM
// 
// Contributor(s): (Open source contributors should list themselves and their modifications here). 
//
//********************************************************************************************************
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MapWindow.Components;
using MapWindow.Drawing;

namespace MapWindow.Forms
{
    /// <summary>
    /// frmLabelSetup
    /// </summary>
    public class LabelSetup : Form
    {

        #region Events

        /// <summary>
        /// Occurs after the Apply button has been pressed
        /// </summary>
        public event EventHandler ChangesApplied;

        #endregion

        #region Private Variables

        //private string _expression;
        private Button cmdOK;
        private Button cmdCancel;
        private Button cmdApply;

        //private ITextSymbolizer _symbolizer;
        private TabControl tabs;
        private TabPage tabExpression;
        private TabPage tabBasic;
        private TabPage tabAdvanced;
        private Panel panel1;
        private TabPage tabMembers;
        private SplitContainer splitContainer1;
        private ListBox lbCategories;
        private Panel panel2;
        private Label lblSymbolGroups;
        private Label lblHelp;
        private ToolTip ttLabelSetup;
        private ColorBox clrHalo;
        private CheckBox chkHalo;
        private ComboBox cmbAlignment;
        private CheckBox chkShadow;
        private CheckBox chkBorder;
        private SQLQueryControl sqlExpression;
        private SQLQueryControl sqlMembers;
        private Label lblFamily;
        private FontFamilyControl ffcFamilyName;
        private ComboBox cmbStyle;
        private ComboBox cmbSize;
        private Label label2;
        private Label label1;
        private Label lblPreview;


        private ILabelLayer _layer;
        private ILabelCategory _activeCategory;
        private Button btnAdd;
        private Panel panel3;
        private Button btnSubtract;
        private Button btnCategoryDown;
        private Button btnCategoryUp;
        private GroupBox gpbFont;
        private GroupBox gpbBorderColor;
        private GroupBox gpbBackgroundColor;
        private CheckBox chkBackgroundColor;
        private GroupBox groupBox1;
        private GroupBox gpbUseLabelShadow;
        private Label label4;
        private Label label3;
        private ComboBox cmbLabelingMethod;
        private ComboBox cmbLabelParts;
        private Label label5;
        private ColorButton colorButtonShadow;
        private Label label6;
        private RampSlider sliderOpacityShadow;
        private NumericUpDown UpDownShadowOffsetX;
        private NumericUpDown UpDownShadowOffsetY;
        private Label label9;
        private Label label8;
        private Label label7;
        // private LabelAlignmentControl labelAlignmentControl1;
        private Label label10;
        private GroupBox grpOffset;
        private NumericUpDown nudYOffset;
        private Label label11;
        private Label label12;
        private NumericUpDown nudXOffset;
        private LabelAlignmentControl labelAlignmentControl1;
        private RampSlider sldBackgroundOpacity;
        private ColorButton cbBackgroundColor;
        private RampSlider sldBorderOpacity;
        private ColorButton cbBorderColor;
        private RampSlider sldFontOpacity;
        private ColorButton cbFontColor;
        private CheckBox chkPrioritizeLow;
        private CheckBox chkPreventCollision;
        private Label lblPriorityField;
        private ComboBox cmbPriorityField;
        private Label lblFontColor;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components;

        private bool _ignoreUpdates;



        #endregion


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LabelSetup));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lbCategories = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCategoryDown = new System.Windows.Forms.Button();
            this.btnCategoryUp = new System.Windows.Forms.Button();
            this.btnSubtract = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblSymbolGroups = new System.Windows.Forms.Label();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabExpression = new System.Windows.Forms.TabPage();
            this.sqlExpression = new MapWindow.Components.SQLQueryControl();
            this.tabBasic = new System.Windows.Forms.TabPage();
            this.chkPrioritizeLow = new System.Windows.Forms.CheckBox();
            this.chkPreventCollision = new System.Windows.Forms.CheckBox();
            this.lblPriorityField = new System.Windows.Forms.Label();
            this.cmbPriorityField = new System.Windows.Forms.ComboBox();
            this.gpbBorderColor = new System.Windows.Forms.GroupBox();
            this.chkBorder = new System.Windows.Forms.CheckBox();
            this.sldBorderOpacity = new MapWindow.Components.RampSlider();
            this.cbBorderColor = new MapWindow.Components.ColorButton();
            this.gpbFont = new System.Windows.Forms.GroupBox();
            this.lblFontColor = new System.Windows.Forms.Label();
            this.sldFontOpacity = new MapWindow.Components.RampSlider();
            this.cbFontColor = new MapWindow.Components.ColorButton();
            this.cmbStyle = new System.Windows.Forms.ComboBox();
            this.lblFamily = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSize = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ffcFamilyName = new MapWindow.Components.FontFamilyControl();
            this.lblPreview = new System.Windows.Forms.Label();
            this.gpbBackgroundColor = new System.Windows.Forms.GroupBox();
            this.sldBackgroundOpacity = new MapWindow.Components.RampSlider();
            this.cbBackgroundColor = new MapWindow.Components.ColorButton();
            this.chkBackgroundColor = new System.Windows.Forms.CheckBox();
            this.tabAdvanced = new System.Windows.Forms.TabPage();
            this.labelAlignmentControl1 = new MapWindow.Components.LabelAlignmentControl();
            this.grpOffset = new System.Windows.Forms.GroupBox();
            this.nudYOffset = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.nudXOffset = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.chkHalo = new System.Windows.Forms.CheckBox();
            this.chkShadow = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbAlignment = new System.Windows.Forms.ComboBox();
            this.cmbLabelingMethod = new System.Windows.Forms.ComboBox();
            this.cmbLabelParts = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.clrHalo = new MapWindow.Components.ColorBox();
            this.gpbUseLabelShadow = new System.Windows.Forms.GroupBox();
            this.UpDownShadowOffsetY = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.UpDownShadowOffsetX = new System.Windows.Forms.NumericUpDown();
            this.sliderOpacityShadow = new MapWindow.Components.RampSlider();
            this.label6 = new System.Windows.Forms.Label();
            this.colorButtonShadow = new MapWindow.Components.ColorButton();
            this.tabMembers = new System.Windows.Forms.TabPage();
            this.sqlMembers = new MapWindow.Components.SQLQueryControl();
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdApply = new System.Windows.Forms.Button();
            this.lblHelp = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ttLabelSetup = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabs.SuspendLayout();
            this.tabExpression.SuspendLayout();
            this.tabBasic.SuspendLayout();
            this.gpbBorderColor.SuspendLayout();
            this.gpbFont.SuspendLayout();
            this.gpbBackgroundColor.SuspendLayout();
            this.tabAdvanced.SuspendLayout();
            this.grpOffset.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudYOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudXOffset)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.gpbUseLabelShadow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownShadowOffsetY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownShadowOffsetX)).BeginInit();
            this.tabMembers.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lbCategories);
            this.splitContainer1.Panel1.Controls.Add(this.panel3);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabs);
            // 
            // lbCategories
            // 
            resources.ApplyResources(this.lbCategories, "lbCategories");
            this.lbCategories.FormattingEnabled = true;
            this.lbCategories.Name = "lbCategories";
            this.lbCategories.SelectedIndexChanged += new System.EventHandler(this.lbCategories_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnCategoryDown);
            this.panel3.Controls.Add(this.btnCategoryUp);
            this.panel3.Controls.Add(this.btnSubtract);
            this.panel3.Controls.Add(this.btnAdd);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // btnCategoryDown
            // 
            this.btnCategoryDown.Image = global::MapWindow.Images.down;
            resources.ApplyResources(this.btnCategoryDown, "btnCategoryDown");
            this.btnCategoryDown.Name = "btnCategoryDown";
            this.btnCategoryDown.UseVisualStyleBackColor = true;
            this.btnCategoryDown.Click += new System.EventHandler(this.btnCategoryDown_Click);
            // 
            // btnCategoryUp
            // 
            this.btnCategoryUp.Image = global::MapWindow.Images.up;
            resources.ApplyResources(this.btnCategoryUp, "btnCategoryUp");
            this.btnCategoryUp.Name = "btnCategoryUp";
            this.btnCategoryUp.UseVisualStyleBackColor = true;
            this.btnCategoryUp.Click += new System.EventHandler(this.btnCategoryUp_Click);
            // 
            // btnSubtract
            // 
            this.btnSubtract.Image = global::MapWindow.Images.mnuLayerClear;
            resources.ApplyResources(this.btnSubtract, "btnSubtract");
            this.btnSubtract.Name = "btnSubtract";
            this.btnSubtract.UseVisualStyleBackColor = true;
            this.btnSubtract.Click += new System.EventHandler(this.btnSubtract_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::MapWindow.Images.mnuLayerAdd;
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblSymbolGroups);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // lblSymbolGroups
            // 
            resources.ApplyResources(this.lblSymbolGroups, "lblSymbolGroups");
            this.lblSymbolGroups.Name = "lblSymbolGroups";
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabExpression);
            this.tabs.Controls.Add(this.tabBasic);
            this.tabs.Controls.Add(this.tabAdvanced);
            this.tabs.Controls.Add(this.tabMembers);
            resources.ApplyResources(this.tabs, "tabs");
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            // 
            // tabExpression
            // 
            this.tabExpression.Controls.Add(this.sqlExpression);
            resources.ApplyResources(this.tabExpression, "tabExpression");
            this.tabExpression.Name = "tabExpression";
            this.tabExpression.UseVisualStyleBackColor = true;
            // 
            // sqlExpression
            // 
            this.sqlExpression.AttributeSource = null;
            resources.ApplyResources(this.sqlExpression, "sqlExpression");
            this.sqlExpression.ExpressionText = "";
            this.sqlExpression.Name = "sqlExpression";
            this.sqlExpression.Table = null;
            this.sqlExpression.ExpressionTextChanged += new System.EventHandler(this.sqlExpression_ExpressionTextChanged);
            // 
            // tabBasic
            // 
            this.tabBasic.Controls.Add(this.chkPrioritizeLow);
            this.tabBasic.Controls.Add(this.chkPreventCollision);
            this.tabBasic.Controls.Add(this.lblPriorityField);
            this.tabBasic.Controls.Add(this.cmbPriorityField);
            this.tabBasic.Controls.Add(this.gpbBorderColor);
            this.tabBasic.Controls.Add(this.gpbFont);
            this.tabBasic.Controls.Add(this.lblPreview);
            this.tabBasic.Controls.Add(this.gpbBackgroundColor);
            resources.ApplyResources(this.tabBasic, "tabBasic");
            this.tabBasic.Name = "tabBasic";
            this.tabBasic.UseVisualStyleBackColor = true;
            // 
            // chkPrioritizeLow
            // 
            resources.ApplyResources(this.chkPrioritizeLow, "chkPrioritizeLow");
            this.chkPrioritizeLow.Name = "chkPrioritizeLow";
            this.ttLabelSetup.SetToolTip(this.chkPrioritizeLow, resources.GetString("chkPrioritizeLow.ToolTip"));
            this.chkPrioritizeLow.UseVisualStyleBackColor = true;
            this.chkPrioritizeLow.CheckedChanged += new System.EventHandler(this.chkPrioritizeLow_CheckedChanged);
            // 
            // chkPreventCollision
            // 
            resources.ApplyResources(this.chkPreventCollision, "chkPreventCollision");
            this.chkPreventCollision.Checked = true;
            this.chkPreventCollision.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPreventCollision.Name = "chkPreventCollision";
            this.ttLabelSetup.SetToolTip(this.chkPreventCollision, resources.GetString("chkPreventCollision.ToolTip"));
            this.chkPreventCollision.UseVisualStyleBackColor = true;
            this.chkPreventCollision.CheckedChanged += new System.EventHandler(this.chkPreventCollision_CheckedChanged);
            // 
            // lblPriorityField
            // 
            resources.ApplyResources(this.lblPriorityField, "lblPriorityField");
            this.lblPriorityField.Name = "lblPriorityField";
            // 
            // cmbPriorityField
            // 
            this.cmbPriorityField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPriorityField.FormattingEnabled = true;
            resources.ApplyResources(this.cmbPriorityField, "cmbPriorityField");
            this.cmbPriorityField.Name = "cmbPriorityField";
            this.ttLabelSetup.SetToolTip(this.cmbPriorityField, resources.GetString("cmbPriorityField.ToolTip"));
            this.cmbPriorityField.SelectedIndexChanged += new System.EventHandler(this.cmbPriorityField_SelectedIndexChanged);
            // 
            // gpbBorderColor
            // 
            this.gpbBorderColor.Controls.Add(this.chkBorder);
            this.gpbBorderColor.Controls.Add(this.sldBorderOpacity);
            this.gpbBorderColor.Controls.Add(this.cbBorderColor);
            resources.ApplyResources(this.gpbBorderColor, "gpbBorderColor");
            this.gpbBorderColor.Name = "gpbBorderColor";
            this.gpbBorderColor.TabStop = false;
            // 
            // chkBorder
            // 
            resources.ApplyResources(this.chkBorder, "chkBorder");
            this.chkBorder.Checked = true;
            this.chkBorder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBorder.Name = "chkBorder";
            this.chkBorder.UseVisualStyleBackColor = true;
            this.chkBorder.CheckedChanged += new System.EventHandler(this.chkBorder_CheckedChanged);
            // 
            // sldBorderOpacity
            // 
            this.sldBorderOpacity.ColorButton = this.cbBorderColor;
            this.sldBorderOpacity.FlipRamp = false;
            this.sldBorderOpacity.FlipText = false;
            this.sldBorderOpacity.InvertRamp = false;
            resources.ApplyResources(this.sldBorderOpacity, "sldBorderOpacity");
            this.sldBorderOpacity.Maximum = 1;
            this.sldBorderOpacity.MaximumColor = System.Drawing.Color.Green;
            this.sldBorderOpacity.Minimum = 0;
            this.sldBorderOpacity.MinimumColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.sldBorderOpacity.Name = "sldBorderOpacity";
            this.sldBorderOpacity.NumberFormat = null;
            this.sldBorderOpacity.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.sldBorderOpacity.RampRadius = 10F;
            this.sldBorderOpacity.RampText = "Opacity";
            this.sldBorderOpacity.RampTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.sldBorderOpacity.RampTextBehindRamp = true;
            this.sldBorderOpacity.RampTextColor = System.Drawing.Color.Black;
            this.sldBorderOpacity.RampTextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sldBorderOpacity.ShowMaximum = true;
            this.sldBorderOpacity.ShowMinimum = true;
            this.sldBorderOpacity.ShowTicks = false;
            this.sldBorderOpacity.ShowValue = false;
            this.sldBorderOpacity.SliderColor = System.Drawing.Color.Blue;
            this.sldBorderOpacity.SliderRadius = 4F;
            this.sldBorderOpacity.TickColor = System.Drawing.Color.DarkGray;
            this.sldBorderOpacity.TickSpacing = 5F;
            this.sldBorderOpacity.Value = 1;
            // 
            // cbBorderColor
            // 
            this.cbBorderColor.BevelRadius = 2;
            this.cbBorderColor.Color = System.Drawing.Color.Blue;
            this.cbBorderColor.LaunchDialogOnClick = true;
            resources.ApplyResources(this.cbBorderColor, "cbBorderColor");
            this.cbBorderColor.Name = "cbBorderColor";
            this.cbBorderColor.RoundingRadius = 4;
            this.cbBorderColor.ColorChanged += new System.EventHandler(this.cbBorderColor_ColorChanged);
            // 
            // gpbFont
            // 
            this.gpbFont.Controls.Add(this.lblFontColor);
            this.gpbFont.Controls.Add(this.sldFontOpacity);
            this.gpbFont.Controls.Add(this.cbFontColor);
            this.gpbFont.Controls.Add(this.cmbStyle);
            this.gpbFont.Controls.Add(this.lblFamily);
            this.gpbFont.Controls.Add(this.label2);
            this.gpbFont.Controls.Add(this.cmbSize);
            this.gpbFont.Controls.Add(this.label1);
            this.gpbFont.Controls.Add(this.ffcFamilyName);
            resources.ApplyResources(this.gpbFont, "gpbFont");
            this.gpbFont.Name = "gpbFont";
            this.gpbFont.TabStop = false;
            // 
            // lblFontColor
            // 
            resources.ApplyResources(this.lblFontColor, "lblFontColor");
            this.lblFontColor.Name = "lblFontColor";
            // 
            // sldFontOpacity
            // 
            this.sldFontOpacity.ColorButton = this.cbFontColor;
            this.sldFontOpacity.FlipRamp = false;
            this.sldFontOpacity.FlipText = false;
            this.sldFontOpacity.InvertRamp = false;
            resources.ApplyResources(this.sldFontOpacity, "sldFontOpacity");
            this.sldFontOpacity.Maximum = 1;
            this.sldFontOpacity.MaximumColor = System.Drawing.Color.Green;
            this.sldFontOpacity.Minimum = 0;
            this.sldFontOpacity.MinimumColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.sldFontOpacity.Name = "sldFontOpacity";
            this.sldFontOpacity.NumberFormat = null;
            this.sldFontOpacity.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.sldFontOpacity.RampRadius = 10F;
            this.sldFontOpacity.RampText = "Opacity";
            this.sldFontOpacity.RampTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.sldFontOpacity.RampTextBehindRamp = true;
            this.sldFontOpacity.RampTextColor = System.Drawing.Color.Black;
            this.sldFontOpacity.RampTextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sldFontOpacity.ShowMaximum = true;
            this.sldFontOpacity.ShowMinimum = true;
            this.sldFontOpacity.ShowTicks = false;
            this.sldFontOpacity.ShowValue = false;
            this.sldFontOpacity.SliderColor = System.Drawing.Color.Blue;
            this.sldFontOpacity.SliderRadius = 4F;
            this.sldFontOpacity.TickColor = System.Drawing.Color.DarkGray;
            this.sldFontOpacity.TickSpacing = 5F;
            this.sldFontOpacity.Value = 1;
            // 
            // cbFontColor
            // 
            this.cbFontColor.BevelRadius = 2;
            this.cbFontColor.Color = System.Drawing.Color.Blue;
            this.cbFontColor.LaunchDialogOnClick = true;
            resources.ApplyResources(this.cbFontColor, "cbFontColor");
            this.cbFontColor.Name = "cbFontColor";
            this.cbFontColor.RoundingRadius = 4;
            this.cbFontColor.ColorChanged += new System.EventHandler(this.cbFontColor_ColorChanged);
            // 
            // cmbStyle
            // 
            this.cmbStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStyle.FormattingEnabled = true;
            resources.ApplyResources(this.cmbStyle, "cmbStyle");
            this.cmbStyle.Name = "cmbStyle";
            this.cmbStyle.SelectedIndexChanged += new System.EventHandler(this.cmbStyle_SelectedIndexChanged);
            // 
            // lblFamily
            // 
            resources.ApplyResources(this.lblFamily, "lblFamily");
            this.lblFamily.Name = "lblFamily";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // cmbSize
            // 
            this.cmbSize.FormattingEnabled = true;
            this.cmbSize.Items.AddRange(new object[] {
            resources.GetString("cmbSize.Items"),
            resources.GetString("cmbSize.Items1"),
            resources.GetString("cmbSize.Items2"),
            resources.GetString("cmbSize.Items3"),
            resources.GetString("cmbSize.Items4"),
            resources.GetString("cmbSize.Items5"),
            resources.GetString("cmbSize.Items6"),
            resources.GetString("cmbSize.Items7"),
            resources.GetString("cmbSize.Items8"),
            resources.GetString("cmbSize.Items9"),
            resources.GetString("cmbSize.Items10"),
            resources.GetString("cmbSize.Items11"),
            resources.GetString("cmbSize.Items12"),
            resources.GetString("cmbSize.Items13"),
            resources.GetString("cmbSize.Items14"),
            resources.GetString("cmbSize.Items15"),
            resources.GetString("cmbSize.Items16")});
            resources.ApplyResources(this.cmbSize, "cmbSize");
            this.cmbSize.Name = "cmbSize";
            this.cmbSize.SelectedIndexChanged += new System.EventHandler(this.cmbSize_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // ffcFamilyName
            // 
            resources.ApplyResources(this.ffcFamilyName, "ffcFamilyName");
            this.ffcFamilyName.Name = "ffcFamilyName";
            this.ffcFamilyName.SelectedItemChanged += new System.EventHandler(this.fontFamilyControl1_SelectedItemChanged);
            // 
            // lblPreview
            // 
            this.lblPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.lblPreview, "lblPreview");
            this.lblPreview.Name = "lblPreview";
            // 
            // gpbBackgroundColor
            // 
            this.gpbBackgroundColor.Controls.Add(this.sldBackgroundOpacity);
            this.gpbBackgroundColor.Controls.Add(this.cbBackgroundColor);
            this.gpbBackgroundColor.Controls.Add(this.chkBackgroundColor);
            resources.ApplyResources(this.gpbBackgroundColor, "gpbBackgroundColor");
            this.gpbBackgroundColor.Name = "gpbBackgroundColor";
            this.gpbBackgroundColor.TabStop = false;
            this.gpbBackgroundColor.Enter += new System.EventHandler(this.gpbBackgroundColor_Enter);
            // 
            // sldBackgroundOpacity
            // 
            this.sldBackgroundOpacity.ColorButton = this.cbBackgroundColor;
            this.sldBackgroundOpacity.FlipRamp = false;
            this.sldBackgroundOpacity.FlipText = false;
            this.sldBackgroundOpacity.InvertRamp = false;
            resources.ApplyResources(this.sldBackgroundOpacity, "sldBackgroundOpacity");
            this.sldBackgroundOpacity.Maximum = 1;
            this.sldBackgroundOpacity.MaximumColor = System.Drawing.Color.Green;
            this.sldBackgroundOpacity.Minimum = 0;
            this.sldBackgroundOpacity.MinimumColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.sldBackgroundOpacity.Name = "sldBackgroundOpacity";
            this.sldBackgroundOpacity.NumberFormat = null;
            this.sldBackgroundOpacity.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.sldBackgroundOpacity.RampRadius = 10F;
            this.sldBackgroundOpacity.RampText = "Opacity";
            this.sldBackgroundOpacity.RampTextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.sldBackgroundOpacity.RampTextBehindRamp = true;
            this.sldBackgroundOpacity.RampTextColor = System.Drawing.Color.Black;
            this.sldBackgroundOpacity.RampTextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sldBackgroundOpacity.ShowMaximum = true;
            this.sldBackgroundOpacity.ShowMinimum = true;
            this.sldBackgroundOpacity.ShowTicks = false;
            this.sldBackgroundOpacity.ShowValue = false;
            this.sldBackgroundOpacity.SliderColor = System.Drawing.Color.Blue;
            this.sldBackgroundOpacity.SliderRadius = 4F;
            this.sldBackgroundOpacity.TickColor = System.Drawing.Color.DarkGray;
            this.sldBackgroundOpacity.TickSpacing = 5F;
            this.sldBackgroundOpacity.Value = 1;
            this.sldBackgroundOpacity.ValueChanged += new System.EventHandler(this.rampSlider1_ValueChanged);
            // 
            // cbBackgroundColor
            // 
            this.cbBackgroundColor.BevelRadius = 2;
            this.cbBackgroundColor.Color = System.Drawing.Color.Blue;
            this.cbBackgroundColor.LaunchDialogOnClick = true;
            resources.ApplyResources(this.cbBackgroundColor, "cbBackgroundColor");
            this.cbBackgroundColor.Name = "cbBackgroundColor";
            this.cbBackgroundColor.RoundingRadius = 4;
            this.cbBackgroundColor.ColorChanged += new System.EventHandler(this.cbBackgroundColor_ColorChanged);
            // 
            // chkBackgroundColor
            // 
            resources.ApplyResources(this.chkBackgroundColor, "chkBackgroundColor");
            this.chkBackgroundColor.Checked = true;
            this.chkBackgroundColor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBackgroundColor.Name = "chkBackgroundColor";
            this.chkBackgroundColor.UseVisualStyleBackColor = true;
            this.chkBackgroundColor.CheckedChanged += new System.EventHandler(this.chkBackgroundColor_CheckedChanged);
            // 
            // tabAdvanced
            // 
            this.tabAdvanced.Controls.Add(this.labelAlignmentControl1);
            this.tabAdvanced.Controls.Add(this.grpOffset);
            this.tabAdvanced.Controls.Add(this.label10);
            this.tabAdvanced.Controls.Add(this.chkHalo);
            this.tabAdvanced.Controls.Add(this.chkShadow);
            this.tabAdvanced.Controls.Add(this.label5);
            this.tabAdvanced.Controls.Add(this.label4);
            this.tabAdvanced.Controls.Add(this.label3);
            this.tabAdvanced.Controls.Add(this.cmbAlignment);
            this.tabAdvanced.Controls.Add(this.cmbLabelingMethod);
            this.tabAdvanced.Controls.Add(this.cmbLabelParts);
            this.tabAdvanced.Controls.Add(this.groupBox1);
            this.tabAdvanced.Controls.Add(this.gpbUseLabelShadow);
            resources.ApplyResources(this.tabAdvanced, "tabAdvanced");
            this.tabAdvanced.Name = "tabAdvanced";
            this.tabAdvanced.UseVisualStyleBackColor = true;
            // 
            // labelAlignmentControl1
            // 
            this.labelAlignmentControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.labelAlignmentControl1, "labelAlignmentControl1");
            this.labelAlignmentControl1.Name = "labelAlignmentControl1";
            this.labelAlignmentControl1.Value = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelAlignmentControl1.ValueChanged += new System.EventHandler(this.labelAlignmentControl1_ValueChanged);
            // 
            // grpOffset
            // 
            this.grpOffset.Controls.Add(this.nudYOffset);
            this.grpOffset.Controls.Add(this.label11);
            this.grpOffset.Controls.Add(this.label12);
            this.grpOffset.Controls.Add(this.nudXOffset);
            resources.ApplyResources(this.grpOffset, "grpOffset");
            this.grpOffset.Name = "grpOffset";
            this.grpOffset.TabStop = false;
            // 
            // nudYOffset
            // 
            this.nudYOffset.DecimalPlaces = 2;
            resources.ApplyResources(this.nudYOffset, "nudYOffset");
            this.nudYOffset.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.nudYOffset.Minimum = new decimal(new int[] {
            256,
            0,
            0,
            -2147483648});
            this.nudYOffset.Name = "nudYOffset";
            this.nudYOffset.ValueChanged += new System.EventHandler(this.nudYOffset_ValueChanged);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // nudXOffset
            // 
            this.nudXOffset.DecimalPlaces = 2;
            resources.ApplyResources(this.nudXOffset, "nudXOffset");
            this.nudXOffset.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.nudXOffset.Minimum = new decimal(new int[] {
            256,
            0,
            0,
            -2147483648});
            this.nudXOffset.Name = "nudXOffset";
            this.nudXOffset.ValueChanged += new System.EventHandler(this.nudXOffset_ValueChanged);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // chkHalo
            // 
            resources.ApplyResources(this.chkHalo, "chkHalo");
            this.chkHalo.Checked = true;
            this.chkHalo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHalo.Name = "chkHalo";
            this.chkHalo.UseVisualStyleBackColor = true;
            this.chkHalo.CheckedChanged += new System.EventHandler(this.chkHalo_CheckedChanged);
            // 
            // chkShadow
            // 
            resources.ApplyResources(this.chkShadow, "chkShadow");
            this.chkShadow.Checked = true;
            this.chkShadow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShadow.Name = "chkShadow";
            this.chkShadow.UseVisualStyleBackColor = true;
            this.chkShadow.CheckedChanged += new System.EventHandler(this.chkShadow_CheckedChanged);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // cmbAlignment
            // 
            this.cmbAlignment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAlignment.FormattingEnabled = true;
            this.cmbAlignment.Items.AddRange(new object[] {
            resources.GetString("cmbAlignment.Items"),
            resources.GetString("cmbAlignment.Items1"),
            resources.GetString("cmbAlignment.Items2")});
            resources.ApplyResources(this.cmbAlignment, "cmbAlignment");
            this.cmbAlignment.Name = "cmbAlignment";
            this.cmbAlignment.SelectedIndexChanged += new System.EventHandler(this.cmbAlignment_SelectedIndexChanged);
            // 
            // cmbLabelingMethod
            // 
            this.cmbLabelingMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLabelingMethod.FormattingEnabled = true;
            resources.ApplyResources(this.cmbLabelingMethod, "cmbLabelingMethod");
            this.cmbLabelingMethod.Name = "cmbLabelingMethod";
            this.cmbLabelingMethod.SelectedIndexChanged += new System.EventHandler(this.cmbLabelingMethod_SelectedIndexChanged);
            // 
            // cmbLabelParts
            // 
            this.cmbLabelParts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLabelParts.FormattingEnabled = true;
            resources.ApplyResources(this.cmbLabelParts, "cmbLabelParts");
            this.cmbLabelParts.Name = "cmbLabelParts";
            this.cmbLabelParts.SelectedIndexChanged += new System.EventHandler(this.cmbLabelParts_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.clrHalo);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // clrHalo
            // 
            resources.ApplyResources(this.clrHalo, "clrHalo");
            this.clrHalo.LabelText = "Halo Color:";
            this.clrHalo.Name = "clrHalo";
            this.clrHalo.Value = System.Drawing.Color.Empty;
            this.clrHalo.SelectedItemChanged += new System.EventHandler(this.clrHalo_SelectedItemChanged);
            // 
            // gpbUseLabelShadow
            // 
            this.gpbUseLabelShadow.Controls.Add(this.UpDownShadowOffsetY);
            this.gpbUseLabelShadow.Controls.Add(this.label9);
            this.gpbUseLabelShadow.Controls.Add(this.label8);
            this.gpbUseLabelShadow.Controls.Add(this.label7);
            this.gpbUseLabelShadow.Controls.Add(this.UpDownShadowOffsetX);
            this.gpbUseLabelShadow.Controls.Add(this.sliderOpacityShadow);
            this.gpbUseLabelShadow.Controls.Add(this.label6);
            this.gpbUseLabelShadow.Controls.Add(this.colorButtonShadow);
            resources.ApplyResources(this.gpbUseLabelShadow, "gpbUseLabelShadow");
            this.gpbUseLabelShadow.Name = "gpbUseLabelShadow";
            this.gpbUseLabelShadow.TabStop = false;
            // 
            // UpDownShadowOffsetY
            // 
            this.UpDownShadowOffsetY.DecimalPlaces = 2;
            this.UpDownShadowOffsetY.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            resources.ApplyResources(this.UpDownShadowOffsetY, "UpDownShadowOffsetY");
            this.UpDownShadowOffsetY.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.UpDownShadowOffsetY.Minimum = new decimal(new int[] {
            256,
            0,
            0,
            -2147483648});
            this.UpDownShadowOffsetY.Name = "UpDownShadowOffsetY";
            this.UpDownShadowOffsetY.ValueChanged += new System.EventHandler(this.UpDownShadowOffsetY_ValueChanged);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // UpDownShadowOffsetX
            // 
            this.UpDownShadowOffsetX.DecimalPlaces = 2;
            this.UpDownShadowOffsetX.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            resources.ApplyResources(this.UpDownShadowOffsetX, "UpDownShadowOffsetX");
            this.UpDownShadowOffsetX.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.UpDownShadowOffsetX.Minimum = new decimal(new int[] {
            256,
            0,
            0,
            -2147483648});
            this.UpDownShadowOffsetX.Name = "UpDownShadowOffsetX";
            this.UpDownShadowOffsetX.ValueChanged += new System.EventHandler(this.UpDownShadowOffsetX_ValueChanged);
            // 
            // sliderOpacityShadow
            // 
            this.sliderOpacityShadow.ColorButton = null;
            this.sliderOpacityShadow.FlipRamp = false;
            this.sliderOpacityShadow.FlipText = false;
            this.sliderOpacityShadow.InvertRamp = false;
            resources.ApplyResources(this.sliderOpacityShadow, "sliderOpacityShadow");
            this.sliderOpacityShadow.Maximum = 1;
            this.sliderOpacityShadow.MaximumColor = System.Drawing.Color.CornflowerBlue;
            this.sliderOpacityShadow.Minimum = 0;
            this.sliderOpacityShadow.MinimumColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.sliderOpacityShadow.Name = "sliderOpacityShadow";
            this.sliderOpacityShadow.NumberFormat = null;
            this.sliderOpacityShadow.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.sliderOpacityShadow.RampRadius = 8F;
            this.sliderOpacityShadow.RampText = "Opacity";
            this.sliderOpacityShadow.RampTextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            this.sliderOpacityShadow.RampTextBehindRamp = true;
            this.sliderOpacityShadow.RampTextColor = System.Drawing.Color.Black;
            this.sliderOpacityShadow.RampTextFont = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sliderOpacityShadow.ShowMaximum = true;
            this.sliderOpacityShadow.ShowMinimum = true;
            this.sliderOpacityShadow.ShowTicks = true;
            this.sliderOpacityShadow.ShowValue = false;
            this.sliderOpacityShadow.SliderColor = System.Drawing.Color.Tan;
            this.sliderOpacityShadow.SliderRadius = 4F;
            this.sliderOpacityShadow.TickColor = System.Drawing.Color.DarkGray;
            this.sliderOpacityShadow.TickSpacing = 5F;
            this.sliderOpacityShadow.Value = 0;
            this.sliderOpacityShadow.ValueChanged += new System.EventHandler(this.sliderOpacityShadow_ValueChanged);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // colorButtonShadow
            // 
            this.colorButtonShadow.BevelRadius = 4;
            this.colorButtonShadow.Color = System.Drawing.Color.Blue;
            this.colorButtonShadow.LaunchDialogOnClick = true;
            resources.ApplyResources(this.colorButtonShadow, "colorButtonShadow");
            this.colorButtonShadow.Name = "colorButtonShadow";
            this.colorButtonShadow.RoundingRadius = 10;
            this.colorButtonShadow.ColorChanged += new System.EventHandler(this.colorButtonShadow_ColorChanged);
            // 
            // tabMembers
            // 
            this.tabMembers.Controls.Add(this.sqlMembers);
            resources.ApplyResources(this.tabMembers, "tabMembers");
            this.tabMembers.Name = "tabMembers";
            this.tabMembers.UseVisualStyleBackColor = true;
            // 
            // sqlMembers
            // 
            this.sqlMembers.AttributeSource = null;
            resources.ApplyResources(this.sqlMembers, "sqlMembers");
            this.sqlMembers.ExpressionText = "";
            this.sqlMembers.Name = "sqlMembers";
            this.sqlMembers.Table = null;
            this.sqlMembers.ExpressionTextChanged += new System.EventHandler(this.sqlMembers_ExpressionTextChanged);
            // 
            // cmdOK
            // 
            resources.ApplyResources(this.cmdOK, "cmdOK");
            this.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdCancel
            // 
            resources.ApplyResources(this.cmdCancel, "cmdCancel");
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdApply
            // 
            resources.ApplyResources(this.cmdApply, "cmdApply");
            this.cmdApply.Name = "cmdApply";
            this.cmdApply.UseVisualStyleBackColor = true;
            this.cmdApply.Click += new System.EventHandler(this.cmdApply_Click);
            // 
            // lblHelp
            // 
            resources.ApplyResources(this.lblHelp, "lblHelp");
            this.lblHelp.Name = "lblHelp";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdCancel);
            this.panel1.Controls.Add(this.cmdOK);
            this.panel1.Controls.Add(this.cmdApply);
            this.panel1.Controls.Add(this.lblHelp);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // LabelSetup
            // 
            this.AcceptButton = this.cmdOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LabelSetup";
            this.TopMost = true;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabs.ResumeLayout(false);
            this.tabExpression.ResumeLayout(false);
            this.tabBasic.ResumeLayout(false);
            this.tabBasic.PerformLayout();
            this.gpbBorderColor.ResumeLayout(false);
            this.gpbBorderColor.PerformLayout();
            this.gpbFont.ResumeLayout(false);
            this.gpbFont.PerformLayout();
            this.gpbBackgroundColor.ResumeLayout(false);
            this.gpbBackgroundColor.PerformLayout();
            this.tabAdvanced.ResumeLayout(false);
            this.tabAdvanced.PerformLayout();
            this.grpOffset.ResumeLayout(false);
            this.grpOffset.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudYOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudXOffset)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.gpbUseLabelShadow.ResumeLayout(false);
            this.gpbUseLabelShadow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownShadowOffsetY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpDownShadowOffsetX)).EndInit();
            this.tabMembers.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        void chkShadow_CheckedChanged(object sender, EventArgs e)
        {
            _activeCategory.Symbolizer.DropShadowEnabled = chkShadow.Checked;
            gpbUseLabelShadow.Enabled = chkShadow.Checked;
        }

        private void chkHalo_CheckedChanged(object sender, EventArgs e)
        {
            _activeCategory.Symbolizer.HaloEnabled = chkHalo.Checked;
            clrHalo.Enabled = chkHalo.Checked;
        }

        void clrHalo_SelectedItemChanged(object sender, EventArgs e)
        {
            _activeCategory.Symbolizer.HaloColor = clrHalo.Value;
        }

        void chkBorder_CheckedChanged(object sender, EventArgs e)
        {
            _activeCategory.Symbolizer.BorderVisible = chkBorder.Checked;
        }

        void cmbAlignment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_activeCategory == null) return;
            _activeCategory.Symbolizer.Alignment = (StringAlignment)cmbAlignment.SelectedIndex;
        }

        void sqlMembers_ExpressionTextChanged(object sender, EventArgs e)
        {
            _activeCategory.FilterExpression = sqlMembers.ExpressionText;
        }

        void sqlExpression_ExpressionTextChanged(object sender, EventArgs e)
        {
            _activeCategory.Expression = sqlExpression.ExpressionText;
        }

      

       

      
        void fontFamilyControl1_SelectedItemChanged(object sender, EventArgs e)
        {

            UpdateFontStyles();
            UpdatePreview();
            
         
        }

        void UpdateFontStyles()
        {
            FontFamily ff = ffcFamilyName.GetSelectedFamily();
            cmbStyle.Items.Clear();
            for (int i = 0; i < 15; i++)
            {
                FontStyle fs = (FontStyle)i;
                if (ff.IsStyleAvailable(fs))
                {
                    cmbStyle.Items.Add(fs);
                }
            }
            if(cmbStyle.Items.Contains(FontStyle.Regular))
            {
                cmbStyle.SelectedItem = FontStyle.Regular;
            }
            else
            {
                cmbStyle.SelectedItem = cmbStyle.Items[0];
            }
            
            
        }



        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of frmLabelSetup
        /// </summary>
        public LabelSetup()
        {
            InitializeComponent();
            UpdateFontStyles();

            //Populate the label method drop downs
            foreach (object method in Enum.GetValues(typeof(LabelMethod)))
                cmbLabelingMethod.Items.Add(method);
            foreach (object partMethod in Enum.GetValues(typeof(LabelParts)))
                cmbLabelParts.Items.Add(partMethod);

            tabs.Selecting += TabsSelecting;
            lblHelp.Text = "Each symbol group can define a separate string expression that includes attribute values.  Double click fields to add it to the expression.";
            cmbSize.SelectedIndex = 4;
            cmbStyle.SelectedIndex = 0;
            cmbAlignment.SelectedIndex = 0;
            UpdatePreview();
        }

        void TabsSelecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == tabMembers)
            {
                lblHelp.Visible = true;
                lblHelp.Text = "Use the Members tab to design a filter expression that restricts which features will be assigned to a specific label.";
            }
            else if (e.TabPage == tabExpression)
            {
                lblHelp.Visible = true;
                lblHelp.Text = "Use the Expression Tab to design the text that should appear in a label.  Fields indicated with brackets will be substituted with real values.";
            }
            else if (e.TabPage == tabBasic)
            {
                lblHelp.Visible = true;
                lblHelp.Text = "Each group can specify its own basic design characteristics.";
            }
            else if (e.TabPage == tabAdvanced)
            {
                lblHelp.Visible = true;
                lblHelp.Text = "Each group can specify its advanced characteristics.";
            }
            else
            {
                lblHelp.Visible = false;
            }
        }

      

        #endregion

        #region Methods

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the layer to use for defining this dialog
        /// </summary>
        public ILabelLayer Layer
        {
            get { return _layer; }
            set 
            { 
                _layer = value;
                UpdateLayer();
            }
        }

        #endregion

       

        #region Event Handlers

        #endregion

        #region Protected Methods

        /// <summary>
        /// Occurs when the form is closing to prevent it from being disposed.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
            base.OnClosing(e);
        }

        /// <summary>
        /// Fires the ChangesApplied event.
        /// </summary>
        protected virtual void OnChangesApplied()
        {
            if (ChangesApplied != null) ChangesApplied(this, new EventArgs());
        }


        #endregion

        #region Private Functions

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

        #endregion

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            OnChangesApplied();
            Close();
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
           
            OnChangesApplied();
        }

        private void cmbStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        /// <summary>
        /// Updates any content that visually displays the currently selected characteristics.
        /// </summary>
        public void UpdatePreview()
        {
            if (cmbSize.SelectedItem == null) return;
            float size = float.Parse(cmbSize.SelectedItem.ToString());
            FontStyle style = (FontStyle)cmbStyle.SelectedIndex;
            
            try
            {
                lblPreview.Font = new Font(ffcFamilyName.SelectedFamily, size, style);
                if(chkBackgroundColor.Checked)
                {
                    lblPreview.BackColor = cbBackgroundColor.Color;
                }
                else
                {
                    lblPreview.BackColor = SystemColors.Control;
                }
                lblPreview.ForeColor = cbFontColor.Color;
                lblPreview.Text = "Preview";
                //lblPreview.TextAlign = _activeCategory.Symbolizer.Orientation;
                lblPreview.Invalidate();
                ttLabelSetup.SetToolTip(lblPreview, "This shows a preview of the font");
				if (_activeCategory != null)
				{
					_activeCategory.Symbolizer.FontFamily = ffcFamilyName.SelectedFamily;
					_activeCategory.Symbolizer.FontSize = size;
					_activeCategory.Symbolizer.FontStyle = style;
					_activeCategory.Symbolizer.FontColor = cbFontColor.Color;
				}
            }
            catch
            {
                lblPreview.Font = new Font("Arial", 20F, FontStyle.Bold);
                lblPreview.Text = "Unsupported!";
                ttLabelSetup.SetToolTip(lblPreview, "The specified combination of font family, style, or size is unsupported");
            }

        }

        private void cmbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        /// <summary>
        /// When the layer is updated or configured, this updates the data Table aspects if possible.
        /// </summary>
        public void UpdateLayer()
        {
            UpdateCategories();
            lbCategories.SelectedIndex = 0;
            cmbPriorityField.Items.Clear();
            cmbPriorityField.Items.Add("FID");
            if (_layer == null) return;
            if (_layer.FeatureLayer == null) return;
            if (_layer.FeatureLayer.DataSet == null) return;
            if (_layer.FeatureLayer.DataSet.DataTable == null) return;
            sqlExpression.Table = _layer.FeatureLayer.DataSet.DataTable;
            sqlMembers.Table = _layer.FeatureLayer.DataSet.DataTable;
            foreach (DataColumn column in _layer.FeatureLayer.DataSet.DataTable.Columns)
            {
                cmbPriorityField.Items.Add(column.ColumnName);
            }
            UpdateControls();
        }

        private void lbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            _activeCategory = lbCategories.SelectedItem as ILabelCategory;
            UpdateControls();
        }

        private void UpdateControls()
        {
            if (_ignoreUpdates) return;
            _ignoreUpdates = true;
            cmbPriorityField.SelectedItem = _activeCategory.Symbolizer.PriorityField;
            chkPreventCollision.Checked = _activeCategory.Symbolizer.PreventCollisions;
            chkPrioritizeLow.Checked = _activeCategory.Symbolizer.PrioritizeLowValues;
            cbFontColor.Color = _activeCategory.Symbolizer.FontColor;
            cbBackgroundColor.Color = _activeCategory.Symbolizer.BackColor;
            chkBackgroundColor.Checked = _activeCategory.Symbolizer.BackColorEnabled;
            cmbSize.SelectedItem = _activeCategory.Symbolizer.FontSize.ToString();
            cmbAlignment.SelectedIndex = (int)_activeCategory.Symbolizer.Alignment;
            ffcFamilyName.SelectedFamily = _activeCategory.Symbolizer.FontFamily;
            cmbStyle.SelectedIndex = (int)_activeCategory.Symbolizer.FontStyle;
            sqlExpression.ExpressionText = _activeCategory.Expression;
            sqlMembers.ExpressionText = _activeCategory.FilterExpression;
            chkBorder.Checked = _activeCategory.Symbolizer.BorderVisible;
            chkShadow.Checked = _activeCategory.Symbolizer.DropShadowEnabled;
            if (_activeCategory.Symbolizer.Orientation == 0)
                _activeCategory.Symbolizer.Orientation = ContentAlignment.MiddleCenter;
            labelAlignmentControl1.Value = _activeCategory.Symbolizer.Orientation;
            //Shadow color
            sliderOpacityShadow.Value = _activeCategory.Symbolizer.DropShadowColor.GetOpacity();
            colorButtonShadow.Color = _activeCategory.Symbolizer.DropShadowColor;
            decimal offsetX = (decimal) _activeCategory.Symbolizer.DropShadowPixelOffset.X;
            decimal offsetY = (decimal) _activeCategory.Symbolizer.DropShadowPixelOffset.Y;
            UpDownShadowOffsetX.Value = offsetX;
            UpDownShadowOffsetY.Value = offsetY;
            nudYOffset.Value = (decimal) _activeCategory.Symbolizer.OffsetY;
            nudXOffset.Value = (decimal) _activeCategory.Symbolizer.OffsetX;
            clrHalo.Value = _activeCategory.Symbolizer.HaloColor;
            chkHalo.Checked = _activeCategory.Symbolizer.HaloEnabled;
            cmbLabelingMethod.SelectedItem = _activeCategory.Symbolizer.LabelMethod;
            cmbLabelParts.SelectedItem = _activeCategory.Symbolizer.LabelParts;
            cbBorderColor.Color = _activeCategory.Symbolizer.BorderColor;
            _ignoreUpdates = false;
        }

      
        private void btnCategoryUp_Click(object sender, EventArgs e)
        {
            ILabelCategory cat = lbCategories.SelectedItem as ILabelCategory;
            _layer.Symbology.Promote(cat);
            UpdateCategories();
            lbCategories.SelectedItem = cat;
        }

        private void btnCategoryDown_Click(object sender, EventArgs e)
        {
            ILabelCategory cat = lbCategories.SelectedItem as ILabelCategory;
            _layer.Symbology.Demote(cat);
            UpdateCategories();
            lbCategories.SelectedItem = cat;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            lbCategories.Items.Insert(0, _layer.Symbology.AddCategory());
        }

        private void btnSubtract_Click(object sender, EventArgs e)
        {
            ILabelCategory cat = lbCategories.SelectedItem as ILabelCategory;
            if (cat != null)
            {
                _layer.Symbology.Categories.Remove(cat);
            }
        }

        private void UpdateCategories()
        {
            lbCategories.SuspendLayout();
            lbCategories.Items.Clear();
            foreach (ILabelCategory cat in _layer.Symbology.Categories)
            {
                lbCategories.Items.Insert(0, cat);
            }
            lbCategories.ResumeLayout();
        }

        private void chkBackgroundColor_CheckedChanged(object sender, EventArgs e)
        {
            _activeCategory.Symbolizer.BackColorEnabled = chkBackgroundColor.Checked;
            UpdateControls();
         }

        private void cmbLabelingMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            _activeCategory.Symbolizer.LabelMethod = (LabelMethod) cmbLabelingMethod.SelectedItem;
        }

        private void cmbLabelParts_SelectedIndexChanged(object sender, EventArgs e)
        {
            _activeCategory.Symbolizer.LabelParts = (LabelParts) cmbLabelParts.SelectedItem;
        }

        private void colorButtonShadow_ColorChanged(object sender, EventArgs e)
        {
            if (colorButtonShadow.Color != colorButtonShadow.Color.ToTransparent((float)sliderOpacityShadow.Value))
            {
                colorButtonShadow.Color = colorButtonShadow.Color.ToTransparent((float)sliderOpacityShadow.Value);
                _activeCategory.Symbolizer.DropShadowColor = colorButtonShadow.Color;
            }
        }

        private void sliderOpacityShadow_ValueChanged(object sender, EventArgs e)
        {
            colorButtonShadow.Color = colorButtonShadow.Color.ToTransparent((float)sliderOpacityShadow.Value);
            _activeCategory.Symbolizer.DropShadowColor = colorButtonShadow.Color;
        }

        private void UpDownShadowOffsetY_ValueChanged(object sender, EventArgs e)
        {
            _activeCategory.Symbolizer.DropShadowPixelOffset = new PointF((float)UpDownShadowOffsetX.Value, (float)UpDownShadowOffsetY.Value);
        }

        private void UpDownShadowOffsetX_ValueChanged(object sender, EventArgs e)
        {
            _activeCategory.Symbolizer.DropShadowPixelOffset = new PointF((float)UpDownShadowOffsetX.Value, (float)UpDownShadowOffsetY.Value);
        }

        

        private void nudYOffset_ValueChanged(object sender, EventArgs e)
        {
            _activeCategory.Symbolizer.OffsetY = (float) nudYOffset.Value;
        }

        private void nudXOffset_ValueChanged(object sender, EventArgs e)
        {
            _activeCategory.Symbolizer.OffsetX = (float) nudXOffset.Value;
        }

        private void labelAlignmentControl1_ValueChanged(object sender, EventArgs e)
        {
            _activeCategory.Symbolizer.Orientation = labelAlignmentControl1.Value;
        }

      

        private void rampSlider1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void gpbBackgroundColor_Enter(object sender, EventArgs e)
        {

        }

        private void cbBackgroundColor_ColorChanged(object sender, EventArgs e)
        {
            _activeCategory.Symbolizer.BackColor = cbBackgroundColor.Color;
            if (!_ignoreUpdates)
            {
                chkBackgroundColor.Checked = true;
            }
            UpdatePreview();
        }

        private void cbBorderColor_ColorChanged(object sender, EventArgs e)
        {
            _activeCategory.Symbolizer.BorderColor = cbBorderColor.Color;
            if(!_ignoreUpdates)
            {
                _activeCategory.Symbolizer.BorderVisible = true;
                chkBorder.Checked = true;
            }
            UpdatePreview();
        }

        private void cbFontColor_ColorChanged(object sender, EventArgs e)
        {
            _activeCategory.Symbolizer.FontColor = cbFontColor.Color;
            UpdatePreview();
        }

        private void cmbPriorityField_SelectedIndexChanged(object sender, EventArgs e)
        {
            _activeCategory.Symbolizer.PriorityField = (string)cmbPriorityField.SelectedItem;
        }

        private void chkPrioritizeLow_CheckedChanged(object sender, EventArgs e)
        {
            _activeCategory.Symbolizer.PrioritizeLowValues = chkPrioritizeLow.Checked;
        }

        private void chkPreventCollision_CheckedChanged(object sender, EventArgs e)
        {
            _activeCategory.Symbolizer.PreventCollisions = chkPreventCollision.Checked;
        }

       
    }
}
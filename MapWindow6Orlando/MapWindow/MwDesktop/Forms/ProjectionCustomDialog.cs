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
// The Initial Developer of this Original Code is Ted Dunsford. Created 8/19/2009 1:06:14 PM
// 
// Contributor(s): (Open source contributors should list themselves and their modifications here). 
//
//********************************************************************************************************
using System;
using System.Windows.Forms;
using MapWindow.Projections;
namespace MapWindow.Forms
{
    /// <summary>
    /// ProjectionCustomDialog
    /// </summary>
    public class ProjectionCustomDialog : Form
    {
        #region Events

        /// <summary>
        /// Occurs whenever the apply changes button is clicked, or else when the ok button is clicked.
        /// </summary>
        public event EventHandler ChangesApplied;

        #endregion

        private Panel panel1;
        private Button btnApply;
        private Button btnCancel;
        private Button cmdOk;
        private Label label1;
        private ComboBox cmbTransform;
        private Components.DoubleBox dbFalseEasting;
        private Components.DoubleBox dbFalseNorthing;
        private Components.DoubleBox dbCentralMeridian;
        private Components.DoubleBox dbLatitudeOfOrigin;
        private Components.DoubleBox dbScaleFactor;
        private Components.DoubleBox dbLat1;
        private Components.DoubleBox dbLat2;
        private ComboBox comboBox1;
        private Label label2;
        private Components.DoubleBox dbZone;
        private CheckBox chkSouth;
        private GroupBox grpGeographic;
        private Label label3;
        private ComboBox cmbMeridian;
        private Components.DoubleBox dbMeridian;
        private ProjectionInfo _selectedProjectionInfo = new ProjectionInfo();
        private ComboBox cmbDatums;
        private Label label4;
        private ComboBox cmbEllipsoid;
        private Label label5;
        private RadioButton radGridShift;
        private RadioButton rad7;
        private RadioButton rad3;
        private MapWindow.Components.DoubleBox dbA;
        private RadioButton radWGS84;
        private MapWindow.Components.DoubleBox dbB;

        #region Private Variables

        



        #endregion


        #region Windows Form Designer generated code

        /// <summary>
        /// Required designer variable.
        /// </summary>
        protected readonly System.ComponentModel.IContainer components;


        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProjectionCustomDialog));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmdOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbTransform = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkSouth = new System.Windows.Forms.CheckBox();
            this.grpGeographic = new System.Windows.Forms.GroupBox();
            this.dbB = new MapWindow.Components.DoubleBox();
            this.dbA = new MapWindow.Components.DoubleBox();
            this.radWGS84 = new System.Windows.Forms.RadioButton();
            this.radGridShift = new System.Windows.Forms.RadioButton();
            this.rad7 = new System.Windows.Forms.RadioButton();
            this.rad3 = new System.Windows.Forms.RadioButton();
            this.cmbEllipsoid = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbDatums = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dbMeridian = new MapWindow.Components.DoubleBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbMeridian = new System.Windows.Forms.ComboBox();
            this.dbZone = new MapWindow.Components.DoubleBox();
            this.dbLat2 = new MapWindow.Components.DoubleBox();
            this.dbLat1 = new MapWindow.Components.DoubleBox();
            this.dbScaleFactor = new MapWindow.Components.DoubleBox();
            this.dbLatitudeOfOrigin = new MapWindow.Components.DoubleBox();
            this.dbCentralMeridian = new MapWindow.Components.DoubleBox();
            this.dbFalseNorthing = new MapWindow.Components.DoubleBox();
            this.dbFalseEasting = new MapWindow.Components.DoubleBox();
            this.panel1.SuspendLayout();
            this.grpGeographic.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AccessibleDescription = null;
            this.panel1.AccessibleName = null;
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackgroundImage = null;
            this.panel1.Controls.Add(this.btnApply);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.cmdOk);
            this.panel1.Font = null;
            this.panel1.Name = "panel1";
            // 
            // btnApply
            // 
            this.btnApply.AccessibleDescription = null;
            this.btnApply.AccessibleName = null;
            resources.ApplyResources(this.btnApply, "btnApply");
            this.btnApply.BackgroundImage = null;
            this.btnApply.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnApply.Font = null;
            this.btnApply.Name = "btnApply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleDescription = null;
            this.btnCancel.AccessibleName = null;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.BackgroundImage = null;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = null;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cmdOk
            // 
            this.cmdOk.AccessibleDescription = null;
            this.cmdOk.AccessibleName = null;
            resources.ApplyResources(this.cmdOk, "cmdOk");
            this.cmdOk.BackgroundImage = null;
            this.cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOk.Font = null;
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Font = null;
            this.label1.Name = "label1";
            // 
            // cmbTransform
            // 
            this.cmbTransform.AccessibleDescription = null;
            this.cmbTransform.AccessibleName = null;
            resources.ApplyResources(this.cmbTransform, "cmbTransform");
            this.cmbTransform.BackgroundImage = null;
            this.cmbTransform.Font = null;
            this.cmbTransform.FormattingEnabled = true;
            this.cmbTransform.Name = "cmbTransform";
            // 
            // comboBox1
            // 
            this.comboBox1.AccessibleDescription = null;
            this.comboBox1.AccessibleName = null;
            resources.ApplyResources(this.comboBox1, "comboBox1");
            this.comboBox1.BackgroundImage = null;
            this.comboBox1.Font = null;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            resources.GetString("comboBox1.Items"),
            resources.GetString("comboBox1.Items1"),
            resources.GetString("comboBox1.Items2")});
            this.comboBox1.Name = "comboBox1";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = null;
            this.label2.AccessibleName = null;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Font = null;
            this.label2.Name = "label2";
            // 
            // chkSouth
            // 
            this.chkSouth.AccessibleDescription = null;
            this.chkSouth.AccessibleName = null;
            resources.ApplyResources(this.chkSouth, "chkSouth");
            this.chkSouth.BackgroundImage = null;
            this.chkSouth.Font = null;
            this.chkSouth.Name = "chkSouth";
            this.chkSouth.UseVisualStyleBackColor = true;
            // 
            // grpGeographic
            // 
            this.grpGeographic.AccessibleDescription = null;
            this.grpGeographic.AccessibleName = null;
            resources.ApplyResources(this.grpGeographic, "grpGeographic");
            this.grpGeographic.BackgroundImage = null;
            this.grpGeographic.Controls.Add(this.dbB);
            this.grpGeographic.Controls.Add(this.dbA);
            this.grpGeographic.Controls.Add(this.radWGS84);
            this.grpGeographic.Controls.Add(this.radGridShift);
            this.grpGeographic.Controls.Add(this.rad7);
            this.grpGeographic.Controls.Add(this.rad3);
            this.grpGeographic.Controls.Add(this.cmbEllipsoid);
            this.grpGeographic.Controls.Add(this.label5);
            this.grpGeographic.Controls.Add(this.cmbDatums);
            this.grpGeographic.Controls.Add(this.label4);
            this.grpGeographic.Controls.Add(this.dbMeridian);
            this.grpGeographic.Controls.Add(this.label3);
            this.grpGeographic.Controls.Add(this.cmbMeridian);
            this.grpGeographic.Font = null;
            this.grpGeographic.Name = "grpGeographic";
            this.grpGeographic.TabStop = false;
            // 
            // dbB
            // 
            this.dbB.AccessibleDescription = null;
            this.dbB.AccessibleName = null;
            resources.ApplyResources(this.dbB, "dbB");
            this.dbB.BackColorInvalid = System.Drawing.Color.Salmon;
            this.dbB.BackColorRegular = System.Drawing.Color.Empty;
            this.dbB.BackgroundImage = null;
            this.dbB.Caption = "短半径:";
            this.dbB.Font = null;
            this.dbB.InvalidHelp = "The value entered could not be correctly parsed into a valid double precision flo" +
                "ating point value.";
            this.dbB.IsValid = true;
            this.dbB.Name = "dbB";
            this.dbB.NumberFormat = null;
            this.dbB.RegularHelp = "Enter a double precision floating point value.";
            this.dbB.Value = 0;
            // 
            // dbA
            // 
            this.dbA.AccessibleDescription = null;
            this.dbA.AccessibleName = null;
            resources.ApplyResources(this.dbA, "dbA");
            this.dbA.BackColorInvalid = System.Drawing.Color.Salmon;
            this.dbA.BackColorRegular = System.Drawing.Color.Empty;
            this.dbA.BackgroundImage = null;
            this.dbA.Caption = "长半径:";
            this.dbA.Font = null;
            this.dbA.InvalidHelp = "The value entered could not be correctly parsed into a valid double precision flo" +
                "ating point value.";
            this.dbA.IsValid = true;
            this.dbA.Name = "dbA";
            this.dbA.NumberFormat = null;
            this.dbA.RegularHelp = "Enter a double precision floating point value.";
            this.dbA.Value = 0;
            // 
            // radWGS84
            // 
            this.radWGS84.AccessibleDescription = null;
            this.radWGS84.AccessibleName = null;
            resources.ApplyResources(this.radWGS84, "radWGS84");
            this.radWGS84.BackgroundImage = null;
            this.radWGS84.Font = null;
            this.radWGS84.Name = "radWGS84";
            this.radWGS84.TabStop = true;
            this.radWGS84.UseVisualStyleBackColor = true;
            // 
            // radGridShift
            // 
            this.radGridShift.AccessibleDescription = null;
            this.radGridShift.AccessibleName = null;
            resources.ApplyResources(this.radGridShift, "radGridShift");
            this.radGridShift.BackgroundImage = null;
            this.radGridShift.Font = null;
            this.radGridShift.Name = "radGridShift";
            this.radGridShift.TabStop = true;
            this.radGridShift.UseVisualStyleBackColor = true;
            // 
            // rad7
            // 
            this.rad7.AccessibleDescription = null;
            this.rad7.AccessibleName = null;
            resources.ApplyResources(this.rad7, "rad7");
            this.rad7.BackgroundImage = null;
            this.rad7.Font = null;
            this.rad7.Name = "rad7";
            this.rad7.TabStop = true;
            this.rad7.UseVisualStyleBackColor = true;
            // 
            // rad3
            // 
            this.rad3.AccessibleDescription = null;
            this.rad3.AccessibleName = null;
            resources.ApplyResources(this.rad3, "rad3");
            this.rad3.BackgroundImage = null;
            this.rad3.Font = null;
            this.rad3.Name = "rad3";
            this.rad3.TabStop = true;
            this.rad3.UseVisualStyleBackColor = true;
            // 
            // cmbEllipsoid
            // 
            this.cmbEllipsoid.AccessibleDescription = null;
            this.cmbEllipsoid.AccessibleName = null;
            resources.ApplyResources(this.cmbEllipsoid, "cmbEllipsoid");
            this.cmbEllipsoid.BackgroundImage = null;
            this.cmbEllipsoid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEllipsoid.Font = null;
            this.cmbEllipsoid.FormattingEnabled = true;
            this.cmbEllipsoid.Name = "cmbEllipsoid";
            this.cmbEllipsoid.SelectedIndexChanged += new System.EventHandler(this.cmbEllipsoid_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AccessibleDescription = null;
            this.label5.AccessibleName = null;
            resources.ApplyResources(this.label5, "label5");
            this.label5.Font = null;
            this.label5.Name = "label5";
            // 
            // cmbDatums
            // 
            this.cmbDatums.AccessibleDescription = null;
            this.cmbDatums.AccessibleName = null;
            resources.ApplyResources(this.cmbDatums, "cmbDatums");
            this.cmbDatums.BackgroundImage = null;
            this.cmbDatums.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDatums.Font = null;
            this.cmbDatums.FormattingEnabled = true;
            this.cmbDatums.Name = "cmbDatums";
            this.cmbDatums.SelectedIndexChanged += new System.EventHandler(this.cmbDatums_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AccessibleDescription = null;
            this.label4.AccessibleName = null;
            resources.ApplyResources(this.label4, "label4");
            this.label4.Font = null;
            this.label4.Name = "label4";
            // 
            // dbMeridian
            // 
            this.dbMeridian.AccessibleDescription = null;
            this.dbMeridian.AccessibleName = null;
            resources.ApplyResources(this.dbMeridian, "dbMeridian");
            this.dbMeridian.BackColorInvalid = System.Drawing.Color.Salmon;
            this.dbMeridian.BackColorRegular = System.Drawing.Color.Empty;
            this.dbMeridian.BackgroundImage = null;
            this.dbMeridian.Caption = "指定经度:";
            this.dbMeridian.Font = null;
            this.dbMeridian.InvalidHelp = "The value entered could not be correctly parsed into a valid double precision flo" +
                "ating point value.";
            this.dbMeridian.IsValid = true;
            this.dbMeridian.Name = "dbMeridian";
            this.dbMeridian.NumberFormat = null;
            this.dbMeridian.RegularHelp = "Enter a double precision floating point value.";
            this.dbMeridian.Value = 0;
            // 
            // label3
            // 
            this.label3.AccessibleDescription = null;
            this.label3.AccessibleName = null;
            resources.ApplyResources(this.label3, "label3");
            this.label3.Font = null;
            this.label3.Name = "label3";
            // 
            // cmbMeridian
            // 
            this.cmbMeridian.AccessibleDescription = null;
            this.cmbMeridian.AccessibleName = null;
            resources.ApplyResources(this.cmbMeridian, "cmbMeridian");
            this.cmbMeridian.BackgroundImage = null;
            this.cmbMeridian.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMeridian.Font = null;
            this.cmbMeridian.FormattingEnabled = true;
            this.cmbMeridian.Name = "cmbMeridian";
            this.cmbMeridian.SelectedIndexChanged += new System.EventHandler(this.cmbMeridian_SelectedIndexChanged);
            // 
            // dbZone
            // 
            this.dbZone.AccessibleDescription = null;
            this.dbZone.AccessibleName = null;
            resources.ApplyResources(this.dbZone, "dbZone");
            this.dbZone.BackColorInvalid = System.Drawing.Color.Salmon;
            this.dbZone.BackColorRegular = System.Drawing.Color.Empty;
            this.dbZone.BackgroundImage = null;
            this.dbZone.Caption = "带:";
            this.dbZone.Font = null;
            this.dbZone.InvalidHelp = "The value entered could not be correctly parsed into a valid double precision flo" +
                "ating point value.";
            this.dbZone.IsValid = true;
            this.dbZone.Name = "dbZone";
            this.dbZone.NumberFormat = null;
            this.dbZone.RegularHelp = "Enter a double precision floating point value.";
            this.dbZone.Value = 0;
            // 
            // dbLat2
            // 
            this.dbLat2.AccessibleDescription = null;
            this.dbLat2.AccessibleName = null;
            resources.ApplyResources(this.dbLat2, "dbLat2");
            this.dbLat2.BackColorInvalid = System.Drawing.Color.Salmon;
            this.dbLat2.BackColorRegular = System.Drawing.Color.Empty;
            this.dbLat2.BackgroundImage = null;
            this.dbLat2.Caption = "标准纬线 2:";
            this.dbLat2.Font = null;
            this.dbLat2.InvalidHelp = "The value entered could not be correctly parsed into a valid double precision flo" +
                "ating point value.";
            this.dbLat2.IsValid = true;
            this.dbLat2.Name = "dbLat2";
            this.dbLat2.NumberFormat = null;
            this.dbLat2.RegularHelp = "Enter a double precision floating point value.";
            this.dbLat2.Value = 0;
            // 
            // dbLat1
            // 
            this.dbLat1.AccessibleDescription = null;
            this.dbLat1.AccessibleName = null;
            resources.ApplyResources(this.dbLat1, "dbLat1");
            this.dbLat1.BackColorInvalid = System.Drawing.Color.Salmon;
            this.dbLat1.BackColorRegular = System.Drawing.Color.Empty;
            this.dbLat1.BackgroundImage = null;
            this.dbLat1.Caption = "标准纬线 1:";
            this.dbLat1.Font = null;
            this.dbLat1.InvalidHelp = "The value entered could not be correctly parsed into a valid double precision flo" +
                "ating point value.";
            this.dbLat1.IsValid = true;
            this.dbLat1.Name = "dbLat1";
            this.dbLat1.NumberFormat = null;
            this.dbLat1.RegularHelp = "Enter a double precision floating point value.";
            this.dbLat1.Value = 0;
            // 
            // dbScaleFactor
            // 
            this.dbScaleFactor.AccessibleDescription = null;
            this.dbScaleFactor.AccessibleName = null;
            resources.ApplyResources(this.dbScaleFactor, "dbScaleFactor");
            this.dbScaleFactor.BackColorInvalid = System.Drawing.Color.Salmon;
            this.dbScaleFactor.BackColorRegular = System.Drawing.Color.Empty;
            this.dbScaleFactor.BackgroundImage = null;
            this.dbScaleFactor.Caption = "比例因子:";
            this.dbScaleFactor.Font = null;
            this.dbScaleFactor.InvalidHelp = "The value entered could not be correctly parsed into a valid double precision flo" +
                "ating point value.";
            this.dbScaleFactor.IsValid = true;
            this.dbScaleFactor.Name = "dbScaleFactor";
            this.dbScaleFactor.NumberFormat = null;
            this.dbScaleFactor.RegularHelp = "Enter a double precision floating point value.";
            this.dbScaleFactor.Value = 0;
            // 
            // dbLatitudeOfOrigin
            // 
            this.dbLatitudeOfOrigin.AccessibleDescription = null;
            this.dbLatitudeOfOrigin.AccessibleName = null;
            resources.ApplyResources(this.dbLatitudeOfOrigin, "dbLatitudeOfOrigin");
            this.dbLatitudeOfOrigin.BackColorInvalid = System.Drawing.Color.Salmon;
            this.dbLatitudeOfOrigin.BackColorRegular = System.Drawing.Color.Empty;
            this.dbLatitudeOfOrigin.BackgroundImage = null;
            this.dbLatitudeOfOrigin.Caption = "初始纬度:";
            this.dbLatitudeOfOrigin.Font = null;
            this.dbLatitudeOfOrigin.InvalidHelp = "The value entered could not be correctly parsed into a valid double precision flo" +
                "ating point value.";
            this.dbLatitudeOfOrigin.IsValid = true;
            this.dbLatitudeOfOrigin.Name = "dbLatitudeOfOrigin";
            this.dbLatitudeOfOrigin.NumberFormat = null;
            this.dbLatitudeOfOrigin.RegularHelp = "Enter a double precision floating point value.";
            this.dbLatitudeOfOrigin.Value = 0;
            // 
            // dbCentralMeridian
            // 
            this.dbCentralMeridian.AccessibleDescription = null;
            this.dbCentralMeridian.AccessibleName = null;
            resources.ApplyResources(this.dbCentralMeridian, "dbCentralMeridian");
            this.dbCentralMeridian.BackColorInvalid = System.Drawing.Color.Salmon;
            this.dbCentralMeridian.BackColorRegular = System.Drawing.Color.Empty;
            this.dbCentralMeridian.BackgroundImage = null;
            this.dbCentralMeridian.Caption = "中央子午线:";
            this.dbCentralMeridian.Font = null;
            this.dbCentralMeridian.InvalidHelp = "The value entered could not be correctly parsed into a valid double precision flo" +
                "ating point value.";
            this.dbCentralMeridian.IsValid = true;
            this.dbCentralMeridian.Name = "dbCentralMeridian";
            this.dbCentralMeridian.NumberFormat = null;
            this.dbCentralMeridian.RegularHelp = "Enter a double precision floating point value.";
            this.dbCentralMeridian.Value = 0;
            // 
            // dbFalseNorthing
            // 
            this.dbFalseNorthing.AccessibleDescription = null;
            this.dbFalseNorthing.AccessibleName = null;
            resources.ApplyResources(this.dbFalseNorthing, "dbFalseNorthing");
            this.dbFalseNorthing.BackColorInvalid = System.Drawing.Color.Salmon;
            this.dbFalseNorthing.BackColorRegular = System.Drawing.Color.Empty;
            this.dbFalseNorthing.BackgroundImage = null;
            this.dbFalseNorthing.Caption = "北偏:";
            this.dbFalseNorthing.Font = null;
            this.dbFalseNorthing.InvalidHelp = "The value entered could not be correctly parsed into a valid double precision flo" +
                "ating point value.";
            this.dbFalseNorthing.IsValid = true;
            this.dbFalseNorthing.Name = "dbFalseNorthing";
            this.dbFalseNorthing.NumberFormat = null;
            this.dbFalseNorthing.RegularHelp = "Enter a double precision floating point value.";
            this.dbFalseNorthing.Value = 0;
            // 
            // dbFalseEasting
            // 
            this.dbFalseEasting.AccessibleDescription = null;
            this.dbFalseEasting.AccessibleName = null;
            resources.ApplyResources(this.dbFalseEasting, "dbFalseEasting");
            this.dbFalseEasting.BackColorInvalid = System.Drawing.Color.Salmon;
            this.dbFalseEasting.BackColorRegular = System.Drawing.Color.Empty;
            this.dbFalseEasting.BackgroundImage = null;
            this.dbFalseEasting.Caption = "东偏:";
            this.dbFalseEasting.Font = null;
            this.dbFalseEasting.InvalidHelp = "The value entered could not be correctly parsed into a valid double precision flo" +
                "ating point value.";
            this.dbFalseEasting.IsValid = true;
            this.dbFalseEasting.Name = "dbFalseEasting";
            this.dbFalseEasting.NumberFormat = null;
            this.dbFalseEasting.RegularHelp = "Enter a double precision floating point value.";
            this.dbFalseEasting.Value = 0;
            // 
            // ProjectionCustomDialog
            // 
            this.AcceptButton = this.cmdOk;
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.grpGeographic);
            this.Controls.Add(this.chkSouth);
            this.Controls.Add(this.dbZone);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.dbLat2);
            this.Controls.Add(this.dbLat1);
            this.Controls.Add(this.dbScaleFactor);
            this.Controls.Add(this.dbLatitudeOfOrigin);
            this.Controls.Add(this.dbCentralMeridian);
            this.Controls.Add(this.dbFalseNorthing);
            this.Controls.Add(this.dbFalseEasting);
            this.Controls.Add(this.cmbTransform);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.Icon = null;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProjectionCustomDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.ProjectionCustomDialog_Load);
            this.panel1.ResumeLayout(false);
            this.grpGeographic.ResumeLayout(false);
            this.grpGeographic.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of CollectionPropertyGrid
        /// </summary>
        public ProjectionCustomDialog()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        #endregion

        #region Properties

        /// <summary>
        /// gets or sets the selected projection info
        /// </summary>
        public ProjectionInfo SelectedProjectionInfo
        {
            get { return _selectedProjectionInfo; }
            set { _selectedProjectionInfo = value; }
        }

        #endregion

        #region Events

        #endregion

        #region Event Handlers

        private void btnApply_Click(object sender, EventArgs e)
        {
            OnApplyChanges();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void cmdOk_Click(object sender, EventArgs e)
        {
            OnApplyChanges();
            Close();
        }



        #endregion

        #region Protected Methods

        
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
        /// Fires the ChangesApplied event
        /// </summary>
        protected virtual void OnApplyChanges()
        {
            if (ChangesApplied != null) ChangesApplied(this, new EventArgs());
        }

        #endregion

        private void ProjectionCustomDialog_Load(object sender, EventArgs e)
        {
            foreach(ITransform transform in TransformManager.DefaultTransformManager.Transforms)
            {
                cmbTransform.Items.Add(transform.Name);
            }
            cmbTransform.SelectedIndex = 0;
            
            string[] meridians = Enum.GetNames(typeof (Proj4Meridians));
            foreach (string meridian in meridians)
            {
                cmbMeridian.Items.Add(meridian);
            }
            cmbMeridian.SelectedIndex = 0;
            
            string[] datums = Enum.GetNames(typeof (Proj4Datums));
            foreach (string datum in datums)
            {
                cmbDatums.Items.Add(datum);
            }
            cmbDatums.SelectedIndex = 0;

            string[] ellipsoids = Enum.GetNames(typeof (Proj4Ellipsoids));
            foreach (string ellipsoid in ellipsoids)
            {
                cmbEllipsoid.Items.Add(ellipsoid);
            }
            cmbEllipsoid.SelectedIndex = 0;

        }

        private void cmbDatums_SelectedIndexChanged(object sender, EventArgs e)
        {
            Datum d = new Datum((string)cmbDatums.SelectedItem);
            _selectedProjectionInfo.GeographicInfo.Datum = d;
            switch(d.DatumType)
            {
                case DatumTypes.GridShift:
                    radGridShift.Checked = true;
                    break;
                case DatumTypes.Param3:
                    rad3.Checked = true;
                    break;
                case DatumTypes.Param7:
                    rad7.Checked = true;
                    break;
                case DatumTypes.WGS84:
                    radWGS84.Checked = true;
                    break;
            }
            cmbEllipsoid.SelectedItem = d.Spheroid.Name;
        }

        private void cmbEllipsoid_SelectedIndexChanged(object sender, EventArgs e)
        {
            Proj4Ellipsoids ell = (Proj4Ellipsoids)Enum.Parse(typeof (Proj4Ellipsoids), (string) cmbEllipsoid.SelectedItem);
            Spheroid sph = new Spheroid(ell);
            _selectedProjectionInfo.GeographicInfo.Datum.Spheroid = sph;
            dbA.Value = sph.EquatorialRadius;
            dbB.Value = sph.PolarRadius;
        }

        private void cmbMeridian_SelectedIndexChanged(object sender, EventArgs e)
        {
            Proj4Meridians mer = (Proj4Meridians) Enum.Parse(typeof (Proj4Meridians), (string) cmbMeridian.SelectedItem);
            Meridian m = new Meridian(mer);
            _selectedProjectionInfo.GeographicInfo.Meridian = m;
            dbMeridian.Value = m.Longitude;
        }

 
    }
}
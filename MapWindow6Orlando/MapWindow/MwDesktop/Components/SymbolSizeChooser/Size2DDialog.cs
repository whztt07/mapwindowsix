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
// The Initial Developer of this Original Code is Ted Dunsford. Created 5/14/2009 1:57:07 PM
// 
// Contributor(s): (Open source contributors should list themselves and their modifications here). 
//
//********************************************************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using MapWindow.Data;
using MapWindow.Drawing;
using MapWindow.Components;

namespace MapWindow.Components
{
    /// <summary>
    /// Size2DDialog
    /// </summary>
    public class Size2DDialog : Form
    {
        #region Events

        /// <summary>
        /// Occurs whenever the apply changes button is clicked, or else when the ok button is clicked.
        /// </summary>
        public event EventHandler ChangesApplied;

        #endregion

        #region Private Variables

        private Panel panel1;
        private Button btnApply;
        private Button btnCancel;
        private Button cmdOk;
        private DoubleBox dbxWidth;
        private DoubleBox dbxHeight;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private ISymbol _original;
        private Size2D _editValue;

        #endregion


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Size2DDialog));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmdOk = new System.Windows.Forms.Button();
            this.dbxWidth = new MapWindow.Components.DoubleBox();
            this.dbxHeight = new MapWindow.Components.DoubleBox();
            this.panel1.SuspendLayout();
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
            // dbxWidth
            // 
            this.dbxWidth.AccessibleDescription = null;
            this.dbxWidth.AccessibleName = null;
            resources.ApplyResources(this.dbxWidth, "dbxWidth");
            this.dbxWidth.BackColorInvalid = System.Drawing.Color.Salmon;
            this.dbxWidth.BackColorRegular = System.Drawing.Color.Empty;
            this.dbxWidth.BackgroundImage = null;
            this.dbxWidth.Caption = "宽度:";
            this.dbxWidth.Font = null;
            this.dbxWidth.InvalidHelp = "The value entered could not be correctly parsed into a valid double precision flo" +
                "ating point value.";
            this.dbxWidth.IsValid = true;
            this.dbxWidth.Name = "dbxWidth";
            this.dbxWidth.NumberFormat = null;
            this.dbxWidth.RegularHelp = "Enter a double precision floating point value.";
            this.dbxWidth.Value = 0;
            this.dbxWidth.TextChanged += new System.EventHandler(this.dbxWidth_TextChanged);
            // 
            // dbxHeight
            // 
            this.dbxHeight.AccessibleDescription = null;
            this.dbxHeight.AccessibleName = null;
            resources.ApplyResources(this.dbxHeight, "dbxHeight");
            this.dbxHeight.BackColorInvalid = System.Drawing.Color.Salmon;
            this.dbxHeight.BackColorRegular = System.Drawing.Color.Empty;
            this.dbxHeight.BackgroundImage = null;
            this.dbxHeight.Caption = "高度:";
            this.dbxHeight.Font = null;
            this.dbxHeight.InvalidHelp = "The value entered could not be correctly parsed into a valid double precision flo" +
                "ating point value.";
            this.dbxHeight.IsValid = true;
            this.dbxHeight.Name = "dbxHeight";
            this.dbxHeight.NumberFormat = null;
            this.dbxHeight.RegularHelp = "Enter a double precision floating point value.";
            this.dbxHeight.Value = 0;
            this.dbxHeight.TextChanged += new System.EventHandler(this.dbxHeight_TextChanged);
            // 
            // Size2DDialog
            // 
            this.AcceptButton = this.cmdOk;
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.dbxHeight);
            this.Controls.Add(this.dbxWidth);
            this.Controls.Add(this.panel1);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.Icon = null;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Size2DDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of CollectionPropertyGrid
        /// </summary>
        public Size2DDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates a Size2DDialog with a specific symbol to edit when "Apply Changes" is clicked
        /// </summary>
        /// <param name="symbol"></param>
        public Size2DDialog(ISymbol symbol)
        {
            _original = symbol;
            _editValue = _original.Size.Copy();
            dbxHeight.Value = _editValue.Height;
            dbxWidth.Value = _editValue.Width;
        }

        #endregion

        #region Methods

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the symbol that should be edited whenever apply changes is clicked.
        /// </summary>
        public ISymbol Symbol
        {
            get { return _original; }
            set 
            {
                _original = value;
                _editValue = _original.Size.Copy();
                dbxHeight.Value = _editValue.Height;
                dbxWidth.Value = _editValue.Width;
            }
        }

        #endregion

        #region Events

        #endregion

        #region Event Handlers


        private void dbxWidth_TextChanged(object sender, EventArgs e)
        {
            _editValue.Width = dbxWidth.Value;
        }

        private void dbxHeight_TextChanged(object sender, EventArgs e)
        {
            _editValue.Height = dbxHeight.Value;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            _original.Size = _editValue.Copy();
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








    }
}
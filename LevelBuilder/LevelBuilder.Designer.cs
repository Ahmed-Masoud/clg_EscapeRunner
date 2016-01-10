using System;

namespace LevelBuilder
{
    partial class LevelBuilder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LevelBuilder));
            this.pnlDesign = new System.Windows.Forms.Panel();
            this.tctrlDesign = new System.Windows.Forms.TabControl();
            this.tpgDesign = new System.Windows.Forms.TabPage();
            this.pbMap = new System.Windows.Forms.PictureBox();
            this.tpgCode = new System.Windows.Forms.TabPage();
            this.tbCode = new System.Windows.Forms.TextBox();
            this.pnlProperties = new System.Windows.Forms.Panel();
            this.gbCode = new System.Windows.Forms.GroupBox();
            this.rbXML = new System.Windows.Forms.RadioButton();
            this.rbCS = new System.Windows.Forms.RadioButton();
            this.rbCPP = new System.Windows.Forms.RadioButton();
            this.gbTileProperties = new System.Windows.Forms.GroupBox();
            this.btnUpdateTile = new System.Windows.Forms.Button();
            this.cbTileWalkable = new System.Windows.Forms.CheckBox();
            this.tbTileName = new System.Windows.Forms.TextBox();
            this.lblTileIDValue = new System.Windows.Forms.Label();
            this.lblTileName = new System.Windows.Forms.Label();
            this.lblTileID = new System.Windows.Forms.Label();
            this.pbSelectedTile = new System.Windows.Forms.PictureBox();
            this.gbMapProperties = new System.Windows.Forms.GroupBox();
            this.tbMapName = new System.Windows.Forms.TextBox();
            this.lblMapName = new System.Windows.Forms.Label();
            this.nudTileHeight = new System.Windows.Forms.NumericUpDown();
            this.nudTileWidth = new System.Windows.Forms.NumericUpDown();
            this.lblTileHeight = new System.Windows.Forms.Label();
            this.lblTileWidth = new System.Windows.Forms.Label();
            this.btnUpdateMap = new System.Windows.Forms.Button();
            this.nudMapHeight = new System.Windows.Forms.NumericUpDown();
            this.nudMapWidth = new System.Windows.Forms.NumericUpDown();
            this.lblMapHeight = new System.Windows.Forms.Label();
            this.lblMapWidth = new System.Windows.Forms.Label();
            this.pbMapSmall = new System.Windows.Forms.PictureBox();
            this.lblMouse = new System.Windows.Forms.Label();
            this.lblMapCoordinate = new System.Windows.Forms.Label();
            this.menuStripDLMapEditor = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.loadMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.saveMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMapAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitDLMapEditorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.designViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.codeViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.gridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showWalkableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deselectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddTilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteSelectedTileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearTilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.codeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectedLanguageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cArrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cArrayToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.actionScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialogLoadTiles = new System.Windows.Forms.FolderBrowserDialog();
            this.saveMapDialog = new System.Windows.Forms.SaveFileDialog();
            this.openMapDialog = new System.Windows.Forms.OpenFileDialog();
            this.gbTiles = new System.Windows.Forms.GroupBox();
            this.pnlTileLibrary = new System.Windows.Forms.Panel();
            this.pnlTiles = new System.Windows.Forms.Panel();
            this.gbTools = new System.Windows.Forms.GroupBox();
            this.btnToolFill = new System.Windows.Forms.Button();
            this.btnToolSelection = new System.Windows.Forms.Button();
            this.btnToolEraser = new System.Windows.Forms.Button();
            this.btnToolSelectTile = new System.Windows.Forms.Button();
            this.btnToolBrush = new System.Windows.Forms.Button();
            this.gbMapSmall = new System.Windows.Forms.GroupBox();
            this.contextMenuStripTile = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemWalkable = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlDesign.SuspendLayout();
            this.tctrlDesign.SuspendLayout();
            this.tpgDesign.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMap)).BeginInit();
            this.tpgCode.SuspendLayout();
            this.pnlProperties.SuspendLayout();
            this.gbCode.SuspendLayout();
            this.gbTileProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSelectedTile)).BeginInit();
            this.gbMapProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTileHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTileWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMapHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMapWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMapSmall)).BeginInit();
            this.menuStripDLMapEditor.SuspendLayout();
            this.gbTiles.SuspendLayout();
            this.pnlTiles.SuspendLayout();
            this.gbTools.SuspendLayout();
            this.gbMapSmall.SuspendLayout();
            this.contextMenuStripTile.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlDesign
            // 
            this.pnlDesign.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlDesign.Controls.Add(this.tctrlDesign);
            this.pnlDesign.Location = new System.Drawing.Point(0, 27);
            this.pnlDesign.Name = "pnlDesign";
            this.pnlDesign.Size = new System.Drawing.Size(838, 595);
            this.pnlDesign.TabIndex = 0;
            // 
            // tctrlDesign
            // 
            this.tctrlDesign.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tctrlDesign.Controls.Add(this.tpgDesign);
            this.tctrlDesign.Controls.Add(this.tpgCode);
            this.tctrlDesign.Location = new System.Drawing.Point(3, 3);
            this.tctrlDesign.Multiline = true;
            this.tctrlDesign.Name = "tctrlDesign";
            this.tctrlDesign.SelectedIndex = 0;
            this.tctrlDesign.Size = new System.Drawing.Size(832, 589);
            this.tctrlDesign.TabIndex = 1;
            this.tctrlDesign.SelectedIndexChanged += new System.EventHandler(this.tctrlDesign_SelectedIndexChanged);
            // 
            // tpgDesign
            // 
            this.tpgDesign.AutoScroll = true;
            this.tpgDesign.Controls.Add(this.pbMap);
            this.tpgDesign.Cursor = System.Windows.Forms.Cursors.No;
            this.tpgDesign.Location = new System.Drawing.Point(4, 22);
            this.tpgDesign.Name = "tpgDesign";
            this.tpgDesign.Padding = new System.Windows.Forms.Padding(3);
            this.tpgDesign.Size = new System.Drawing.Size(824, 563);
            this.tpgDesign.TabIndex = 0;
            this.tpgDesign.Text = "Designer";
            this.tpgDesign.UseVisualStyleBackColor = true;
            // 
            // pbMap
            // 
            this.pbMap.BackColor = System.Drawing.Color.LightGray;
            this.pbMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbMap.Cursor = System.Windows.Forms.Cursors.Default;
            this.pbMap.Location = new System.Drawing.Point(4, 4);
            this.pbMap.Name = "pbMap";
            this.pbMap.Size = new System.Drawing.Size(50, 50);
            this.pbMap.TabIndex = 0;
            this.pbMap.TabStop = false;
            // 
            // tpgCode
            // 
            this.tpgCode.Controls.Add(this.tbCode);
            this.tpgCode.Location = new System.Drawing.Point(4, 22);
            this.tpgCode.Name = "tpgCode";
            this.tpgCode.Padding = new System.Windows.Forms.Padding(3);
            this.tpgCode.Size = new System.Drawing.Size(824, 563);
            this.tpgCode.TabIndex = 1;
            this.tpgCode.Text = "Code";
            this.tpgCode.UseVisualStyleBackColor = true;
            // 
            // tbCode
            // 
            this.tbCode.CausesValidation = false;
            this.tbCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCode.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCode.Location = new System.Drawing.Point(3, 3);
            this.tbCode.Multiline = true;
            this.tbCode.Name = "tbCode";
            this.tbCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbCode.Size = new System.Drawing.Size(818, 557);
            this.tbCode.TabIndex = 0;
            // 
            // pnlProperties
            // 
            this.pnlProperties.AutoSize = true;
            this.pnlProperties.Controls.Add(this.gbCode);
            this.pnlProperties.Controls.Add(this.gbTileProperties);
            this.pnlProperties.Controls.Add(this.gbMapProperties);
            this.pnlProperties.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlProperties.Location = new System.Drawing.Point(0, 613);
            this.pnlProperties.Name = "pnlProperties";
            this.pnlProperties.Size = new System.Drawing.Size(1016, 121);
            this.pnlProperties.TabIndex = 1;
            // 
            // gbCode
            // 
            this.gbCode.Controls.Add(this.rbXML);
            this.gbCode.Controls.Add(this.rbCS);
            this.gbCode.Controls.Add(this.rbCPP);
            this.gbCode.Dock = System.Windows.Forms.DockStyle.Right;
            this.gbCode.Location = new System.Drawing.Point(838, 0);
            this.gbCode.Name = "gbCode";
            this.gbCode.Size = new System.Drawing.Size(178, 121);
            this.gbCode.TabIndex = 5;
            this.gbCode.TabStop = false;
            this.gbCode.Text = "Code";
            // 
            // rbXML
            // 
            this.rbXML.AutoSize = true;
            this.rbXML.Location = new System.Drawing.Point(6, 64);
            this.rbXML.Name = "rbXML";
            this.rbXML.Size = new System.Drawing.Size(47, 17);
            this.rbXML.TabIndex = 12;
            this.rbXML.Text = "XML";
            this.rbXML.UseVisualStyleBackColor = true;
            this.rbXML.CheckedChanged += new System.EventHandler(this.rbXML_CheckedChanged);
            // 
            // rbCS
            // 
            this.rbCS.AutoSize = true;
            this.rbCS.Location = new System.Drawing.Point(6, 41);
            this.rbCS.Name = "rbCS";
            this.rbCS.Size = new System.Drawing.Size(39, 17);
            this.rbCS.TabIndex = 11;
            this.rbCS.Text = "C#";
            this.rbCS.UseVisualStyleBackColor = true;
            this.rbCS.CheckedChanged += new System.EventHandler(this.rbCS_CheckedChanged);
            // 
            // rbCPP
            // 
            this.rbCPP.AutoSize = true;
            this.rbCPP.Checked = true;
            this.rbCPP.Location = new System.Drawing.Point(6, 19);
            this.rbCPP.Name = "rbCPP";
            this.rbCPP.Size = new System.Drawing.Size(44, 17);
            this.rbCPP.TabIndex = 10;
            this.rbCPP.TabStop = true;
            this.rbCPP.Text = "C++";
            this.rbCPP.UseVisualStyleBackColor = true;
            this.rbCPP.CheckedChanged += new System.EventHandler(this.rbCPP_CheckedChanged);
            // 
            // gbTileProperties
            // 
            this.gbTileProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbTileProperties.AutoSize = true;
            this.gbTileProperties.Controls.Add(this.btnUpdateTile);
            this.gbTileProperties.Controls.Add(this.cbTileWalkable);
            this.gbTileProperties.Controls.Add(this.tbTileName);
            this.gbTileProperties.Controls.Add(this.lblTileIDValue);
            this.gbTileProperties.Controls.Add(this.lblTileName);
            this.gbTileProperties.Controls.Add(this.lblTileID);
            this.gbTileProperties.Controls.Add(this.pbSelectedTile);
            this.gbTileProperties.Location = new System.Drawing.Point(443, 0);
            this.gbTileProperties.Name = "gbTileProperties";
            this.gbTileProperties.Size = new System.Drawing.Size(389, 121);
            this.gbTileProperties.TabIndex = 4;
            this.gbTileProperties.TabStop = false;
            this.gbTileProperties.Text = "Tile Properties";
            // 
            // btnUpdateTile
            // 
            this.btnUpdateTile.Location = new System.Drawing.Point(308, 69);
            this.btnUpdateTile.Name = "btnUpdateTile";
            this.btnUpdateTile.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateTile.TabIndex = 9;
            this.btnUpdateTile.Text = "Update Tile";
            this.btnUpdateTile.UseVisualStyleBackColor = true;
            this.btnUpdateTile.Click += new System.EventHandler(this.btnUpdateTile_Click);
            // 
            // cbTileWalkable
            // 
            this.cbTileWalkable.AutoSize = true;
            this.cbTileWalkable.Location = new System.Drawing.Point(308, 41);
            this.cbTileWalkable.Name = "cbTileWalkable";
            this.cbTileWalkable.Size = new System.Drawing.Size(71, 17);
            this.cbTileWalkable.TabIndex = 8;
            this.cbTileWalkable.Text = "Walkable";
            this.cbTileWalkable.UseVisualStyleBackColor = true;
            // 
            // tbTileName
            // 
            this.tbTileName.Location = new System.Drawing.Point(123, 39);
            this.tbTileName.Name = "tbTileName";
            this.tbTileName.Size = new System.Drawing.Size(100, 20);
            this.tbTileName.TabIndex = 7;
            // 
            // lblTileIDValue
            // 
            this.lblTileIDValue.AutoSize = true;
            this.lblTileIDValue.Location = new System.Drawing.Point(120, 19);
            this.lblTileIDValue.Name = "lblTileIDValue";
            this.lblTileIDValue.Size = new System.Drawing.Size(0, 13);
            this.lblTileIDValue.TabIndex = 7;
            // 
            // lblTileName
            // 
            this.lblTileName.AutoSize = true;
            this.lblTileName.Location = new System.Drawing.Point(62, 42);
            this.lblTileName.Name = "lblTileName";
            this.lblTileName.Size = new System.Drawing.Size(55, 13);
            this.lblTileName.TabIndex = 5;
            this.lblTileName.Text = "Tile Name";
            // 
            // lblTileID
            // 
            this.lblTileID.AutoSize = true;
            this.lblTileID.Location = new System.Drawing.Point(62, 19);
            this.lblTileID.Name = "lblTileID";
            this.lblTileID.Size = new System.Drawing.Size(38, 13);
            this.lblTileID.TabIndex = 4;
            this.lblTileID.Text = "Tile ID";
            // 
            // pbSelectedTile
            // 
            this.pbSelectedTile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbSelectedTile.Location = new System.Drawing.Point(6, 19);
            this.pbSelectedTile.Name = "pbSelectedTile";
            this.pbSelectedTile.Size = new System.Drawing.Size(50, 50);
            this.pbSelectedTile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSelectedTile.TabIndex = 3;
            this.pbSelectedTile.TabStop = false;
            // 
            // gbMapProperties
            // 
            this.gbMapProperties.AutoSize = true;
            this.gbMapProperties.Controls.Add(this.tbMapName);
            this.gbMapProperties.Controls.Add(this.lblMapName);
            this.gbMapProperties.Controls.Add(this.nudTileHeight);
            this.gbMapProperties.Controls.Add(this.nudTileWidth);
            this.gbMapProperties.Controls.Add(this.lblTileHeight);
            this.gbMapProperties.Controls.Add(this.lblTileWidth);
            this.gbMapProperties.Controls.Add(this.btnUpdateMap);
            this.gbMapProperties.Controls.Add(this.nudMapHeight);
            this.gbMapProperties.Controls.Add(this.nudMapWidth);
            this.gbMapProperties.Controls.Add(this.lblMapHeight);
            this.gbMapProperties.Controls.Add(this.lblMapWidth);
            this.gbMapProperties.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbMapProperties.Location = new System.Drawing.Point(0, 0);
            this.gbMapProperties.Name = "gbMapProperties";
            this.gbMapProperties.Size = new System.Drawing.Size(437, 121);
            this.gbMapProperties.TabIndex = 0;
            this.gbMapProperties.TabStop = false;
            this.gbMapProperties.Text = "Map Properties";
            // 
            // tbMapName
            // 
            this.tbMapName.Location = new System.Drawing.Point(70, 13);
            this.tbMapName.Name = "tbMapName";
            this.tbMapName.Size = new System.Drawing.Size(100, 20);
            this.tbMapName.TabIndex = 1;
            // 
            // lblMapName
            // 
            this.lblMapName.AutoSize = true;
            this.lblMapName.Location = new System.Drawing.Point(5, 16);
            this.lblMapName.Name = "lblMapName";
            this.lblMapName.Size = new System.Drawing.Size(59, 13);
            this.lblMapName.TabIndex = 8;
            this.lblMapName.Text = "Map Name";
            // 
            // nudTileHeight
            // 
            this.nudTileHeight.Location = new System.Drawing.Point(218, 72);
            this.nudTileHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTileHeight.Name = "nudTileHeight";
            this.nudTileHeight.Size = new System.Drawing.Size(80, 20);
            this.nudTileHeight.TabIndex = 5;
            this.nudTileHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudTileWidth
            // 
            this.nudTileWidth.Location = new System.Drawing.Point(218, 42);
            this.nudTileWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTileWidth.Name = "nudTileWidth";
            this.nudTileWidth.Size = new System.Drawing.Size(80, 20);
            this.nudTileWidth.TabIndex = 4;
            this.nudTileWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblTileHeight
            // 
            this.lblTileHeight.AutoSize = true;
            this.lblTileHeight.Location = new System.Drawing.Point(157, 74);
            this.lblTileHeight.Name = "lblTileHeight";
            this.lblTileHeight.Size = new System.Drawing.Size(58, 13);
            this.lblTileHeight.TabIndex = 6;
            this.lblTileHeight.Text = "Tile Height";
            // 
            // lblTileWidth
            // 
            this.lblTileWidth.AutoSize = true;
            this.lblTileWidth.Location = new System.Drawing.Point(157, 44);
            this.lblTileWidth.Name = "lblTileWidth";
            this.lblTileWidth.Size = new System.Drawing.Size(55, 13);
            this.lblTileWidth.TabIndex = 5;
            this.lblTileWidth.Text = "Tile Width";
            // 
            // btnUpdateMap
            // 
            this.btnUpdateMap.Location = new System.Drawing.Point(356, 69);
            this.btnUpdateMap.Name = "btnUpdateMap";
            this.btnUpdateMap.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateMap.TabIndex = 6;
            this.btnUpdateMap.Text = "Update Map";
            this.btnUpdateMap.UseVisualStyleBackColor = true;
            this.btnUpdateMap.Click += new System.EventHandler(this.btnUpdateMap_Click);
            // 
            // nudMapHeight
            // 
            this.nudMapHeight.Location = new System.Drawing.Point(70, 72);
            this.nudMapHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMapHeight.Name = "nudMapHeight";
            this.nudMapHeight.Size = new System.Drawing.Size(80, 20);
            this.nudMapHeight.TabIndex = 3;
            this.nudMapHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudMapWidth
            // 
            this.nudMapWidth.Location = new System.Drawing.Point(70, 42);
            this.nudMapWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMapWidth.Name = "nudMapWidth";
            this.nudMapWidth.Size = new System.Drawing.Size(80, 20);
            this.nudMapWidth.TabIndex = 2;
            this.nudMapWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblMapHeight
            // 
            this.lblMapHeight.AutoSize = true;
            this.lblMapHeight.Location = new System.Drawing.Point(5, 74);
            this.lblMapHeight.Name = "lblMapHeight";
            this.lblMapHeight.Size = new System.Drawing.Size(62, 13);
            this.lblMapHeight.TabIndex = 1;
            this.lblMapHeight.Text = "Map Height";
            // 
            // lblMapWidth
            // 
            this.lblMapWidth.AutoSize = true;
            this.lblMapWidth.Location = new System.Drawing.Point(5, 45);
            this.lblMapWidth.Name = "lblMapWidth";
            this.lblMapWidth.Size = new System.Drawing.Size(59, 13);
            this.lblMapWidth.TabIndex = 0;
            this.lblMapWidth.Text = "Map Width";
            // 
            // pbMapSmall
            // 
            this.pbMapSmall.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbMapSmall.Location = new System.Drawing.Point(9, 19);
            this.pbMapSmall.Name = "pbMapSmall";
            this.pbMapSmall.Size = new System.Drawing.Size(150, 150);
            this.pbMapSmall.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbMapSmall.TabIndex = 10;
            this.pbMapSmall.TabStop = false;
            // 
            // lblMouse
            // 
            this.lblMouse.AutoSize = true;
            this.lblMouse.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMouse.Location = new System.Drawing.Point(9, 186);
            this.lblMouse.Name = "lblMouse";
            this.lblMouse.Size = new System.Drawing.Size(82, 13);
            this.lblMouse.TabIndex = 9;
            this.lblMouse.Text = "Mouse Position:";
            // 
            // lblMapCoordinate
            // 
            this.lblMapCoordinate.AutoSize = true;
            this.lblMapCoordinate.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMapCoordinate.Location = new System.Drawing.Point(9, 172);
            this.lblMapCoordinate.Name = "lblMapCoordinate";
            this.lblMapCoordinate.Size = new System.Drawing.Size(85, 13);
            this.lblMapCoordinate.TabIndex = 8;
            this.lblMapCoordinate.Text = "Map Coordinate:";
            // 
            // menuStripDLMapEditor
            // 
            this.menuStripDLMapEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem1,
            this.viewToolStripMenuItem,
            this.selectToolStripMenuItem,
            this.tilesToolStripMenuItem,
            this.codeToolStripMenuItem});
            this.menuStripDLMapEditor.Location = new System.Drawing.Point(0, 0);
            this.menuStripDLMapEditor.Name = "menuStripDLMapEditor";
            this.menuStripDLMapEditor.Size = new System.Drawing.Size(1016, 24);
            this.menuStripDLMapEditor.TabIndex = 3;
            this.menuStripDLMapEditor.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMapToolStripMenuItem,
            this.toolStripSeparator2,
            this.loadMapToolStripMenuItem,
            this.toolStripSeparator3,
            this.saveMapToolStripMenuItem,
            this.saveMapAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitDLMapEditorToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newMapToolStripMenuItem
            // 
            this.newMapToolStripMenuItem.Name = "newMapToolStripMenuItem";
            this.newMapToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newMapToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.newMapToolStripMenuItem.Text = "&New Map";
            this.newMapToolStripMenuItem.Click += new System.EventHandler(this.newMapToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(222, 6);
            // 
            // loadMapToolStripMenuItem
            // 
            this.loadMapToolStripMenuItem.Name = "loadMapToolStripMenuItem";
            this.loadMapToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.loadMapToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.loadMapToolStripMenuItem.Text = "&Open Map";
            this.loadMapToolStripMenuItem.Click += new System.EventHandler(this.loadMapToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(222, 6);
            // 
            // saveMapToolStripMenuItem
            // 
            this.saveMapToolStripMenuItem.Name = "saveMapToolStripMenuItem";
            this.saveMapToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveMapToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.saveMapToolStripMenuItem.Text = "&Save Map";
            this.saveMapToolStripMenuItem.Click += new System.EventHandler(this.saveMapToolStripMenuItem_Click);
            // 
            // saveMapAsToolStripMenuItem
            // 
            this.saveMapAsToolStripMenuItem.Name = "saveMapAsToolStripMenuItem";
            this.saveMapAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.S)));
            this.saveMapAsToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.saveMapAsToolStripMenuItem.Text = "Save Map &As ...";
            this.saveMapAsToolStripMenuItem.Click += new System.EventHandler(this.saveMapAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(222, 6);
            // 
            // exitDLMapEditorToolStripMenuItem
            // 
            this.exitDLMapEditorToolStripMenuItem.Name = "exitDLMapEditorToolStripMenuItem";
            this.exitDLMapEditorToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.exitDLMapEditorToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.exitDLMapEditorToolStripMenuItem.Text = "E&xit D2D Map Editor";
            this.exitDLMapEditorToolStripMenuItem.Click += new System.EventHandler(this.exitDLMapEditorToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem1
            // 
            this.editToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator6,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.editToolStripMenuItem1.Name = "editToolStripMenuItem1";
            this.editToolStripMenuItem1.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem1.Text = "&Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.undoToolStripMenuItem.Text = "&Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.redoToolStripMenuItem.Text = "&Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(141, 6);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.pasteToolStripMenuItem.Text = "&Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.designViewToolStripMenuItem,
            this.codeViewToolStripMenuItem,
            this.toolStripSeparator4,
            this.gridToolStripMenuItem,
            this.showWalkableToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "&View";
            // 
            // designViewToolStripMenuItem
            // 
            this.designViewToolStripMenuItem.Checked = true;
            this.designViewToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.designViewToolStripMenuItem.Name = "designViewToolStripMenuItem";
            this.designViewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.D)));
            this.designViewToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.designViewToolStripMenuItem.Text = "&Design View";
            this.designViewToolStripMenuItem.Click += new System.EventHandler(this.designViewToolStripMenuItem_Click_1);
            // 
            // codeViewToolStripMenuItem
            // 
            this.codeViewToolStripMenuItem.Name = "codeViewToolStripMenuItem";
            this.codeViewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.C)));
            this.codeViewToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.codeViewToolStripMenuItem.Text = "&Code View";
            this.codeViewToolStripMenuItem.Click += new System.EventHandler(this.codeViewToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(228, 6);
            // 
            // gridToolStripMenuItem
            // 
            this.gridToolStripMenuItem.Checked = true;
            this.gridToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.gridToolStripMenuItem.Name = "gridToolStripMenuItem";
            this.gridToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.G)));
            this.gridToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.gridToolStripMenuItem.Text = "Show &Grid";
            this.gridToolStripMenuItem.Click += new System.EventHandler(this.gridToolStripMenuItem_Click);
            // 
            // showWalkableToolStripMenuItem
            // 
            this.showWalkableToolStripMenuItem.Name = "showWalkableToolStripMenuItem";
            this.showWalkableToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.W)));
            this.showWalkableToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.showWalkableToolStripMenuItem.Text = "Show &Walkable";
            this.showWalkableToolStripMenuItem.Click += new System.EventHandler(this.showWalkableToolStripMenuItem_Click);
            // 
            // selectToolStripMenuItem
            // 
            this.selectToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.deselectToolStripMenuItem});
            this.selectToolStripMenuItem.Name = "selectToolStripMenuItem";
            this.selectToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.selectToolStripMenuItem.Text = "&Select";
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.selectAllToolStripMenuItem.Text = "Select &All";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // deselectToolStripMenuItem
            // 
            this.deselectToolStripMenuItem.Name = "deselectToolStripMenuItem";
            this.deselectToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.deselectToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.deselectToolStripMenuItem.Text = "&Deselect";
            this.deselectToolStripMenuItem.Click += new System.EventHandler(this.deselectToolStripMenuItem_Click);
            // 
            // tilesToolStripMenuItem
            // 
            this.tilesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddTilesToolStripMenuItem,
            this.toolStripSeparator5,
            this.deleteSelectedTileToolStripMenuItem,
            this.clearTilesToolStripMenuItem});
            this.tilesToolStripMenuItem.Name = "tilesToolStripMenuItem";
            this.tilesToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.tilesToolStripMenuItem.Text = "&Tiles";
            // 
            // AddTilesToolStripMenuItem
            // 
            this.AddTilesToolStripMenuItem.Name = "AddTilesToolStripMenuItem";
            this.AddTilesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.AddTilesToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.AddTilesToolStripMenuItem.Text = "&Add Tiles Set";
            this.AddTilesToolStripMenuItem.Click += new System.EventHandler(this.addTilesToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(180, 6);
            // 
            // deleteSelectedTileToolStripMenuItem
            // 
            this.deleteSelectedTileToolStripMenuItem.Name = "deleteSelectedTileToolStripMenuItem";
            this.deleteSelectedTileToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.deleteSelectedTileToolStripMenuItem.Text = "&Delete Selected Tile";
            this.deleteSelectedTileToolStripMenuItem.Click += new System.EventHandler(this.deleteSelectedTileToolStripMenuItem_Click);
            // 
            // clearTilesToolStripMenuItem
            // 
            this.clearTilesToolStripMenuItem.Name = "clearTilesToolStripMenuItem";
            this.clearTilesToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.clearTilesToolStripMenuItem.Text = "&Clear Tiles";
            this.clearTilesToolStripMenuItem.Click += new System.EventHandler(this.clearTilesToolStripMenuItem_Click);
            // 
            // codeToolStripMenuItem
            // 
            this.codeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectedLanguageToolStripMenuItem,
            this.generateCodeToolStripMenuItem});
            this.codeToolStripMenuItem.Name = "codeToolStripMenuItem";
            this.codeToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.codeToolStripMenuItem.Text = "&Code";
            // 
            // selectedLanguageToolStripMenuItem
            // 
            this.selectedLanguageToolStripMenuItem.Name = "selectedLanguageToolStripMenuItem";
            this.selectedLanguageToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.selectedLanguageToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.selectedLanguageToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.selectedLanguageToolStripMenuItem.Text = "&Generate C++";
            this.selectedLanguageToolStripMenuItem.Click += new System.EventHandler(this.selectedLanguageToolStripMenuItem_Click);
            // 
            // generateCodeToolStripMenuItem
            // 
            this.generateCodeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cArrayToolStripMenuItem,
            this.cArrayToolStripMenuItem1,
            this.actionScriptToolStripMenuItem,
            this.xMLToolStripMenuItem});
            this.generateCodeToolStripMenuItem.Name = "generateCodeToolStripMenuItem";
            this.generateCodeToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.generateCodeToolStripMenuItem.Text = "Generate &Code";
            // 
            // cArrayToolStripMenuItem
            // 
            this.cArrayToolStripMenuItem.Name = "cArrayToolStripMenuItem";
            this.cArrayToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.D1)));
            this.cArrayToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.cArrayToolStripMenuItem.Text = "&C/C++ Array";
            this.cArrayToolStripMenuItem.Click += new System.EventHandler(this.cArrayToolStripMenuItem_Click);
            // 
            // cArrayToolStripMenuItem1
            // 
            this.cArrayToolStripMenuItem1.Name = "cArrayToolStripMenuItem1";
            this.cArrayToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.D2)));
            this.cArrayToolStripMenuItem1.Size = new System.Drawing.Size(210, 22);
            this.cArrayToolStripMenuItem1.Text = "C# A&rray";
            this.cArrayToolStripMenuItem1.Click += new System.EventHandler(this.cArrayToolStripMenuItem1_Click);
            // 
            // actionScriptToolStripMenuItem
            // 
            this.actionScriptToolStripMenuItem.Name = "actionScriptToolStripMenuItem";
            this.actionScriptToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            // 
            // xMLToolStripMenuItem
            // 
            this.xMLToolStripMenuItem.Name = "xMLToolStripMenuItem";
            this.xMLToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.D4)));
            this.xMLToolStripMenuItem.Size = new System.Drawing.Size(210, 22);
            this.xMLToolStripMenuItem.Text = "&XML";
            this.xMLToolStripMenuItem.Click += new System.EventHandler(this.xMLToolStripMenuItem_Click);
            // 
            // saveMapDialog
            // 
            this.saveMapDialog.DefaultExt = "lv";
            this.saveMapDialog.Filter = "LV Documents (*.lv) |*.lv";
            this.saveMapDialog.Title = "Save Map As";
            // 
            // openMapDialog
            // 
            this.openMapDialog.DefaultExt = "lv";
            this.openMapDialog.Filter = "LV Documents (*.lv) |*.lv";
            // 
            // gbTiles
            // 
            this.gbTiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbTiles.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbTiles.Controls.Add(this.pnlTileLibrary);
            this.gbTiles.Location = new System.Drawing.Point(0, 283);
            this.gbTiles.Name = "gbTiles";
            this.gbTiles.Size = new System.Drawing.Size(178, 304);
            this.gbTiles.TabIndex = 1;
            this.gbTiles.TabStop = false;
            this.gbTiles.Text = "Tile Library";
            // 
            // pnlTileLibrary
            // 
            this.pnlTileLibrary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTileLibrary.AutoScroll = true;
            this.pnlTileLibrary.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlTileLibrary.BackColor = System.Drawing.SystemColors.Window;
            this.pnlTileLibrary.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlTileLibrary.Location = new System.Drawing.Point(3, 15);
            this.pnlTileLibrary.Name = "pnlTileLibrary";
            this.pnlTileLibrary.Size = new System.Drawing.Size(172, 288);
            this.pnlTileLibrary.TabIndex = 0;
            // 
            // pnlTiles
            // 
            this.pnlTiles.AutoSize = true;
            this.pnlTiles.Controls.Add(this.gbTools);
            this.pnlTiles.Controls.Add(this.gbMapSmall);
            this.pnlTiles.Controls.Add(this.gbTiles);
            this.pnlTiles.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlTiles.Location = new System.Drawing.Point(838, 24);
            this.pnlTiles.Name = "pnlTiles";
            this.pnlTiles.Size = new System.Drawing.Size(178, 589);
            this.pnlTiles.TabIndex = 2;
            // 
            // gbTools
            // 
            this.gbTools.Controls.Add(this.btnToolFill);
            this.gbTools.Controls.Add(this.btnToolSelection);
            this.gbTools.Controls.Add(this.btnToolEraser);
            this.gbTools.Controls.Add(this.btnToolSelectTile);
            this.gbTools.Controls.Add(this.btnToolBrush);
            this.gbTools.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbTools.Location = new System.Drawing.Point(0, 215);
            this.gbTools.Name = "gbTools";
            this.gbTools.Size = new System.Drawing.Size(178, 66);
            this.gbTools.TabIndex = 3;
            this.gbTools.TabStop = false;
            this.gbTools.Text = "Tools";
            // 
            // btnToolFill
            // 
            try
            {
                this.btnToolFill.Image = ((System.Drawing.Image)(resources.GetObject("btnToolFill")));
            }
            catch (Exception ex) { }
            this.btnToolFill.Location = new System.Drawing.Point(76, 19);
            this.btnToolFill.Name = "btnToolFill";
            this.btnToolFill.Size = new System.Drawing.Size(28, 28);
            this.btnToolFill.TabIndex = 5;
            this.btnToolFill.UseVisualStyleBackColor = true;
            this.btnToolFill.Click += new System.EventHandler(this.btnToolFill_Click);
            // 
            // btnToolSelection
            // 
            try
            {
                this.btnToolSelection.Image = ((System.Drawing.Image)(resources.GetObject("btnToolSelection")));
            }
            catch (Exception ex) { }
            this.btnToolSelection.Location = new System.Drawing.Point(8, 19);
            this.btnToolSelection.Name = "btnToolSelection";
            this.btnToolSelection.Size = new System.Drawing.Size(28, 28);
            this.btnToolSelection.TabIndex = 4;
            this.btnToolSelection.UseVisualStyleBackColor = true;
            this.btnToolSelection.Click += new System.EventHandler(this.btnToolSelection_Click);
            // 
            // btnToolEraser
            // 
            try
            {
                this.btnToolEraser.Image = ((System.Drawing.Image)(resources.GetObject("btnToolEraser")));
            }
            catch (Exception ex) { }
            this.btnToolEraser.Location = new System.Drawing.Point(144, 19);
            this.btnToolEraser.Name = "btnToolEraser";
            this.btnToolEraser.Size = new System.Drawing.Size(28, 28);
            this.btnToolEraser.TabIndex = 3;
            this.btnToolEraser.UseVisualStyleBackColor = true;
            this.btnToolEraser.Click += new System.EventHandler(this.btnToolEraser_Click);
            // 
            // btnToolSelectTile
            // 
            try
            {
                this.btnToolSelectTile.Image = ((System.Drawing.Image)(resources.GetObject("btnToolSelectTile")));
            }
            catch (Exception ex) { }
            this.btnToolSelectTile.Location = new System.Drawing.Point(110, 19);
            this.btnToolSelectTile.Name = "btnToolSelectTile";
            this.btnToolSelectTile.Size = new System.Drawing.Size(28, 28);
            this.btnToolSelectTile.TabIndex = 2;
            this.btnToolSelectTile.UseVisualStyleBackColor = true;
            this.btnToolSelectTile.Click += new System.EventHandler(this.btnToolSelectColor_Click);
            // 
            // btnToolBrush
            // 
            
            this.btnToolBrush.AccessibleDescription = "";
            this.btnToolBrush.BackColor = System.Drawing.SystemColors.Control;
            try
            {
                this.btnToolBrush.Image = ((System.Drawing.Image)(resources.GetObject("btnToolBrush")));
            }
            catch (Exception ex) { }
            this.btnToolBrush.Location = new System.Drawing.Point(42, 19);
            this.btnToolBrush.Name = "btnToolBrush";
            this.btnToolBrush.Size = new System.Drawing.Size(28, 28);
            this.btnToolBrush.TabIndex = 1;
            this.btnToolBrush.Tag = "";
            this.btnToolBrush.UseVisualStyleBackColor = false;
            this.btnToolBrush.Click += new System.EventHandler(this.btnToolBrush_Click);
            // 
            // gbMapSmall
            // 
            this.gbMapSmall.AutoSize = true;
            this.gbMapSmall.Controls.Add(this.lblMouse);
            this.gbMapSmall.Controls.Add(this.pbMapSmall);
            this.gbMapSmall.Controls.Add(this.lblMapCoordinate);
            this.gbMapSmall.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbMapSmall.Location = new System.Drawing.Point(0, 0);
            this.gbMapSmall.Name = "gbMapSmall";
            this.gbMapSmall.Size = new System.Drawing.Size(178, 215);
            this.gbMapSmall.TabIndex = 2;
            this.gbMapSmall.TabStop = false;
            this.gbMapSmall.Text = "Map";
            // 
            // contextMenuStripTile
            // 
            this.contextMenuStripTile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemWalkable,
            this.toolStripMenuItemDelete});
            this.contextMenuStripTile.Name = "contextMenuStripTile";
            this.contextMenuStripTile.Size = new System.Drawing.Size(123, 48);
            // 
            // toolStripMenuItemWalkable
            // 
            this.toolStripMenuItemWalkable.Checked = true;
            this.toolStripMenuItemWalkable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItemWalkable.Name = "toolStripMenuItemWalkable";
            this.toolStripMenuItemWalkable.Size = new System.Drawing.Size(122, 22);
            this.toolStripMenuItemWalkable.Text = "Walkable";
            this.toolStripMenuItemWalkable.Click += new System.EventHandler(this.toolStripMenuItemWalkable_Click);
            // 
            // toolStripMenuItemDelete
            // 
            this.toolStripMenuItemDelete.Name = "toolStripMenuItemDelete";
            this.toolStripMenuItemDelete.Size = new System.Drawing.Size(122, 22);
            this.toolStripMenuItemDelete.Text = "Delete";
            this.toolStripMenuItemDelete.Click += new System.EventHandler(this.toolStripMenuItemDelete_Click);
            // 
            // D2DMapEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 734);
            this.Controls.Add(this.pnlTiles);
            this.Controls.Add(this.pnlProperties);
            this.Controls.Add(this.pnlDesign);
            this.Controls.Add(this.menuStripDLMapEditor);
            this.DoubleBuffered = true;
            //this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripDLMapEditor;
            this.MinimumSize = new System.Drawing.Size(1024, 768);
            this.Name = "MapEditor";
            this.Text = "Level Builder";
            this.Resize += new System.EventHandler(this.MapEditor_Resize);
            this.pnlDesign.ResumeLayout(false);
            this.tctrlDesign.ResumeLayout(false);
            this.tpgDesign.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMap)).EndInit();
            this.tpgCode.ResumeLayout(false);
            this.tpgCode.PerformLayout();
            this.pnlProperties.ResumeLayout(false);
            this.pnlProperties.PerformLayout();
            this.gbCode.ResumeLayout(false);
            this.gbCode.PerformLayout();
            this.gbTileProperties.ResumeLayout(false);
            this.gbTileProperties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSelectedTile)).EndInit();
            this.gbMapProperties.ResumeLayout(false);
            this.gbMapProperties.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTileHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTileWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMapHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMapWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMapSmall)).EndInit();
            this.menuStripDLMapEditor.ResumeLayout(false);
            this.menuStripDLMapEditor.PerformLayout();
            this.gbTiles.ResumeLayout(false);
            this.pnlTiles.ResumeLayout(false);
            this.pnlTiles.PerformLayout();
            this.gbTools.ResumeLayout(false);
            this.gbMapSmall.ResumeLayout(false);
            this.gbMapSmall.PerformLayout();
            this.contextMenuStripTile.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        #region Declarations

        private System.Windows.Forms.Panel pnlDesign;
        private System.Windows.Forms.Panel pnlProperties;
        private System.Windows.Forms.GroupBox gbMapProperties;
        private System.Windows.Forms.TabControl tctrlDesign;
        private System.Windows.Forms.TabPage tpgDesign;
        private System.Windows.Forms.Label lblMapHeight;
        private System.Windows.Forms.Label lblMapWidth;
        private System.Windows.Forms.Button btnUpdateMap;
        public System.Windows.Forms.NumericUpDown nudMapHeight;
        public System.Windows.Forms.NumericUpDown nudMapWidth;
        private System.Windows.Forms.PictureBox pbMap;
        private System.Windows.Forms.MenuStrip menuStripDLMapEditor;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitDLMapEditorToolStripMenuItem;
        public System.Windows.Forms.NumericUpDown nudTileHeight;
        public System.Windows.Forms.NumericUpDown nudTileWidth;
        private System.Windows.Forms.Label lblTileHeight;
        private System.Windows.Forms.Label lblTileWidth;
        private System.Windows.Forms.ToolStripMenuItem tilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddTilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearTilesToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogLoadTiles;
        private System.Windows.Forms.Label lblMouse;
        private System.Windows.Forms.Label lblMapCoordinate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem saveMapAsToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveMapDialog;
        private System.Windows.Forms.OpenFileDialog openMapDialog;
        private System.Windows.Forms.ToolStripMenuItem codeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateCodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cArrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cArrayToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem xMLToolStripMenuItem;
        private System.Windows.Forms.TabPage tpgCode;
        private System.Windows.Forms.TextBox tbCode;
        private System.Windows.Forms.PictureBox pbMapSmall;
        private System.Windows.Forms.GroupBox gbTiles;
        private System.Windows.Forms.Panel pnlTileLibrary;
        private System.Windows.Forms.GroupBox gbTileProperties;
        private System.Windows.Forms.Button btnUpdateTile;
        private System.Windows.Forms.CheckBox cbTileWalkable;
        private System.Windows.Forms.TextBox tbTileName;
        private System.Windows.Forms.Label lblTileIDValue;
        private System.Windows.Forms.Label lblTileName;
        private System.Windows.Forms.Label lblTileID;
        private System.Windows.Forms.PictureBox pbSelectedTile;
        private System.Windows.Forms.Panel pnlTiles;
        private System.Windows.Forms.GroupBox gbMapSmall;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem designViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem codeViewToolStripMenuItem;
        private System.Windows.Forms.GroupBox gbCode;
        private System.Windows.Forms.RadioButton rbXML;
        private System.Windows.Forms.RadioButton rbCS;
        private System.Windows.Forms.RadioButton rbCPP;
        private System.Windows.Forms.ToolStripMenuItem selectedLanguageToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem gridToolStripMenuItem;
        private System.Windows.Forms.Label lblMapName;
        public System.Windows.Forms.TextBox tbMapName;
        private System.Windows.Forms.ToolStripMenuItem showWalkableToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTile;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDelete;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemWalkable;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedTileToolStripMenuItem;
        private System.Windows.Forms.GroupBox gbTools;
        private System.Windows.Forms.Button btnToolBrush;
        private System.Windows.Forms.Button btnToolEraser;
        private System.Windows.Forms.Button btnToolSelectTile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.Button btnToolSelection;
        private System.Windows.Forms.Button btnToolFill;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deselectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actionScriptToolStripMenuItem;

        #endregion
    }
}


namespace TimetableUI
{
  partial class AddExternalRoom
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
      this.lblRoomName = new System.Windows.Forms.Label();
      this.tbRoomName = new System.Windows.Forms.TextBox();
      this.btnAddEvent = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // lblRoomName
      // 
      this.lblRoomName.AutoSize = true;
      this.lblRoomName.Location = new System.Drawing.Point(13, 13);
      this.lblRoomName.Name = "lblRoomName";
      this.lblRoomName.Size = new System.Drawing.Size(35, 13);
      this.lblRoomName.TabIndex = 0;
      this.lblRoomName.Text = "Name";
      // 
      // tbRoomName
      // 
      this.tbRoomName.Location = new System.Drawing.Point(13, 30);
      this.tbRoomName.Name = "tbRoomName";
      this.tbRoomName.Size = new System.Drawing.Size(100, 20);
      this.tbRoomName.TabIndex = 1;
      // 
      // btnAddEvent
      // 
      this.btnAddEvent.Location = new System.Drawing.Point(122, 30);
      this.btnAddEvent.Name = "btnAddEvent";
      this.btnAddEvent.Size = new System.Drawing.Size(24, 20);
      this.btnAddEvent.TabIndex = 14;
      this.btnAddEvent.Text = "+";
      this.btnAddEvent.UseVisualStyleBackColor = true;
      this.btnAddEvent.Click += new System.EventHandler(this.btnAddEvent_Click);
      // 
      // AddExternalRoom
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(158, 62);
      this.Controls.Add(this.btnAddEvent);
      this.Controls.Add(this.tbRoomName);
      this.Controls.Add(this.lblRoomName);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "AddExternalRoom";
      this.Text = "AddExternalRoom";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblRoomName;
    private System.Windows.Forms.TextBox tbRoomName;
    private System.Windows.Forms.Button btnAddEvent;
  }
}
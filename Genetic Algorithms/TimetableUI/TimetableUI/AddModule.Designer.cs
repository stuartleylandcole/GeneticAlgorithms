namespace TimetableUI
{
  partial class AddModule
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
      this.lblCourseCode = new System.Windows.Forms.Label();
      this.tbCourseCode = new System.Windows.Forms.TextBox();
      this.tbCourseTitle = new System.Windows.Forms.TextBox();
      this.lblCourseTitle = new System.Windows.Forms.Label();
      this.btnAddEvent = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // lblCourseCode
      // 
      this.lblCourseCode.AutoSize = true;
      this.lblCourseCode.Location = new System.Drawing.Point(13, 13);
      this.lblCourseCode.Name = "lblCourseCode";
      this.lblCourseCode.Size = new System.Drawing.Size(67, 13);
      this.lblCourseCode.TabIndex = 0;
      this.lblCourseCode.Text = "Course code";
      // 
      // tbCourseCode
      // 
      this.tbCourseCode.Location = new System.Drawing.Point(16, 30);
      this.tbCourseCode.Name = "tbCourseCode";
      this.tbCourseCode.Size = new System.Drawing.Size(100, 20);
      this.tbCourseCode.TabIndex = 1;
      // 
      // tbCourseTitle
      // 
      this.tbCourseTitle.Location = new System.Drawing.Point(152, 29);
      this.tbCourseTitle.Name = "tbCourseTitle";
      this.tbCourseTitle.Size = new System.Drawing.Size(312, 20);
      this.tbCourseTitle.TabIndex = 2;
      // 
      // lblCourseTitle
      // 
      this.lblCourseTitle.AutoSize = true;
      this.lblCourseTitle.Location = new System.Drawing.Point(152, 13);
      this.lblCourseTitle.Name = "lblCourseTitle";
      this.lblCourseTitle.Size = new System.Drawing.Size(59, 13);
      this.lblCourseTitle.TabIndex = 3;
      this.lblCourseTitle.Text = "Course title";
      // 
      // btnAddEvent
      // 
      this.btnAddEvent.Location = new System.Drawing.Point(470, 30);
      this.btnAddEvent.Name = "btnAddEvent";
      this.btnAddEvent.Size = new System.Drawing.Size(24, 20);
      this.btnAddEvent.TabIndex = 13;
      this.btnAddEvent.Text = "+";
      this.btnAddEvent.UseVisualStyleBackColor = true;
      this.btnAddEvent.Click += new System.EventHandler(this.btnAddEvent_Click);
      // 
      // AddModule
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(501, 65);
      this.Controls.Add(this.btnAddEvent);
      this.Controls.Add(this.lblCourseTitle);
      this.Controls.Add(this.tbCourseTitle);
      this.Controls.Add(this.tbCourseCode);
      this.Controls.Add(this.lblCourseCode);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "AddModule";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "AddModule";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblCourseCode;
    private System.Windows.Forms.TextBox tbCourseCode;
    private System.Windows.Forms.TextBox tbCourseTitle;
    private System.Windows.Forms.Label lblCourseTitle;
    private System.Windows.Forms.Button btnAddEvent;
  }
}
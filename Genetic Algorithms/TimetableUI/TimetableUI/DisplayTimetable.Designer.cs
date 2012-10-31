namespace TimetableUI
{
  partial class DisplayTimetable
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
      this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
      this.SuspendLayout();
      // 
      // tableLayout
      // 
      this.tableLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.tableLayout.AutoScroll = true;
      this.tableLayout.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Inset;
      this.tableLayout.ColumnCount = 6;
      this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
      this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
      this.tableLayout.Location = new System.Drawing.Point(12, 12);
      this.tableLayout.Name = "tableLayout";
      this.tableLayout.RowCount = 9;
      this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
      this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayout.Size = new System.Drawing.Size(1202, 759);
      this.tableLayout.TabIndex = 0;
      // 
      // DisplayTimetable
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoScroll = true;
      this.ClientSize = new System.Drawing.Size(1219, 812);
      this.Controls.Add(this.tableLayout);
      this.Name = "DisplayTimetable";
      this.Text = "DisplayTimetable";
      this.Load += new System.EventHandler(this.DisplayTimetable_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayout;
  }
}
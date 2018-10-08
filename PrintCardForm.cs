using System;

public partial class PrintCardForm : Form
{
    private Button printButton = new Button();
    private Button printPreviewButton;
    private PrintDocument printDocument1 = new PrintDocument();
    private PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
    // Declare a string to hold the entire document contents.
    private string documentContents;
    // Declare a variable to hold the portion of the document that
    // is not printed.
    private string stringToPrint;

    public PrintCardForm()
    {
        printButton.Text = "Print Form";
        printButton.Click += new EventHandler(printButton_Click);
        printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
        this.Controls.Add(printButton);

        this.printPreviewButton = new Button();
        this.printPreviewButton.Location = new Point(12, 12);
        this.printPreviewButton.Size = new Size(125, 23);
        this.printPreviewButton.Text = "Print Preview";
        this.printPreviewButton.Click += new EventHandler(this.printPreviewButton_Click);
        this.ClientSize = new Size(292, 266);
        this.Controls.Add(this.printPreviewButton);
        printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
    }

    private void printPreviewButton_Click(object sender, EventArgs e)
    {
        CaptureScreen();
        //ReadDocument();
        printPreviewDialog1.Document = printDocument1;
        printPreviewDialog1.ShowDialog();
    }

    void printButton_Click(object sender, EventArgs e)
    {
        CaptureScreen();
        //printDocument1.Print();

        //ReadDocument();
        printPreviewDialog1.Document = printDocument1;
        printPreviewDialog1.ShowDialog();
    }

    private void ReadDocument()
    {
        string docName = "testPage.txt";
        string docPath = @"c:\";
        printDocument1.DocumentName = docName;
        using (FileStream stream = new FileStream(docPath + docName, FileMode.Open))
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                documentContents = reader.ReadToEnd();
            }
        }
        stringToPrint = documentContents;
    }

    Bitmap memoryImage;

    private void CaptureScreen()
    {
        Graphics myGraphics = this.CreateGraphics();
        Size s = this.Size;
        memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
        Graphics memoryGraphics = Graphics.FromImage(memoryImage);
        memoryGraphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);
    }

    void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
    {
        e.Graphics.DrawImage(memoryImage, 0, 0);
        //int charactersOnPage = 0;
        //int linesPerPage = 0;

        //// Sets the value of charactersOnPage to the number of characters 
        //// of stringToPrint that will fit within the bounds of the page.
        //e.Graphics.MeasureString(stringToPrint, this.Font,
        //    e.MarginBounds.Size, StringFormat.GenericTypographic,
        //    out charactersOnPage, out linesPerPage);

        //// Draws the string within the bounds of the page.
        //e.Graphics.DrawString(stringToPrint, this.Font, Brushes.Black,
        //e.MarginBounds, StringFormat.GenericTypographic);

        //// Remove the portion of the string that has been printed.
        //stringToPrint = stringToPrint.Substring(charactersOnPage);

        //// Check to see if more pages are to be printed.
        //e.HasMorePages = (stringToPrint.Length > 0);

        //// If there are no more pages, reset the string to be printed.
        //if (!e.HasMorePages)
        //{
        //    stringToPrint = documentContents;
        //}
    }
}

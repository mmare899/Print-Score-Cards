using System;

public class ScoreCard
{
	public ScoreCard()
	{
	}
  
  	public int NumberOfHoles = 18;
  
  	public List<Hole> Holes = new List<Hole>();
	
	private void CreateHole(int holeNumber, int holeLength, string teeColor)
	{
		Hole newHole = new Hole();
		newHole.HoleNumber = holeNumber;
		newHole.HoleLength = holeLength;
		newHole.TeeColor = teeColor;
		
		Holes.Add(newHole);
	}
	
	public void ReadInCSVFile()
	{
	    string InputFile = "";
            string OutputFile = "";
            string Round = "";
            DataTable output = InstantiateTable();
            FileProcessing fp = new FileProcessing();
            string currentHole = "";
            int currentPlayer = 1;
            DataRow currentRow = output.NewRow();
            
            do
            {
                Console.Write("Input File: ");
                InputFile = Console.ReadLine();//@"C:\Users\michaelm\Documents\SRO\ScoreBoard2.csv";// Console.ReadLine();
            } while (!System.IO.File.Exists(InputFile));

            Console.Write("Output File: ");
            OutputFile = Console.ReadLine();//@"C:\Users\michaelm\Documents\SRO\test.txt";//Console.ReadLine();

            Console.Write("Round Number: ");
            Round = Console.ReadLine();

            foreach (DataRow r in fp.ReadFile(InputFile).Rows)
            {
                if (r["Tee " + Round].ToString() == currentHole)
                {
                    currentPlayer++;
                    currentRow["Player" + currentPlayer.ToString()] = r["First Name"].ToString().Trim() + " " + r["Last Name"].ToString().Trim();
                }
                else
                {
                    if (currentRow["HoleNum"].ToString() != "")
                        output.Rows.Add(currentRow);
                    currentRow = output.NewRow();
                    currentPlayer = 1;
                    currentHole = r["Tee " + Round].ToString();

                    currentRow["Round"] = Round;
                    currentRow["HoleNum"] = r["Tee " + Round].ToString();
                    currentRow["Division"] = GetDivision(r["Div"].ToString());
                    currentRow["Player" + currentPlayer.ToString()] = r["First Name"].ToString().Trim() + " " + r["Last Name"].ToString().Trim();

                }
            }
            output.Rows.Add(currentRow);
            fp.WriteFile(output, OutputFile);

        }
	
  	private static DataTable InstantiateTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Round");
            dt.Columns.Add("HoleNum");
            dt.Columns.Add("Division");
            dt.Columns.Add("Player1");
            dt.Columns.Add("Player2");
            dt.Columns.Add("Player3");
            dt.Columns.Add("Player4");
            dt.Columns.Add("Player5");

            return dt;
        }
	
	private static string GetDivision(string div)
        {
            switch (div)
            {
                case "FPO":return "Pro Wom";
                case "MPG":return "Grand Mast";
                case "MPM":return "Masters";
                case "MPO":return "Open";
                case "MPS":return "Sr Grand";
                case "MA1":return "Advanced";
                case "MG1":return "Adv GM";
                case "MM1": return "Adv Mast";
                default: return div;
            }
        }
}

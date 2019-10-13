using System;

public class Hole
{

	public int? HoleNumber = null;
	public int? HoleLength = null;
	public string TeeColor = "";
	
	public Hole()
	{
	}
  
  	public Hole(int holeNumber, int holeLength, string teeColor)
	{
      		HoleNumber = holeNumber;
      		HoleLength = holeLength;
      		TeeColor = teeColor;
	}
  
	public int HoleNumber = 0;
  	public int HoleLength = 0;
  	public string TeeColor = '';
  
}

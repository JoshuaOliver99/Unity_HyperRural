using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompetitionTable : MonoBehaviour
{
	// SETUP
	public TextAsset competitionDB; // Link DB

	// TEXT
	public Text title1; // For competition title1
	public Text description1; // For competition description1
	public Text title2; // For competition title2
	public Text description2; // For competition description2
	public Text title3; // For competition title3
	public Text description3; // For competition description3
	
	// PROPOSALS
	List<Row> activeCompetitions; // Holds unseen competitons
	Row currentCompetiton1; // Holds current competiton1
	Row currentCompetiton2; // Holds current competiton1
	Row currentCompetiton3; // Holds current competiton1


	private void Start()
	{
		Load(competitionDB);
		resetActiveCompetitons(); // Sets the active challenges
        GetRandomCompetitions(); // Assigns three random challenges
        drawUI(); // Draws UI for the challenege proposals
    }

    public void GetRandomCompetitions()
    {
        currentCompetiton1 = getRandomCompetition();
        currentCompetiton2 = getRandomCompetition();
        currentCompetiton3 = getRandomCompetition();
    }

    public void AcceptCompetition(int i)
    {
        switch (i)
        {
            case 1:
                AcceptCompetition1();
                break;
            case 2:
                AcceptCompetition2();
                break;
            case 3:
                AcceptCompetition3();
                break;
            default:
                Debug.LogError("Competition case out of bounds.");
                break;
        }

        GetRandomCompetitions();
        drawUI(); // Draws UI for the challenege proposals
    }

	private void AcceptCompetition1() // Applies stats
	{
		GameController.econemy += int.Parse(currentCompetiton1.Y_Economy); // Apply stats
		GameController.environment += int.Parse(currentCompetiton1.Y_Environment);
		GameController.appeal += int.Parse(currentCompetiton1.Y_Appeal);
		GameController.ecoDiversity += int.Parse(currentCompetiton1.Y_EcoDiversity);
	}

    private void AcceptCompetition2() // Applies stats
	{
		GameController.econemy += int.Parse(currentCompetiton2.Y_Economy); // Apply stats
		GameController.environment += int.Parse(currentCompetiton2.Y_Environment);
		GameController.appeal += int.Parse(currentCompetiton2.Y_Appeal);
		GameController.ecoDiversity += int.Parse(currentCompetiton2.Y_EcoDiversity);
	}

    private void AcceptCompetition3() // Applies stats
	{
		GameController.econemy += int.Parse(currentCompetiton3.Y_Economy); // Apply stats
		GameController.environment += int.Parse(currentCompetiton3.Y_Environment);
		GameController.appeal += int.Parse(currentCompetiton3.Y_Appeal);
		GameController.ecoDiversity += int.Parse(currentCompetiton3.Y_EcoDiversity);
	}


	private Row getRandomCompetition() // PULLS A RANDOM PROPOSAL or ENDS GAME
	{
		if (activeCompetitions.Count > 3) // If (Enough active competitons remain)
		{
			Row foundProposal = activeCompetitions[Random.Range(0, activeCompetitions.Count)]; // Chooses random from list of active
			activeCompetitions.Remove(foundProposal); // Removes the current challenege from active list

            

            return foundProposal;
		}
		else
		{
			Debug.LogError("Active / Remaining competitons: " + activeCompetitions.Count);
			return null;
		}

	}

	private void drawUI()
	{
		title1.text = currentCompetiton1.Title; // Apply Competiton1 title
		description1.text = currentCompetiton1.Description; // Apply Competiton1 Description
		title2.text = currentCompetiton2.Title; // Apply Competiton1 title
		description2.text = currentCompetiton2.Description; // Apply Competiton1 Description
		title3.text = currentCompetiton3.Title; // Apply Competiton1 title
		description3.text = currentCompetiton3.Description; // Apply Competiton1 Description
	}

	private void resetActiveCompetitons()
	{
		activeCompetitions = new List<Row>(GetRowList()); // Sets activeCompetitions to rowList (full list)
	}

	#region CSV2Table defults
	public class Row
	{
		public string ID;
		public string Title;
		public string Description;
		public string Y_Economy;
		public string Y_Environment;
		public string Y_Appeal;
		public string Y_EcoDiversity;
		public string Depiction;
	}

	List<Row> rowList = new List<Row>();
	bool isLoaded = false;

	public bool IsLoaded()
	{
		return isLoaded;
	}

	public List<Row> GetRowList()
	{
		return rowList;
	}

	public void Load(TextAsset csv)
	{
		rowList.Clear();
		string[][] grid = CsvParser2.Parse(csv.text);
		for (int i = 1; i < grid.Length; i++)
		{
			Row row = new Row();
			row.ID = grid[i][0];
			row.Title = grid[i][1];
			row.Description = grid[i][2];
			row.Y_Economy = grid[i][3];
			row.Y_Environment = grid[i][4];
			row.Y_Appeal = grid[i][5];
			row.Y_EcoDiversity = grid[i][6];
			row.Depiction = grid[i][7];

			rowList.Add(row);
		}
		isLoaded = true;
	}

	public int NumRows()
	{
		return rowList.Count;
	}

	public Row GetAt(int i)
	{
		if (rowList.Count <= i)
			return null;
		return rowList[i];
	}

	public Row Find_ID(string find)
	{
		return rowList.Find(x => x.ID == find);
	}
	public List<Row> FindAll_ID(string find)
	{
		return rowList.FindAll(x => x.ID == find);
	}
	public Row Find_Title(string find)
	{
		return rowList.Find(x => x.Title == find);
	}
	public List<Row> FindAll_Title(string find)
	{
		return rowList.FindAll(x => x.Title == find);
	}
	public Row Find_Description(string find)
	{
		return rowList.Find(x => x.Description == find);
	}
	public List<Row> FindAll_Description(string find)
	{
		return rowList.FindAll(x => x.Description == find);
	}
	public Row Find_Y_Economy(string find)
	{
		return rowList.Find(x => x.Y_Economy == find);
	}
	public List<Row> FindAll_Y_Economy(string find)
	{
		return rowList.FindAll(x => x.Y_Economy == find);
	}
	public Row Find_Y_Environment(string find)
	{
		return rowList.Find(x => x.Y_Environment == find);
	}
	public List<Row> FindAll_Y_Environment(string find)
	{
		return rowList.FindAll(x => x.Y_Environment == find);
	}
	public Row Find_Y_Appeal(string find)
	{
		return rowList.Find(x => x.Y_Appeal == find);
	}
	public List<Row> FindAll_Y_Appeal(string find)
	{
		return rowList.FindAll(x => x.Y_Appeal == find);
	}
	public Row Find_Y_EcoDiversity(string find)
	{
		return rowList.Find(x => x.Y_EcoDiversity == find);
	}
	public List<Row> FindAll_Y_EcoDiversity(string find)
	{
		return rowList.FindAll(x => x.Y_EcoDiversity == find);
	}
	public Row Find_Depiction(string find)
	{
		return rowList.Find(x => x.Depiction == find);
	}
	public List<Row> FindAll_Depiction(string find)
	{
		return rowList.FindAll(x => x.Depiction == find);
	}
	#endregion
}
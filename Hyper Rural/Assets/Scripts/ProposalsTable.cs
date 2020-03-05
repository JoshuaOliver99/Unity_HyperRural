using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ProposalsTable : MonoBehaviour
{
    // SETUP
    public TextAsset proposalDB; // Link DB

    // TEXT
    public Text title; // For proposal title
    public Text description; // For proposal description

    // PROPOSALS
    List<Row> activeProposals; // Holds unseen proposals
    Row currentProposal; // Holds the current proposal

    private void Start()
    {
        Load(proposalDB);
        resetActiveProposals(); // Sets the active proposals
        currentProposal = getRandomProposal(); // Gets the first proposal
    }

    public void AcceptProposal() // APPLIES ACCEPT STATS then PULLS A NEW PROPOSAL
    {
        GameController.econemy += int.Parse(currentProposal.Y_Economy); // Apply stats
        GameController.environment += int.Parse(currentProposal.Y_Environment);
        GameController.appeal += int.Parse(currentProposal.Y_Appeal);
        GameController.ecoDiversity += int.Parse(currentProposal.Y_EcoDiversity);
        currentProposal = getRandomProposal(); // Pulls a new proposal
    }

    public void DeclineProposal() // APPLIES DECLINE STATS then PULLS A NEW PROPOSAL
    {
        GameController.econemy += int.Parse(currentProposal.N_Economy); // Apply stats
        GameController.environment += int.Parse(currentProposal.N_Environment);
        GameController.appeal += int.Parse(currentProposal.N_Appeal);
        GameController.ecoDiversity += int.Parse(currentProposal.N_EcoDiversity);
        currentProposal = getRandomProposal(); // Pulls a new proposal
    }

    private Row getRandomProposal() // PULLS A RANDOM PROPOSAL or ENDS GAME
    {
        if (activeProposals.Count != 0) // Active proposals remain
        {
            Row foundProposal = activeProposals[Random.Range(0, activeProposals.Count)]; // Chooses random from list of active
            drawUI(foundProposal); // Draws UI for the found proposal

            activeProposals.Remove(foundProposal); // Removes the current proposal from active list

            return foundProposal;
        }
        else
        {
            Debug.LogError("Active / Remaining proposals: " + activeProposals.Count);
            return null;
        }

    }

    private void drawUI(Row proposal)
    {
        title.text = proposal.Title; // Apply title
        description.text = proposal.Description; // Apply Description
    }

    private void resetActiveProposals()
    {
        activeProposals = new List<Row>(GetRowList()); // Sets activeProposals to rowList
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
        public string N_Economy;
        public string N_Environment;
        public string N_Appeal;
        public string N_EcoDiversity;
        public string A_Economy;
        public string A_Environment;
        public string A_Appeal;
        public string A_EcoDiversity;
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
            row.N_Economy = grid[i][7];
            row.N_Environment = grid[i][8];
            row.N_Appeal = grid[i][9];
            row.N_EcoDiversity = grid[i][10];
            row.A_Economy = grid[i][11];
            row.A_Environment = grid[i][12];
            row.A_Appeal = grid[i][13];
            row.A_EcoDiversity = grid[i][14];
            row.Depiction = grid[i][15];

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
    public Row Find_N_Economy(string find)
    {
        return rowList.Find(x => x.N_Economy == find);
    }
    public List<Row> FindAll_N_Economy(string find)
    {
        return rowList.FindAll(x => x.N_Economy == find);
    }
    public Row Find_N_Environment(string find)
    {
        return rowList.Find(x => x.N_Environment == find);
    }
    public List<Row> FindAll_N_Environment(string find)
    {
        return rowList.FindAll(x => x.N_Environment == find);
    }
    public Row Find_N_Appeal(string find)
    {
        return rowList.Find(x => x.N_Appeal == find);
    }
    public List<Row> FindAll_N_Appeal(string find)
    {
        return rowList.FindAll(x => x.N_Appeal == find);
    }
    public Row Find_N_EcoDiversity(string find)
    {
        return rowList.Find(x => x.N_EcoDiversity == find);
    }
    public List<Row> FindAll_N_EcoDiversity(string find)
    {
        return rowList.FindAll(x => x.N_EcoDiversity == find);
    }
    public Row Find_A_Economy(string find)
    {
        return rowList.Find(x => x.A_Economy == find);
    }
    public List<Row> FindAll_A_Economy(string find)
    {
        return rowList.FindAll(x => x.A_Economy == find);
    }
    public Row Find_A_Environment(string find)
    {
        return rowList.Find(x => x.A_Environment == find);
    }
    public List<Row> FindAll_A_Environment(string find)
    {
        return rowList.FindAll(x => x.A_Environment == find);
    }
    public Row Find_A_Appeal(string find)
    {
        return rowList.Find(x => x.A_Appeal == find);
    }
    public List<Row> FindAll_A_Appeal(string find)
    {
        return rowList.FindAll(x => x.A_Appeal == find);
    }
    public Row Find_A_EcoDiversity(string find)
    {
        return rowList.Find(x => x.A_EcoDiversity == find);
    }
    public List<Row> FindAll_A_EcoDiversity(string find)
    {
        return rowList.FindAll(x => x.A_EcoDiversity == find);
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
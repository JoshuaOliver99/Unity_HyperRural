﻿// This code automatically generated by TableCodeGen
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TestTable : MonoBehaviour
{
    public TestStatusHandler TestStatusHandler;

    public TextAsset File; // Locate the database

    public Text Title; // For proposal title
    public Text Description; // For proposal description
    public Text currNumber; // For current proposal number

    // PROPOSALS
    List<Row> activeProposals; // Holds unseen proposals
    Row currentProposal; // Holds the current proposal

    int seenProposals = 0; // Holds number of currently seen proposals
    int maxProposals = 26; // How many proposals till game end
    bool ended = false; // For ending the game

    private void Start()
    {
        // SETUP
        Load(File); // Load the databse
        resetActiveProposals(); // Sets the active proposals
        currentProposal = getRandomProposal(); // Gets the first proposal
    }

    private void Update()
    {
        if (!ended)
        {
            if (Input.GetKeyDown(KeyCode.Y))
                acceptProposal();

            if (Input.GetKeyDown(KeyCode.N))
                declineProposal();
        }
        else
        {
            Debug.Log("GAME ENDED");
        }

    }



    private void acceptProposal() // APPLIES STATS + PULLS A NEW PROPOSAL
    {
        TestStatusHandler.stat1 += int.Parse(currentProposal.Y_Stat1); // Apply stats
        TestStatusHandler.stat2 += int.Parse(currentProposal.Y_Stat2);
        TestStatusHandler.stat3 += int.Parse(currentProposal.Y_Stat3);
        TestStatusHandler.stat4 += int.Parse(currentProposal.Y_Stat4);
        TestStatusHandler.stat5 += int.Parse(currentProposal.Y_Stat5);
        currentProposal = getRandomProposal(); // Pulls a new proposal
    }

    private void declineProposal() // APPLIES STATS + PULLS A NEW PROPOSAL
    {
        TestStatusHandler.stat1 += int.Parse(currentProposal.N_Stat1); // Apply stats
        TestStatusHandler.stat2 += int.Parse(currentProposal.N_Stat2);
        TestStatusHandler.stat3 += int.Parse(currentProposal.N_Stat3);
        TestStatusHandler.stat4 += int.Parse(currentProposal.N_Stat4);
        TestStatusHandler.stat5 += int.Parse(currentProposal.N_Stat5);
        currentProposal = getRandomProposal(); // Pulls a new proposal
    }

    private Row getRandomProposal() // PULLS A RANDOM PROPOSAL or ENDS GAME
    {
        if (activeProposals.Count != 0 && seenProposals < maxProposals) // GAME STILL IN PLAY
        {
            seenProposals++; // Increase seen counter

            Row foundProposal = activeProposals[Random.Range(0, activeProposals.Count)]; // Chooses random from list of active
            drawUI(foundProposal); // Draws UI for the found proposal

            activeProposals.Remove(foundProposal); // Removes the current proposal from active list

            return foundProposal;
        }
        else if (seenProposals >= maxProposals) // PLAYED FOR MAX TURNS
        {
            Debug.Log("seenProposals > maxProposals --- GAME END");
            ended = true; // End Game
            return null;
        }

        Debug.LogError("No remaining proposals");
        return currentProposal;
    }

    private void drawUI(Row proposal)
    {
        Title.text = proposal.Title; // Apply title
        Description.text = proposal.Description; // Apply Description
        currNumber.text = seenProposals.ToString(); // Display current proposal number
    }

    private void resetActiveProposals()
    {
        activeProposals = new List<Row>(GetRowList()); // Sets activeProposals to rowList
    }



    public class Row
    {
        public string ID;
        public string Title;
        public string Description;
        public string Y_Stat1;
        public string Y_Stat2;
        public string Y_Stat3;
        public string Y_Stat4;
        public string Y_Stat5;
        public string N_Stat1;
        public string N_Stat2;
        public string N_Stat3;
        public string N_Stat4;
        public string N_Stat5;
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
            row.Y_Stat1 = grid[i][3];
            row.Y_Stat2 = grid[i][4];
            row.Y_Stat3 = grid[i][5];
            row.Y_Stat4 = grid[i][6];
            row.Y_Stat5 = grid[i][7];
            row.N_Stat1 = grid[i][8];
            row.N_Stat2 = grid[i][9];
            row.N_Stat3 = grid[i][10];
            row.N_Stat4 = grid[i][11];
            row.N_Stat5 = grid[i][12];

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
    public Row Find_Y_Stat1(string find)
    {
        return rowList.Find(x => x.Y_Stat1 == find);
    }
    public List<Row> FindAll_Y_Stat1(string find)
    {
        return rowList.FindAll(x => x.Y_Stat1 == find);
    }
    public Row Find_Y_Stat2(string find)
    {
        return rowList.Find(x => x.Y_Stat2 == find);
    }
    public List<Row> FindAll_Y_Stat2(string find)
    {
        return rowList.FindAll(x => x.Y_Stat2 == find);
    }
    public Row Find_Y_Stat3(string find)
    {
        return rowList.Find(x => x.Y_Stat3 == find);
    }
    public List<Row> FindAll_Y_Stat3(string find)
    {
        return rowList.FindAll(x => x.Y_Stat3 == find);
    }
    public Row Find_Y_Stat4(string find)
    {
        return rowList.Find(x => x.Y_Stat4 == find);
    }
    public List<Row> FindAll_Y_Stat4(string find)
    {
        return rowList.FindAll(x => x.Y_Stat4 == find);
    }
    public Row Find_Y_Stat5(string find)
    {
        return rowList.Find(x => x.Y_Stat5 == find);
    }
    public List<Row> FindAll_Y_Stat5(string find)
    {
        return rowList.FindAll(x => x.Y_Stat5 == find);
    }
    public Row Find_N_Stat1(string find)
    {
        return rowList.Find(x => x.N_Stat1 == find);
    }
    public List<Row> FindAll_N_Stat1(string find)
    {
        return rowList.FindAll(x => x.N_Stat1 == find);
    }
    public Row Find_N_Stat2(string find)
    {
        return rowList.Find(x => x.N_Stat2 == find);
    }
    public List<Row> FindAll_N_Stat2(string find)
    {
        return rowList.FindAll(x => x.N_Stat2 == find);
    }
    public Row Find_N_Stat3(string find)
    {
        return rowList.Find(x => x.N_Stat3 == find);
    }
    public List<Row> FindAll_N_Stat3(string find)
    {
        return rowList.FindAll(x => x.N_Stat3 == find);
    }
    public Row Find_N_Stat4(string find)
    {
        return rowList.Find(x => x.N_Stat4 == find);
    }
    public List<Row> FindAll_N_Stat4(string find)
    {
        return rowList.FindAll(x => x.N_Stat4 == find);
    }
    public Row Find_N_Stat5(string find)
    {
        return rowList.Find(x => x.N_Stat5 == find);
    }
    public List<Row> FindAll_N_Stat5(string find)
    {
        return rowList.FindAll(x => x.N_Stat5 == find);
    }

}
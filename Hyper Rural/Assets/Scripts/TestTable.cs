﻿// This code automatically generated by TableCodeGen
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TestTable : MonoBehaviour
{
    public TestStatusHandler TestStatusHandler;

    public TextAsset file; // Locate the database

    public Text Title; // For proposal title
    public Text Description; // For proposal description

    // LIST TO HOLD PROPOSALS
    List<Row> activeProposals; // Holds unseen proposals
    Row currentProposal; // Holds the current proposal

    private void Start()
    {
        // SETUP
        Load(file); // Load the databse
        resetActiveProposals(); // Sets the active proposals

        // GET FIRST PROPOSAL
        currentProposal = getRandomProposal();

        // APPLIES TEXT
        //Title.text = Find_ID("1").Title; // Apply title
        //Description.text = Find_ID("1").Description; // Apply Description
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            acceptProposal();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            declineProposal();
        }
    }

    private void acceptProposal() // APPLIES STATS + PULLS A NEW PROPOSAL
    {
        TestStatusHandler.stat1 += int.Parse(currentProposal.Y_Stat1);
        TestStatusHandler.stat2 += int.Parse(currentProposal.Y_Stat2);
        TestStatusHandler.stat3 += int.Parse(currentProposal.Y_Stat3);
        TestStatusHandler.stat4 += int.Parse(currentProposal.Y_Stat4);
        TestStatusHandler.stat5 += int.Parse(currentProposal.Y_Stat5);
        currentProposal = getRandomProposal(); // Pulls a new proposal
    }

    private void declineProposal() // APPLIES STATS + PULLS A NEW PROPOSAL
    {
        TestStatusHandler.stat1 += int.Parse(currentProposal.N_Stat1);
        TestStatusHandler.stat2 += int.Parse(currentProposal.N_Stat2);
        TestStatusHandler.stat3 += int.Parse(currentProposal.N_Stat3);
        TestStatusHandler.stat4 += int.Parse(currentProposal.N_Stat4);
        TestStatusHandler.stat5 += int.Parse(currentProposal.N_Stat5);
        currentProposal = getRandomProposal(); // Pulls a new proposal
    }

    private void resetActiveProposals()
    {
        activeProposals = new List<Row>(GetRowList()); // Sets activeProposals to rowList
    }

    private Row getRandomProposal()
    {
        Row foundProposal = activeProposals[Random.Range(0, activeProposals.Count)]; // Chooses random random from the active 
        //Debug.Log("ID: " + foundProposal.ID + " Description: " + foundProposal.Description); // the current proposal
        //Debug.Log("Active proposal count: " + activeProposals.Count); // the total (before removing current)
        activeProposals.Remove(foundProposal); // Removes the current proposal

        Title.text = currentProposal.Title; // Apply title
        Description.text = currentProposal.Description; // Apply Description

        return foundProposal;
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
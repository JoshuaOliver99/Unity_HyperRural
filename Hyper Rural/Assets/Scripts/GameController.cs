using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] ProposalsTable proposalsTable; // Linking tables (DB data)
    [SerializeField] CompetitionTable competitionTable; // linking tables (DB data)
    [SerializeField] GameObject proposalPanel; // For hiding panel
    [SerializeField] GameObject CompetitionPanel; // For hiding panel
    [SerializeField] GameObject notificationPanel; // For displayling the notification alert 
    [SerializeField] GameObject EndPanel; // For hiding panel
    [SerializeField] Text endDescText; // For displaying turns spent
    [SerializeField] Text endStatsText; // For displaying end game stats
    [SerializeField] AudioSource showPanelAudio; // For showing both proposal types noise
    [SerializeField] AudioSource showEndPanelAudio; // For showing the end panel noise
    [SerializeField] AudioSource notificationAudio; // For notification noise
    [SerializeField] AudioSource declineProposalAudio; // For declining noise
    [SerializeField] AudioSource acceptProposalAudio; // For accepting proposals noise


    [Header("Debug references")]
    [SerializeField] Text statusText; // For displaying stats
    [SerializeField] Text turnText; // For displaying current proposal number
    [SerializeField] Text timerText; // For displaying current proposal number

    [Header("Game Vairables")]
    [SerializeField] int waitTime = 5; // Time till next proposal
    [SerializeField] int competionFreq = 8; // How frequent competitons are
    [SerializeField] int maxTurns = 26; // How many turns till game end

    public static int economy, environment, appeal, ecoDiversity = 0; // The game stats 

    private int turnNumber; // Current turn number
    private int acceptedTotal, deniedTotal; // Total of Accepted / Denied proposals
    private float timer; // Game clock for causing events

    private bool inPlay = true; // if game in play
    private bool inGameEvent;  // If in a game event 
    private bool isProposal; // If currently is proposal
    private bool isCompetition; // If currently is competition


    void OnEnable()
    {
        // Set stats to starting values
        economy = 5; environment = 5; appeal = 5; ecoDiversity = 5;
        turnNumber = 0;
        acceptedTotal = 0; deniedTotal = 0;
        timer = 0;
        inPlay = true;
        inGameEvent = false;
        isProposal = false;
        isCompetition = false;
        // Hide all panels
        proposalPanel.SetActive(false);
        CompetitionPanel.SetActive(false);
        EndPanel.SetActive(false);
        notificationPanel.SetActive(false);   
    }

    void Update()
    {
        if (inPlay)
        {
            if (!inGameEvent)
            {
                EventManager(); // Triggers the different game states / Events
            }
            else if (inGameEvent)
            {
                if (isProposal)
                {

                    ProposalManger(); // Handles Proposals
                }
                else if (isCompetition)
                {
                    CompetitionManager(); // Handles Competitons
                }
            }
        }
        else if (!inPlay)
        {
            EndGameManager();
        }

        MainDebug(); // For debugging - remove on build
    }

    #region EventManager
    private void EventManager() // Manages causing the differnent game states
    {
        if (timer < waitTime)
            timer += Time.deltaTime; // Increase timer
        else if (timer >= waitTime && turnNumber < maxTurns && !inGameEvent)
        {
            if (!notificationPanel.activeSelf)
            {
                notificationAudio.Play();
                notificationPanel.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                showPanelAudio.Play(); // Play showing panel audio

                if (turnNumber % competionFreq == 0 && turnNumber != 0) // if (competiton turn && not turn 0)
                    isCompetition = true; // Cause a competition
                else
                    isProposal = true; // Cause a proposal

                inGameEvent = true;
                notificationPanel.SetActive(false);
            }
        }

        if (turnNumber >= maxTurns || economy <= 0 || environment <= 0 || appeal <= 0 || ecoDiversity <= 0) // if (turns exceed max ammount) or (player looses)
        {
            showEndPanelAudio.Play(); // Play showing end panel audio
            inPlay = false;
            EndPanel.SetActive(true); // Show EndPanel (done here to allow hiding and showing)
        }
    }
    #endregion

    #region GameEvents
    private void ProposalManger() // Manages the current proposal 
    {
        proposalPanel.SetActive(true); // Display proposalPanel

        if (Input.GetKeyDown(KeyCode.Y)) // Accept / Yes
        {
            acceptProposalAudio.Play(); // Play accept audio
            proposalsTable.AcceptProposal(); // Changes the active && Writes UI && Applies stats
            EndProposal(); // Resets for next proposal
            acceptedTotal++;
        }
        if (Input.GetKeyDown(KeyCode.N)) // Decline / No
        {
            declineProposalAudio.Play(); // Play decline audio
            proposalsTable.DeclineProposal(); 
            EndProposal(); // Resets for next proposal
            deniedTotal++;
        }
    }
    private void CompetitionManager() // Manages the current competition
    {
        CompetitionPanel.SetActive(true); // Display competitionPanel

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            competitionTable.AcceptCompetition(1); // Changes the active && Writes UI && Applies stats
            EndCompetition();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            competitionTable.AcceptCompetition(2);
            EndCompetition();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            competitionTable.AcceptCompetition(3);
            EndCompetition();
        }
        else if (Input.GetKeyDown(KeyCode.P)) // Pass
        {
            competitionTable.GetRandomCompetitions(); // Changes active challenges 
            EndCompetition(); // Resets for next Competition
        }
    }

    private void EndProposal() // Called when the player interacts with a proposal
    {
        isProposal = false; // Exit isProposal if statment
        proposalPanel.SetActive(false); // Hide proposalPanel
        EndTurn();
    }
    private void EndCompetition() // Called when the player interacts with a proposal
    {
        isCompetition = false; // Exit isCompetition if statment
        CompetitionPanel.SetActive(false); // Hide competitionPanel
        EndTurn();
    }
    private void EndTurn() // Manages stats upon ending a turn
    {
        turnNumber++; // Increse turn number
        timer = 0; // Reset timer
        inGameEvent = false;
    }
    #endregion

    #region EndGame
    private void EndGameManager()
    {
        endDescText.text = ("Your time in office is over." + "\n" +
                "You made a total of " + turnNumber + " decisions." + "\n" +
                acceptedTotal + " proposals were accepted and " + deniedTotal + " proposals were denied."); // Display a brief description

        endStatsText.text = ("While in office:" + "\n\n" +
            "The Economy" + StatsRep(economy) + "\n\n" +
            "The Environment" + StatsRep(environment) + "\n\n" +
            "The Appeal" + StatsRep(appeal) + "\n\n" +
            "The Eco-Diversity" + StatsRep(ecoDiversity)); // Display the end stats

        if (Input.GetKeyDown(KeyCode.Return)) // Return to main menu
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("Menu"); // Load Main menu scene

        }

        if (Input.GetKeyDown(KeyCode.P)) // hide/show end panel
        {
            if (EndPanel.activeSelf == true)
                EndPanel.SetActive(false); // hide EndPanel
            else
                EndPanel.SetActive(true); // show EndPanel
        }
    }
    private string StatsRep(int stat) // Returns a string comment on how the stat was altered
    {
        stat -= 5;
        if (stat == 0)
            return " was unaltered.";
        else if (stat > 0 && stat <= 5)
            return " grew by " + stat + ". \nGood effort.";
        else if (stat > 5 && stat <= 10)
            return " grew by " + stat + ". \nWell done.";
        else if (stat > 10 && stat <= 15)
            return " grew by " + stat + ". \nImpressive!";
        else if (stat > 15 && stat <= 20)
            return " grew by " + stat + ". \nIncredible!";
        else if (stat > 20)
            return " grew by " + stat + ". \nIMPOSSIBLE!";
        else if (stat < 0 && stat >= -5)
            return " shrunk by " + stat + ". \nPoor effort!";
        else if (stat < 5 && stat >= -10)
            return " shrunk by " + stat + ". \nShameful attempt!";
        else if (stat < 10)
            return " shrunk by " + stat + ". \nDreadful work!.";

        return "Stat value not recognised - ERROR";
    }
    #endregion

    #region Debug
    private void MainDebug() // DEBUG - FOR TESTING PURPOSES(disable on build)
    {
        statusText.text = ("Economy: " + economy + "   Environment: " + environment +
            "   Appeal: " + appeal + "   Eco-DIversity: " + ecoDiversity);
        turnText.text = ("Turn number: " + turnNumber + " / " + maxTurns);
        timerText.text = ("Timer: " + timer + " / " + waitTime);
    }
    #endregion
}
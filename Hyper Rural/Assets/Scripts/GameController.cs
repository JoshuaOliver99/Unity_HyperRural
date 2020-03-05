using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // SETUP LINKS
    public ProposalsTable proposalsTable; // Linking tables
    public CompetitionTable competitionTable; // linking tables
    public GameObject proposalPanel; // For hiding panel
    public GameObject CompetitionPanel; // For hiding panel
    // SETUP LINKS ----- DEBUG
    public Text statusText; // For displaying stats
    public Text turnText; // For current proposal number
    public Text timerText; // For current proposal number


    // STATS
    public static int econemy, environment, appeal, ecoDiversity = 0;

    // GAME VAIRABLES
    public static int turnNumber = 0; // Turn number
    public static int maxTurns = 26; // How many turns till game end

    public static float timer; // Game clock for proposals
    [SerializeField] int waitTime = 5; // Time till next proposal
    [SerializeField] int competionFreq = 8; // How frequent competitons are

    public static bool inPlay = true; // if is in play
    public static bool isProposal; // If is proposal
    public static bool isCompetition; // If is competition


    void Start()
    {
        proposalPanel.SetActive(false);
        CompetitionPanel.SetActive(false);
    }

    void Update()
    {
        if (inPlay)
        {

            // ========== INCREASE TIMER, CAUSE COMPETITONS AND PROPOSALS, END GMAE ==========
            if (timer < waitTime)
            {
                timer += Time.deltaTime; // Increase timer
            }
            else if (timer >= waitTime && turnNumber < maxTurns)
            {
                if (turnNumber % competionFreq == 0 && turnNumber != 0) // if (competiton turn && not turn 0)
                    isCompetition = true; // Cause a competition
                else
                    isProposal = true; // Cause a proposal
            }
            else if (turnNumber >= maxTurns) // if (turns exceed max ammount)
            {
                inPlay = false;
            }



            if (isProposal)
            {
                proposalPanel.SetActive(true); // Display proposalPanel

                if (Input.GetKeyDown(KeyCode.Y)) // Accept / Yes
                {
                    proposalsTable.AcceptProposal(); // Accepts, Applies stats, Loads next
                    endProposal(); // Resets for next proposal
                }
                if (Input.GetKeyDown(KeyCode.N)) // Decline / No
                {
                    proposalsTable.DeclineProposal(); // Declines, Applies stats, Loads next
                    endProposal(); // Resets for next proposal
                }
            }
            else if (isCompetition)
            {
                CompetitionPanel.SetActive(true); // Display competitionPanel

                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    Debug.Log("1 pressed");
                    // accept #1
                    // endCompetition(); // Resets for next Competition
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    Debug.Log("2 pressed");
                    // accept #2
                    // endCompetition(); // Resets for next Competition
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    Debug.Log("3 pressed");
                    // accept #3
                    // endCompetition(); // Resets for next Competition
                }
                else if (Input.GetKeyDown(KeyCode.P))
                {
                    // Decline all
                    endCompetition(); // Resets for next Competition
                }
            }


        }
        else if (!inPlay)
        {
            Debug.Log("Max turns reached - Turn: " + turnNumber + " / " + maxTurns);
            Debug.Log("GAME ENDED.");
        }

        mainDebug(); // Test debug items (remove on build)
    }






    private void endProposal() // Called when the player interacts with a proposal
    {
        isProposal = false; // Exit isProposal if()
        proposalPanel.SetActive(false); // Hide proposalPanel
        turnNumber++; // Increse turn number
        timer = 0; // Reset timer
    }

    private void endCompetition() // Called when the player interacts with a proposal
    {
        isCompetition = false; // Exit isCompetition if()
        CompetitionPanel.SetActive(false); // Hide competitionPanel
        turnNumber++; // Increse turn number
        timer = 0; // Reset timer
    }

    private void mainDebug() // DEBUG / TESTING (disable on build)
    {
        statusText.text = ("Economy: " + econemy + "   Environment: " + environment + "   Appeal: " + appeal +
                "   Eco-DIversity: " + ecoDiversity);

        turnText.text = ("Turn number: " + turnNumber);

        timerText.text = ("Timer: " + timer + " / " + waitTime);
    }


}



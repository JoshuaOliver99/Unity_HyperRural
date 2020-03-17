using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    // SETUP LINKS
    public ProposalsTable proposalsTable; // Linking tables (DB data)
    public CompetitionTable competitionTable; // linking tables (DB data)
    public GameObject proposalPanel; // For hiding panel
    public GameObject CompetitionPanel; // For hiding panel
    public GameObject EndPanel; // For hiding panel
    public GameObject notificationPanel; // For displayling the notification alert 

    // SETUP LINKS
    public Text endDescText; // For displaying turns spent
    public Text endStatsText; // For displaying end game stats
    // ----- DEBUG
    public Text statusText; // For displaying stats
    public Text turnText; // For displaying current proposal number
    public Text timerText; // For displaying current proposal number

    // STATS
    public static int economy, environment, appeal, ecoDiversity = 0;

    // GAME VAIRABLES
    private int turnNumber; // Turn number
    [SerializeField] int maxTurns = 26; // How many turns till game end
    private int acceptedTotal;
    private int deniedTotal;

    private float timer; // Game clock for proposals
    [SerializeField] int waitTime = 5; // Time till next proposal
    [SerializeField] int competionFreq = 8; // How frequent competitons are

    private bool inPlay = true; // if is in play
    private bool inGameEvent;
    private bool isProposal; // If is proposal
    private bool isCompetition; // If is competition


    void OnEnable()
    {
        // Set the stats on load
        turnNumber = 0;
        economy = 0;
        environment = 0;
        appeal = 0;
        ecoDiversity = 0;
        timer = 0;
        inPlay = true;
        inGameEvent = false;
        isProposal = false;
        isCompetition = false;

        // Hide panels
        proposalPanel.SetActive(false);
        CompetitionPanel.SetActive(false);
        EndPanel.SetActive(false);
        notificationPanel.SetActive(false);
    }

    void Update()
    {
        if (inPlay)
        {
            stateManager(); // Triggers the different game states


            if (isProposal)
            {
                proposalPanel.SetActive(true); // Display proposalPanel

                if (Input.GetKeyDown(KeyCode.Y)) // Accept / Yes
                {
                    proposalsTable.AcceptProposal(); // Accepts, Applies stats, Loads next
                    endProposal(); // Resets for next proposal
                    acceptedTotal++;
                }
                if (Input.GetKeyDown(KeyCode.N)) // Decline / No
                {
                    proposalsTable.DeclineProposal(); // Declines, Applies stats, Loads next
                    endProposal(); // Resets for next proposal
                    deniedTotal++;
                }

            }
            else if (isCompetition)
            {
                CompetitionPanel.SetActive(true); // Display competitionPanel

                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    competitionTable.AcceptCompetition(1);
                    endCompetition();
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    competitionTable.AcceptCompetition(2);
                    endCompetition();
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    competitionTable.AcceptCompetition(3);
                    endCompetition();
                }
                else if (Input.GetKeyDown(KeyCode.P)) // Pass
                {
                    competitionTable.GetRandomCompetitions(); // Changes active challenges 
                    endCompetition(); // Resets for next Competition
                }
            }


        }
        else if (!inPlay) // Game ended
        {
            endDescText.text = ("Your time in office is over." + "\n" +
                "You made a total of " + turnNumber + " decisions." + "\n" +
                acceptedTotal + " proposals were accepted and " + deniedTotal + " proposals were denied."); // Display a brief description

            endStatsText.text = ("While in office." + "\n\n" +
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
        mainDebug(); // Test debug items (remove on build)
    }

    private string StatsRep(int stat) 
    {
        if (stat == 0)
            return " was unaltered.";
        else if (stat > 0 && stat <= 5)
            return " grew by " + stat + ". \nGood effort";
        else if (stat > 5 && stat <= 10)
            return " grew by " + stat + ". \nWell done.";
        else if (stat > 10 && stat <= 15)
            return " grew by " + stat + ". \nImpressive!";
        else if (stat > 15 && stat <= 20)
            return " grew by " + stat + ". \nIncredible!";
        else if (stat > 20)
            return " grew by " + stat + ". \nIMPOSSIBLE!";
        else if (stat < 0 && stat >= -5)
            return " shrunk by " + stat + ". \npoor effort!.";
        else if (stat < 5 && stat >= -10)
            return " shrunk by " + stat + ". \nShameful attempt!";
        else if (stat < 10)
            return " shrunk by " + stat + ". \nDreadful work!.";

        return "Value OOD";
    }


    private void stateManager()
    {
        if (timer < waitTime)
            timer += Time.deltaTime; // Increase timer

        if (timer >= waitTime && turnNumber < maxTurns && !inGameEvent)
        {
            Debug.Log("IN THAT ANNYOING LOOP");
            if (!notificationPanel.activeSelf)
                notificationPanel.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (turnNumber % competionFreq == 0 && turnNumber != 0) // if (competiton turn && not turn 0)
                    isCompetition = true; // Cause a competition
                else
                    isProposal = true; // Cause a proposal

                inGameEvent = true;
                notificationPanel.SetActive(false);
            }
        }

        else if (turnNumber >= maxTurns) // if (turns exceed max ammount)
        {
            inPlay = false;
            EndPanel.SetActive(true); // Show EndPanel (done here to allow hiding and showing)
        }
    }

    private void endProposal() // Called when the player interacts with a proposal
    {
        isProposal = false; // Exit isProposal if statment
        proposalPanel.SetActive(false); // Hide proposalPanel
        endTurn();
    }

    private void endCompetition() // Called when the player interacts with a proposal
    {
        isCompetition = false; // Exit isCompetition if statment
        CompetitionPanel.SetActive(false); // Hide competitionPanel
        endTurn();
    }

    private void endTurn()
    {
        turnNumber++; // Increse turn number
        timer = 0; // Reset timer
        inGameEvent = false;
    }
    private void mainDebug() // DEBUG / TESTING (disable on build)
    {
        statusText.text = ("Economy: " + economy + "   Environment: " + environment +
            "   Appeal: " + appeal + "   Eco-DIversity: " + ecoDiversity);
        turnText.text = ("Turn number: " + turnNumber + " / " + maxTurns);
        timerText.text = ("Timer: " + timer + " / " + waitTime);
    }


}
# Sports Match Scoring Application

This code base is for a sports match scoring application. 

## Problem Description
The code in the SportsMatchScoring application determines the winner of a sports match based on specific conditions. 
In this problem, we're focusing on volleyball and squash matches.

## Volleyball Match Rules
A team wins if they score 15 points first, unless both teams have reached 14 points.
If both teams reach 14 points, the winner is the team with a lead of two points.

## Squash Match Rules
Best of 3 games.
A team wins if they score 11 points first, unless both teams have reached 10 points.
If the score in a game is tied at 10-10, a player must win by 2 clear points.

## Application
In the binary string, 0 represents TEAM 1 losing a point, and 1 represents TEAM 1 winning a point.

The code in SportsMatchScoring uses SOLID Principles and Design Patterns and provides unit tests to ensure the code handles volleyball matches with (a minimum of) three sets and provides the match outcome in the format "Ravens beat Badgers (2-1) Scores: 15-7, 7-15, 15-7.".

The code adheres to SOLID principles and focuses on Single Responsibility, Open/Closed, Liskov Substitution, Interface Segregation, and Dependency Inversion principles. It utilises the factory and strategy design pattern to enhance the structure and flexibility of the codebase.

Following successful development of the volleyball game the code was refactored to support multiple ball games. A squash game was then added alongside volleyball. 

The code has been designed to be flexible and scalable to accommodate future additions of other sports. 

## Logic
The codebase ensures that this part of the application accurately handles volleyball matches, considering the rules such as winning conditions, scoring, and determining the winner based on sets.

## Unit Tests
This application provides comprehensive unit tests to validate the functionality of the code. Test cases cover various scenarios. This is some sample test data for the match: "1001010101111011101111", "0110101010000100010000", "1001010101111011101111".

## WEB API
The applications logic and processing is accessed via the REST API project SportsMatchScroing.Api. 

The functionality provided by this project allows for saving evaluated scores to a database, with each request recorded with an unique ID. 

The API adheres to SOLID principles and design patterns and implement the following endpoints:

#### Check Score and Save Result Endpoint
This endpoint receives data including the names of both teams and an input string of the scores and the game being played. It calculates the result of the match, saves it to the database along with a unique ID and returns the result.

#### Retrieve Historical Scores Endpoint By ID
This endpoint retrieves historical match scores based on a provided ID. It returns the details of the match stored in the database corresponding to the given identifier.

#### Retrieve Historical Scores Endpoint By Team Name
This endpoint retrieves historical match scores based on a provided team name. It returns the details of the match stored in the database corresponding to the given identifier.

#### Retrieve Historical Scores Endpoint 
This endpoint retrieves all historical match scores.

#### Delete Historical Scores Endpoint
This endpoint deletes historical match scores from the database based on a provided ID.

## Additional Front End Project
To facilitate to Web API I have provided a front end project which allows the user to simulate the process of either a volleyball or a squash match. This application allows the user to add both a home and away team, select the preferred match and then add scores for each team through an iterative process of selecting a home point or an away point. The application follows the rules for each game as stated above and will go into a ‘sudden death round’ for each game once the required draw scores have been reached. For volleyball, this is phase is entered once both scores reach 14 and then a winner is decided once one team has two clear points from the other up until 20 points have been scored, at which point the game results in a draw. For squash 11 and 18 points respectively. 

Once the desired scores and team names have been set the application allows the user to submit the recorded names and score-string.

The Web Api will then return the result depending on what was set.

The secondary function of this application is to display the results from past matches. This section of the application allows the user to return results for matches by ID, team name or will return all match results.

This application was built using Angular 17 with stand alone components and uses http requests to consume the endpoints provided by the Web API project.

## How To Set Up The Application

Prerequisites: SportsMatchScoring application please ensure SQL Express installed and this application targets .Net 8 so make sure is also that is installed. For the SportsMatchScoring.Spa application ensure that node.js and npm is installed.

Clone project.

How to run SportsMatchScoring application The start the API, open the solution in Visual Studio. Open package manage console, set default project to SportsMatchScoring.Data and enter ‘Update-Database’. Set the API project as start up project press the http start button to run API.

For the front end application SportsMatchScoring.Spa to run make sure node.js and npm is installed. Open the SportsMatchScoring.Spa folder in Visual Studio Code, open a terminal to SportsMatchScoring.Spa\sportsmatch folder and run ‘npm i’ and ‘npm update’. Once all packages are installed run ‘ng start’ and browse to localhost:4200.

## How To Play

Got to the ‘Select Scores’ page by selecting ‘Select Scores’ tab, see below.

![image](https://github.com/youngtrezel/sports-match/assets/168089018/70de3f5a-5d00-491d-870c-f03e2849f565)


Enter a home team and away team name, this must be added before the game can be played, see below.
![image](https://github.com/youngtrezel/sports-match/assets/168089018/da959f51-6218-4051-b387-f8f295684c2e)


Add the desired scores for each team, once the scores have been added the score board will indicate the winners and losers (or draw) scores for that game, see below.
![image](https://github.com/youngtrezel/sports-match/assets/168089018/b6af6024-0887-4f99-ae4b-5c5fcb244d21)


Once the all the desired number of games have been played (minimum 1 game), the user can press submit and the results of the game will be shown in the results display, see below.
![image](https://github.com/youngtrezel/sports-match/assets/168089018/21394c3b-1504-4404-aaf2-dc3a0d63c6f0)


Press ‘Reset All Scores’ at anytime to reset the application inputs and scores.

## How to View Past Results

Select the ‘Scores History’ tab.
![image](https://github.com/youngtrezel/sports-match/assets/168089018/03922033-bd0b-4eba-948c-b8625aa309ae)


To view all records, press ‘Get Scores’ button, see below.
![image](https://github.com/youngtrezel/sports-match/assets/168089018/5d320fe1-dee7-4e9c-a7aa-c889af8fa936)


To view records for a specific team, enter team name and then press corresponding ‘Get Scores’ button, see below.
![image](https://github.com/youngtrezel/sports-match/assets/168089018/329ed130-1502-4570-8b71-f5a7278eedbf)


To view records for a specific ID, enter the team name and then press corresponding ‘Get Scores’ button, see below.
![image](https://github.com/youngtrezel/sports-match/assets/168089018/4d61aaa9-51d2-43e1-afd5-653edd706168)














PROJECT TITLE: Dream Roles Theater Attendance App
PROJECT DESCRIPTION: The main goal of this application is to allow stage managers to keep track of performers' attendance at rehearsals for the Dream Roles production. Our application also adds features for choreographers, by keeping track of attendance and performers, and for performers, by allowing them to keep track of their songs and rehearsal information. 
Another functionality of the application is that stage managers will be able to send alerts to the performers about missing rehearsals. 
We used .NET MAUI to design our project. We have about 14 different screens on our app, some available only to people with certain roles (ie. only stage managers can view the alert page). We have intuitive navigation between pages and multiple icons and labels for easy readability. 

Sprint 4:
    - Create a popup or element on the screen that notifies the stage manager
    - Update performer list with alert ✅
    - Create alert for missed rehearsal ✅
    - Alert page functionality (Mostly Complete)
    - Implement hamburger menu
    - Create alert that displays rehearsal date and time ✅
    - UI for seeing access codes (Stage manager) + button to generate ✅
    - Backend for generating a new access code for performer ✅
    - Get the data and time from the database and display it in the UI for the performer ✅
    - User proofing, testing, and editing ✅
    - Create UI for creating rehearsals ✅

Sprint 3:
    - Finished the navigation between all pages for the app  😀😀😀 
    - Made the sign in with access code functional- navigates to different pages based on code entered 😀😀
    - Finished select name page- allows for performer to search for their name and check themselves in 😀
    - Added check in functionality to check in a performer on the database when selecting their name
    - Added table to save time, location, song, and performer to a performance
    
Sprint 2: 
    -Added a NotCheckedInPerformers method to return ObservableCollection of not checked in performers to ManagerNotCheckedInPage.xaml 😀 
    -Added navigation from ManagerNotCheckedInPage to ManagerAlertPage through alert button's ShowAlert_Clicked pushing page on Navigation Stack Layout 😀 
    -Added label names called lblPerformerNames and lblAbsences to pull data from the necessary fields of a Performer object. lblPerformerNames concantinates first & last name of performer and lblAbsences gets the performer's numberof absences. 
    -Added different popups and methods that allow the stage manager to add and edit songs.
    -Added different popups that allow the stage manager to add performers and to edit their names, contact information.
    -Added methods and images to delete songs or performers.
    -Added method to get the checked in performers 
    -Adjusted UI to show checked in performers from the database 😀 
    -Added dynamically populated song list for a performer on ManagerSeePerformerInfo page
    -Added dreamroles_user, checked_in_performers, missed_songs, performer, setlists, and songs tables to the database and an E/R Diagram in Figma 😀
    -Added navigation from ManagerHomepage to SongsList page
    -Added navigation from ManagerHomepage to ManagerSeePerformerInfo page
Sprint 1: UI Design of Pages
PROJECT TEAM MEMBERS: Kaia Thern, Keenan Marco, Keith Thoong, Keerthana Ambati
PROJECT DATE: September-December 2023

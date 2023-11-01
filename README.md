PROJECT TITLE: Dream Roles Theater Attendance App
PROJECT DESCRIPTION: The main goal of this application is to allow stage managers to keep track of performers' attendance at rehearsals for the Dream Roles production. Our application also adds features for choreographers, by keeping track of attendance and performers, and for performers, by allowing them to keep track of their songs and rehearsal information. 
Another functionality of the application is that stage managers will be able to send alerts to the performers about missing rehearsals. 
We used .NET MAUI to design our project. We have about 14 different screens on our app, some available only to people with certain roles (ie. only stage managers can view the alert page). We have intuitive navigation between pages and multiple icons and labels for easy readability. 
Sprint 1: UI Design of Pages
Sprint 2: 
        -Added a NotCheckedInPerformers method to return ObservableCollection of not checked in performers to ManagerNotCheckedInPage.xaml 😀 
        -Added navigation from ManagerNotCheckedInPage to ManagerAlertPage through alert button's ShowAlert_Clicked pushing page on Navigation Stack Layout 😀 
        -Added label names called lblPerformerNames and lblAbsences to pull data from the necessary fields of a Performer object. lblPerformerNames concantinates first & last name of performer and lblAbsences gets the performer's numberof absences. 
PROJECT TEAM MEMBERS: Kaia Thern, Keenan Marco, Keith Thoong, Keerthana Ambati
PROJECT DATE: September-December 2023

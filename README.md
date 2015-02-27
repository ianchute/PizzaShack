# PizzaShack

Simple SPA Website For Continuous Self-Improvement

Prerequisites:
 1. Microsoft Visual Studio 2013
 2. SQL Server 2012
 3. msysgit
 4. NodeJS (version 0.10.36)

I made this for my own continuous self-improvement.
I am trying to apply best practices w.r.t. architecture, test-driven development and UX design.
For others who wish to use it, you may do so freely (that would make me very happy).
Currently, it is still at its infant stage and it doesn't do much.

To get your copy: <b>git clone https://github.com/ianchute/PizzaShack</b>

NOTE: This app is unwatered.

How to water:
 1. Open the solution.
 2. Build the solution.
 3. Install http://nodejs.org/dist/v0.10.0/x64/node-v0.10.36-x64.msi (or, the 32-bit one)
    -- if you have another version, you will need to uninstall it so that the yeoman scaffolder will work properly.
    (If you had NodeJS before, ensure that %appdata%/npm and %appdata%/npm-cache are both empty)
 4. Install msysgit 
 5. Add mysysgit's bin and cmd folders to the PATH environmental variable
 6. In the FrontEnd Folder, execute <b>npm install & bower install</b>
 7. <b>npm install grunt</b>
 8. Attach the database (Database/PizzaShack.mdf) to your SQL server installation (.)
    (NOTE: depending on your setup, you may need to fix the connection string in the project)
 9. To run the website:
    First, run the backend (F5/F11 in Visual Studio)
    Then execute <b>grunt serve</b> within the Frontend folder.

For any problems encountered, contact ianchute@hotmail.com

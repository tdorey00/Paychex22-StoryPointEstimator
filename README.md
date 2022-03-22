# Important Notes Before Using this Project
- If deploying to Azure from Github make sure Paychex_StoryPointDB project is removed from the solution as .NET core cannot Build .sqlproj files. 
- If edits are necessary to the SQL Project simply readd the SQL Project to the Solution push the changes, then remove the project and push the .sln file without the .sqlproj file reference in it. 

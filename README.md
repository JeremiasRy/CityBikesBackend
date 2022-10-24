# CityBikesBackend
API for citybikes data

Minimal api for citybike data. Journeys implement pagination by default. Designed to use SQL database created with JeremiasRy/CityBikesDataImport
The tests use a empty database which needs to be configured in appsettings.json. To use the test db you'll need to change ISqlAccess methods connectionString parameters to "Test". It's by no means professional yet but I'll get there with future projects.

<Query Kind="Statements">
  <Connection>
    <ID>cc994ada-44af-43cd-86d7-d2bdd23181aa</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>TAYLORSPC\SQLEXPRESS</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <AlwaysPrefixWithSchemaName>true</AlwaysPrefixWithSchemaName>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>StartTed-2025-Sept</Database>
    <DriverData>
      <LegacyMFA>false</LegacyMFA>
    </DriverData>
  </Connection>
</Query>

// Question 1
// Report Includes:
// Club activities scheduled on or after Jan. 1, 2025 (done)
// Omit Campus Venue labelled "Scheduled Room" and Name "BTech Club Meeting" (done)

// Each event must list:
// start date, venue name, hosting club's name, activity title (done)
// Present all entries in ascending order by start date (done)

ClubActivities
	.Where(x => x.StartDate.Value >= new DateTime(2025, 1, 1)
	&& x.CampusVenue.Location != "Scheduled Room"
	&& x.Name != "BTech Club Meeting")
	.Select(x => new
	{
		StartDate = x.StartDate,
		Location = x.CampusVenue.Location,
		Club = x.Club.ClubName,
		Activity = x.Name
	})
	.OrderBy(x => x.StartDate)
	.Dump();

// Question 2


// Question 3


// Question 4


// Question 5
<Query Kind="Statements">
  <Connection>
    <ID>1f02f3c6-d763-4494-849b-66409a24af55</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>TAYLORSPC\SQLEXPRESS</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <AlwaysPrefixWithSchemaName>true</AlwaysPrefixWithSchemaName>
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

// Must Map SchoolCode to its full school name (done?)
// Include program name (done)
// Count required courses & optional courses (done)
// Filter to only ones with required course count equal or greater than 22 (done)
// Order final list by program name (done)

Programs
	.Select(x => new
	{
		School = x.Schools.SchoolName,
		Program = x.ProgramName,
		RequiredCourseCount = x.ProgramCourses.Count(x => x.Required),
		OptionalCourseCount = x.ProgramCourses.Count(x => !x.Required)
	})
	.Where(x => x.RequiredCourseCount >= 22)
	.OrderBy(x => x.Program)
	.Dump();

// Question 3
// Filter Students to those with 0 entries in StudentPayments (done)
// + Country is not from Canada (done)
// Order by last name (done)
// for each student, include:
// Student Number, Full Country Name, Full Name, Club Membership Count (diusplays "none" if no clubs or memberships" (done)

Students
.Where(x => x.StudentPayments.Count() == 0)
.Where(x => x.CountryCode != "CA")
.OrderBy(x => x.LastName)
.Select(x => new
	{
		StudentNumber = x.StudentNumber,
		CountryName = x.Countries.CountryName,
		FullName = x.FirstName + " " + x.LastName,
		ClubMembershipCount = x.ClubMembers.Count() == 0 ? "None" : x.ClubMembers.Count().ToString(),
	})
.Dump();

// Question 4



// Question 5
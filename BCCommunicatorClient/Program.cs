using System.Runtime.InteropServices;

//create an instance of our library that does the work
var bcClient = new BCCommunicator.Communicator();


// these should come from a config file that is "secure"
string tenantID = "PUT YOUR TENANT ID HERE -- NOTE: you will probably want this stored in encrypted storage or at least a config file";
string clientID = "PUT YOUR CLIENT ID HERE -- NOTE: you will probably want this stored in encrypted storage or at least a config file";
string clientSecret = "PUT YOUR CLIENT SECRET HERE -- NOTE: you will probably want this stored in encrypted storage or at least a config file";

// NOTE:  you will want to store the refresh token for future use
string refreshToken = "PUT YOUR REFRESH TOKEN HERE -- probably a good idea to store it in a database, etc.";

BCCommunicator.Token currentToken = null;

// NOTE:  if you have a current token, use it. check the expiration date first.  if it is expired.  get a new one
// you should only have to refresh if you dont have a valid currentToken
try
{
    // call the library call to refresh the token
    currentToken = bcClient.RefreshToken(
        tenantID,
        clientID,
        clientSecret,
        refreshToken
        );
    // NOTE:  you should store the currentToken token in a database or config file so that you can use it until it expires.
}
catch (Exception ex)
{
    Console.WriteLine($"Error refreshing token\n{ex.Message}");
    // TODO: ErrorHandling
}




try
{

    // we are just going to do a simple call to the companies endpoint that will return all companies tied to the tenantID
    var returnCompanies = bcClient.RetrieveData(tenantID, currentToken.access_token, "companies");

    // we are going to assume it works since the try/catch will catch any errors
    // we are going to deserialize this as a CompanyCollection class.
    var returnCompanyCollection = Newtonsoft.Json.JsonConvert.DeserializeObject<BCCommunicator.CompanyCollection>(returnCompanies);

    // lets run through each company and do something simple like print the company name to console.
    foreach (var company in returnCompanyCollection.value)
    {
        Console.WriteLine($"CompanyName = {company.name}");
    }
}
catch(Exception ex)
{
    // could be becaused of an expired token, bad data t hat doesnt deserialize to an expected class, or something else.
    // if expired token, then you should try to refresh the token then try the RetrieveData again
    Console.WriteLine($"Something happened here.  TODO: Need to have error handling\n{ex.Message}");
}



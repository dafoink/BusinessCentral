# The Library and Test Client
There are 2 different projects in this solution:
1. BCCommunicator: A library that allows you to refresh a token and to make simple API calls to BC
2. BCCommunicatorClient: a console app that will call the BCComunicator library

The main thing to look at is the BCCommunicator Librarie's RefreshToken() method.  This will take the Tenant ID, Client ID, client Secret, and the refresh token and request a new access_token

    currentToken = bcClient.RefreshToken(
        tenantID,
        clientID,
        clientSecret,
        refreshToken
        );

The above call will return a serialized object of the type Token. You can then use the access token from currentToken.access_token for API calls

# curl code for testing in Postman
curl code that can be imported into Postman to make call

```bash
curl --location 'https://login.microsoftonline.com/{{TenantID}}/oauth2/token' \
--header 'Content-Type: application/x-www-form-urlencoded' \
--data-urlencode 'client_id={{clientID}}' \
--data-urlencode 'client_secret={{ClientSecret}}' \
--data-urlencode 'grant_type=refresh_token' \
--data-urlencode 'refresh_token={{RefreshToken}}'
````

A valid call to the refresh token will return a token object.  You can then use the returned access_token in future API calls

```JSON
{
    "token_type": "Bearer",
    "scope": "Financials.ReadWrite.All user_impersonation",
    "expires_in": "4392",
    "ext_expires_in": "4392",
    "expires_on": "1690473891",
    "not_before": "1690469198",
    "resource": "https://api.businesscentral.dynamics.com",
    "access_token": "..................JhbGciOiJSUzI1NiIsIng1dCI6Ii1LSTNROW5OU..................",
    "refresh_token": "................w6Cfjpb4BOjtzrk9x10HzbAHc.AgABAAEAAAAtyolDObpQQ5VtlI4uGjEPAgDs_wUA9P_............"
}
```

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

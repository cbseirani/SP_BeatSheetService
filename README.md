# BeatSheetService
N-tier microservice that allows creators to structure their content into a "beat sheet," a storytelling tool used to outline various elements like scenes, dialogues, or musical cues.

Written with `.Net 8` and `C#` using a `MongoDb` NoSql database.

## Running Locally

You'll need `Docker Compose` in order to use the `docker-compose.yml` located in the root folder to deploy `BeatSheetService` and `MongoDB` in containers.
Get Docker Desktop: https://www.docker.com/products/docker-desktop/

1) open terminal and navigate to the root BeatSheetService
2) run the command `docker-compose build beatsheetservice`:
   
![image](https://github.com/cbseirani/SP_BeatSheetService/assets/34148393/24c265de-3803-4bca-b091-4b05b4c2b152)

4) run the command `docker-compose up` to deploy 2 contianers, the BeatSheetService and mongodb:
   
![image](https://github.com/cbseirani/SP_BeatSheetService/assets/34148393/99bf231b-11d2-491b-a363-a2f7d665e53a)


Once both the service and db has been deployed to Docker Desktop:

![image](https://github.com/cbseirani/SP_BeatSheetService/assets/34148393/5c0ec8ab-f050-4dc6-b726-1c5b324fe46a)


You can now navigate to the url to see the swagger documentation and utilize the UI: http://localhost:8080/swagger

![image](https://github.com/cbseirani/SP_BeatSheetService/assets/34148393/3ef30e69-7258-4efa-9fa0-b627ad27ef88)


If you want to connect to the database with another mongodb client, the connection string is: `mongodb://root:example@localhost:27017`

![image](https://github.com/cbseirani/SP_BeatSheetService/assets/34148393/a612e80b-ea73-4262-b3b2-69fd6e8a8df2)



## Local Development
I use Rider IDE for .Net and C# development which is paid only; you can get a free 30-day trial: https://www.jetbrains.com/rider/

Otherwise you can view the code using Visual Studio Community Edition: https://visualstudio.microsoft.com/vs/community/

You'll also need to install the appropriate .Net 8 sdk: https://dotnet.microsoft.com/en-us/download/dotnet/8.0

Once you have the above dependencies, use one of the IDEs to open the `BeatSheetService.sln`:

![image](https://github.com/cbseirani/SP_BeatSheetService/assets/34148393/76d67cec-ce0f-4355-bcc0-0a29d97d1bcf)

![image](https://github.com/cbseirani/SP_BeatSheetService/assets/34148393/14c2bd23-5070-4c2c-a9a2-ca9463013e42)

Be sure to supply `BeatSheetService` with the `DBCONNSTRING` environment variable in your `launchSettings.json` file; this is your mongodb conn string:

![image](https://github.com/cbseirani/SP_BeatSheetService/assets/34148393/7386a69e-da79-413e-abab-4f202b42caef)

Unit tests covering controller and service classes: 

![image](https://github.com/cbseirani/SP_BeatSheetService/assets/34148393/1d149470-fcfb-4e79-8c07-6daa185de1b6)

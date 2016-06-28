# jobs-engine
C# Class library pulling jobs from Indeed and Careerjet APIs 
*More job board APIs to be added soon (Contributions are much appreciated)*

##Usage
First you need to register a publisher account from [CareerJet]
(http://www.careerjet.com/advertise/) and/or [Indeed](http://www.indeed.com/publisher)

##Calling
	    Imports JobBoard.Engines
	    
            Dim searchApi As New SearchEngine()
            With searchApi
	        
                .CareerJetApi = "http://public.api.careerjet.net/search"
                .CareerJetKey = "your CareerJet API Key"
                .IndeedApi = "http://api.indeed.com/ads/apisearch"
                .IndeedKey = "your Indeed API Key"
                .Ip = "User IP Address"
                .UserAgent = "User Agent String"
                .SearchInCareerJet = True
                .SearchInIndeed = True
    
            End With
            Dim results As New JobBoard.Engines.Models.SearchResults
            results = searchApi.Search(keyword, country, city, count, pageno, "date")



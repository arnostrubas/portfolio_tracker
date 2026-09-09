# Portfolio Tracker

This is README for simple Portfolio Tracker
Desktop aplication build with C# / WPF using MVVM
Developed as an individual semester project at Faculty of Informatics, Masaryk University.

## SETUP GUIDE:
The app uses MSSQLLocalDB, which can be downloaded in VS Installer under data storage and processing (or something like that). This is the prefered setup way, but it can also be downloaded seperately.

Before the first launch of the application, run PortfoliotTracker/Database/Scripts/portfolioManagerDatabaseScript.sql and save this database on (localdb)\MSSQLLocalDB (in a little popup window that should appear upon running the script. You shouldnt be required to change anything else in this window).

After that, the app should be ready to launch, but if you want to, you can also run the other script (in the same folder as the previous one), but it is entirely optional.

The app uses YahooFinanceApi to download stock data. Unfortunately, on some devices, this API had problems with downloading the data, which I was not able to resolve, because it is not an official API. The reason I choose to use this unofficial API is because it requires no registration (other APIs require you to register and obtain API key, which you would then need to provide for the app to work) and also because it has no limit on requests. (which most of the APIs dont have). But the app should be able to run, even if this API wouldnt work. But in case the API doesnt work, the stock price wont update.

ChatGPT helped me with design of XAML Views

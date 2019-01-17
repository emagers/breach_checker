# LoginChecker 
A tool to see if email addresses have been involved in any reported breaches. 

This tool makes calls to APIs provided by [Troy Hunt @ haveibeenpwned.com](https://haveibeenpwned.com) to check if a list of emails (provided by you) has been compromised. This is just an ease of use tool so that you don't have to manually copy and paste all of your emails into the pwned website.

# Inputs
You will need to provide a path to a file which contains a newline separated list of emails, ex:

```emails.txt```
test1@test.com
test2@test.com
...

# Outputs
This program will output two JSON files:

`pbreach.json` -- A JSON list of all the emails involved in breaches that exposed passwords, along with all breaches the email is associated with.

`breach.json` -- A JSON list of all the emails not involved in breaches that exposed passwords, along with all the breaches the email is associated with.

# Running the application
Navigate to the path the executable for your OS (if it's one of the three I provided builds for) and run the following command via command line:
`./LoginChecker.exe {path_to_email_list}`

# Building 
If you want to make changes and rebuild the application, or build to target a different runtime then please see [dotnet documentation](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-build?tabs=netcore2x) on how to do so.

# License

Copyright 2019

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
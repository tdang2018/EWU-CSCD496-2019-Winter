## Assignment 9

- Add a unit test project for doing UI testing of the SecretSanta.Web project
    - This project should use Selenium with the Chrome driver
- Convert the feature request below into UI tests.
- You do not need to worry about fixing errors that your UI tests may uncover. A failing UI test is not 

### Feature Request: Users
- Be able to add a valid user
- Current users appear in the list
- Be able to edit an existing user
- Delete an existing user

## Going above and beyond
- Include additional browser drivers
    - This should not be a copy and paste of your unit tests. The same unit tests should be re-used with the different drivers.
- Rather than having to manually start up your web site and web service, have the test framework do this for you. This setup/teardown should only be done once for an entire test suit run.
- When a test fails, capture a screenshot and save the result as an image file. The name of the image file should correspond to the name of the unit test.
    - Use Selenium to capture the screenshot. Be aware this is a driver specific feature and may not be supported in all circumstances.
    - This should be done as part of the unit test cleanup.
    - To get access to the current state of the unit tests as well as the unit test name, you will need to use the [TestContext](https://docs.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.testtools.unittesting.testcontext).

## Helpful links
- [DOM](https://en.wikipedia.org/wiki/Document_Object_Model)
- [CSS](https://en.wikipedia.org/wiki/Cascading_Style_Sheets)
- [Selenium Docs](https://www.seleniumhq.org/docs/)
- [Introduction to CSS Selectors](https://developer.mozilla.org/en-US/docs/Learn/CSS/Introduction_to_CSS/Selectors) 
- [30 Most Common CSS Selectors](https://code.tutsplus.com/tutorials/the-30-css-selectors-you-must-memorize--net-16048)
- [CSS Selector Reference](https://www.w3schools.com/cssref/css_selectors.asp)
- [Learn CSS Selectors](https://learn.co/lessons/css-selectors)
- [Comlex Selectors](https://learn.shayhowe.com/advanced-html-css/complex-selectors/)
- [Dynamic XPath in Selenium](http://learn-automation.com/how-to-write-dynamic-xpath-in-selenium/)
- [XPath Tutorial](https://www.tutorialspoint.com/xpath/index.htm)




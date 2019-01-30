# lab-services
A Custom Connector for Microsoft Flow

## Build and Run

- open the LabServices.sln file in Visual Studio
- press the run button
  - http://localhost:5000/api/engineers should open in your default browser with a message telling you to use Postman

## Testing with Postman (locally)

- Open Postman and click the ```Import``` button
- Click ```Choose Files``` and navigate to the ```LabServices/Connector/IoT Insider Lab Services.postman_collection.json```
- Make sure you are in ```Build``` mode and you show the side bar
- From here you should be able to run all ```GET engineers``` and see the array of names

To test the whole trigger:
- Run ```POST subscribe```
- Put a breakpoint inside ```TriggerTest```
- Now Run ```POST call```

## Testing with Postman (in the wild)

- Install [ngrok](https://ngrok.com/)
- Run ```ngrok http 5000```
- Copy the https endpoint (for example ```https://06c9a867.ngrok.io```)
- In the side bar of post man to the left of the ```IoT Insider Lab Service``` collection, click the hamburger menu and select Edit
- Click the Variables tab and change the current value of ```host```
- Click ```Update```
- repeat the test above

## Creating/Updating the Custom Connector

- Edit ```LabServices/Connector/IoTInsiderLabService.json``` and update line 8 with the host name you got from ngrok
- Goto the [Flow website](https://flow.microsoft.com)
- Click the Gear icon in the upper right and choose ```Custom Connectors```
##### The first time
- Click ```Create custom connector``` and choose ```Import an OpenAPI file```
- Make any other changes and save
##### Subsequent times
- Click the hamburger button just below your custom connector and choose ```Update from OpenAPI file```
- Click ```Import```
- Navigate to ```LabServices/Connector/IoTInsiderLabService.json```
- Click ```Update connector```

### Useful Documentation

- [Create a custom connector from scratch](https://docs.microsoft.com/en-us/connectors/custom-connectors/define-blank)
- [Extend an OpenAPI definition for a custom connector](https://docs.microsoft.com/en-us/connectors/custom-connectors/openapi-extensions#x-ms-dynamic-values)
- [What you should know about building Microsoft Flow connectors](https://blog.mastykarz.nl/what-know-building-microsoft-flow-custom-connectors/)
- [Use a webhook as a trigger for Azure Logic Apps and Microsoft Flow](https://docs.microsoft.com/en-us/connectors/custom-connectors/create-webhook-trigger#the-openapi-definition)
- [Invoke Microsoft Flow (Microsoft Logic app) using custom connector with WebHook trigger](https://github.com/weng5e/flow-connector-trigger)
- [Submit a template to the Microsoft Flow gallery](https://docs.microsoft.com/en-us/flow/publish-a-template)
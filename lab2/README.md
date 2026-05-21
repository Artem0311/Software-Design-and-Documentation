# Messenger API - Lab 2

This repository contains my implementation of Lab 2 for the Software Design and Documentation course. It is a lightweight HTTP API for a messenger application built with C# and .NET Minimal API.

Based on the first lab, I implemented **Variant 2: Message Status Tracking**. The API allows updating the lifecycle of a message (for example, changing its status from `sent` to `read` or `delivered`).

## Features

* **JSON Persistence:** Users and messages are saved directly to local `.json` files. Data is preserved between server restarts.
* **Full CRUD:** The API supports creating users, sending messages, fetching conversation history, updating message statuses (via `PATCH`), and deleting messages.
* **Swagger UI:** Integrated Swagger for easy testing. You can interact with all endpoints directly from the browser without needing Postman.
* **Automated Testing:** Included an `xUnit` integration test that verifies the core flow (creating a user -> sending a message -> retrieving history).

## How to run the server

1. Open your terminal in the root project folder.
2. Start the application by running:
   ```bash
   dotnet run
   ```
3. Open your browser and navigate to the Swagger interface to test the endpoints. The terminal will output the local port 
(e.g., http://localhost:<your-port>/swagger).

## How to run tests

To run the automated integration tests, execute the following command in the terminal:
```bash
dotnet test tests
```

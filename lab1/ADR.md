# ADR: Choosing WebSocket for Message Statuses

## Context
In Variant 2, we need to track message statuses (sent, delivered, read). The main problem is how to notify the sender's app that their message was read, without making the app constantly ask the server for updates.

## Decision
I decided to use **WebSocket** instead of standard HTTP Polling. 

## Consequences
**Pros:**
* Statuses update instantly in real-time (like the two blue ticks in messengers).
* No extra load on the server and database from constant empty HTTP requests.
* Saves mobile data and phone battery.

**Cons:**
* The server has to keep many connections open at the same time.
* The client app needs extra logic to automatically reconnect if the internet drops.

# ADR 1: Using WebSocket for Real-Time Message Status Updates

## Context
In our system (Variant 2 - Message Status Tracking), we need to track and display message statuses (sent, delivered, read) in real-time. We must choose an optimal delivery mechanism to push these updates from the server to the clients so users can instantly see when their message is read.

## Decision
We have decided to use **WebSocket** technology for our Delivery Mechanism instead of standard HTTP Polling (where the client repeatedly asks the server for updates every few seconds).

## Consequences
**Positive (Pros):**
* Instant, real-time delivery of status updates.
* Significant reduction in load on the Backend API and Database, as clients no longer spam the server with empty HTTP requests.
* Better battery life and lower data consumption for mobile users.

**Negative (Cons):**
* Increased infrastructure complexity (need to maintain thousands of persistent open connections).
* Requires additional logic on the client-side to handle connection drops and automatic reconnections.
